using System;

namespace WYS.BusinessLayer.BusinessHelpers
{
    public static class DataTypeConversionExtender
    {
        #region Integer Conversions

        /// <summary>
        ///   Returns -1 when selected object is null or not applicable otherwise returns converted value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static Int32 ToInt32(this object dataType)
        {
            Int32 output;
            if (dataType == null)
            {
                output = -1;
                return output;
            }
            if (!int.TryParse(dataType.ToString(), out output))
            {
                output = -1;
                return output;
            }
            return output;
        }

        /// <summary>
        ///   Returns -1 when selected object is null or not applicable otherwise returns converted value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static Int64 ToInt64(this object dataType)
        {
            Int64 output;
            if (dataType == null)
            {
                output = -1;
                return output;
            }
            if (!Int64.TryParse(dataType.ToString(), out output))
            {
                output = -1;
                return output;
            }
            return output;
        }

        /// <summary>
        ///   Returns -1 when selected object is null or not applicable otherwise returns converted value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static Int32? ToNullableInt32(this object dataType)
        {
            Int32 output;
            if (dataType == null)
            {
                return null;
            }
            if (!Int32.TryParse(dataType.ToString(), out output))
            {
                return null;
            }
            return output;
        }

        /// <summary>
        ///   Returns -1 when selected object is null or not applicable otherwise returns converted value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static Int64? ToNullableInt64(this object dataType)
        {
            Int64 output;
            if (dataType == null)
            {
                return null;
            }
            if (!Int64.TryParse(dataType.ToString(), out output))
            {
                return null;
            }
            return output;
        }

        #endregion

        #region String Conversions

        /// <summary>
        ///   Converts To String and If Object Is NUll Return Null Value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static String ObjectToString(this object dataType)
        {
            return (dataType == DBNull.Value || dataType == null) ? null : dataType.ToString();
        }

        #endregion

        #region Boolean Conversions

        /// <summary>
        ///   Converts To String and If Object Is NUll Return Null Value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static bool ToBool(this object dataType)
        {
            if (dataType == null)
            {
                return false;
            }
            switch (dataType.ToString())
            {
                case "0":
                    dataType = false;
                    break;
                case "1":
                    dataType = true;
                    break;
            }
            bool outPut;
            return bool.TryParse(dataType.ToString(), out outPut) && outPut;
        }


        /// <summary>
        ///   Converts To String and If Object Is NUll Return Null Value
        /// </summary>
        /// <param name = "dataType"></param>
        /// <returns>Returns -1 when selected object is null or not applicable otherwise returns converted value</returns>
        public static bool? ToNullableBool(this object dataType)
        {
            if (dataType == null)
            {
                return null;
            }

            switch (dataType.ToString())
            {
                case "0":
                    dataType = false;
                    break;
                case "1":
                    dataType = true;
                    break;
            }

            bool outPut;
            if (!bool.TryParse(dataType.ToString(), out outPut))
            {
                return null;
            }

            return outPut;
        }

        #endregion

        #region FLoat Conversion

        public static double ToDouble(this object dataType)
        {
            double output;
            if (dataType == null)
            {
                output = -1;
                return output;
            }
            if (!double.TryParse(dataType.ToString(), out output))
            {
                output = -1;
                return output;
            }
            return output;
        }

        public static double? ToNullableDouble(this object dataType)
        {
            double output;
            if (dataType == null)
            {
                return null;
            }
            if (!double.TryParse(dataType.ToString(), out output))
            {
                return null;
            }
            return output;
        }


        public static float ToFloat(this object dataType)
        {
            float output;
            if (dataType == null)
            {
                output = -1;
                return output;
            }
            if (!float.TryParse(dataType.ToString(), out output))
            {
                output = -1;
                return output;
            }
            return output;
        }

        public static float? ToNullableFloat(this object dataType)
        {
            float output;
            if (dataType == null)
            {
                return null;
            }
            if (!float.TryParse(dataType.ToString(), out output))
            {
                return null;
            }
            return output;
        }

        #endregion

        #region Decimal Conversion

        public static decimal ToDecimal(this object dataType)
        {
            decimal output;
            if (dataType == null)
            {
                output = -1;
                return output;
            }
            if (!decimal.TryParse(dataType.ToString(), out output))
            {
                output = -1;
                return output;
            }
            return output;
        }

        public static decimal? ToNullableDecimal(this object dataType)
        {
            decimal output;
            if (dataType == null)
            {
                return null;
            }
            if (!decimal.TryParse(dataType.ToString(), out output))
            {
                return null;
            }
            return output;
        }

        #endregion

        #region Date Time Conversion

        public static DateTime ToDateTime(this object dataType)
        {
            DateTime output;
            if (dataType == null)
            {
                return default(DateTime);
            }
            return !DateTime.TryParse(dataType.ToString(), out output) ? default(DateTime) : output;
        }

        public static DateTime? ToNullableDateTime(this object dataType)
        {
            DateTime output;
            if (dataType == null)
            {
                return null;
            }
            if (!DateTime.TryParse(dataType.ToString(), out output))
            {
                return null;
            }
            return output;
        }


        public static TimeSpan ToTimeSpan(this object dataType)
        {
            TimeSpan output;
            if(dataType == null)
            {
                return default(TimeSpan);
            }
            return !TimeSpan.TryParse(dataType.ToString(), out output) ? default(TimeSpan) : output;
        }

        #endregion
    }
}