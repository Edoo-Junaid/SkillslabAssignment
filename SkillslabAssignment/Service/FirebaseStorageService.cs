//using Firebase.Storage;
//using SkillslabAssignment.Interface;
//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Threading.Tasks;

//namespace SkillslabAssignment.Service
//{
//    public class FirebaseStorageService : IStorrageService
//    {
//        private readonly FirebaseStorage _storage;
//        public FirebaseStorageService(string bucket)
//        {
//            _storage = new FirebaseStorage(bucket);
//        }
//        public async Task<string> UploadFileAsync(Stream stream, int trainingId, Guid fileName)
//        {
//            try
//            {
//                var task = _storage
//                    .Child($"training_{trainingId}")
//                    .Child($"{fileName}")
//                    .PutAsync(stream);
//                var downloadUrl = await task;
//                return downloadUrl;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine($"Error uploading image: {ex.Message}");
//                throw;
//            }


//        }
//    }

//}
