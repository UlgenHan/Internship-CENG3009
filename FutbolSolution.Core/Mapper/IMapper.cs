using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Mapper
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source,object parameter);
        TSource Map(TDestination destination,object parameter);
    }

}
