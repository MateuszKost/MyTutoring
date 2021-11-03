namespace DataEntities
{
    public class MaterialsGroup
    {
        public MaterialsGroup()
        {
            Materials = new HashSet<Material>();
            MaterialsGroupVisibilities = new HashSet<MaterialsGroupVisibility>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<MaterialsGroupVisibility> MaterialsGroupVisibilities { get; set; }
    }
}
