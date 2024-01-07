using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class FirebaseStorageServiceV2 : IStorrageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "skillslab-9e0a3.appspot.com";
        private readonly UrlSigner _urlSigner;

        public FirebaseStorageServiceV2()
        {
            var googleCredential = GoogleCredential.FromFile("C:\\Users\\P12AD74\\skillslab\\final\\SkillslabAssignment\\SkillslabAssignment\\auth.json");
            _storageClient = StorageClient.Create(googleCredential);
            _urlSigner = UrlSigner.FromCredential(googleCredential);
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UploadFileAsync(Stream stream, int trainingId, Guid fileId, string contentType)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            _storageClient.UploadObjectAsync(bucket: _bucketName,
                                                        objectName: fileId.ToString(),
                                                        contentType: contentType,
                                                        source: stream).ConfigureAwait(false);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public string GetSignedUrlAsync(string fileName)
        {
            fileName = "test";
            string url = _urlSigner.Sign(_bucketName, fileName, TimeSpan.FromMinutes(100), HttpMethod.Get);
            return url;
        }

        public async Task<string> GetSignedUrlByObjectNameAsync(string fileName)
        {
            return await _urlSigner.SignAsync(_bucketName, fileName, TimeSpan.FromMinutes(100), HttpMethod.Get);
        }
    }
}
