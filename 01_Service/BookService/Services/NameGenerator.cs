using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookService.Services
{
    /// <summary>
    /// Implements a generator for dummy book names
    /// </summary>
    public class DummyDataNameGenerator : INameGenerator
    {
        // This is just a dummy. We need to read that from blob storage later
        private const string DummyBookNameTokens = "The,A|Red,Blue|Street,Road";

        public ValueTask<string> GenerateBookTitleAsync()
        {
            var rand = new Random();
            var result = new StringBuilder();

            var parts = DummyBookNameTokens.Split('|').Select(s => s.Split(','));

            var bookTitle = string.Join(' ', parts.Select(p => p[rand.Next(p.Length)]));

            return new ValueTask<string>(bookTitle);
        }
    }
}
