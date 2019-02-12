-- =============================================
-- Author:		Muddassar Latif
-- Create date: 19-06-2018
-- Description:	Update Event Log Instance
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEventLogInstance]
	@instanceId INT,
	@xml XML
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF EXISTS (
	       SELECT *
	       FROM   EventHistory AS eh
	       WHERE  eh.Instance = @instanceId
	              AND eh.[Status] = 0
	   )
	BEGIN
	    UPDATE Instance
	    SET    CaseStatus     = 0,
	           DCRXML         = @xml
	    WHERE  Id             = @instanceId
	END
	ELSE
	BEGIN
	    UPDATE Instance
	    SET    CaseStatus     = 1,
	           DCRXML         = @xml
	    WHERE  Id             = @instanceId
	END
END