using BookService.Controllers;
using BookService.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookServiceTests
{
    public class BooksControllerTests
    {
        private readonly INameGenerator nameGenerator = A.Fake<INameGenerator>();
        private readonly BooksDemoDataOptions optionsValue;

        private readonly BooksController subjectUnderTest;

        public BooksControllerTests()
        {
            optionsValue = new BooksDemoDataOptions
            {
                MinimumNumberOfBooks = 1,
                MaximumNumberOfBooks = 1
            };
            var optionsMonitor = A.Fake<IOptionsMonitor<BooksDemoDataOptions>>();
            A.CallTo(() => optionsMonitor.CurrentValue).Returns(optionsValue);
            subjectUnderTest = new BooksController(nameGenerator, optionsMonitor);
        }

        [Fact]
        public async Task ItShallReturnBooksContainingTitle()
        {
            // Given
            A.CallTo(()=> nameGenerator.GenerateBookTitleAsync()).Returns(new ValueTask<string>("expected-book-title"));

            // When
            var books = await subjectUnderTest.Get();

            // Then
            books.Should().HaveCount(1);
            books.All(b => b.Title == "expected-book-title").Should().BeTrue();
        }

        
        [Fact]
        public async Task ItShallRespectChangedLimitConfiguration()
        {
            // Given
            optionsValue.MinimumNumberOfBooks = 10;
            optionsValue.MaximumNumberOfBooks = 10;

            // When
            var books = await subjectUnderTest.Get();

            // Then
            books.Should().HaveCount(10);
        }
    }
}
