using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yozian.Validation
{
    public sealed class Validator<T>
        where T : class
    {

        private readonly T model;

        private readonly IList<string> errorMsgs = new List<string>();


        #region NotAllowed

        public Validator<T> NotAllowedFor(bool matchCondition, string errorMsg)
        {
            this.addErrorsWhen(matchCondition, errorMsg);
            return this;
        }

        public Validator<T> NotAllowedFor(Func<T, bool> matchCondition, string errorMsg)
        {
            this.addErrorsWhen(matchCondition(model), errorMsg);
            return this;
        }

        public Validator<T> NotAllowedForWhen(bool validateWhen, bool matchCondition, string errorMsg)
        {
            if (!validateWhen)
            {
                return this;
            }

            this.addErrorsWhen(matchCondition, errorMsg);
            return this;
        }

        public Validator<T> NotAllowedForWhen(Func<T, bool> validateWhen, Func<T, bool> matchCondition, string errorMsg)
        {
            if (!validateWhen(this.model))
            {
                return this;
            }

            this.addErrorsWhen(matchCondition(model), errorMsg);
            return this;
        }




        #endregion


        #region OnlyAccept

        public Validator<T> OnlyAcceptFor(Func<T, bool> matchCondition, string errorMsg)
        {
            this.addErrorsWhen(!matchCondition(model), errorMsg);
            return this;
        }

        public Validator<T> OnlyAcceptFor(bool matchCondition, string errorMsg)
        {
            this.addErrorsWhen(!matchCondition, errorMsg);
            return this;
        }

        public Validator<T> OnlyAcceptForWhen(Func<T, bool> validateWhen, Func<T, bool> matchCondition, string errorMsg)
        {
            if (!validateWhen(this.model))
            {
                return this;
            }

            this.addErrorsWhen(!matchCondition(model), errorMsg);
            return this;
        }

        public Validator<T> OnlyAcceptForWhen(bool validateWhen, bool matchCondition, string errorMsg)
        {
            if (!validateWhen)
            {
                return this;
            }

            this.addErrorsWhen(!matchCondition, errorMsg);
            return this;
        }

        #endregion

        private void addErrorsWhen(bool condition, string errorMsg)
        {
            if (condition)
            {
                this.errorMsgs.Add(errorMsg);
            }
        }


        internal Validator(T model)
        {
            this.model = model;
        }

        public string GetFirstError()
        {
            return this.errorMsgs.FirstOrDefault();
        }

        public string GetAggregateErrosAsString()
        {
            return string.Join("\n", this.errorMsgs);
        }

        public IEnumerable<string> GetAggregateErros()
        {
            return this.errorMsgs.AsEnumerable();
        }

        public void ThrowErrorIfPresents()
        {
            if (this.errorMsgs.Count > 0)
            {
                throw new ValidationException(this.errorMsgs.First(), this.model);
            }
        }

        public void ThrowAllErrorsIfPresents()
        {
            if (this.errorMsgs.Count > 0)
            {
                throw new AggregateValidationException(this.errorMsgs, this.model);
            }
        }

    }
}
