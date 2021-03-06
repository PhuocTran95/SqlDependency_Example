USE master
GO

CREATE DATABASE SQLDependency_Example
GO

USE SQLDependency_Example
GO

CREATE TABLE [dbo].[Conversation_Detail](
	[CD_ID] [int] PRIMARY KEY IDENTITY,
	[AccountSend] [int],
	[Conversation_ID] [int],
	[SendDate] [datetime],
	[ReadDate] [datetime],
	[Content] [varchar](max)
)
GO

ALTER DATABASE SQLDependency_Example SET ENABLE_BROKER