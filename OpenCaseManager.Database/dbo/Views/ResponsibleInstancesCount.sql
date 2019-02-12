CREATE VIEW dbo.ResponsibleInstancesCount
AS
SELECT        u.Id, u.Name, COUNT(i.Id) AS Count, i.GraphId
FROM            dbo.Instance AS i INNER JOIN
                         dbo.[User] AS u ON u.Id = i.Responsible
WHERE        (i.IsOpen = 1)
GROUP BY u.Id, u.Name, i.GraphId