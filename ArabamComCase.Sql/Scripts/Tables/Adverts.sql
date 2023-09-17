USE ArabamComCaseDB;
GO

CREATE TABLE Adverts(
   id          BIGINT  NOT NULL PRIMARY KEY 
  ,memberId    BIGINT  NOT NULL
  ,cityId      BIGINT  NOT NULL
  ,CityName    VARCHAR(8000) NOT NULL
  ,townId      BIGINT  NOT NULL
  ,TownName    VARCHAR(8000) NOT NULL
  ,modelId     BIGINT  NOT NULL
  ,modelName   VARCHAR(8000) NOT NULL
  ,year        BIGINT  NOT NULL
  ,price       DECIMAL(22,2)  NOT NULL
  ,title       VARCHAR(8000) NOT NULL
  ,date        VARCHAR(8000) NOT NULL
  ,categoryId  BIGINT  NOT NULL
  ,category    VARCHAR(8000) NOT NULL
  ,km          BIGINT  NOT NULL
  ,color       VARCHAR(8000)
  ,gear        VARCHAR(8000) NOT NULL
  ,fuel        VARCHAR(8000) NOT NULL
  ,firstPhoto  VARCHAR(8000) NOT NULL
  ,secondPhoto VARCHAR(8000) NOT NULL
  ,userInfo    VARCHAR(8000) NOT NULL
  ,userPhone   BIGINT 
  ,text        VARCHAR(max)
);
GO
CREATE TABLE [dbo].[AdvertVisits](
	[AdvertId] [int] NULL,
	[IpAdress] [nvarchar](35) NULL,
	[VisitDate] [datetime] NULL);
GO