#nullable disable

namespace DataEntities
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialGroupId { get; set; }

        public virtual MaterialType MaterialType { get; set; }
        public virtual MaterialsGroup MaterialGroup { get; set; }
    }
}
