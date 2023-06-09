CREATE PROCEDURE [dbo].[PopulateEmployeeTable]
AS
BEGIN
    SET NOCOUNT ON;

    -- Drop the Employee table if it exists
    IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL
        DROP TABLE [dbo].[Employee];

    -- Create the Employee table
    CREATE TABLE [dbo].[Employee] (
        [ID]           INT           NOT NULL,
		[EmpNo]        VARCHAR(6)   NOT NULL,
		[FirstName]    VARCHAR(15)  NOT NULL,
		[LastName]     VARCHAR(15)  NOT NULL,
		[Birthdate]    DATE          NOT NULL,
		[ContactNo]    VARCHAR(11)  NULL,
		[EmailAddress] VARCHAR(255) NULL,
        PRIMARY KEY CLUSTERED ([ID] ASC),
        CONSTRAINT UC_EmpNo UNIQUE ([EmpNo]),
        CONSTRAINT UC_FirstNameLastName UNIQUE ([FirstName], [LastName])
    );

    -- Insert sample records into the Employee table
    INSERT INTO [dbo].[Employee] (ID, EmpNo, FirstName, LastName, Birthdate, ContactNo, EmailAddress)
    VALUES
        (1, 'E00001', 'John', 'Doe', '1990-01-01', '09123456789', 'johndoe@example.com'),
        (2, 'E00002', 'Jane', 'Smith', '1992-03-15', '09123456788', 'janesmith@example.com'),
        (3, 'E00003', 'Michael', 'Johnson', '1985-07-20', '09123456787', 'michaeljohnson@example.com'),
        (4, 'E00004', 'Sarah', 'Williams', '1991-12-05', '09123456786', 'sarahwilliams@example.com'),
        (5, 'E00005', 'David', 'Brown', '1988-09-10', '09123456785', 'davidbrown@example.com'),
        (6, 'E00006', 'Emily', 'Taylor', '1993-06-25', '09123456784', 'emilytaylor@example.com'),
        (7, 'E00007', 'Daniel', 'Anderson', '1987-04-18', '09123456783', 'danielanderson@example.com'),
        (8, 'E00008', 'Olivia', 'Jackson', '1995-11-30', '09123456782', 'oliviajackson@example.com'),
        (9, 'E00009', 'James', 'Thomas', '1994-02-12', '09123456781', 'jamesthomas@example.com'),
        (10, 'E00010', 'Sophia', 'White', '1989-08-07', '09123456780', 'sophiawhite@example.com');
END