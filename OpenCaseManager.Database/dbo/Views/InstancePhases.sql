CREATE VIEW [dbo].[InstancePhases]
AS
SELECT        dbo.Process.Id AS ProcessId, dbo.Instance.GraphId, dbo.Instance.Id AS InstanceId, dbo.ProcessPhase.Title, dbo.ProcessPhase.SequenceNumber, dbo.Instance.CurrentPhaseNo AS CurrentPhase, 
                         dbo.ProcessPhase.Id AS PhaseId
FROM            dbo.Instance INNER JOIN
                         dbo.Process ON dbo.Instance.GraphId = dbo.Process.GraphId INNER JOIN
                         dbo.ProcessPhase ON dbo.Process.Id = dbo.ProcessPhase.ProcessId
GO



GO




