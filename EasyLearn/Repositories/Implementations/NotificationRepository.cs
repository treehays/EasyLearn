using EasyLearn.Data;
using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Implementations;

public class NotificationRepository : BaseRepository<Notification>
{
    public NotificationRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}