using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Models
{
    public class HouseholdMembers
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid HouseholdId { get; set; }
        public Boolean CommunityAgreement { get; set; }
        public Boolean NewVow { get; set; }
        public Boolean Redeal { get; set; }
    }
}
