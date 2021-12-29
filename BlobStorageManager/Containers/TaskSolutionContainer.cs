namespace MyTutoring.BlobStorageManager.Containers
{
    public class TaskSolutionContainer : IStorageContainer
    {
        public string GetContainerName()
        {
            return "tasksolutions";
        }
    }
}
