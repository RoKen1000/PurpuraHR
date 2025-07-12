using Purpura.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Models.ViewModels
{
    public class BaseViewModel
    {
        public string UserId { get; set; }
        public string? ExternalReference { get; set; }
        public Result? Result { get; set; }
    }
}
