using System;

namespace Yozian.Validation
{
    public static class Validation
    {
        public static Validator<T> Entry<T>(T @this)
            where T : class
        {
            return new Validator<T>(@this);
        }
    }
}
