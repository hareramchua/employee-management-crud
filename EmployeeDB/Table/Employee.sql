CREATE TABLE [dbo].[Employee] (
    [ID]           INT           NOT NULL,
    [EmpNo]        VARCHAR (6)   NOT NULL,
    [FirstName]    VARCHAR (15)  NOT NULL,
    [LastName]     VARCHAR (15)  NOT NULL,
    [Birthdate]    DATE          NOT NULL,
    [ContactNo]    VARCHAR (11)  NULL,
    [EmailAddress] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UC_FirstNameLastName] UNIQUE NONCLUSTERED ([FirstName] ASC, [LastName] ASC),
    CONSTRAINT [UC_EmpNo] UNIQUE NONCLUSTERED ([EmpNo] ASC)
);

