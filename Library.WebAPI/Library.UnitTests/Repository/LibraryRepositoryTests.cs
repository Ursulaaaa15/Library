using FoodDelivery.DataAccess;
using Library.DataAccess.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Library.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class LibraryRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllLibrariesTest()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var libraries = new LibraryEntity[]
        {
            new LibraryEntity()
            {
                Title = "City Library",
                Address = "123 Main Street, Cityville",
                BuildingHistory = "Founded in 1950",
                Users = new List<UserEntity>()
            },
            new LibraryEntity()
            {
                Title = "University Library",
                Address = "456 College Avenue, Townsville",
                BuildingHistory = "Established in 1975",
                Users = new List<UserEntity>()
            }
        };
        context.Libraries.AddRange(libraries);
        context.SaveChanges();

        // Execute
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        var actualLibraries = repository.GetAll();

        // Assert
        actualLibraries.Should().BeEquivalentTo(libraries, options => options.Excluding(x => x.Users));
    }

    [Test]
    public void GetAllLibrariesWithFilterTest()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var libraries = new LibraryEntity[]
        {
            new LibraryEntity()
            {
                Title = "City Library",
                Address = "123 Main Street, Cityville",
                BuildingHistory = "Founded in 1950",
                Users = new List<UserEntity>()
            },
            new LibraryEntity()
            {
                Title = "University Library",
                Address = "456 College Avenue, Townsville",
                BuildingHistory = "Established in 1975",
                Users = new List<UserEntity>()
            }
        };
        context.Libraries.AddRange(libraries);
        context.SaveChanges();

        // Execute
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        var actualLibraries = repository.GetAll(x => x.Title == "City Library").ToArray();

        // Assert
        actualLibraries.Should().BeEquivalentTo(libraries.Where(x => x.Title == "City Library"),
            options => options.Excluding(x => x.Users));
    }

    [Test]
    public void SaveNewLibraryTest()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        // Execute
        var library = new LibraryEntity()
        {
            Title = "Community Library",
            Address = "789 Community Square, Villageland",
            BuildingHistory = "Established in 1985",
            Users = new List<UserEntity>()
        };
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        repository.Save(library);

        // Assert
        var actualLibrary = context.Libraries.SingleOrDefault();
        actualLibrary.Should().BeEquivalentTo(library, options => options.Excluding(x => x.Users)
            .Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime));

        actualLibrary.Id.Should().NotBe(default);
        actualLibrary.ModificationTime.Should().NotBe(default);
        actualLibrary.CreationTime.Should().NotBe(default);
    }

    [Test]
    public void UpdateLibraryTest()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "City Library",
            Address = "123 Main Street, Cityville",
            BuildingHistory = "Founded in 1950",
            Users = new List<UserEntity>()
        };
        context.Libraries.Add(library);
        context.SaveChanges();

        // Execute
        library.Title = "City Central Library";
        library.Address = "456 Downtown Avenue, Cityville";
        library.BuildingHistory = "Expanded in 2005";
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        repository.Save(library);

        // Assert
        var actualLibrary = context.Libraries.SingleOrDefault();
        actualLibrary.Should().BeEquivalentTo(library, options => options.Excluding(x => x.Users));
    }

    [Test]
    public void DeleteLibraryTest()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "City Library",
            Address = "123 Main Street, Cityville",
            BuildingHistory = "Founded in 1950",
            Users = new List<UserEntity>()
        };
        context.Libraries.Add(library);
        context.SaveChanges();

        // Execute
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        repository.Delete(library);

        // Assert
        context.Libraries.Count().Should().Be(0);
    }

    [Test]
    public void GetById_IntId_ReturnsLibrary()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "City Library",
            Address = "123 Main Street, Cityville",
            BuildingHistory = "Founded in 1950",
            Users = new List<UserEntity>()
        };

        context.Libraries.Add(library);
        context.SaveChanges();

        // Execute
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        var retrievedLibrary = repository.GetById(library.Id);

        // Assert
        retrievedLibrary.Should().NotBeNull();
        retrievedLibrary.Should().BeEquivalentTo(library, options => options.Excluding(x => x.Users));
    }

    [Test]
    public void GetById_GuidId_ReturnsLibrary()
    {
        // Prepare
        using var context = DbContextFactory.CreateDbContext();

        var library = new LibraryEntity()
        {
            Title = "City Library",
            Address = "123 Main Street, Cityville",
            BuildingHistory = "Founded in 1950",
            Users = new List<UserEntity>()
        };

        context.Libraries.Add(library);
        context.SaveChanges();

        // Execute
        var repository = new Repository<LibraryEntity>(DbContextFactory);
        var retrievedLibrary = repository.GetById(library.ExternalId);

        // Assert
        retrievedLibrary.Should().NotBeNull();
        retrievedLibrary.Should().BeEquivalentTo(library, options => options.Excluding(x => x.Users));
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

