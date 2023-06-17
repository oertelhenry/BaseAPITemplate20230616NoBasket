using System;

namespace Data.Data
{
    public interface IAuditEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}