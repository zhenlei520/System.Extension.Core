namespace EInfrastructure.Core.Configuration.Configurations
{
    /// <summary>
    /// 点
    /// </summary>
    public struct Points<T1, T2>
    {
        /// <summary>
        /// X
        /// 经度
        /// </summary>
        public T1 X { get; set; }

        /// <summary>
        /// Y
        /// 纬度
        /// </summary>
        public T2 Y { get; set; }
    }
}
