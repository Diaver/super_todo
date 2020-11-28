using System;

namespace Database.Models
{
    public abstract class DbEntity
    {
        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }

        public abstract Guid GetPrimaryKey();

        public bool IsNew()
        {
            return GetPrimaryKey() == Guid.Empty;
        }
    }
}