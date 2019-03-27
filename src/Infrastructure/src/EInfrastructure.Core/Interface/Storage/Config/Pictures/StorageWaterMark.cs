namespace EInfrastructure.Core.Interface.Storage.Config.Pictures
{
    /// <summary>
    /// 图片水印
    /// </summary>
    public class StorageWaterMark
    {
        #region 类型  0表示关闭水印 1表示文字水印 2表示图片水印
        /// <summary>
        /// 类型  0表示关闭水印 1表示文字水印 2表示图片水印
        /// </summary>
        public int WaterMarkType { get; set; }
        #endregion

        #region 0左上 1中上 2右上 3左中 4居中 5右中 6左下 7中下 8右下
        /// <summary>
        /// 0左上 1中上 2右上 3左中 4居中 5右中 6左下 7中下 8右下
        /// </summary>
        public int WaterMarkPosition { get; set; }
        #endregion

        #region 图片水印地址
        /// <summary>
        /// 图片水印地址
        /// </summary>
        public string WaterMarkPic { get; set; }
        #endregion

        #region 水印的透明度
        /// <summary>
        /// 水印的透明度
        /// </summary>
        public int WaterMarkTransparency { get; set; }
        #endregion

        #region 文字水印文字的名称
        /// <summary>
        /// 文字水印文字的名称
        /// </summary>
        public string WaterMarkText { get; set; }
        #endregion

        #region 文字水印 字体
        /// <summary>
        /// 文字水印 字体
        /// </summary>
        public string WaterMarkFont { get; set; }
        #endregion

        #region 文字水印 字体大小
        /// <summary>
        /// 文字水印 字体大小
        /// </summary>
        public int WaterMarkFontSize { get; set; }
        #endregion
    }
}
