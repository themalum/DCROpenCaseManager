CREATE VIEW dbo.PhaseInstances
AS
SELECT        TOP (100) PERCENT pp.Id, COUNT(*) AS InstanceCount, pp.Title, pp.ProcessId, i.Responsible
FROM            dbo.Instance AS i INNER JOIN
                         dbo.ProcessPhase AS pp ON pp.Id = i.CurrentPhaseNo
WHERE        (i.IsOpen = 1)
GROUP BY pp.Title, pp.SequenceNumber, pp.Id, pp.ProcessId, i.Responsible
ORDER BY pp.SequenceNumber