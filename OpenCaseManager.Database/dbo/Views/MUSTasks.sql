
CREATE VIEW [dbo].[MUSTasks]
AS
SELECT EventId,
       EventTitle,
       Responsible,
       NAME,
       Due,
       EventIsOpen,
       IsEnabled,
       IsPending,
       IsIncluded,
       IsExecuted,
       EventType,
       InstanceId,
       SimulationId,
       GraphId,
       DESCRIPTION,
       [Case],
       CaseLink,
       CaseTitle,
       InstanceIsOpen,
       IsUIEvent,
       UIEventValue,
       UIEventCssClass
FROM   dbo.InstanceEvents AS ie
WHERE  (IsPending = 1)
       AND (IsEnabled = 1)
       AND (IsIncluded = 1)