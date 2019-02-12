CREATE VIEW [dbo].[InstanceEventHistory]
AS
SELECT        TOP (100) PERCENT dbo.Instance.Id, dbo.Instance.CaseNoForeign AS CaseId, dbo.Instance.CaseStatus, dbo.EventHistory.EventId, dbo.EventHistory.Sequence, dbo.EventHistory.Description, dbo.EventHistory.Status, 
                         dbo.Instance.Title AS CaseTitle, dbo.Instance.GraphId, dbo.EventHistory.Details
FROM            dbo.EventHistory INNER JOIN
                         dbo.Instance ON dbo.EventHistory.Instance = dbo.Instance.Id
ORDER BY dbo.Instance.Id, dbo.EventHistory.Sequence
GO



GO


