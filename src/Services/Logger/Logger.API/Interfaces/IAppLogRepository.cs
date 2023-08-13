using Logger.API.Models;

namespace Logger.API.Interfaces
{
    public interface IAppLogRepository
    {
        Task SaveLog(AppLogDto dto);
    }
}