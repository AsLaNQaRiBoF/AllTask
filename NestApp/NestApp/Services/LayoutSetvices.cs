using NestApp.DAL;

namespace NestApp.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _dbContext;

        public LayoutServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Dictionary<string, string> GetService()
        {
            return  _dbContext.Settings.ToList().ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
