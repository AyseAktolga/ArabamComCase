USE ArabamComCaseDB;
GO

CREATE TABLE Adverts(
   id          BIGINT  NOT NULL PRIMARY KEY 
  ,memberId    BIGINT  NOT NULL
  ,cityId      BIGINT  NOT NULL
  ,CityName    NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,townId      BIGINT  NOT NULL
  ,TownName    NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,modelId     BIGINT  NOT NULL
  ,modelName   NVARCHAR(200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,[year]      BIGINT  NOT NULL
  ,price       DECIMAL(22,2)  NOT NULL
  ,title       NVARCHAR(200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,[date]      DATETIME NOT NULL
  ,categoryId  BIGINT  NOT NULL
  ,category    NVARCHAR(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,km          BIGINT  NOT NULL
  ,color       NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
  ,gear        NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,fuel        NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,firstPhoto  NVARCHAR(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
  ,secondPhoto NVARCHAR(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
  ,userInfo    NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,userPhone   BIGINT 
  ,[text]      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);
GO
CREATE TABLE [dbo].[AdvertVisits](
	[AdvertId] [int] NULL,
	[IpAdress] [nvarchar](35) NULL,
	[VisitDate] [datetime] NULL);
GO
CREATE PROCEDURE dbo.GetFilteredAndSortedCarsWithPaging
    @CategoryId INT = NULL,
    @PriceMin DECIMAL = NULL,
    @PriceMax DECIMAL = NULL,
    @Gear VARCHAR(50) = NULL,
    @Fuel VARCHAR(50) = NULL,
    @Page INT = NULL,
    @PageSize INT = NULL,
    @Date DATE = NULL,
    @KmMin INT = NULL,
    @KmMax INT = NULL,
    @Color VARCHAR(50) = NULL,
    @SortingColumn VARCHAR(50) = NULL,
    @SortingOrder VARCHAR(4) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Geçici bir tablo oluþturarak filtrelemeyi yapmaya baþlayýn
      CREATE TABLE #FilteredCars
    (
   id          BIGINT  NOT NULL PRIMARY KEY 
  ,memberId    BIGINT  NOT NULL
  ,cityId      BIGINT  NOT NULL
  ,CityName    NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,townId      BIGINT  NOT NULL
  ,TownName    NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,modelId     BIGINT  NOT NULL
  ,modelName   NVARCHAR(200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,[year]      BIGINT  NOT NULL
  ,price       DECIMAL(22,2)  NOT NULL
  ,title       NVARCHAR(200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,[date]      DATETIME NOT NULL
  ,categoryId  BIGINT  NOT NULL
  ,category    NVARCHAR(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,km          BIGINT  NOT NULL
  ,color       NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
  ,gear        NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,fuel        NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,firstPhoto  NVARCHAR(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
  ,secondPhoto NVARCHAR(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
  ,userInfo    NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
  ,userPhone   BIGINT 
  ,[text]      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NULL
  ,TotalCount  INT  NULL
);

    -- Kriterlere göre filtrelemeleri gerçekleþtirin
    INSERT INTO #FilteredCars
    SELECT *,
       (SELECT COUNT(*) 
        FROM dbo.Adverts
        WHERE (@CategoryId IS NULL OR categoryId = @CategoryId)
            AND (@PriceMin IS NULL OR price >= @PriceMin)
            AND (@PriceMax IS NULL OR price <= @PriceMax)
            AND (@Gear IS NULL OR [gear] = @Gear)
            AND (@Fuel IS NULL OR [fuel] = @Fuel)
       ) AS tot
FROM dbo.Adverts
WHERE (@CategoryId IS NULL OR categoryId = @CategoryId)
    AND (@PriceMin IS NULL OR price >= @PriceMin)
    AND (@PriceMax IS NULL OR price <= @PriceMax)
    AND (@Gear IS NULL OR [gear] = @Gear)
    AND (@Fuel IS NULL OR [fuel] = @Fuel);

	DECLARE @Offset INT;
        SET @Offset = (@Page - 1) * @PageSize;
    -- Sýralama iþlemini uygulayýn

    IF @SortingColumn IS NOT NULL
    BEGIN
        DECLARE @OrderByColumn NVARCHAR(50);

        SET @OrderByColumn = 
            CASE 
                WHEN @SortingColumn = 'price' THEN 'price'
                WHEN @SortingColumn = 'year' THEN 'year'
                WHEN @SortingColumn = 'km' THEN 'km'
                ELSE 'id' -- Varsayýlan sýralama sütunu
            END;

        DECLARE @OrderByDirection NVARCHAR(4);

        SET @OrderByDirection = 
            CASE 
                WHEN @SortingOrder = 'asc' THEN 'ASC'
                ELSE 'DESC'
            END;

        -- Sayfalama iþlemini uygulayýn


        EXEC('SELECT * FROM #FilteredCars 
              ORDER BY ' + @OrderByColumn + ' ' + @OrderByDirection + '
              OFFSET ' + @Offset + ' ROWS 
              FETCH NEXT ' + @PageSize + ' ROWS ONLY');
    END
    ELSE
    BEGIN
        -- Sýralama parametresi gönderilmediyse, varsayýlan sýralama ile sonuçlarý döndürün
       
        EXEC('SELECT * FROM #FilteredCars 
              ORDER BY id -- Varsayýlan sýralama sütunu
              OFFSET ' + @Offset + ' ROWS 
              FETCH NEXT ' + @PageSize+ ' ROWS ONLY');
    END;

    DROP TABLE #FilteredCars;
END;
GO