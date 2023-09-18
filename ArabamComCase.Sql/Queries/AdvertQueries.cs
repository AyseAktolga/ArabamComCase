using System.Diagnostics.CodeAnalysis;

namespace ArabamComCase.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class AdvertQueries
    {
        public static string AllAdvert => "SELECT * FROM [Adverts] (NOLOCK)";

        public static string AdvertById => "SELECT * FROM [Adverts] (NOLOCK) WHERE [id] = @Id";

        public static string AddAdvert => @"INSERT INTO [Advert] ([memberId]
      ,[cityId]
      ,[CityName]
      ,[townId]
      ,[TownName]
      ,[modelId]
      ,[modelName]
      ,[year]
      ,[price]
      ,[title]
      ,[date]
      ,[categoryId]
      ,[category]
      ,[km]
      ,[color]
      ,[gear]
      ,[fuel]
      ,[firstPhoto]
      ,[secondPhoto]
      ,[userInfo]
      ,[userPhone]
      ,[text]) 
				VALUES (@memberId, @cityId, @CityName,@townId,@TownName,@modelId,@modelName,@year,@price,@title,@date,@categoryId,@category,@km,@color,@gear,@fuel,@firstPhoto,@secondPhoto,@userInfo,@userPhone,@text)";

        public static string UpdateAdvert => @"UPDATE [Advert] 
            SET [memberId] =@memberId
      ,[cityId] =@cityId
      ,[CityName] =@CityName
      ,[townId] =@townId
      ,[TownName] =@TownName
      ,[modelId] =@modelId
      ,[modelName] =@modelName
      ,[year] =@year
      ,[price] =@price
      ,[title] =@title
      ,[date] =@date
      ,[categoryId] =@categoryId
      ,[category] =@category
      ,[km] =@km
      ,[color] =@color
      ,[gear] =@gear
      ,[fuel] =@fuel
      ,[firstPhoto] =@firstPhoto
      ,[secondPhoto] =@secondPhoto
      ,[userInfo] =@userInfo
      ,[userPhone] =@userPhone
      ,[text] =@text
            WHERE [Id] = @Id";

        public static string DeleteAdvert => "DELETE FROM [Adverts] WHERE [Id] = @Id";

        public static string AllAdvertDto => "SELECT  id,ModelName,Year,Price,Title,Date,CategoryId,Category,Km,Color,Gear,Fuel,FirstPhoto FROM [Adverts] (NOLOCK)";

        public static string FilterGetAdvert => "CREATE PROCEDURE dbo.GetFilteredAndSortedCars\r\n    @CategoryId INT = NULL,\r\n    @PriceMin DECIMAL = NULL,\r\n    @PriceMax DECIMAL = NULL,\r\n    @Gear VARCHAR(50) = NULL,\r\n    @Fuel VARCHAR(50) = NULL,\r\n    @KmMin INT = NULL,\r\n    @KmMax INT = NULL,\r\n    @SortingColumn VARCHAR(50) = NULL,\r\n    @SortingOrder VARCHAR(4) = NULL\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\r\n    -- Geçici bir tablo oluşturarak filtrelemeyi yapmaya başlayın\r\n    CREATE TABLE #FilteredCars\r\n    (\r\n        id INT,\r\n        memberId INT,\r\n        cityId INT,\r\n        CityName VARCHAR(50),\r\n        townId INT,\r\n        TownName VARCHAR(50),\r\n        modelId INT,\r\n        modelName VARCHAR(50),\r\n        [year] INT,\r\n        price DECIMAL,\r\n        [title] VARCHAR(100),\r\n        [date] DATE,\r\n        categoryId INT,\r\n        [category] VARCHAR(50),\r\n        km INT,\r\n        [color] VARCHAR(50),\r\n        [gear] VARCHAR(50),\r\n        [fuel] VARCHAR(50),\r\n        firstPhoto VARCHAR(255),\r\n        secondPhoto VARCHAR(255),\r\n        userInfo VARCHAR(255),\r\n        userPhone VARCHAR(20),\r\n        [text] VARCHAR(MAX)\r\n    );\r\n\r\n    -- Kriterlere göre filtrelemeleri gerçekleştirin\r\n    INSERT INTO #FilteredCars\r\n    SELECT *\r\n    FROM dbo.Adverts\r\n    WHERE (@CategoryId IS NULL OR categoryId = @CategoryId)\r\n        AND (@PriceMin IS NULL OR price >= @PriceMin)\r\n        AND (@PriceMax IS NULL OR price <= @PriceMax)\r\n        AND (@Gear IS NULL OR [gear] = @Gear)\r\n        AND (@Fuel IS NULL OR [fuel] = @Fuel)\r\n        AND (@KmMin IS NULL OR km >= @KmMin)\r\n        AND (@KmMax IS NULL OR km <= @KmMax);\r\n\r\n    -- Sıralama işlemini uygulayın\r\n    IF @SortingColumn IS NOT NULL\r\n    BEGIN\r\n        DECLARE @OrderByColumn NVARCHAR(50);\r\n\r\n        SET @OrderByColumn = \r\n            CASE \r\n                WHEN @SortingColumn = 'price' THEN 'price'\r\n                WHEN @SortingColumn = 'year' THEN 'year'\r\n                WHEN @SortingColumn = 'km' THEN 'km'\r\n                ELSE 'id' -- Varsayılan sıralama sütunu\r\n            END;\r\n\r\n        DECLARE @OrderByDirection NVARCHAR(4);\r\n\r\n        SET @OrderByDirection = \r\n            CASE \r\n                WHEN @SortingOrder = 'asc' THEN 'ASC'\r\n                ELSE 'DESC'\r\n            END;\r\n\r\n        EXEC('SELECT * FROM #FilteredCars ORDER BY ' + @OrderByColumn + ' ' + @OrderByDirection);\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        -- Sıralama parametresi gönderilmediyse, varsayılan sıralama ile sonuçları döndürün\r\n        SELECT * FROM #FilteredCars;\r\n    END;\r\n\r\n    DROP TABLE #FilteredCars;\r\nEND";
    }
}
