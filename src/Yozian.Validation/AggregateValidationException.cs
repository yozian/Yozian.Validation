using System;
using System.Collections.Generic;
using System.Text;

namespace Yozian.Validation
{
    public class AggregateValidationException : Exception
    {
        public object Model { get; set; }

        public IEnumerable<string> ValidationErrors { get; set; }

        public AggregateValidationException(IEnumerable<string> errors, object model = null)
            : base(string.Join("\n", errors))
        {
            this.ValidationErrors = errors;
            this.Model = model;
        }

    }
}
