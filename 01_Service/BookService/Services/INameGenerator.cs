using System.Threading.Tasks;

namespace BookService.Services
{
    public interface INameGenerator
    {
        ValueTask<string> GenerateBookTitleAsync();
    }
}
