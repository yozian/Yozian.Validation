using System;

namespace Yozian.Validation
{
    public static class Validation
    {
        public static Validator<T> Entry<T>(T @this)
            where T : class
        {
            if (null == @this)
            {
                throw new ArgumentException("Validation object cant NOT be NULL");
            }

            return new Validator<T>(@this);
        }
    }
}
