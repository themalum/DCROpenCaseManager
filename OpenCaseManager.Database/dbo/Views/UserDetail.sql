CREATE VIEW [dbo].[UserDetail]
AS
SELECT        Id, SamAccountName, Name, Title, Department, ManagerId, CASE WHEN managerId IS NULL THEN
                             (SELECT DISTINCT 1
                               FROM            [User] AS u2
                               WHERE        u2.ManagerId = u.Id) ELSE 0 END AS IsManager
FROM            dbo.[User] AS u
GO



GO


