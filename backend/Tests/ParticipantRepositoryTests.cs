using Domain.Models;
using FluentAssertions;
using Infrastructure.Repository;
using Infrastructure.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.ParticipantUnitTests;

public class ParticipantRepositoryTests
{
    private DbContextOptions<RepositoryContext> _dbContextOptions =
        new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "TestEventsWebAppDB")
            .Options;

    [Fact]
    public async Task GetParticipantAsync_ShouldReturnNull_WhenParticipantDoesNotExist()
    {
        #region Arrange

        var cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;
        
        var context = new RepositoryContext(_dbContextOptions);
        var eventId = Guid.NewGuid();
        var participantId = Guid.NewGuid();
        var repository = new ParticipantsRepository(context);

        #endregion

        #region Act

        var result = await repository
            .GetParticipantByIdAsync(eventId, participantId, trackChanges: false, cancellationToken);

        #endregion

        #region Assert

        result.Should().BeNull();
        
        #endregion
    }

    [Fact]
    public async Task CreateAndGetParticipantAsync_ShouldReturnParticipant_WhenValidDto()
    {
        #region Arrange

        //test data
        var participantId = Guid.NewGuid();
        var name = "John";
        var surname = "Smith";
        var dateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12));
        var email = "john.smith@gmail.com";
        var registrationDateTime = new DateTime(2020, 01, 01, 01, 00, 00);
        var eventId = Guid.NewGuid();
        
        var participantEvent = new Event
        {
            Id = eventId,
            Name = "testevent",
            Description = "testeventdescription",
            StartDate = new DateTime(2020, 02, 01, 12, 00, 00),
            Location = "testeventlocation",
            Category = Category.Other,
            MaxParticipants = 100,
            Image = ""
        };

        var participant = new Participant
        {
            Id = participantId,
            Name = name,
            Surname = surname,
            DateOfBirth = dateOfBirth,
            Email = email,
            RegistrationTime = registrationDateTime,
            EventId = eventId,
            Event = participantEvent
        };

        var cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;
        
        //db preparation
        var context = new RepositoryContext(_dbContextOptions);
        var repository = new ParticipantsRepository(context);

        context.Participants.Add(participant);
        await context.SaveChangesAsync();

        #endregion

        #region Act

        var result = await repository
            .GetParticipantByIdAsync(eventId, participantId, trackChanges: false, cancellationToken);

        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Id.Should().Be(participantId);
        result.Name.Should().Be(name);
        result.Surname.Should().Be(surname);
        result.DateOfBirth.Should().Be(dateOfBirth);
        result.Email.Should().Be(email);
        result.RegistrationTime.Should().Be(registrationDateTime);
        result.EventId.Should().Be(eventId);

        #endregion
    }
}