using System;

namespace Domain.Common
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("D");
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
    }
}
