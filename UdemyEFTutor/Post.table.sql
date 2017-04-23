USE [ef_dbfirst]
GO

/****** Object:  Table [dbo].[Post]    Script Date: 07.01.2017 14:03:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Post](
	[Id] [int] NOT NULL,
	[DatePublished] [smalldatetime] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Body] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


