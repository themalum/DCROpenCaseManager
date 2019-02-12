CREATE VIEW dbo.MyInstances
AS
SELECT        Id, GraphId, SimulationId, Title, CaseNoForeign, CaseLink, Responsible, IsAccepting, CurrentPhaseNo, NextDelay, NextDeadline, IsOpen
FROM            dbo.Instance AS i
GO



GO


