using System.Threading.Tasks;

namespace CorporateBlog.DAL.DbContextProvider
{
    public class CorporateBlogContextProvider : IContextProvider
    {
        private readonly IContextCreator _contextCreator;


        public CorporateBlogContextProvider(IContextCreator contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public Task SaveChangesAsync()
        {
            return _contextCreator.GetContext.SaveChangesAsync();
        }
    }
}
