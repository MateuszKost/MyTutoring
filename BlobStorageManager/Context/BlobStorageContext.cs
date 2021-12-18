﻿using Azure.Storage.Blobs;
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

            // Get a reference to a blob
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

        public async Task<byte[]> GetAsync(TContainer containter, string name)
        {
            var containerClient = _storage.GetBlobContainerClient(containter.GetContainerName());

            // Get a reference to a blob
            var blobClient = containerClient.GetBlobClient(name);
            var tmpp = blobClient.Uri;

            //var blob = containerClient.Get .GetBlockBlobReference("image.png");
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            MemoryStream downloadFileStream = new MemoryStream();
            
            await download.Content.CopyToAsync(downloadFileStream);
            
            downloadFileStream.Close();
            
            return downloadFileStream.ToArray();
        }
    }
}
