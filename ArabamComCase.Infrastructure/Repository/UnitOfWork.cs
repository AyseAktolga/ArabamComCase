using ArabamComCase.Application.Interfaces;

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
