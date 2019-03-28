// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text;
using EInfrastructure.Core.Words.Extension.ImportDict.Common.Entities;
using EInfrastructure.Core.Words.Extension.ImportDict.Common.Enum;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common
{
    internal interface IWordLibraryImport
    {
        int CountWord { get; set; }
        
        int CurrentStatus { get; set; }
        
        bool IsText { get; }
        
        CodeTypeEnum CodeType { get; }
        
        WordLibraryList Import(string path);
        
        WordLibraryList ImportLine(string str);
        
    }

    internal interface IWordLibraryTextImport : IWordLibraryImport
    {
        Encoding Encoding { get; }
        
        WordLibraryList ImportText(string text);
    }

    internal interface IWordLibraryExport
    {
        Encoding Encoding { get; }
        
        CodeTypeEnum CodeType { get; }
        
        string Export(WordLibraryList wlList);
        
        string ExportLine(WordLibrary wl);
    }

    internal interface IMultiCodeType
    {
        CodeTypeEnum CodeType { get; }
    }

    internal interface IStreamPrepare
    {
        void Prepare();
    }
}