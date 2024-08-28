using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Models
{
    public class TeamImage
    {
        public int TeamId { get; set; }
        public byte[] ImageData { get; set; }
    }
}
