using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Models
{
    /// <summary>
    /// Format an exception caused by some DB error
    /// </summary>
    public class FormattedDbEntityValidationException : Exception
    {
        DbEntityValidationException _exception;
        public FormattedDbEntityValidationException(DbEntityValidationException innerException)
            : base(null, innerException)
        {
            _exception = innerException;
        }

        /// <summary>
        /// Override Message property for format the error and show more detail about it
        /// </summary>
        public override string Message
        {
            get
            {
                var sb = new StringBuilder("\r\n\r\n");

                foreach (var errors in _exception.EntityValidationErrors)
                {
                    sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        errors.Entry.Entity.GetType().FullName, errors.Entry.State));

                    foreach (var error in errors.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            error.PropertyName,
                            errors.Entry.CurrentValues.GetValue<object>(error.PropertyName),
                            error.ErrorMessage));
                    }
                }
                sb.AppendLine();

                return sb.ToString();
            }
        }
    }
}
