using System.Collections.Generic;
using Bongo.DataAccess.Repository;
using Bongo.Models.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bongo.DataAccess.Test;

public class StudyRoomRepositoryTest
{
    private readonly StudyRoom _studyRoomOne;
    private readonly StudyRoom _studyRoomTwo;

    public StudyRoomRepositoryTest()
    {
        _studyRoomOne = new StudyRoom { Id = 1, RoomName = "1", RoomNumber = "1" };
        _studyRoomTwo = new StudyRoom { Id = 2, RoomName = "2", RoomNumber = "2" };
    }

    [Fact]
    public void GetAll_ReturnStudyRooms()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Temp_Bongo_4")
            .Options;

        using var writeContext = new ApplicationDbContext(options);

        writeContext.StudyRooms.AddRange(new List<StudyRoom> { _studyRoomOne, _studyRoomTwo });
        writeContext.SaveChanges();

        using var readContext = new ApplicationDbContext(options);
        var repository = new StudyRoomRepository(readContext);
        repository.GetAll().Should().BeEquivalentTo(new List<StudyRoom> { _studyRoomOne, _studyRoomTwo });
    }
}