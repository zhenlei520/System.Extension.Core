// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Tools
{
  /// <summary>
  /// 特殊需求，针对编号做处理
  /// 此类严格执行(方法设置>初始化预设>系统预设)的规则
  /// </summary>
  public class NumberTypeCommon
  {
    #region 私有属性
    /// <summary>
    /// 判断连号的数字长度，满足多少位可以称为连号
    /// </summary>
    private static int IsSeriesCount { get; set; }

    /// <summary>
    /// 吉祥号码规则
    /// </summary>
    private static List<int> AuspiciousNumberList { get; set; }

    /// <summary>
    /// 默认设置连号数字长度规则为5位
    /// </summary>
    private const int IsSeriesCountPresent = 5;
    #endregion

    #region 判断是否全部相等

    /// <summary>
    /// 判断数字是否全部相等
    /// </summary>
    /// <param name="number">待验证的数字</param>
    /// <returns></returns>
    public static bool IsEqualNumber(int number)
    {
      int[] num = number.ToString().Select(s => int.Parse(s.ToString())).ToArray();
      for (int i = 0; i < num.Length - 1; i++)
      {
        if (num[i] != num[i + 1])
        {
          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// 判断字符串是否全部相等
    /// </summary>
    /// <param name="number">待验证的字符串</param>
    /// <returns></returns>
    public static bool IsEqualNumber(string number)
    {
      int[] num = number.Select(s => int.Parse(s.ToString())).ToArray();
      for (int i = 0; i < num.Length - 1; i++)
      {
        if (num[i] != num[i + 1])
        {
          return false;
        }
      }
      return true;
    }

    #endregion

    #region 判断号码是否连续（非从首位验证，中间存在连续数字也可以）
    /// <summary>
    /// 判断号码是否连续（非从首位验证，中间存在连续数字也可以）
    /// </summary>
    /// <param name="number">代校验号码信息</param>
    /// <param name="isSeriesCount">判断连号的数字长度,默认为-1，不设定，优先级高于预设连续号码规则</param>
    /// <returns></returns>
    public static bool IsSeries(int number, int isSeriesCount = -1)
    {
      int[] num = number.ToString().Select(s => int.Parse(s.ToString())).ToArray();
      int isSeriesCountTemp = isSeriesCount == -1 ? IsSeriesCount : isSeriesCount;
      if (isSeriesCountTemp < 1)
        isSeriesCountTemp = IsSeriesCountPresent;
      int executionCount = 0;//记录连号的长度
      for (int i = 0; i < num.Length - 1; i++)
      {
        if (num[i] == num[i + 1] + 1)
        {
          executionCount++;
        }
        else
        {
          if (executionCount > 0)
          {
            executionCount--;
          }
        }
      }
      if (executionCount >= isSeriesCountTemp)
        return true;//超过IsSeriesCount位数字连续视为连号
      executionCount = 0;
      for (int i = 0; i < num.Length - 1; i++)
      {
        if (num[i] == num[i + 1] - 1)
        {
          executionCount++;
        }
        else
        {
          if (executionCount > 0)
          {
            executionCount--;
          }
        }
      }
      return executionCount >= isSeriesCountTemp;
    }
    #endregion

    #region 吉祥号码规则

    /// <summary>
    /// 是否吉祥号
    /// </summary>
    /// <param name="number">需要验证的号码</param>
    /// <param name="auspiciousNumberList">吉祥号码规则，临时使用,优先级高于预设吉祥号码规则</param>
    /// <returns></returns>
    public static bool IsAuspiciousNumber(int number, List<int> auspiciousNumberList = null)
    {
      var auspiciousNumberListTemp = auspiciousNumberList ?? AuspiciousNumberList;
      auspiciousNumberListTemp.IsNullOrEmptyTip("吉祥号码规则未设置");
      if (auspiciousNumberListTemp != null && auspiciousNumberListTemp.Contains(number))
        return true;
      return false;
    }

    #endregion

    #region 初始化规则配置
    /// <summary>
    /// 初始化规则配置
    /// </summary>
    /// <param name="isSeriesCount">满足多少位视为连号，默认不配置此规则</param>
    /// <param name="auspiciousNumberList">输入预订吉祥号码列表</param>
    public static void InitRegularConfig(int isSeriesCount = -1, List<int> auspiciousNumberList = null)
    {
      IsSeriesCount = isSeriesCount != -1 ? IsSeriesCount : IsSeriesCountPresent;
      if (auspiciousNumberList != null)
        AuspiciousNumberList = auspiciousNumberList;
    }

    #endregion
  }
}
