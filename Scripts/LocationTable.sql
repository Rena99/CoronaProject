USE [CoronaDB]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 5/27/2020 12:08:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Location](
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[PatientID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([ID])
GO

ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Patient]
GO


