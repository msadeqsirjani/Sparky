using System;
using Bongo.Models.Models;
using Bongo.Models.Models.ViewModels;
using Bongo.Services.IServices;
using Bongo.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Bongo.Web.Test;

public class RoomBookingControllerTest
{
    private readonly Mock<IStudyRoomBookingService> _studyRoomBookingServiceMock;
    private readonly RoomBookingController _roomBookingController;
    private readonly StudyRoomBooking _studyRoomBooking;

    public RoomBookingControllerTest()
    {
        _studyRoomBookingServiceMock = new Mock<IStudyRoomBookingService>();

        _roomBookingController = new RoomBookingController(_studyRoomBookingServiceMock.Object);

        _studyRoomBooking = new StudyRoomBooking
        {
            FirstName = "Ben",
            LastName = "Spark",
            Date = new DateTime(2022, 1, 1),
            Email = "ben.spark@gmail.com",
            StudyRoomId = 1
        };
    }

    [Fact]
    public void Index_CallRequest_ReturnAllRoomBooking()
    {
        _roomBookingController.Index();

        _studyRoomBookingServiceMock.Verify(x => x.GetAllBooking(), Times.Once);
    }

    [Fact]
    public void Book_CallRequestWithInvalidModelState_ReturnBookView()
    {
        _roomBookingController.ModelState.AddModelError("test", "test");

        var result = _roomBookingController.Book(new StudyRoomBooking()).As<ViewResult>();

        result.ViewName.Should().Be("Book");
    }

    [Fact]
    public void Book_CallRequest_ReturnNotAvailableRoom()
    {
        _studyRoomBookingServiceMock.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns(new StudyRoomBookingResult { Code = StudyRoomBookingCode.NoRoomAvailable });

        var result = _roomBookingController.Book(new StudyRoomBooking()).As<ViewResult>();

        result.Should().BeOfType<ViewResult>();
        result.ViewData.Should().NotBeNullOrEmpty();
        result.ViewData["Error"].Should().NotBeNull();
        result.ViewData["Error"].Should().Be("No Study Room available for selected date");
    }

    [Fact]
    public void Book_CallRequest_ReturnAvailableRoomAndRedirect()
    {
        _studyRoomBookingServiceMock.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking?>()))
            .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult
            {
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                Date = booking.Date,
                Email = booking.Email,
                Code = StudyRoomBookingCode.Success
            });

        var result = _roomBookingController.Book(_studyRoomBooking).As<RedirectToActionResult>();

        result.ActionName.Should().BeEquivalentTo("BookingConfirmation");
        result.RouteValues.Should().NotBeNullOrEmpty();
        result.RouteValues["FirstName"].Should().NotBeNull();
        result.RouteValues["LastName"].Should().Be(_studyRoomBooking.LastName);
        result.RouteValues["Date"].Should().Be(_studyRoomBooking.Date);
        result.RouteValues["Code"].Should().Be(StudyRoomBookingCode.Success);
        result.RouteValues["Email"].Should().Be(_studyRoomBooking.Email);
    }
}