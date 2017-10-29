using System;
using System.Collections.Generic;

namespace AgeRanger.Entity
{
    public partial class AgeGroup
    {
        public long Id { get; set; }
        public long? MinAge { get; set; }
        public long? MaxAge { get; set; }
        public string Description { get; set; }
    }
}
