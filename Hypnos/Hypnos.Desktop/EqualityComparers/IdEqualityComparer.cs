using System;
using System.Collections;

namespace Hypnos.Desktop.EqualityComparers
{
    public class IdEqualityComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null ||
                x == DBNull.Value || y == DBNull.Value)
            {
                return false;
            }

            return x is short left
                && y is short right
                && left == right;
        }

        public int GetHashCode(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
