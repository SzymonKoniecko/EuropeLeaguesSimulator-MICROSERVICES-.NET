using Logger.API.Context;
using Logger.API.Interfaces;
using Logger.API.Models;

namespace Logger.API.Repositories
{
    public class AppLogRepository : IAppLogRepository
    {
        private readonly LoggerContext _loggerContext;

        public AppLogRepository(LoggerContext loggerContext)
        {
            _loggerContext = loggerContext;
        }
        public async Task SaveLog(AppLogDto dto)
        {
            _loggerContext.Add(dto);
            _loggerContext.SaveChanges();
        }
    }
}
