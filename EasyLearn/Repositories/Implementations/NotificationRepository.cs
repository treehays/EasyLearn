using EasyLearn.Data;
using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Implementations;

public class NotificationRepository : BaseRepository<Notification>
{
    private readonly EasyLearnDbContext _context;

    public NotificationRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}