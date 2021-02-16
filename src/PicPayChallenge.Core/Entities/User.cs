using System;

namespace PicPayChallenge.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public byte UserTypePriority { get; set; }
    }

}
