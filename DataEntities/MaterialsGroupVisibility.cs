namespace DataEntities
{
    public class MaterialsGroupVisibility
    {
        public Guid StudentId { get; set; }
        public int MaterialsGroupId { get; set; }
        public bool IsVisible { get; set; }

        public virtual Student Student { get; set; }
        public virtual MaterialsGroup MaterialsGroup { get; set; }
    }
}
