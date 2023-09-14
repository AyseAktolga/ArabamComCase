using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Core.Entities
{
    public class AdvertVisit
    {
        public int Id { get; set; }

        public int AdvertId { get; set; }

        public string? IpAdress { get; set; }

        public DateTime VisitDate { get; set; }
    }
}
