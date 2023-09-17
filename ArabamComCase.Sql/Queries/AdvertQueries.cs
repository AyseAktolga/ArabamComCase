using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
