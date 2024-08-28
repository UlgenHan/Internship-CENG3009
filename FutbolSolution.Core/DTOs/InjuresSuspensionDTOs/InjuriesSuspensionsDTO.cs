using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.DTOs.InjuresSuspensionDTOs
{
    public class InjuriesSuspensionsDTO : BaseInjuriesSuspensionsDTO
    {
        public int? PlayerId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public InjuriesLink InjuriesLink { get; set; }
    }
}
