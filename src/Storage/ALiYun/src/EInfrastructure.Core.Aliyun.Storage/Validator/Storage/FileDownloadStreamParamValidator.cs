using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Tools;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    /// 文件下载流
    /// </summary>
    public class FileDownloadStreamParamValidator : AbstractValidator<FileDownloadStreamParam>
    {
        /// <summary>
        ///
        /// </summary>
        public FileDownloadStreamParamValidator()
        {
            RuleFor(x => x.Url).Must(x => x.IsUrl()).WithMessage("访问地址异常");
        }
    }
}
