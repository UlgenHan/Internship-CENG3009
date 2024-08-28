using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.WPF.Validations
{
    public interface IEntityValidator<T>
    {
        (bool isValid, Dictionary<string, string> messages) Validate(T entity);
    }
}
