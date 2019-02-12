CREATE TABLE [dbo].[Event] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [InstanceId]    INT              NOT NULL,
    [EventId]       NVARCHAR (50)    NULL,
    [Title]         NVARCHAR (500)   NOT NULL,
    [Responsible]   NVARCHAR (50)    NOT NULL,
    [Due]           DATETIME         NULL,
    [PhaseId]       NVARCHAR (500)   NULL,
    [IsEnabled]     BIT              NOT NULL,
    [IsPending]     BIT              NOT NULL,
    [IsIncluded]    BIT              NOT NULL,
    [IsExecuted]    BIT              NOT NULL,
    [EventType]     NVARCHAR (50)    NOT NULL,
    [isOpen]        AS               (case [isincluded] when (1) then case when [isenabled]=(1) then (1) else [ispending] end else (0) end),
    [Description]   NVARCHAR (MAX)   NULL,
    [EventTypeData] XML              NULL,
    [Type]          NVARCHAR (100)   NULL,
    [Token]         UNIQUEIDENTIFIER CONSTRAINT [DF_Event_Token] DEFAULT (newid()) NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Event_Instance] FOREIGN KEY ([InstanceId]) REFERENCES [dbo].[Instance] ([Id])
);

