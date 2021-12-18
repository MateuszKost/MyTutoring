using MyTutoring.BlobStorageManager.Containers;

namespace MyTutoring.BlobStorageManager.Context
{
    public interface IStorageContext<TContainter>  where TContainter: IStorageContainer
    {
       public void AddAsync(TContainter containter, byte[] file, string name);
       public void DeleteAsync(TContainter containter, IEnumerable<string> names);
       public Task<byte[]> GetAsync(TContainter containter, string name);

    }
}
