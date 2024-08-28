using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.WPF.Utils
{
    public class PropertyFilterDel
    {
        public delegate bool PropertyFilter(PropertyInfo property);
    }
}
