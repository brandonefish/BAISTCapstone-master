CREATE DATABASE CentreHigh3DPrintingServicesDB
go
USE CentreHigh3DPrintingServicesDB
go

CREATE TABLE Color
(
	ColorName VARCHAR(40) NOT NULL,
	Available BIT NULL
	CONSTRAINT PK_Color PRIMARY KEY (ColorName)
)


CREATE TABLE SearchRequest
(
	SearchRequestID INT IDENTITY (1,1),
	Description VARCHAR(MAX) NULL,
	MultiPart VARCHAR(3) NOT NULL
)


Create Procedure GetSearchRequests
		AS
		DECLARE @ReturnCode INT
		SET @ReturnCode = 1

			BEGIN
				SELECT Description, MultiPart FROM SearchRequest
				IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetSearchRequests - SELECT error: SearchRequest table.',16,1)
		END

-- Ashtons piece, turned into script, not going to touch
-- Deals with App 3d Files storage and retrieval
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AppFiles](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [FileName] [varchar](max) NULL,
    [FileType] [varchar](20) NULL,
    [Content] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

----------------------------- ^^ Create Tables

-- COLOR STORED PROCEDURES
CREATE PROCEDURE AddColor(@ColorName VARCHAR(40) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @ColorName IS NULL
		RAISERROR('AddColor - Required parameter: @ColorName',16,1)
	ELSE
		BEGIN
			INSERT INTO Color
			(ColorName, Available)
			VALUES
			(@ColorName, '1')
					
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddColor - ADD error: Color table.',16,1)
		END
			RETURN @ReturnCode

EXECUTE AddColor 'Yellow'
EXECUTE RemoveColor 'Yellow'
EXECUTE GetAvailableColors
EXECUTE SetAvailable 'Yellow'
EXECUTE SetUnavailable 'Yellow'
EXECUTE GetAllColors


CREATE PROCEDURE RemoveColor(@ColorNameRemove VARCHAR(40) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @ColorNameRemove IS NULL
		RAISERROR('RemoveColor - Required parameter: @ColorNameRemove',16,1)
	ELSE
		BEGIN
			DELETE FROM Color
			WHERE ColorName = @ColorNameRemove
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('RemoveColor - REMOVE error: Color table.',16,1)
		END
			RETURN @ReturnCode

-- for DDL in selecting a color for student
CREATE PROCEDURE GetAvailableColors
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

		BEGIN
			SELECT ColorName
			FROM Color 
			WHERE Available = 1
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('Error getting colors from database',16,1)
		END
			RETURN @ReturnCode


CREATE PROCEDURE SetAvailable(@ColorName VARCHAR(40) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @ColorName IS NULL
		RAISERROR('SetAvailable - Required parameter: @ColorName',16,1)
	ELSE
		BEGIN
			UPDATE Color
			SET Available = 1 WHERE ColorName = @ColorName
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('Error getting colors from database',16,1)
		END
			RETURN @ReturnCode

CREATE PROCEDURE SetUnavailable(@ColorName VARCHAR(40) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @ColorName IS NULL
		RAISERROR('SetUnavailable - Required parameter: @ColorName',16,1)
	ELSE
		BEGIN
			UPDATE Color
			SET Available = 0 WHERE ColorName = @ColorName
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('Error setting color as available',16,1)
		END
			RETURN @ReturnCode

CREATE PROCEDURE GetAllColors
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

		BEGIN
			SELECT ColorName, Available
			FROM Color
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('Error getting all colors from database',16,1)
		END
			RETURN @ReturnCode


CREATE PROCEDURE CreateSearchRequest (@Description VARCHAR(MAX) = NULL, @MultiPart VARCHAR(3) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	--IF @StudentID IS NULL 
	--	RAISERROR('CreateSearchRequest - Required parameter:@StudentID',16,1)
	--ELSE
	IF @Description IS NULL 
		RAISERROR('CreateSearchRequest - Required parameter:@Description',16,1)
	ELSE
		IF @MultiPart IS NULL
			RAISERROR ('CreateSearchRequest - Required parameter:@MultiPart',16,1)
				ELSE
				BEGIN
					INSERT INTO SearchRequest
					(Description, MultiPart)
					--(StudentID, Description, MultiPart)
					VALUES
					(@Description, @MultiPart)
					--(@StudentID, @Description, @MultiPart)

					IF @@ERROR = 0
					SET @ReturnCode = 0
					ELSE
					RAISERROR('CreateSearchRequest -INSERT error:SearchRequest Table',16,1)
			END
	RETURN @ReturnCode





---------------------------- ^^ Stored Procedures

------------------------- Ashtons stored procedures

/** Object:  StoredProcedure [dbo].[DownloadFile]    Script Date: 03/03/2020 1:37:24 PM **/


CREATE PROCEDURE [dbo].[DownloadFile](@ID int= null)
AS 
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

        BEGIN
            SELECT FileName,FileType,Content
            FROM AppFiles
            Where
                Id = @ID

            IF @@ERROR = 0
                SET @ReturnCode = 0
            ELSE
                RAISERROR('GetStoredFiles -SELECT error :AppFile Table',16,1)
        END
    RETURN @ReturnCode
GO
----------------------------------------------------------------------------------------------------


CREATE PROCEDURE [dbo].[GetStoredFiles]
AS 
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

        BEGIN
            SELECT Id, FileName,FileType,Content
            FROM AppFiles
            IF @@ERROR = 0
                SET @ReturnCode = 0
            ELSE
                RAISERROR('GetStoredFiles -SELECT error :AppFile Table',16,1)
        END
    RETURN @ReturnCode
GO
-------------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE [dbo].[UploadFile] (@Filename VARCHAR(MAx),@FileType VARCHAR(20),@Content VARBINARY(MAX) = NULL)
AS 
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @Content IS NULL 
        RAISERROR('UploadFile - Required parameter:@Content',16,1)
    ELSE
        IF @Filename IS NULL
            RAISERROR('UploadFile - Required parameter:@FileName',16,1)
                IF @FileType IS NULL
                    RAISERROR('UploadFile - Required parameter:@FileType',16,1)
        BEGIN
            INSERT INTO AppFiles
            (FileName,FileType,Content)
            VALUES
            (@FileName,@FileType,@Content)
            IF @@ERROR = 0
                SET @ReturnCode = 0
            ELSE
                RAISERROR('UploadFile -INSERT error :AppFile Table',16,1)
        END
    RETURN @ReturnCode
GO