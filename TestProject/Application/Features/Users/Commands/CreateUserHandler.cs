using Application.Features.Users.Command;
using Application.Features.Users.Handler;
using Application.Interfaces;
using Domain;
using Moq;


namespace TestProject.Application.Features.Users.Commands
{
    public class CreateUserHandler
    {
        [Fact]
        public async Task WhenUserIsCreatedSuccess()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object);

            var command = new CreateUserCommand(
                "John",
                "Doe",
                null,
                null,
                new DateTime(1990, 1, 1),
                5000);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenRepositoryThrowsException()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object);

            var command = new CreateUserCommand(
                "Ana",
                "xxx",
                null,
                null,
                new DateTime(1990, 1, 1),
                5000);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }

        
        [Fact]
        public async Task WhenSalaryIsZero()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object);

            var command = new CreateUserCommand(
                "Ana",
                null,
                "lopez",
                null,
                new DateTime(1990, 1, 1),
                0); // Salario inválido

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
