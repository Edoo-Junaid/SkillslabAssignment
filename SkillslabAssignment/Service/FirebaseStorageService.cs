using Firebase.Storage;
using SkillslabAssignment.Interface;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class FirebaseStorageService : IStorrageService
    {
        private readonly FirebaseStorage _storage;
        public FirebaseStorageService()
        {
            _storage = new FirebaseStorage("skillslab-9e0a3.appspot.com");
        }
        public async Task<string> UploadFileAsync(Stream stream, int trainingId, string fileName)
        {
            try
            {
                var task = _storage
                    .Child($"training_{trainingId}")
                    .Child(fileName)
                    .PutAsync(stream);
                var downloadUrl = await task;
                return downloadUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error uploading image: {ex.Message}");
                throw;
            }
        }
    }

}
