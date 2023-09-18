namespace ArabamComCase.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAdvertRepository Adverts { get; }

        IAdvertVisitRepository AdvertVisits { get; }
    }
}
