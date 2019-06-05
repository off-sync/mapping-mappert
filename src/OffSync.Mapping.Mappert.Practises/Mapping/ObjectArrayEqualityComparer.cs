using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.Practises.Mapping
{
    public class ObjectArrayEqualityComparer :
        IEqualityComparer<object[]>
    {
        public bool Equals(
            object[] x,
            object[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }

            for (var i = 0; i < x.Length; i++)
            {
                if (!ReferenceEquals(
                    x[i],
                    y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(
            object[] obj)
        {
            var hashCode = 59083738;

            unchecked
            {
                for (var i = 0; i < obj.Length; i++)
                {
                    hashCode =
                        hashCode * -1521134295 +
                        (obj[i] == null ?
                            0 :
                            obj[i].GetHashCode());
                }
            }

            return hashCode;
        }
    }
}
