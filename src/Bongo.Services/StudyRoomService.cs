using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Models;
using Bongo.Services.IServices;

namespace Bongo.Services;

public class StudyRoomService : IStudyRoomService
{
    private readonly IStudyRoomRepository _studyRoomRepository;
    public StudyRoomService(IStudyRoomRepository studyRoomRepository)
    {
        _studyRoomRepository = studyRoomRepository;
    }

    public IEnumerable<StudyRoom> GetAll()
    {
        return _studyRoomRepository.GetAll();
    }
}