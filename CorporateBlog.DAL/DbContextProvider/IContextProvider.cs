using System.Threading.Tasks;

namespace CorporateBlog.DAL.DbContextProvider
{
    public interface IContextProvider
    {
        Task SaveChangesAsync();
    }
}
