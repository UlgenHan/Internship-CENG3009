using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.WPF.Validations
{
    public abstract class BaseEntityValidator<T> : IEntityValidator<T>
    {
        public virtual (bool isValid, Dictionary<string, string> messages) Validate(T entity)
        {
            var messages = new Dictionary<string, string>();

            // Check if the entity is null
            if (entity == null)
            {
                messages["Entity"] = "The entity cannot be null.";
                return (false, messages);
            }

            // Add other common validations here if needed

            return (true, messages); // If no validation errors, return valid
        }
    }
}
