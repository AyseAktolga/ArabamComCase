﻿USE [ArabamComCaseDB]
GO

/****** Object:  Table [dbo].[AdvertVisits] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdvertVisits](
	[AdvertId] [int] NULL,
	[IpAdress] [nvarchar](35) NULL,
	[VisitDate] [datetime] NULL)
GO