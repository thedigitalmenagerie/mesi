using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Models
{
    public class CardWithDetail
    {
        public Guid CardId { get; set; }
        public Guid HouseholdId { get; set; }
        public Guid NeedTypeId { get; set; }
        public Guid CategoryTypeId { get; set; }
        public Guid AssignedUserId { get; set; }
        public string CardName { get; set; }
        public string CardImage { get; set; }
        public string CardDefinition { get; set; }
        public string Conception { get; set; }
        public string Planning { get; set; }
        public string Execution { get; set; }
        public string MSOC { get; set; }
        public Boolean DailyGrind { get; set; }
        public string NeedTypeName { get; set; }
        public string CategoryTypeName { get; set; }
        public List<UserDeclarations> LineItems { get; set; }
    }

    public class UserDeclarations
    {
        public Guid DeclarationId { get; set; }
        public Guid UserId { get; set; }
        public Guid DCardId { get; set; }
        public Guid UserValues { get; set; }
        public Guid UserDeletes { get; set; }
        public Guid ProfilePicture { get; set; }
    }
}
