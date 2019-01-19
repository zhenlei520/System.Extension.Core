using System.Text;
using EInfrastructure.Core.Words.Extension.ImportDict.Common.Entities;
using EInfrastructure.Core.Words.Extension.ImportDict.Common.Enum;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common
{
    public interface IWordLibraryImport
    {
        int CountWord { get; set; }
        
        int CurrentStatus { get; set; }
        
        bool IsText { get; }
        
        CodeTypeEnum CodeType { get; }
        
        WordLibraryList Import(string path);
        
        WordLibraryList ImportLine(string str);
        
    }

    public interface IWordLibraryTextImport : IWordLibraryImport
    {
        Encoding Encoding { get; }
        
        WordLibraryList ImportText(string text);
    }

    public interface IWordLibraryExport
    {
        Encoding Encoding { get; }
        
        CodeTypeEnum CodeType { get; }
        
        string Export(WordLibraryList wlList);
        
        string ExportLine(WordLibrary wl);
    }

    public interface IMultiCodeType
    {
        CodeTypeEnum CodeType { get; }
    }

    public interface IStreamPrepare
    {
        void Prepare();
    }
}