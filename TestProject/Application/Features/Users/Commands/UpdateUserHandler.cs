using Application.Features.Users.Command;
using Application.Features.Users.Handler;
using Application.Interfaces;
using Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Application.Features.Users.Commands
{
    public class UpdateUserHandler
    {

        [Fact]
        public async Task WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateCommand = new UpdateUserCommand
            (
                userId,             
                "Ana",
                "Camila",
                "Valencia",
                "Osorio",
                new DateTime(1985, 5, 10),
                6000
            ); 

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                              .ReturnsAsync((User?)null);

            var handler = new UpdateUserCommandHandler(userRepositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(updateCommand, CancellationToken.None));
            Assert.Equal("Usuario no encontrado.", exception.Message);

            userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
