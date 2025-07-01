using PurpuraWeb.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Purpura.Models.Entities
{
    public class Goal : BaseEntity
    {
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 100)]
        public int PercentageComplete { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
