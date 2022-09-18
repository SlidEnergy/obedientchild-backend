using System.Threading.Tasks;

namespace ObedientChild.Infrastructure.SearchImages
{
    public interface ISearchImageService
    {
        Task<SearchImagesResult> SearchAsync(string searchText);
    }
}
