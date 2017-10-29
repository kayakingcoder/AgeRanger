
namespace AgeRanger.Service.DTO
{
    public partial class AgeGroupDTO
    {
        public long Id { get; set; }
        public long? MinAge { get; set; }
        public long? MaxAge { get; set; }
        public string Description { get; set; }
    }
}
