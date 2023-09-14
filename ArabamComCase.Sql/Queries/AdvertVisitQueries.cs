using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class AdvertVisitQueries
    {
        public static string AllAdvertVisit => "SELECT * FROM [AdvertVisits] (NOLOCK)";

        public static string AdvertVisitById => "SELECT * FROM [AdvertVisits] (NOLOCK) WHERE [Id] = @Id";

        public static string AddAdvertVisit =>
            @"INSERT INTO [AdvertVisits] ([AdvertId], [IpAdress], [VisitDate]) 
				VALUES (@AdvertId, @IpAdress, @VisitDate)";

        public static string UpdateAdvertVisit =>
            @"UPDATE [AdvertVisits] 
            SET [AdvertId] = @AdvertId, 
				[IpAdress] = @IpAdress, 
				[VisitDate] = @VisitDate, 
            WHERE [Id] = @Id";

        public static string DeleteAdvertVisit => "DELETE FROM [AdvertVisits] WHERE [Id] = @Id";
    }
}
