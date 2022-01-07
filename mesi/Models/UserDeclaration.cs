using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Models
{
    public class UserDeclaration
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public bool? UserValues { get; set; }
        public bool? UserDeletes { get; set; }
    }
    public class UserDeclarationWithPicture
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public bool? UserValues { get; set; }
        public bool? UserDeletes { get; set; }
        public string ProfilePicture { get; set; }
    }
}
