using Bongo.Models.Models;

namespace Bongo.DataAccess.Repository.IRepository;

public interface IStudyRoomRepository
{
    IEnumerable<StudyRoom> GetAll();
}