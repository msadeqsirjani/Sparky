using Bongo.Models.Models;

namespace Bongo.DataAccess.Repository.IRepository;

public interface IStudyRoomBookingRepository
{
    void Book(StudyRoomBooking booking);
    IEnumerable<StudyRoomBooking> GetAll(DateTime? dateTime);
}