-- =============================================
-- Author:		MUDDASSAR LATIF
-- Create date: 17-04-2018
-- Description:	This Stored Procedure will sync events
--				with tasks
-- =============================================
CREATE PROCEDURE [dbo].[SyncEvents]
-- Add the parameters for the stored procedure here
	@InstanceId INT,
	@EventXML XML,
	@LoginUser INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	DECLARE @Id                NVARCHAR(100),
	        @Title             NVARCHAR(500),
	        @Included          BIT,
	        @Enabled           BIT,
	        @Pending           BIT,
	        @Executed          BIT,
	        @eventType         NVARCHAR(500),
	        @groups            NVARCHAR(500),
	        @isAccepting       NVARCHAR(50),
	        @nextDeadline      NVARCHAR(100),
	        @nextDelay         NVARCHAR(100),
	        @role              NVARCHAR(250),
	        @phases            NVARCHAR(250),
	        @description       NVARCHAR(MAX),
	        @eventDeadline     NVARCHAR(100),
	        @type              NVARCHAR(100)
	
	BEGIN TRY
		IF ISNULL(@LoginUser, 0) = 0
		BEGIN
		    SELECT @LoginUser = i.Responsible
		    FROM   Instance AS i
		    WHERE  i.Id = @InstanceId
		END
		
		DECLARE ABC CURSOR  
		FOR
		    SELECT c.p.value('@id', 'varchar(100)') Id,
		           c.p.value('@label', 'varchar(500)') Title,
		           c.p.value('@included', 'varchar(50)') Included,
		           c.p.value('@enabled', 'varchar(50)') ENABLED,
		           c.p.value('@pending', 'varchar(50)') Pending,
		           c.p.value('@executed', 'varchar(50)') Executed,
		           c.p.value('@eventType', 'varchar(100)') eventType,
		           c.p.value('@groups', 'varchar(500)') groups,
		           c.p.value('@roles', 'varchar(250)') [role],
		           c.p.value('@phases', 'varchar(250)') phases,
		           c.p.value('@description', 'varchar(max)') [description],
		           c.p.value('@deadline', 'varchar(100)') [eventdeadline],
		           c.p.value('@type', 'varchar(100)') [type]
		    FROM   @EventXML.nodes('//events/event') AS c(p)
		
		OPEN ABC
		FETCH ABC INTO @Id,@Title,@Included,@Enabled,@Pending,@Executed,@eventType,
		@groups,@role,@phases,@description,@eventDeadline,@type
		WHILE @@FETCH_STATUS = 0
		BEGIN
		    IF EXISTS(
		           SELECT id
		           FROM   [Event] AS e
		           WHERE  e.EventId = @Id
		                  AND e.InstanceId = @instanceId
		       )
		        UPDATE [Event]
		        SET    IsEnabled         = CASE @enabled
		                                WHEN 'true' THEN 1
		                                ELSE 0
		                           END,
		               IsPending         = CASE @Pending
		                                WHEN 'true' THEN 1
		                                ELSE 0
		                           END,
		               IsIncluded        = CASE @Included
		                                 WHEN 'true' THEN 1
		                                 ELSE 0
		                            END,
		               IsExecuted        = CASE @Executed
		                                 WHEN 'true' THEN 1
		                                 ELSE 0
		                            END,
		               [Description]     = @description,
		               PhaseId           = @phases,
		               Due               = CASE @eventDeadline
		                          WHEN '' THEN NULL
		                          ELSE DATEADD(
		                                   mi,
		                                   DATEDIFF(mi, GETUTCDATE(), GETDATE()),
		                                   CAST(@eventDeadline AS DATETIME2)
		                               )
		                     END
		        WHERE  InstanceId        = @instanceId
		               AND EventId       = @Id
		    ELSE 
		    IF @Included = 'true'
		        INSERT INTO [Event]
		          (
		            InstanceId,
		            EventId,
		            Title,
		            Responsible,
		            Due,
		            PhaseId,
		            IsEnabled,
		            IsPending,
		            IsIncluded,
		            IsExecuted,
		            EventType,
		            [Description],
		            [TYPE]
		          )
		        VALUES
		          (
		            @InstanceId,
		            @Id,
		            @Title,
		            CASE 
		                 WHEN @role IN (SELECT [role]
		                                FROM   InstanceRole AS ir
		                                WHERE  instanceId = @InstanceId) THEN (
		                          SELECT userId
		                          FROM   InstanceRole AS ir
		                          WHERE  instanceId = @InstanceId
		                                 AND [ROLE] = @role
		                      )
		                 WHEN LOWER(@role) = 'robot' THEN -1
		                 WHEN LOWER(@role) = 'automatic' THEN -1
		                 ELSE @LoginUser
		            END,
		            CASE @eventDeadline
		                 WHEN '' THEN NULL
		                 ELSE DATEADD(
		                          mi,
		                          DATEDIFF(mi, GETUTCDATE(), GETDATE()),
		                          CAST(@eventDeadline AS DATETIME2)
		                      )
		            END,
		            @phases,
		            @enabled,
		            @pending,
		            @Included,
		            @executed,
		            CASE ISNULL(@eventType, '')
		                 WHEN '' THEN 'Tasks'
		                 ELSE @eventType
		            END,
		            @description,
		            @type
		          )
		    
		    FETCH ABC INTO @Id,@Title,@Included,@Enabled,@Pending,@Executed,@eventType,
		    @groups,@role,@phases,@description,@eventDeadline,@type
		END 
		
		CLOSE ABC
		DEALLOCATE ABC
		
		
		UPDATE [event]
		SET    IsIncluded = 0,
		       IsEnabled = 0
		WHERE  instanceid = @instanceid
		       AND NOT eventid IN (SELECT c.p.value('@id', 'varchar(100)') Id
		                           FROM   @EventXML.nodes('//events/event') AS c(p))
		
		SET @isAccepting = @EventXML.value('(/events/@isAccepting)[1]', 'nvarchar(10)')
		SET @nextDeadline = @EventXML.value('(/events/@nextDeadline)[1]', 'nvarchar(100)')
		SET @nextDelay = @EventXML.value('(/events/@nextDelay)[1]', 'nvarchar(100)')
		SET @phases = @EventXML.value('(/events/@phase)[1]', 'nvarchar(10)')		
		
		UPDATE Instance
		SET    IsAccepting = CASE @isAccepting
		                          WHEN 'True' THEN 1
		                          ELSE 0
		                     END,
		       CurrentPhaseNo = @phases,
		       NextDelay = CASE @nextDelay
		                        WHEN 'None' THEN NULL
		                        ELSE CAST(@nextDelay AS DATETIME2)
		                   END,
		       NextDeadline = CASE @nextDeadline
		                           WHEN 'None' THEN NULL
		                           ELSE CAST(@nextDeadline AS DATETIME2)
		                      END
		WHERE  Id = @InstanceId
		
		-- set current phase
		EXEC SetCurrentPhase @InstanceId = @InstanceId
		
		SELECT 1
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER()     AS ErrorNumber,
		       ERROR_SEVERITY()   AS ErrorSeverity,
		       ERROR_STATE()      AS ErrorState,
		       ERROR_PROCEDURE()  AS ErrorProcedure,
		       ERROR_LINE()       AS ErrorLine,
		       ERROR_MESSAGE()    AS ErrorMessage
	END CATCH
END