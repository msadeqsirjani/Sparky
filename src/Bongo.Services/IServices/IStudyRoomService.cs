using Bongo.Models.Models;

namespace Bongo.Services.IServices;

public interface IStudyRoomService
{
    IEnumerable<StudyRoom> GetAll();
}