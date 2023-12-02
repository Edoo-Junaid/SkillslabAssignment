using Firebase.Storage;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class FirebaseStorageService
    {
        private readonly FirebaseStorage _storage;

        public FirebaseStorageService()
        {
            _storage = new FirebaseStorage("skillslab-9e0a3.appspot.com");
        }

        public async Task<string> SaveImageAsync(Stream stream, string pathname)
        {
            try
            {




                var task = _storage
                    .Child("iamges")
                    .Child(pathname) // Use the random file name
                    .PutAsync(stream);

                var downloadUrl = await task;
                task.Progress.ProgressChanged += (s, e) => Debug.WriteLine($"Progress: {e.Percentage} %");
                Debug.WriteLine($"Finished uploading: {downloadUrl}");
                return downloadUrl;

            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Debug.WriteLine($"Error uploading image: {ex.Message}");
                throw; // Re-throw the exception to propagate it to the caller
            }
        }
    }

}
