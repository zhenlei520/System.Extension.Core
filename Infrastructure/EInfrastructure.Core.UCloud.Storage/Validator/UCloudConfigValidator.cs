using EInfrastructure.Core.UCloud.Storage.Config;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// UCloud存储配置校验
    /// </summary>
    public class UCloudConfigValidator : AbstractValidator<UCloudStorageConfig>
    {
        public UCloudConfigValidator()
        {
            
        }
    }
}