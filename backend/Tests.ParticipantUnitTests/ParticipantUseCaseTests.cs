using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Models;
using Domain.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Application.UseCases.UseCases.Participant;
using Application.DTO.Participants;
using Infrastructure.RequestFeatures;

namespace Tests.ParticipantUnitTests;

public class ParticipantUseCaseTests
{
    private DbContextOptions<RepositoryContext> _dbContextOptions =
        new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "TestEventsWebAppDB")
            .Options;
    
    [Fact]
    public async Task CreateParticipantUseCase_ShouldCreateParticipant_WhenParticipantDtoIsValid()
    {
        #region Arrange

        //test data
        var name = "John";
        var surname = "Smith";
        var dateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12));
        var email = "john.smith@gmail.com";
        var eventId = Guid.NewGuid();

        var participantDto = new ParticipantForCreationDto
        {
            Name = name,
            Surname = surname,
            DateOfBirth = dateOfBirth.ToLongDateString(),
            Email = email
        };

        var participantEntity = new Participant
        {
            Name = name,
            Surname = surname,
            DateOfBirth = dateOfBirth,
            Email = email,
            EventId = eventId
        };
        
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

        //mocking 
        var repositoryMock = new Mock<IRepositoryManager>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(mapper => mapper.Map<Participant>(participantDto))
            .Returns(participantEntity);
        repositoryMock.Setup(repo => repo.Event.GetEventByIdAsync(eventId, false))
            .ReturnsAsync(participantEvent); 
        repositoryMock.Setup(repo => repo.Participant.CreateParticipant(eventId, participantEntity));
        repositoryMock.Setup(repo => repo.SaveAsync())
            .Returns(Task.CompletedTask);
        
        var useCase = new CreateParticipantUseCase(
            repositoryMock.Object,
            mapperMock.Object
        );
        
        #endregion

        #region Act

        await useCase.ExecuteAsync(eventId, participantDto, trackChanges:false);

        #endregion

        #region Assert

        repositoryMock.Verify(repo => repo.Participant
            .CreateParticipant(eventId, participantEntity), Times.Once);
        
        repositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);

        #endregion
    }

    [Fact]
    public async Task DeleteParticipantUseCase_ShouldDeleteParticipant_WhenParticipantExists()
    {
        #region Arrange
    
        //test data
        var participantId = Guid.NewGuid();
        var name = "John";
        var surname = "Smith";
        var dateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12));
        var email = "john.smith@gmail.com";
        var eventId = Guid.NewGuid();
        
        var participantEntity = new Participant
        {
            Id = participantId,
            Name = name,
            Surname = surname,
            DateOfBirth = dateOfBirth,
            Email = email,
            EventId = eventId
        };
        
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
        
        //mocking
        var repositoryMock = new Mock<IRepositoryManager>();

        repositoryMock.Setup(repo => repo.Event.GetEventByIdAsync(eventId, false))
            .ReturnsAsync(participantEvent); 
        repositoryMock.Setup(repo => repo.Participant
                .GetParticipantByIdAsync(eventId, participantId, false))
            .ReturnsAsync(participantEntity);
        repositoryMock.Setup(repo => repo.Participant.DeleteParticipant(participantEntity));
        repositoryMock.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        var useCase = new DeleteParticipantUseCase(repositoryMock.Object);
        
        #endregion

        #region Act

        await useCase.ExecuteAsync(eventId, participantId, trackChanges:false);
        
        #endregion

        #region Assert

        repositoryMock.Verify(repo => 
            repo.Participant.GetParticipantByIdAsync(eventId, participantId, false), Times.Once);
        repositoryMock.Verify(repo 
            => repo.Participant.DeleteParticipant(participantEntity), Times.Once);
        repositoryMock.Verify(repo 
            => repo.SaveAsync(), Times.Once);
        
        #endregion
    }

    [Fact]
    public async Task GetParticipantsUseCase_ShouldReturnPagedResult_WhenParticipantsExist()
    {
        #region Arrange

        //test data
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
        
        var participantParameters = new ParticipantParameters
        {
            PageNumber = 1,
            PageSize = 5
        };

        var participants = new List<Participant>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "1",
                Surname = "1",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12)),
                Email = "test1@gmail.com",
                EventId = eventId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "2",
                Surname = "2",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12)),
                Email = "test2@gmail.com",
                EventId = eventId
            }
        };

        var participantsPagedList = new PagedList<Participant>(
            participants, 
            participants.Count, 
            participantParameters.PageNumber, 
            participantParameters.PageSize);

        var participantsDto = participants.Select(p => new ParticipantDto
        {
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth.ToLongDateString(),
            Email = p.Email,
            RegistrationTime = p.RegistrationTime.ToLongDateString(),
            EventId = p.EventId
        });

        //mocking
        var repositoryMock = new Mock<IRepositoryManager>();
        var mapperMock = new Mock<IMapper>();

        repositoryMock.Setup(repo => repo.Event.GetEventByIdAsync(eventId, false))
            .ReturnsAsync(participantEvent); 
        
        repositoryMock.Setup(repo => repo.Participant
                .GetParticipantsAsync(eventId, participantParameters, false))
            .ReturnsAsync(participantsPagedList);
        
        mapperMock.Setup(mapper => mapper.Map<IEnumerable<ParticipantDto>>(participants))
            .Returns(participantsDto);

        var useCase = new GetParticipantsUseCase(
            repositoryMock.Object,
            mapperMock.Object);
        
        #endregion

        #region Act

        var result = await useCase.ExecuteAsync(eventId, participantParameters, trackChanges: false);
        
        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.participants.Should().HaveCount(participants.Count);
        result.metaData.TotalPages.Should().Be(1); 
        result.metaData.TotalCount.Should().Be(participants.Count); 
        
        #endregion
    }

    [Fact]
    public async Task GetParticipantByIdAsync_ShouldReturnParticipant_WhenParticipantExists()
    {
        #region Arrange

        //test data
        var participantId = Guid.NewGuid();
        var name = "John";
        var surname = "Smith";
        var dateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12));
        var email = "john.smith@gmail.com";
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
        
        var participantDto = new ParticipantDto
        {
            Name = name,
            Surname = surname,
            DateOfBirth = dateOfBirth.ToLongDateString(),
            Email = email
        };

        var participantEntity = new Participant
        {
            Id = participantId,
            Name = name,
            Surname = surname,
            DateOfBirth = dateOfBirth,
            Email = email,
            EventId = eventId
        };
        
        //mocking
        var repositoryMock = new Mock<IRepositoryManager>();
        var mapperMock = new Mock<IMapper>();

        repositoryMock.Setup(repo => repo.Event.GetEventByIdAsync(eventId, false))
            .ReturnsAsync(participantEvent); 
        
        repositoryMock.Setup(repo => repo.Participant
                .GetParticipantByIdAsync(eventId, participantId, false))
            .ReturnsAsync(participantEntity);
        
        mapperMock.Setup(mapper => mapper.Map<ParticipantDto>(participantEntity))
            .Returns(participantDto);
        
        var useCase = new GetParticipantByIdUseCase(
            repositoryMock.Object, 
            mapperMock.Object);
        
        #endregion

        #region Act

        var result = await useCase.ExecuteAsync(eventId, participantId, trackChanges: false);
        
        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Name.Should().Be(name);
        result.Surname.Should().Be(surname);
        result.DateOfBirth.Should().Be(dateOfBirth.ToLongDateString());
        result.Email.Should().Be(email);
        
        #endregion
    }
}