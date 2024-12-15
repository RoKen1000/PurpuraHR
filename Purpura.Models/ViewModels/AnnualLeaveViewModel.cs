using Microsoft.AspNetCore.Mvc.Rendering;
using Purpura.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Purpura.Models.ViewModels
{
    public class AnnualLeaveViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        public LeaveTypes Type { get; set; }
        public IEnumerable<SelectListItem>? LeaveTypeSelectList { get; set; }
        public string? Details { get; set; }
        public bool HasOverlap { get; set; }
    }
}
