using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private INameGenerator nameGenerator;
        private IOptionsMonitor<BooksDemoDataOptions> options;

        public BooksController(INameGenerator nameGenerator, IOptionsMonitor<BooksDemoDataOptions> options)
        {
            this.nameGenerator = nameGenerator;
            this.options = options;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            var numberOfBooks = new Random().Next(this.options.CurrentValue.MinimumNumberOfBooks, this.options.CurrentValue.MaximumNumberOfBooks + 1);

            var result = new Book[numberOfBooks];
            for (var i = 0; i < numberOfBooks; i++)
            {
                result[i] = new Book
                {
                    Id = i,
                    Title = await this.nameGenerator.GenerateBookTitleAsync(),
                    Description = @"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, 
sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea 
rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                    Price = 42.0M
                };
            }

            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book newBook)
        {
            // For demo purposes, return an HTTP 500 error (used to demonstrate logging)
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
