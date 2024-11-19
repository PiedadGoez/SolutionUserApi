using Application.Common;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Application.Features.Users.Queries
{
    public class GetUserByFilter
    {
        [Fact]
        public async Task WhenUsersExist()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = Guid.NewGuid(), FirstName = "Ana", LastName = "Lopez", Salary = 5000 },
        new User { Id = Guid.NewGuid(), FirstName = "Andrea", LastName = "Smith", Salary = 6000 }
    };

            var paginatedUsers = new Pagination<User>(users, totalRecords: 2, page: 1, pageSize: 10);

            var query = new UsersQuery
            (
                "Andrea",
                 null,
                 1,
                 10
            );

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByFilterAsync(query.FirstName, query.LastName, query.Page, query.PageSize, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(paginatedUsers);

            var handler = new GetUserByFilterHandler(userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalRecords);
            Assert.Equal(1, result.Page);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(users.Count, result.Items.Count);

            userRepositoryMock.Verify(repo => repo.GetByFilterAsync(query.FirstName, query.LastName, query.Page, query.PageSize, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenNoUsersExist()
        {
            // Arrange
            var paginatedUsers = new Pagination<User>(new List<User>(), totalRecords: 0, page: 1, pageSize: 10);

            var query = new UsersQuery
            (   "Nonexistent",
                null,
                  1,
                 10
            );

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByFilterAsync(query.FirstName, query.LastName, query.Page, query.PageSize, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(paginatedUsers);

            var handler = new GetUserByFilterHandler(userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalRecords);

            userRepositoryMock.Verify(repo => repo.GetByFilterAsync(query.FirstName, query.LastName, query.Page, query.PageSize, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
