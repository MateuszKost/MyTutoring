namespace MyTutoring.BlobStorageManager.Containers
{
    public class TaskContainer : IStorageContainer
    {
        public string GetContainerName()
        {
            return "tasks";
        }
    }
}
