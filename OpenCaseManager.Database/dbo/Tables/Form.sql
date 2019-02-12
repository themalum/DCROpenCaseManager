CREATE TABLE [dbo].[Form] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [FormTemplateId] INT             NULL,
    [Title]          NVARCHAR (1000) NULL,
    [IsTemplate]     BIT             NOT NULL,
    [UserId]         INT             NOT NULL,
    CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Form_Form] FOREIGN KEY ([FormTemplateId]) REFERENCES [dbo].[Form] ([Id])
);

