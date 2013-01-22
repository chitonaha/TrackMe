using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Support.SafeCast
{
    public static class SafeCast
    {

        public static DateTime NullDate { get { return Convert.ToDateTime("1901-1-1"); } }

        public static DateTime ToDateTime(object value)
        {
            return value == DBNull.Value || value == null ? Convert.ToDateTime("1901-1-1") : Convert.ToDateTime(value);
        }

        public static int ToInteger(object value)
        {
            int result = 0;
            if (value != DBNull.Value && value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    result = Convert.ToInt32(value);
                }
            }
            return result;
        }

        public static decimal ToDecimal(object value)
        {
            decimal result = 0;
            if (value != DBNull.Value && value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    result = Convert.ToDecimal(value);
                }
            }
            return result;
        }

        public static double ToDouble(object value)
        {
            double result = 0;
            if (value != DBNull.Value && value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    result = Convert.ToDouble(value);
                }
            }
            return result;
        }

        public static int? ToNullableInteger(object value)
        {
            return value == DBNull.Value || value == null ? null : (Nullable<int>)Convert.ToInt32(value);
        }

        public static decimal? ToNullableDecimal(object value)
        {
            decimal? result = null;
            if (value != DBNull.Value && value != null)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    result = null;
                else
                    result = Convert.ToDecimal(value);
            }
            return result;
        }

        public static double? ToNullableDouble(object value)
        {
            double? result = null;
            if (value != DBNull.Value && value != null)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    result = null;
                else
                    result = Convert.ToDouble(value);
            }
            return result;
        }

        public static bool ToBoolean(object value)
        {
            bool result = false;
            if (value != DBNull.Value || value != null)
            {
                if (value.ToString().Equals("1"))
                    result = true;
                else
                    result = Convert.ToBoolean(value);
            }
            return result;
        }
    }
}
