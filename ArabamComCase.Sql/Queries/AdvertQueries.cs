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

        public static string AddAdvert =>
            @"";

        public static string UpdateAdvert =>
            @"";

        public static string DeleteAdvert => "DELETE FROM [Adverts] WHERE [Id] = @Id";

        public static string AllAdvertDto => "SELECT  id,ModelName,Year,Price,Title,Date,CategoryId,Category,Km,Color,Gear,Fuel,FirstPhoto FROM [Adverts] (NOLOCK)";
    }
}
