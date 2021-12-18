namespace MyTutoring.BlobStorageManager.Containers
{
    public class FileContainer : IStorageContainer
    { 
        public string GetContainerName()
        {
            return "files";
        }
    }
}
