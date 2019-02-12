CREATE TABLE [dbo].[EventHistory] (
    [Instance]    INT            NULL,
    [Sequence]    INT            NULL,
    [EventId]     NVARCHAR (100) NULL,
    [Status]      INT            NULL,
    [Description] NVARCHAR (200) NULL,
    [Details]     NVARCHAR (MAX) NULL
);

