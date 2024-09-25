using System.Threading.Tasks;

namespace ObedientChild.App.Alice
{
    public interface IAliceService
    {
        Task<string> HandleAsync(string command, AliceNaturalLanguageUnderstanding nlu);
    }
}