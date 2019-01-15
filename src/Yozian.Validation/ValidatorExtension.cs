using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Yozian.Extension;

namespace Yozian.Validation
{
    public static class ValidatorExtension
    {

        public static Validator<T> NotNullOrEmpty<T>(
               this Validator<T> @this,
               Expression<Func<T, string>> expression,
               string fieldName = "",
               string errorMessagePattern = "field {0} is NOT allowed to be null or empty."
           )
            where T : class
        {

            return @this.Validate(
                    expression,
                    v => string.IsNullOrEmpty(v),
                    fieldName,
                    errorMessagePattern
                );
        }

        public static Validator<T> GreaterThan<T>(
                this Validator<T> @this,
                Expression<Func<T, int>> expression,
                int baseValue = 0,
                string fieldName = "",
                string errorMessagePattern = "field {0} should be greater than {baseValue}"
            )
            where T : class
        {

            errorMessagePattern = errorMessagePattern.Replace("{baseValue}", baseValue.ToString());

            return @this.Validate(
                    expression,
                    v => v < baseValue,
                    fieldName,
                    errorMessagePattern
                );
        }

        public static Validator<T> GreaterThan<T>(
                this Validator<T> @this,
                Expression<Func<T, long>> expression,
                int baseValue = 0,
                string fieldName = "",
                string errorMessagePattern = "field {0} should be greater than {baseValue}"
            )
            where T : class
        {

            errorMessagePattern = errorMessagePattern.Replace("{baseValue}", baseValue.ToString());

            return @this.Validate(
                    expression,
                    v => v < baseValue,
                    fieldName,
                    errorMessagePattern
                );
        }


        public static Validator<TModel> Validate<TModel, TProperty>(
                this Validator<TModel> @this,
                Expression<Func<TModel, TProperty>> expression,
                Func<TProperty, bool> matchedForNotAllowed,
                string fieldName = "",
                string errorMessagePattern = "{0} has wrong data format"
            )
            where TModel : class
        {

            var expressionInfo = new MyExpressionInfo<TModel, TProperty>(expression);

            if (string.IsNullOrEmpty(fieldName))
            {
                fieldName = expressionInfo.Name;
            }

            return @this.NotAllowedFor(x => matchedForNotAllowed(expressionInfo.Func(x)), string.Format(errorMessagePattern, fieldName));
        }


        private class MyExpressionInfo<TModel, TValue>
        {
            public string Name { get; private set; }
            public Func<TModel, TValue> Func { get; private set; }

            public MyExpressionInfo(Expression<Func<TModel, TValue>> expression, Type attributeType = null)
            {
                this.Name = expression.GetMemberName();


                // current comment out the DisplayAttribute access

                //if (!(expression.Body is MemberExpression expressionBody))
                //{
                //    expressionBody = (expression.Body as UnaryExpression).Operand as MemberExpression;
                //}
                //if (expressionBody != null)
                //{
                //    var attr = expressionBody
                //           .Member
                //           .GetCustomAttributes(typeof(DisplayAttribute), false)
                //           .FirstOrDefault() as DisplayAttribute;

                //    if (attr != null)
                //    {

                //        this.Name = attr.Name;
                //    }
                //}

                this.Func = expression.Compile();
            }

        }
    }
}
