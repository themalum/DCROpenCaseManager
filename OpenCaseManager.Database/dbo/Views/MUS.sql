CREATE VIEW dbo.MUS
AS
SELECT        TOP (100) PERCENT FullName, Id, Username, InstanceTitle, CaseNoForeign, CaseLink, CurrentPhaseNo, SimulationId, InstanceId, ManagerId, Department, YEAR
FROM            (SELECT        u.Id, u.SamAccountName AS Username, u.Name AS FullName, i.Title AS InstanceTitle, i.CaseNoForeign, i.CaseLink, i.CurrentPhaseNo, i.SimulationId, i.Id AS InstanceId, u.ManagerId, u.Department, ISNULL(ie.Year, 
                                                    YEAR(GETDATE())) AS YEAR
                          FROM            dbo.[User] AS u LEFT OUTER JOIN
                                                    dbo.InstanceExtension AS ie ON u.SamAccountName = ie.Employee AND ie.Year = YEAR(GETDATE()) LEFT OUTER JOIN
                                                    dbo.Instance AS i ON ie.InstanceId = i.Id
                          WHERE        (u.ManagerId IS NOT NULL)
                          UNION
                          SELECT        u.Id, u.SamAccountName AS Username, u.Name AS FullName, i.Title AS InstanceTitle, i.CaseNoForeign, i.CaseLink, i.CurrentPhaseNo, i.SimulationId, i.Id AS InstanceId, u.ManagerId, u.Department, 
                                                   ie.Year AS YEAR
                          FROM            dbo.[User] AS u INNER JOIN
                                                   dbo.InstanceExtension AS ie ON u.SamAccountName = ie.Employee INNER JOIN
                                                   dbo.Instance AS i ON ie.InstanceId = i.Id
                          WHERE        (u.ManagerId IS NOT NULL)) AS MUS_1
ORDER BY FullName
GO



GO


