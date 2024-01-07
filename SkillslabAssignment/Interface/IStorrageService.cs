using System;
using System.IO;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IStorrageService
    {
        Task UploadFileAsync(Stream stream, int trainingId, Guid fileName, string contentType);
        Task<string> GetSignedUrlByObjectNameAsync(string fileName);
    }
}
