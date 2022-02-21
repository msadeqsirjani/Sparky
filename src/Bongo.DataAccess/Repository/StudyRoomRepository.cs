using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Models;

namespace Bongo.DataAccess.Repository;

public class StudyRoomRepository : IStudyRoomRepository
{
    private readonly ApplicationDbContext _db;
    public StudyRoomRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IEnumerable<StudyRoom> GetAll()
    {
        return  _db.StudyRooms.ToList();
    }
}