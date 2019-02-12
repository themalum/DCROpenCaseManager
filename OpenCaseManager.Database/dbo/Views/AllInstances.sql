CREATE VIEW [dbo].[AllInstances]
AS
SELECT        dbo.Instance.Id, dbo.Instance.Title, dbo.Instance.CaseNoForeign, dbo.Instance.CaseLink, dbo.Instance.IsOpen, dbo.[User].Name AS Responsible
FROM            dbo.Instance INNER JOIN
                         dbo.[User] ON dbo.Instance.Responsible = dbo.[User].Id
GO



GO


