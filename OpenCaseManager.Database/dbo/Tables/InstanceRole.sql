CREATE TABLE [dbo].[InstanceRole] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [InstanceId] INT           NULL,
    [Role]       NVARCHAR (50) NULL,
    [UserId]     INT           NULL
);



