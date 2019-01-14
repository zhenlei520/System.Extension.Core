namespace EInfrastructure.Core.AutoConfig.Interface
{
    /// <summary>
    /// 请求获取-（GC回收-主动释放） 每一次获取的对象都不是同一个
    /// </summary>
    public interface ITransientConfigModel : IConfigModel
    {
    }
}