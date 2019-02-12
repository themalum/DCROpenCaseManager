-- =============================================
-- Author:		Muddassar Latif
-- Create date: 14092018
-- Description:	Get time for instances whose time
--				should advance
-- =============================================
CREATE PROCEDURE [dbo].[AdvanceTime]
-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here
	
	DECLARE @Now AS DATETIME
	DECLARE @InstanceId INT
	DECLARE @InstanceXML XML
	DECLARE @GraphId INT
	DECLARE @SimId INT
	DECLARE @NextDeadline AS DATETIME2
	DECLARE @NexDelay AS DATETIME2
	DECLARE @NextTime AS VARCHAR(1000)
	
	SET @Now = GETUTCDATE()
	
	CREATE TABLE #InstancesTime
	(
		Id           INT NOT NULL,
		DCRXML       XML,
		NextTime     VARCHAR(1000),
		GraphId      INT,
		SimId        INT
	) 
	
	DECLARE TimeCursor CURSOR  
	FOR
	    SELECT i.Id,
	           i.DCRXML,
	           i.NextDelay,
	           i.NextDeadline,
	           i.GraphId,
	           i.SimulationId
	    FROM   Instance AS i
	    WHERE  (NextDelay < @now OR NextDeadline < @Now)
	
	OPEN TimeCursor
	FETCH TimeCursor INTO @InstanceId,@InstanceXML,@NexDelay,@NextDeadline,@GraphId,
	@SimId
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    IF @NexDelay IS NOT NULL
	    BEGIN
	        IF @NextDeadline < @Now
	            SET @NextTime = @NextDeadline
	        ELSE
	            SET @NextTime = @Now
	        
	        
	        INSERT INTO #InstancesTime
	          (
	            Id,
	            DCRXML,
	            NextTime,
	            GraphId,
	            SimId
	          )
	        VALUES
	          (
	            @InstanceId,
	            @InstanceXML,
	            @NextTime,
	            @GraphId,
	            @SimId
	          )
	    END
	    
	    FETCH TimeCursor INTO @InstanceId,@InstanceXML,@NexDelay,@NextDeadline,@GraphId,
	    @SimId
	END
	CLOSE TimeCursor
	DEALLOCATE TimeCursor
	
	SELECT *
	FROM   #InstancesTime
END