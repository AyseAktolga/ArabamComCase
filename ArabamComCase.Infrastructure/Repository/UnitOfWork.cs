using ArabamComCase.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAdvertRepository advertRepository, IAdvertVisitRepository advertVisitRepository)
        {
            Adverts = advertRepository;
            AdvertVisits = advertVisitRepository;
        }

        public IAdvertRepository Adverts { get; set; }

        public IAdvertVisitRepository AdvertVisits { get; set; }
    }
}
