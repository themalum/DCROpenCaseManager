CREATE TABLE [dbo].[ProcessPhase] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ProcessId]      INT            NOT NULL,
    [SequenceNumber] INT            NOT NULL,
    [Title]          NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_ProcessPhase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcessPhase_Process] FOREIGN KEY ([ProcessId]) REFERENCES [dbo].[Process] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

