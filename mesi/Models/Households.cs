using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Models
{
    public class HouseholdWithDetail
    {
        public Guid UserId { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public Guid HouseholdId { get; set; }
        public string HouseholdName { get; set; }
        public Boolean HasPets { get; set; }
        public Boolean HasKids { get; set; }
        public Boolean HasRomance { get; set; }
        public string StepName { get; set; }
    }

    public class Household
    {
        public Guid Id { get; set; }
        public string HouseholdName { get; set; }
        public Boolean HasPets { get; set; }
        public Boolean HasKids { get; set; }
        public Boolean HasRomance { get; set; }
        public Guid StepId { get; set; }
    }
}