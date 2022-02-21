using System.Collections.Generic;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bongo.Services.Test;

public class StudyRoomServiceTest
{
    private readonly Mock<IStudyRoomRepository> _studyRoomRepositoryMock;
    private readonly StudyRoomService _studyRoomService;
    private readonly List<StudyRoom> _studyRooms;


    public StudyRoomServiceTest()
    {
        _studyRooms = new List<StudyRoom>()
        {
            new() { Id = 1, RoomName = "Mashhad", RoomNumber = "A101" },
            new() { Id = 2, RoomName = "Mashhad", RoomNumber = "A102" },
            new() { Id = 3, RoomName = "Mashhad", RoomNumber = "A103" },
        };

        _studyRoomRepositoryMock = new Mock<IStudyRoomRepository>();

        _studyRoomRepositoryMock.Setup(x => x.GetAll()).Returns(_studyRooms);

        _studyRoomService = new StudyRoomService(_studyRoomRepositoryMock.Object);
    }

    [Fact]
    public void GetAll_InvokedMethod_ReturnStudyRooms()
    {
        var result = _studyRoomService.GetAll();

        _studyRoomRepositoryMock.Verify(x => x.GetAll(), Times.Once);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_studyRooms);
    }
}