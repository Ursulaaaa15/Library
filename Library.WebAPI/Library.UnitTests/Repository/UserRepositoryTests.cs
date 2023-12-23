using FoodDelivery.DataAccess;
using Library.DataAccess.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using Microsoft.Extensions.DependencyModel;

namespace Library.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class UserRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        var users = new UserEntity[]
        {
            new UserEntity()
            {
                FirstName = "John",
                SecondName = "Doe",
                Patronymic = "Smith",
                Email = "john.doe@example.com",
                ReadingNumber = 12345,
                Birthday = new DateTime(2000, 1, 1),
                Login = "john_doe",
                PasswordHash = "hashedPassword",
                LibraryId = library.Id,
                TakeBookId = takeBook.Id,
                ExternalId = Guid.NewGuid()
            },
             new UserEntity()
             {
                FirstName = "Настя",
                SecondName = "Аксенова",
                Patronymic = "Сергеевна",
                Email = "sergeevnaan2018@gmail.com",
                ReadingNumber = 55555,
                Birthday = new DateTime(2003, 4, 5),
                Login = "ursula",
                PasswordHash = "hashedPassword",
                LibraryId = library.Id,
                TakeBookId = takeBook.Id,
                ExternalId = Guid.NewGuid()
             }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll();

        //assert
        actualUsers.Should().BeEquivalentTo(users, options => options.Excluding(x => x.Library));
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        var users = new UserEntity[]
        {
            new UserEntity()
            {
                FirstName = "John",
                SecondName = "Doe",
                Patronymic = "Smith",
                Email = "john.doe@example.com",
                ReadingNumber = 12345,
                Birthday = new DateTime(2000, 1, 1),
                Login = "john_doe",
                PasswordHash = "hashedPassword",
                LibraryId = library.Id,
                TakeBookId = takeBook.Id,
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                FirstName = "Alice",
                SecondName = "Johnson",
                Patronymic = "Brown",
                Email = "alice.j@example.com",
                ReadingNumber = 54321,
                Birthday = new DateTime(2000, 1, 1),
                Login = "alice_j",
                PasswordHash = "anotherPassword",
                LibraryId = library.Id,
                TakeBookId = takeBook.Id,
                ExternalId = Guid.NewGuid()
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        // Execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll(x => x.FirstName == "John").ToArray();

        // Assert
        actualUsers.Should().BeEquivalentTo(users.Where(x => x.FirstName == "John"),
            options => options.Excluding(x => x.Library));
    }

    [Test]
    public void SaveNewUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        // Execute
        var user = new UserEntity()
        {
            FirstName = "Настя",
            SecondName = "Аксенова",
            Patronymic = "Сергеевна",
            Email = "sergeevnaan2018@gmail.com",
            ReadingNumber = 55555,
            Birthday = new DateTime(2003, 4, 5),
            Login = "ursula",
            PasswordHash = "hashedPassword",
            LibraryId = library.Id,
            TakeBookId = takeBook.Id,
            ExternalId = Guid.NewGuid()
        };

        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        // Assert
        var actualUser = context.Users.SingleOrDefault();

        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Library)
            .Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ExternalId));

        actualUser.Id.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void UpdateUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        var user = new UserEntity()
        {
            FirstName = "Настя",
            SecondName = "Аксенова",
            Patronymic = "Сергеевна",
            Email = "sergeevnaan2018@gmail.com",
            ReadingNumber = 55555,
            Birthday = new DateTime(2003, 4, 5),
            Login = "ursula",
            PasswordHash = "hashedPassword",
            LibraryId = library.Id,
            TakeBookId = takeBook.Id,
            ExternalId = Guid.NewGuid()
        };

        context.Users.Add(user);
        context.SaveChanges();

        // Execute
        user.FirstName = "newName";
        user.Email = "newEmail";
        user.PasswordHash = "newPassword";

        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        // Assert
        var actualUser = context.Users.SingleOrDefault();

        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Library));
    }

    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        var user = new UserEntity()
        {
            FirstName = "Настя",
            SecondName = "Аксенова",
            Patronymic = "Сергеевна",
            Email = "sergeevnaan2018@gmail.com",
            ReadingNumber = 55555,
            Birthday = new DateTime(2003, 4, 5),
            Login = "ursula",
            PasswordHash = "hashedPassword",
            LibraryId = library.Id,
            TakeBookId = takeBook.Id,
            ExternalId = Guid.NewGuid()
        };

        context.Users.Add(user);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Delete(user);

        //assert
        context.Users.Count().Should().Be(0);
    }

    [Test]
    public void GetById_IntId_ReturnsUser()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        var user = new UserEntity()
        {
            FirstName = "John",
            SecondName = "Doe",
            Patronymic = "Smith",
            Email = "john.doe@example.com",
            ReadingNumber = 12345,
            Birthday = new DateTime(2000, 1, 1),
            Login = "john_doe",
            PasswordHash = "hashedPassword",
            LibraryId = library.Id,
            TakeBookId = takeBook.Id,
            ExternalId = Guid.NewGuid()
        };

        context.Users.Add(user);
        context.SaveChanges();

        // Execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var retrievedUser = repository.GetById(user.Id);

        // Assert
        retrievedUser.Should().NotBeNull();
        retrievedUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Library));
    }

    [Test]
    public void GetByIdGuidIdReturnsUser()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "my Library",
            Address = "my Adress",
            BuildingHistory = "my BuildingHistory",
            ExternalId = Guid.NewGuid()
        };
        context.Libraries.Add(library);

        var takeBook = new TakeBookEntity()
        {
            Taken = DateTime.Now,
            ExternalId = Guid.NewGuid()
        };
        context.TakeBooks.Add(takeBook);

        var user = new UserEntity()
        {
            FirstName = "John",
            SecondName = "Doe",
            Patronymic = "Smith",
            Email = "john.doe@example.com",
            ReadingNumber = 12345,
            Birthday = new DateTime(2000, 1, 1),
            Login = "john_doe",
            PasswordHash = "hashedPassword",
            LibraryId = library.Id,
            TakeBookId = takeBook.Id,
            ExternalId = Guid.NewGuid()
        };

        context.Users.Add(user);
        context.SaveChanges();

        // Execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var retrievedUser = repository.GetById(user.ExternalId);

        // Assert
        retrievedUser.Should().NotBeNull();
        retrievedUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Library));
    }


    [SetUp]
    public void SetUp()
    {
        CleanUp();
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        using (var context = DbContextFactory.CreateDbContext())
        {
            context.Users.RemoveRange(context.Users);
            context.Libraries.RemoveRange(context.Libraries);
            context.SaveChanges();
        }
    }
}
