namespace EInfrastructure.Core.Interface.Storage.Enum
{
    /// <summary>
    /// 图片缩放
    /// </summary>
    public enum ImageModeEnum
    {
        /// <summary>
        /// 不处理（原图）
        /// </summary>
        Nothing = -1,
        /// <summary>
        /// 指定高宽缩放（可能变形）   
        /// </summary>
        Hw = 0,
        /// <summary>
        /// 指定宽，高按比例   
        /// </summary>
        W = 1,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H = 2,
        /// <summary>
        /// 指定高宽裁减（不变形） 
        /// </summary>
        Cut = 3,
    }
}
