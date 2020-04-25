using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IImageFileService
    {
        Task<string> Upload(string file, string imageName);
    }
}
