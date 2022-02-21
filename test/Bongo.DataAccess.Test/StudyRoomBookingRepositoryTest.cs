using System;
using System.Collections.Generic;
using System.Linq;
using Bongo.DataAccess.Repository;
using Bongo.Models.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bongo.DataAccess.Test;

public class StudyRoomBookingRepositoryTest
{
    private readonly StudyRoomBooking _studyRoomBookingOne;
    private readonly StudyRoomBooking _studyRoomBookingTwo;

    public StudyRoomBookingRepositoryTest()
    {
        _studyRoomBookingOne = new StudyRoomBooking
        {
            FirstName = "Ben1",
            LastName = "Spark1",
            Date = new DateTime(2023, 1, 1),
            Email = "ben1@gmail.com",
            BookingId = 11,
            StudyRoomId = 1
        };

        _studyRoomBookingTwo = new StudyRoomBooking
        {
            FirstName = "Ben2",
            LastName = "Spark2",
            Date = new DateTime(2023, 1, 2),
            Email = "ben2@gmail.com",
            BookingId = 22,
            StudyRoomId = 2
        };
    }

    [Fact]
    public void SaveBook_BookingOne_CheckTheValuesFromDatabase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Temp_Bongo_0")
            .Options;

        using var writeContext = new ApplicationDbContext(options);
        var repository = new StudyRoomBookingRepository(writeContext);

        repository.Book(_studyRoomBookingOne);

        using var readContext = new ApplicationDbContext(options);
        var studyRoomBookings = readContext.StudyRoomBookings.FirstOrDefault();

        studyRoomBookings.Should().BeEquivalentTo(_studyRoomBookingOne);
    }

    [Fact]
    public void GetAll_BookingOneAndTwoWithNullDataTimeInput_CheckFromDatabase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Temp_Bongo_1")
            .Options;

        using var writeContext = new ApplicationDbContext(options);
        var repository = new StudyRoomBookingRepository(writeContext);

        repository.Book(_studyRoomBookingOne);
        repository.Book(_studyRoomBookingTwo);

        using var readContext = new ApplicationDbContext(options);
        var studyRoomBookings = new StudyRoomBookingRepository(readContext).GetAll(null);

        var expectedResult = new List<StudyRoomBooking>
        {
            _studyRoomBookingOne,
            _studyRoomBookingTwo
        };

        studyRoomBookings.Should().BeEquivalentTo(expectedResult.OrderBy(x => x.BookingId));
    }

    [Fact]
    public void GetAll_BookingOneAndTwoWithDataTimeInput_CheckFromDatabase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Temp_Bongo_2")
            .Options;

        using var writeContext = new ApplicationDbContext(options);
        var repository = new StudyRoomBookingRepository(writeContext);

        repository.Book(_studyRoomBookingOne);
        repository.Book(_studyRoomBookingTwo);

        using var readContext = new ApplicationDbContext(options);
        var studyRoomBookings = new StudyRoomBookingRepository(readContext).GetAll(new DateTime(2023, 1, 1));

        var expectedResult = new List<StudyRoomBooking>
        {
            _studyRoomBookingOne
        };

        studyRoomBookings.Should().BeEquivalentTo(expectedResult);
    }
}