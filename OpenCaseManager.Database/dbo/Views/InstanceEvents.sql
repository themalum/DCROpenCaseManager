CREATE VIEW dbo.InstanceEvents
AS
SELECT        e.EventId, e.Title AS EventTitle, u.Id AS Responsible, u.Name, e.Due, e.isOpen AS EventIsOpen, e.IsEnabled, e.IsPending, e.IsIncluded, e.IsExecuted, e.EventType, e.InstanceId, i.SimulationId, p.GraphId, e.Description, 
                         i.CaseNoForeign AS [Case], i.CaseLink, i.Title AS CaseTitle, i.IsOpen AS InstanceIsOpen, CASE ISNULL([EventTypeData].value('(/parameter[@title="UIEvent"]/@value)[1]', 'varchar(500)'), '') 
                         WHEN '' THEN 0 WHEN '0' THEN 0 ELSE 1 END AS IsUIEvent, e.EventTypeData.value('(/parameter[@title="UIEvent"]/@value)[1]', 'varchar(500)') AS UIEventValue, 
                         e.EventTypeData.value('(/parameter[@title="UIEventClass"]/@value)[1]', 'varchar(500)') AS UIEventCssClass, ISNULL(e.Type, N'') AS Type
FROM            dbo.Event AS e INNER JOIN
                         dbo.Instance AS i ON e.InstanceId = i.Id INNER JOIN
                         dbo.Process AS p ON p.GraphId = i.GraphId INNER JOIN
                         dbo.[User] AS u ON e.Responsible = u.Id
GO



GO





GO




