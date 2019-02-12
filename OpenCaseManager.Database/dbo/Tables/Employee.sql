CREATE TABLE [dbo].[Employee] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [cpr]        VARCHAR (50)   NULL,
    [EmployeeId] NVARCHAR (500) NOT NULL,
    [fullName]   NVARCHAR (500) NULL,
    [cprnr2]     NVARCHAR (500) NULL,
    [department] NVARCHAR (500) NULL,
    [email]      NVARCHAR (500) NULL,
    [navn]       NVARCHAR (500) NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC)
);







