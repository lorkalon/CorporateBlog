namespace CorporateBlog.DAL.DbContextProvider
{
    public class CorporateBlogContextProvider : IContextProvider
    {
        private readonly IContextCreator _contextCreator;


        public CorporateBlogContextProvider(IContextCreator contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public void SaveChanges()
        {
            _contextCreator.GetContext.SaveChanges();
        }
    }
}
