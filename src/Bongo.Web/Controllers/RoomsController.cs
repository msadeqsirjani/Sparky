using Bongo.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bongo.Web.Controllers;

public class RoomsController : Controller
{
    private readonly IStudyRoomService _studyRoomService;
    public RoomsController(IStudyRoomService studyRoomService)
    {
        _studyRoomService = studyRoomService;
    }

    public IActionResult Index()
    {
        return View(_studyRoomService.GetAll());
    }
}