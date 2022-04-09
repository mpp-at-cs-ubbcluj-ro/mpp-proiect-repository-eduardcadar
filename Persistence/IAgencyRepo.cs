using Model;

namespace Persistence
{
    public interface IAgencyRepo : IRepoInterface<Agency, string>
    {
        Agency Get(string name, string password);
    }
}
