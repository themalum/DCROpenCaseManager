CREATE TABLE [dbo].[FormItem] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [FormId]         INT            NOT NULL,
    [IsGroup]        BIT            NOT NULL,
    [ItemId]         INT            NULL,
    [SequenceNumber] INT            NOT NULL,
    [ItemText]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_FormItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormItem_Form] FOREIGN KEY ([FormId]) REFERENCES [dbo].[Form] ([Id]),
    CONSTRAINT [FK_FormItem_FormItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[FormItem] ([Id])
);

