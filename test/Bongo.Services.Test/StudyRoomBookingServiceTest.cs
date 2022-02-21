using System;
using System.Collections.Generic;
using System.Linq;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Models;
using Bongo.Models.Models.ViewModels;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bongo.Services.Test;

public class StudyRoomBookingServiceTest
{
    private readonly Mock<IStudyRoomBookingRepository> _studyRoomBookingRepositoryMock;
    private readonly Mock<IStudyRoomRepository> _studyRoomRepositoryMock;
    private readonly StudyRoomBookingService _studyRoomBookingService;
    private readonly StudyRoomBooking _studyRoomBooking;
    private readonly List<StudyRoom> _studyRooms;

    public StudyRoomBookingServiceTest()
    {
        _studyRoomBooking = new StudyRoomBooking
        {
            FirstName = "Ben",
            LastName = "Spark",
            Email = "ben.spark@gmail.com",
            Date = new DateTime(2022, 1, 1)
        };

        _studyRooms = new List<StudyRoom>
        {
            new() { Id = 10, RoomName = "Michigan", RoomNumber = "A202" }
        };

        _studyRoomBookingRepositoryMock = new Mock<IStudyRoomBookingRepository>();
        _studyRoomRepositoryMock = new Mock<IStudyRoomRepository>();

        _studyRoomRepositoryMock.Setup(x => x.GetAll()).Returns(_studyRooms);

        _studyRoomBookingService =
            new StudyRoomBookingService(_studyRoomBookingRepositoryMock.Object, _studyRoomRepositoryMock.Object);
    }

    [Fact]
    public void GetAllBooking_InvokedMethod_ValidateCallingMethod()
    {
        _studyRoomBookingService.GetAllBooking();
        _studyRoomBookingRepositoryMock.Verify(x => x.GetAll(null), Times.Once);
    }

    [Fact]
    public void BookStudyRoom_InvokedMethodWithNullInput_ThrowArgumentException()
    {
        var action = () =>
        {
            _studyRoomBookingService.BookStudyRoom(null);
        };

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("Value cannot be null. (Parameter 'request')")
            .WithParameterName("request");
    }

    [Fact]
    public void BookStudyRoom_SaveBookingWithAvailable_ReturnsResultWithAllValues()
    {
        StudyRoomBooking savedStudyRoomBooking = null!;
        _studyRoomBookingRepositoryMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
            .Callback<StudyRoomBooking>(studyRoomBooking => savedStudyRoomBooking = studyRoomBooking);

        _studyRoomBookingService.BookStudyRoom(_studyRoomBooking);

        _studyRoomBookingRepositoryMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);
        _studyRoomRepositoryMock.Verify(x => x.GetAll(), Times.Once);

        savedStudyRoomBooking.Should().NotBeNull();

        _studyRoomBooking.StudyRoomId = _studyRooms.Single().Id;

        savedStudyRoomBooking.Should().BeEquivalentTo(_studyRoomBooking);
    }

    [Fact]
    public void BookStudyRoom_ValidateMatchResult()
    {
        var result = _studyRoomBookingService.BookStudyRoom(_studyRoomBooking);

        result.Should().NotBeNull();
        result.FirstName.Should().Be(_studyRoomBooking.FirstName);
        result.LastName.Should().Be(_studyRoomBooking.LastName);
        result.Date.Should().Be(_studyRoomBooking.Date);
        result.Email.Should().Be(_studyRoomBooking.Email);
    }

    [Theory]
    [InlineData(true, StudyRoomBookingCode.Success)]
    [InlineData(false, StudyRoomBookingCode.NoRoomAvailable)]
    public void BookStudyRoom_RoomAvailable_ReturnCorrectCode(bool availability, StudyRoomBookingCode expected)
    {
        if(!availability) _studyRooms.Clear();

        var result = _studyRoomBookingService.BookStudyRoom(_studyRoomBooking);

        result.Should().NotBeNull();
        result.Code.Should().Be(expected);
    }
}