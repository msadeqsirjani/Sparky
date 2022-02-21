using Bongo.Services.IServices;
using Bongo.Web.Controllers;
using Moq;
using Xunit;

namespace Bongo.Web.Test;

public class RoomsControllerTest
{
    private readonly Mock<IStudyRoomService> _studyRoomServiceMock;
    private readonly RoomsController _roomsController;

    public RoomsControllerTest()
    {
        _studyRoomServiceMock = new Mock<IStudyRoomService>();
        _roomsController = new RoomsController(_studyRoomServiceMock.Object);
    }

    [Fact]
    public void Index_CallRequest_ReturnAllRooms()
    {
        _roomsController.Index();
        _studyRoomServiceMock.Verify(x => x.GetAll(), Times.Once);
    }
}