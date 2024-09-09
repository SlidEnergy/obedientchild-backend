using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ObedientChild.Domain
{
    public class Child
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowNull]
        public byte[] Avatar { get; set; }

        public int Balance { get; set; } = 0;

        public int BigGoalId { get; set; }

        public int BigGoalBalance { get; set; } = 0;

        public int? DreamId { get; set; }

        public int DreamBalance { get; set; } = 0;

        public Child()
        {

        }

        public Child(string name)
        {
            Name = name;
        }

        public void SetBigGoal(int bigGoalId)
        {
            BigGoalId = bigGoalId;
        }

        public void SetDream(int dreamId)
        {
            DreamId = dreamId;
        }

        public void EarnCoin(int count = 1)
        {
            Balance += count;
        }

        public void SpendCoin(int count = 1)
        {
            Balance -= count;
        }
    }
}
