using System.ComponentModel.DataAnnotations;

namespace Purpura.Models.ViewModels
{
    public class GoalViewModel : BaseViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(350)]
        public string Description { get; set; }

        [Range(0, 100)]
        public int? PercentageComplete { get; set; }

        public bool OperationFailure { get; set; }
    }
}
