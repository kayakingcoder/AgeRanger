﻿
namespace AgeRanger.Service.DTO
{
    public partial class PersonDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? Age { get; set; }
        public AgeGroupDTO AgeGroup { get; set; }
    }
}
