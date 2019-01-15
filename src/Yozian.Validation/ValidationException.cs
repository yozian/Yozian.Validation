using System;
using System.Collections.Generic;
using System.Text;

namespace Yozian.Validation
{
    public class ValidationException : Exception
    {
        public object Model { get; set; }

        public ValidationException(string msg, object model = null)
            : base(msg)
        {
            this.Model = model;
        }
    }
}
