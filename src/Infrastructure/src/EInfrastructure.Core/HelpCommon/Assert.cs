using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.HelpCommon
{
  /// <summary>
  /// 抛出错误异常
  /// </summary>
  public class Assert
  {
    /// <summary>
    /// 为空错误
    /// </summary>
    /// <param name="name"></param>
    /// <param name="errorMessageFormat">例如：账户名</param>
    public static void NotNull(object name, string errorMessageFormat)
    {
      if (string.IsNullOrEmpty(name?.ToString()))
      {
        throw new BusinessException($"{name}不能为空");
      }
    }

    /// <summary>
    /// 不相等
    /// </summary>
    /// <param name="status"></param>
    /// <param name="errorMessageFormat"></param>
    public static void True(bool status, string errorMessageFormat)
    {
      if (!status)
      {
        throw new BusinessException(string.Format(errorMessageFormat));
      }
    }
  }
}
