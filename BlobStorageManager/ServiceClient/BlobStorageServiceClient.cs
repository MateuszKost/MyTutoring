using Azure.Storage.Blobs;

namespace MyTutoring.BlobStorageManager.ServiceClient
{
    public class BlobStorageServiceClient : BlobServiceClient
    {
        public BlobStorageServiceClient(string connectionString) : base(connectionString)
        {
                
        }
    }
}
