using System.Threading.Tasks;

namespace OneExpense.API.Interfaces
{
    public interface IImageFileService
    {
        Task<string> Upload(string file, string imageName);
    }
}
