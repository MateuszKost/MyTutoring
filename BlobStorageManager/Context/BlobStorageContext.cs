using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MyTutoring.BlobStorageManager.Containers;
using MyTutoring.BlobStorageManager.ServiceClient;

namespace MyTutoring.BlobStorageManager.Context
{
    public class BlobStorageContext<TContainer> : BlobContainerClient, IStorageContext<TContainer> where TContainer:IStorageContainer
    {
        private readonly BlobStorageServiceClient _storage;

        public BlobStorageContext(BlobStorageServiceClient storage)
        {
            _storage = storage;
        }

        public async void AddAsync(TContainer containter, byte[] file, string name)
        {
            var containerClient = _storage.GetBlobContainerClient(containter.GetContainerName());

            var blobClient = containerClient.GetBlobClient(name);

            using(var stream = new MemoryStream(file))
            {
                await blobClient.UploadAsync(stream, true);
                stream.Close();
            }
        }

        public async void DeleteAsync(TContainer containter, IEnumerable<string> names)
        {
            var containerClient = _storage.GetBlobContainerClient(containter.GetContainerName());

            foreach(var name in names)
            {
                await containerClient.DeleteBlobIfExistsAsync(name, snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots);
            }
        }

        public async Task<Uri> GetAsync(TContainer containter, string name)
        {
            var containerClient = _storage.GetBlobContainerClient(containter.GetContainerName());

            var blobClient = containerClient.GetBlobClient(name);
            
            return blobClient.Uri;
        }
    }
}
