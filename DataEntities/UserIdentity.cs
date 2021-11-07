﻿#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntities
{
    public class UserIdentity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string Salt { get; set; }

        public virtual User User { get; set; }
    }
}