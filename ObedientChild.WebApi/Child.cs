using ObedientChild.Domain;
using System.Collections.Generic;

namespace ObedientChild.WebApi.Dto
{
    public class Child
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public int Balance { get; set; }

        public int BigGoalId { get; set; }

        public int BigGoalBalance { get; set; }

        public int DreamId { get; set; }

        public int DreamBalance { get; set; }

        public List<ChildStatus> Statuses { get; set; }
    }
}
