using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 压缩方式
    /// </summary>
    public class CompressType : Enumeration
    {
        /// <summary>
        /// BZip2
        /// </summary>
        public static CompressType BZip2 = new CompressType(1, "BZip2");

        /// <summary>
        /// GZip
        /// </summary>
        public static CompressType GZip = new CompressType(2, "GZip");

        /// <summary>
        /// Lzw
        /// </summary>
        public static CompressType Lzw = new CompressType(3, "Lzw");

        /// <summary>
        /// Tar
        /// </summary>
        public static CompressType Tar = new CompressType(4, "Tar");

        /// <summary>
        /// Zip
        /// </summary>
        public static CompressType Zip = new CompressType(5, "Zip");

        /// <summary>
        /// Zip7
        /// </summary>
        public static CompressType Zip7=new CompressType(6,"Zip7");

        /// <summary>
        /// Rar
        /// </summary>
        public static CompressType Rar=new CompressType(7,"Rar");

        /// <summary>
        /// KZip
        /// </summary>
        public static CompressType KZip=new CompressType(8,"KZip");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">压缩方式</param>
        public CompressType(int id, string name) : base(id, name)
        {
        }
    }
}
