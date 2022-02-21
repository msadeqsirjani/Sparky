using Bongo.Models.Models;
using Bongo.Models.Models.ViewModels;
using Bongo.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bongo.Web.Controllers;

public class RoomBookingController : Controller
{
    private readonly IStudyRoomBookingService _studyRoomBookingService;

    public RoomBookingController(IStudyRoomBookingService studyRoomBookingService)
    {
        _studyRoomBookingService = studyRoomBookingService;
    }

    public IActionResult Index()
    {
        return View(_studyRoomBookingService.GetAllBooking());
    }

    public IActionResult Book()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Book(StudyRoomBooking studyRoomBooking)
    {
        IActionResult actionResult = View("Book");
        if (!ModelState.IsValid) return actionResult;
        var result = _studyRoomBookingService.BookStudyRoom(studyRoomBooking);
        switch (result.Code)
        {
            case StudyRoomBookingCode.Success:
                actionResult = RedirectToAction("BookingConfirmation", result);
                break;
            case StudyRoomBookingCode.NoRoomAvailable:
                ViewData["Error"] = "No Study Room available for selected date";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return actionResult;
    }
    public IActionResult BookingConfirmation(StudyRoomBookingResult studyRoomBooking)
    {
        return View(studyRoomBooking);
    }
}