using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Schedule.io.Core.DomainObjects
{
    public abstract class ValueObject<T>
    where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            if (ReferenceEquals(valueObject, null))
                return false;

            if (GetType() != obj.GetType())
                return false;

            return EqualsCore(valueObject);
        }

        protected virtual bool EqualsCore(T other)
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                object value1 = property.GetValue(this, null);
                object value2 = property.GetValue(other, null);
                if (!value1.Equals(value2))
                    return false;
            }
            return true;
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
