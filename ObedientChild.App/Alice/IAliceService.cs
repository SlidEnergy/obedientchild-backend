using System.Threading.Tasks;

namespace ObedientChild.App.Alice
{
    public interface IAliceService
    {
        Task<bool> HandleAsync(string command, AliceNaturalLanguageUnderstanding nlu);
    }
}