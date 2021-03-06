using MyTutoring.BlobStorageManager.Containers;

namespace MyTutoring.BlobStorageManager.Context
{
    public interface IStorageContext<TContainter>  where TContainter: IStorageContainer
    {
       public Task AddAsync(TContainter containter, byte[] file, string name);
       public void DeleteAsync(TContainter containter, IEnumerable<string> names);
       public Task<Uri> GetAsync(TContainter containter, string name);
    }
}
