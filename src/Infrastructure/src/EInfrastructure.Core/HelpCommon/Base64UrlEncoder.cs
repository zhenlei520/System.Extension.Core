using System;
using System.Globalization;
using System.Text;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// Url编码/解码
    /// </summary>
    public class Base64UrlEncoder
    {
        private static char base64PadCharacter = '=';

        private static readonly string DoubleBase64PadCharacter =
            string.Format(CultureInfo.InvariantCulture, "{0}{0}", base64PadCharacter);

        private static char base64Character62 = '+';
        private static char base64Character63 = '/';
        private static char base64UrlCharacter62 = '-';
        private static char _base64UrlCharacter63 = '_';

        #region 编码

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string Encode(string arg)
        {
            if (null == arg)
            {
                throw new ArgumentNullException(arg);
            }

            return Encode(Encoding.UTF8.GetBytes(arg));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inArray"></param>
        /// <returns></returns>
        public static string Encode(byte[] inArray)
        {
            if (inArray == null)
            {
                throw new ArgumentNullException("inArray");
            }

            string s = Convert.ToBase64String(inArray, 0, inArray.Length);
            s = s.Split(base64PadCharacter)[0]; // Remove any trailing padding
            s = s.Replace(base64Character62, base64UrlCharacter62); // 62nd char of encoding
            s = s.Replace(base64Character63, _base64UrlCharacter63); // 63rd char of encoding

            return s;
        }

        #endregion

        #region 解码

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static byte[] DecodeBytes(string str)
        {
            if (null == str)
            {
                throw new ArgumentNullException(nameof(str));
            }

            // 62nd char of encoding
            str = str.Replace(base64UrlCharacter62, base64Character62);

            // 63rd char of encoding
            str = str.Replace(_base64UrlCharacter63, base64Character63);

            // check for padding
            switch (str.Length % 4)
            {
                case 0:
                    // No pad chars in this case
                    break;
                case 2:
                    // Two pad chars
                    str += DoubleBase64PadCharacter;
                    break;
                case 3:
                    // One pad char
                    str += base64PadCharacter;
                    break;
                default:
                    throw new FormatException(str);
            }

            return Convert.FromBase64String(str);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string Decode(string arg)
        {
            return Encoding.UTF8.GetString(DecodeBytes(arg));
        }

        #endregion
    }
}
