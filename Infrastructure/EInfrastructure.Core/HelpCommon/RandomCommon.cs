using System;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 随机数管理，最大值、最小值可以自己进行设定。
    /// 最大值与最小值统一设置需要在系统初始化执行
    /// 此类严格执行(方法设置>初始化预设>系统预设)的规则
    /// </summary>
    public class RandomCommon
    {
        #region 私有属性

        /// <summary>
        /// 随机数最小值
        /// </summary>
        private static int MiniNum => int.MinValue;

        /// <summary>
        /// 随机数最大值
        /// </summary>
        private static int MaxNum => int.MaxValue;

        /// <summary>
        /// 随机数长度
        /// </summary>
        private static int RandomLength => 6;

        /// <summary>
        /// 随机数来源
        /// </summary>
        private static string RandomString => "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz";

        /// <summary>
        /// 系统默认生成随机数长度
        /// </summary>
        private const int RandomLengthPresent = 6;

        /// <summary>
        /// 系统默认随机数来源
        /// </summary>
        private const string RandomStringPresent = "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz";

        private static readonly Random Random = new Random(DateTime.Now.Second);
        #endregion

        #region 产生随机字符

        /// <summary>
        /// 产生随机字符
        /// </summary>
        /// <param name="randomLength">产生随机数长度,默认为-1</param>
        /// <param name="randomString">随机数来源</param>
        /// <returns></returns>
        public static string GetRandomString(int randomLength = -1, string randomString = "")
        {
            int randomLengthTemp;//随机数长度
            if (randomLength > 0)
                randomLengthTemp = randomLength;
            else if (RandomLength > 0)
                randomLengthTemp = RandomLength;
            else
                randomLengthTemp = RandomLengthPresent;
            string randomStringTemp;//随机数来源
            if (!string.IsNullOrEmpty(randomString))
                randomStringTemp = randomString;
            else if (!string.IsNullOrEmpty(RandomString))
                randomStringTemp = RandomString;
            else
                randomStringTemp = RandomStringPresent;
            string returnValue = string.Empty;
            for (int i = 0; i < randomLengthTemp; i++)
            {
                int r = Random.Next(0, randomStringTemp.Length - 1);
                returnValue += randomStringTemp[r];
            }
            return returnValue;
        }
        #endregion

        #region 产生随机数
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="minNum">最小随机数</param>
        /// <param name="maxNum">最大随机数</param>
        /// <returns></returns>
        public static int GetRandom(int minNum = -1, int maxNum = -1)
        {
            int minNumTemp=minNum==-1? MiniNum:minNum;//最小随机数
            int maxNumTemp = maxNum == -1 ? MaxNum : maxNum;//最大随机数
            return Random.Next(minNumTemp, maxNumTemp);
        }
        #endregion

        #region 生成一个0.0到1.0的随机小数
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public double GetRandomDouble()
        {
            return Random.NextDouble();
        }
        #endregion

        #region 对一个数组进行随机排序
        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public void GetRandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换
            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;
            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int randomNum1 = GetRandom(0, arr.Length);
                int randomNum2 = GetRandom(0, arr.Length);
                //定义临时变量
                //交换两个随机数位置的值
                var temp = arr[randomNum1];
                arr[randomNum1] = arr[randomNum2];
                arr[randomNum2] = temp;
            }
        }
        #endregion
        
    }
}