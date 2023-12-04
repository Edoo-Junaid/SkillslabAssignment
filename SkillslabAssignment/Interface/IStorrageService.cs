using System.IO;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IStorrageService
    {
        Task<string> UploadFileAsync(Stream stream, int trainingId, string fileName);
    }
}
