CREATE DATABASE PicPayChallenge

USE PicPayChallenge
GO

CREATE TABLE UserTypePriorities(
Id TINYINT IDENTITY, 
Name VARCHAR(20)
PRIMARY KEY (Id)
)


CREATE TABLE Users(
Id UNIQUEIDENTIFIER,
Name VARCHAR(200),
UserName VARCHAR(100),
UserTypePriorityId TINYINT,
PRIMARY KEY (Id),
FOREIGN KEY (UserTypePriorityId) REFERENCES UserTypePriorities(Id)
)


CREATE INDEX IX_UserTypePriorityId ON Users (UserTypePriorityId)



CREATE TABLE UsersTemp(
Id UNIQUEIDENTIFIER,
Name VARCHAR(200),
UserName VARCHAR(100),
)


CREATE TABLE PriorityOne(
Id UNIQUEIDENTIFIER
)


CREATE TABLE PriorityTwo(
Id UNIQUEIDENTIFIER
)
BEGIN TRAN
BULK INSERT UsersTemp
FROM 'D:\Documentos\Projects\software-engineer-challenge\files\users.csv\users.csv'
WITH
(
    FIRSTROW = 1,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)


IF @@ERROR > 0
	ROLLBACK
ELSE 
	COMMIT

BEGIN TRAN
BULK INSERT PriorityOne
FROM 'D:\Documentos\Projects\software-engineer-challenge\lista_relevancia_1.txt'
WITH
(
    FIRSTROW = 1,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)


IF @@ERROR > 0
	ROLLBACK
ELSE 
	COMMIT


BEGIN TRAN
BULK INSERT PriorityTwo
FROM 'D:\Documentos\Projects\software-engineer-challenge\lista_relevancia_2.txt'
WITH
(
    FIRSTROW = 1,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)

IF @@ERROR > 0
	ROLLBACK
ELSE 
	COMMIT

--INSERT USER lista_relevancia_1.txt
BEGIN TRAN
INSERT INTO Users
SELECT ut.Id, ut.Name, ut.UserName, 1 FROM UsersTemp ut
join PriorityOne po
on ut.Id = po.Id

IF @@ERROR > 0
	ROLLBACK
ELSE 
	COMMIT


--INSERT USER lista_relevancia_2.txt
BEGIN TRAN
INSERT INTO Users
SELECT ut.Id, ut.Name, ut.UserName, 2 FROM UsersTemp ut
join PriorityTwo po
on ut.Id = po.Id

IF @@ERROR > 0
	ROLLBACK
ELSE 
	COMMIT

--INSERT USER RESTO DO USUARIOS
BEGIN TRAN
INSERT INTO Users
SELECT ut.Id, ut.Name, ut.UserName, 3FROM UsersTemp ut
WHERE UT.ID NOT IN (SELECT ID FROM Users)


IF @@ERROR > 0
	ROLLBACK
ELSE 
	COMMIT

	

