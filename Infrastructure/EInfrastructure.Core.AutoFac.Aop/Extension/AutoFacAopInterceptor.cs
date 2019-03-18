using Castle.DynamicProxy;
using EInfrastructure.Core.Interface.IOC;

namespace EInfrastructure.Core.AutoFac.Aop.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoFacAopInterceptor : IAopInterceptor
    {
        public virtual void Before(object[] parameters)
        {
        }


        public virtual void After(object[] parameters)
        {
        }


        public void Intercept(IInvocation invocation)
        {
            
        }
    }
}