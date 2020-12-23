// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Configuration;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// Mime类型帮助类
    /// </summary>
    public class MimeTypeCommon
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly IList<MimeInfos> Mappings =
            new List<MimeInfos>()
            {
                #region Big freaking list of mime types

                new MimeInfos(".323", "text/h323"),
                new MimeInfos(".3g2", "video/3gpp2"),
                new MimeInfos(".3gp", "video/3gpp"),
                new MimeInfos(".3gp2", "video/3gpp2"),
                new MimeInfos(".3gpp", "video/3gpp"),
                new MimeInfos(".7z", "application/x-7z-compressed"),
                new MimeInfos(".aa", "audio/audible"),
                new MimeInfos(".AAC", "audio/aac"),
                new MimeInfos(".aaf", "application/octet-stream"),
                new MimeInfos(".aax", "audio/vnd.audible.aax"),
                new MimeInfos(".ac3", "audio/ac3"),
                new MimeInfos(".aca", "application/octet-stream"),
                new MimeInfos(".accda", "application/msaccess.addin"),
                new MimeInfos(".accdb", "application/msaccess"),
                new MimeInfos(".accdc", "application/msaccess.cab"),
                new MimeInfos(".accde", "application/msaccess"),
                new MimeInfos(".accdr", "application/msaccess.runtime"),
                new MimeInfos(".accdt", "application/msaccess"),
                new MimeInfos(".accdw", "application/msaccess.webapplication"),
                new MimeInfos(".accft", "application/msaccess.ftemplate"),
                new MimeInfos(".acx", "application/internet-property-stream"),
                new MimeInfos(".AddIn", "text/xml"),
                new MimeInfos(".ade", "application/msaccess"),
                new MimeInfos(".adobebridge", "application/x-bridge-url"),
                new MimeInfos(".adp", "application/msaccess"),
                new MimeInfos(".ADT", "audio/vnd.dlna.adts"),
                new MimeInfos(".ADTS", "audio/aac"),
                new MimeInfos(".afm", "application/octet-stream"),
                new MimeInfos(".ai", "application/postscript"),
                new MimeInfos(".aif", "audio/x-aiff"),
                new MimeInfos(".aifc", "audio/aiff"),
                new MimeInfos(".aiff", "audio/aiff"),
                new MimeInfos(".air", "application/vnd.adobe.air-application-installer-package+zip"),
                new MimeInfos(".amc", "application/x-mpeg"),
                new MimeInfos(".application", "application/x-ms-application"),
                new MimeInfos(".art", "image/x-jg"),
                new MimeInfos(".asa", "application/xml"),
                new MimeInfos(".asax", "application/xml"),
                new MimeInfos(".ascx", "application/xml"),
                new MimeInfos(".asd", "application/octet-stream"),
                new MimeInfos(".asf", "video/x-ms-asf"),
                new MimeInfos(".ashx", "application/xml"),
                new MimeInfos(".asi", "application/octet-stream"),
                new MimeInfos(".asm", "text/plain"),
                new MimeInfos(".asmx", "application/xml"),
                new MimeInfos(".aspx", "application/xml"),
                new MimeInfos(".asr", "video/x-ms-asf"),
                new MimeInfos(".asx", "video/x-ms-asf"),
                new MimeInfos(".atom", "application/atom+xml"),
                new MimeInfos(".au", "audio/basic"),
                new MimeInfos(".avi", "video/x-msvideo"),
                new MimeInfos(".axs", "application/olescript"),
                new MimeInfos(".bas", "text/plain"),
                new MimeInfos(".bcpio", "application/x-bcpio"),
                new MimeInfos(".bin", "application/octet-stream"),
                new MimeInfos(".bmp", "image/bmp"),
                new MimeInfos(".c", "text/plain"),
                new MimeInfos(".cab", "application/octet-stream"),
                new MimeInfos(".caf", "audio/x-caf"),
                new MimeInfos(".calx", "application/vnd.ms-office.calx"),
                new MimeInfos(".cat", "application/vnd.ms-pki.seccat"),
                new MimeInfos(".cc", "text/plain"),
                new MimeInfos(".cd", "text/plain"),
                new MimeInfos(".cdda", "audio/aiff"),
                new MimeInfos(".cdf", "application/x-cdf"),
                new MimeInfos(".cer", "application/x-x509-ca-cert"),
                new MimeInfos(".chm", "application/octet-stream"),
                new MimeInfos(".class", "application/x-java-applet"),
                new MimeInfos(".clp", "application/x-msclip"),
                new MimeInfos(".cmx", "image/x-cmx"),
                new MimeInfos(".cnf", "text/plain"),
                new MimeInfos(".cod", "image/cis-cod"),
                new MimeInfos(".config", "application/xml"),
                new MimeInfos(".contact", "text/x-ms-contact"),
                new MimeInfos(".coverage", "application/xml"),
                new MimeInfos(".cpio", "application/x-cpio"),
                new MimeInfos(".cpp", "text/plain"),
                new MimeInfos(".crd", "application/x-mscardfile"),
                new MimeInfos(".crl", "application/pkix-crl"),
                new MimeInfos(".crt", "application/x-x509-ca-cert"),
                new MimeInfos(".cs", "text/plain"),
                new MimeInfos(".csdproj", "text/plain"),
                new MimeInfos(".csh", "application/x-csh"),
                new MimeInfos(".csproj", "text/plain"),
                new MimeInfos(".css", "text/css"),
                new MimeInfos(".csv", "text/csv"),
                new MimeInfos(".cur", "application/octet-stream"),
                new MimeInfos(".cxx", "text/plain"),
                new MimeInfos(".dat", "application/octet-stream"),
                new MimeInfos(".datasource", "application/xml"),
                new MimeInfos(".dbproj", "text/plain"),
                new MimeInfos(".dcr", "application/x-director"),
                new MimeInfos(".def", "text/plain"),
                new MimeInfos(".deploy", "application/octet-stream"),
                new MimeInfos(".der", "application/x-x509-ca-cert"),
                new MimeInfos(".dgml", "application/xml"),
                new MimeInfos(".dib", "image/bmp"),
                new MimeInfos(".dif", "video/x-dv"),
                new MimeInfos(".dir", "application/x-director"),
                new MimeInfos(".disco", "text/xml"),
                new MimeInfos(".dll", "application/x-msdownload"),
                new MimeInfos(".dll.config", "text/xml"),
                new MimeInfos(".dlm", "text/dlm"),
                new MimeInfos(".doc", "application/msword"),
                new MimeInfos(".docm", "application/vnd.ms-word.document.macroEnabled.12"),
                new MimeInfos(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
                new MimeInfos(".dot", "application/msword"),
                new MimeInfos(".dotm", "application/vnd.ms-word.template.macroEnabled.12"),
                new MimeInfos(".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"),
                new MimeInfos(".dsp", "application/octet-stream"),
                new MimeInfos(".dsw", "text/plain"),
                new MimeInfos(".dtd", "text/xml"),
                new MimeInfos(".dtsConfig", "text/xml"),
                new MimeInfos(".dv", "video/x-dv"),
                new MimeInfos(".dvi", "application/x-dvi"),
                new MimeInfos(".dwf", "drawing/x-dwf"),
                new MimeInfos(".dwp", "application/octet-stream"),
                new MimeInfos(".dxr", "application/x-director"),
                new MimeInfos(".eml", "message/rfc822"),
                new MimeInfos(".emz", "application/octet-stream"),
                new MimeInfos(".eot", "application/octet-stream"),
                new MimeInfos(".eps", "application/postscript"),
                new MimeInfos(".etl", "application/etl"),
                new MimeInfos(".etx", "text/x-setext"),
                new MimeInfos(".evy", "application/envoy"),
                new MimeInfos(".exe", "application/octet-stream"),
                new MimeInfos(".exe.config", "text/xml"),
                new MimeInfos(".fdf", "application/vnd.fdf"),
                new MimeInfos(".fif", "application/fractals"),
                new MimeInfos(".filters", "Application/xml"),
                new MimeInfos(".fla", "application/octet-stream"),
                new MimeInfos(".flr", "x-world/x-vrml"),
                new MimeInfos(".flv", "video/x-flv"),
                new MimeInfos(".fsscript", "application/fsharp-script"),
                new MimeInfos(".fsx", "application/fsharp-script"),
                new MimeInfos(".generictest", "application/xml"),
                new MimeInfos(".gif", "image/gif"),
                new MimeInfos(".group", "text/x-ms-group"),
                new MimeInfos(".gsm", "audio/x-gsm"),
                new MimeInfos(".gtar", "application/x-gtar"),
                new MimeInfos(".gz", "application/x-gzip"),
                new MimeInfos(".h", "text/plain"),
                new MimeInfos(".hdf", "application/x-hdf"),
                new MimeInfos(".hdml", "text/x-hdml"),
                new MimeInfos(".hhc", "application/x-oleobject"),
                new MimeInfos(".hhk", "application/octet-stream"),
                new MimeInfos(".hhp", "application/octet-stream"),
                new MimeInfos(".hlp", "application/winhlp"),
                new MimeInfos(".hpp", "text/plain"),
                new MimeInfos(".hqx", "application/mac-binhex40"),
                new MimeInfos(".hta", "application/hta"),
                new MimeInfos(".htc", "text/x-component"),
                new MimeInfos(".htm", "text/html"),
                new MimeInfos(".html", "text/html"),
                new MimeInfos(".htt", "text/webviewhtml"),
                new MimeInfos(".hxa", "application/xml"),
                new MimeInfos(".hxc", "application/xml"),
                new MimeInfos(".hxd", "application/octet-stream"),
                new MimeInfos(".hxe", "application/xml"),
                new MimeInfos(".hxf", "application/xml"),
                new MimeInfos(".hxh", "application/octet-stream"),
                new MimeInfos(".hxi", "application/octet-stream"),
                new MimeInfos(".hxk", "application/xml"),
                new MimeInfos(".hxq", "application/octet-stream"),
                new MimeInfos(".hxr", "application/octet-stream"),
                new MimeInfos(".hxs", "application/octet-stream"),
                new MimeInfos(".hxt", "text/html"),
                new MimeInfos(".hxv", "application/xml"),
                new MimeInfos(".hxw", "application/octet-stream"),
                new MimeInfos(".hxx", "text/plain"),
                new MimeInfos(".i", "text/plain"),
                new MimeInfos(".ico", "image/x-icon"),
                new MimeInfos(".ics", "application/octet-stream"),
                new MimeInfos(".idl", "text/plain"),
                new MimeInfos(".ief", "image/ief"),
                new MimeInfos(".iii", "application/x-iphone"),
                new MimeInfos(".inc", "text/plain"),
                new MimeInfos(".inf", "application/octet-stream"),
                new MimeInfos(".inl", "text/plain"),
                new MimeInfos(".ins", "application/x-internet-signup"),
                new MimeInfos(".ipa", "application/x-itunes-ipa"),
                new MimeInfos(".ipg", "application/x-itunes-ipg"),
                new MimeInfos(".ipproj", "text/plain"),
                new MimeInfos(".ipsw", "application/x-itunes-ipsw"),
                new MimeInfos(".iqy", "text/x-ms-iqy"),
                new MimeInfos(".isp", "application/x-internet-signup"),
                new MimeInfos(".ite", "application/x-itunes-ite"),
                new MimeInfos(".itlp", "application/x-itunes-itlp"),
                new MimeInfos(".itms", "application/x-itunes-itms"),
                new MimeInfos(".itpc", "application/x-itunes-itpc"),
                new MimeInfos(".IVF", "video/x-ivf"),
                new MimeInfos(".jar", "application/java-archive"),
                new MimeInfos(".java", "application/octet-stream"),
                new MimeInfos(".jck", "application/liquidmotion"),
                new MimeInfos(".jcz", "application/liquidmotion"),
                new MimeInfos(".jfif", "image/pjpeg"),
                new MimeInfos(".jnlp", "application/x-java-jnlp-file"),
                new MimeInfos(".jpb", "application/octet-stream"),
                new MimeInfos(".jpe", "image/jpeg"),
                new MimeInfos(".jpeg", "image/jpeg"),
                new MimeInfos(".jpg", "image/jpeg"),
                new MimeInfos(".js", "application/x-javascript"),
                new MimeInfos(".json", "application/json"),
                new MimeInfos(".jsx", "text/jscript"),
                new MimeInfos(".jsxbin", "text/plain"),
                new MimeInfos(".latex", "application/x-latex"),
                new MimeInfos(".library-ms", "application/windows-library+xml"),
                new MimeInfos(".lit", "application/x-ms-reader"),
                new MimeInfos(".loadtest", "application/xml"),
                new MimeInfos(".lpk", "application/octet-stream"),
                new MimeInfos(".lsf", "video/x-la-asf"),
                new MimeInfos(".lst", "text/plain"),
                new MimeInfos(".lsx", "video/x-la-asf"),
                new MimeInfos(".lzh", "application/octet-stream"),
                new MimeInfos(".m13", "application/x-msmediaview"),
                new MimeInfos(".m14", "application/x-msmediaview"),
                new MimeInfos(".m1v", "video/mpeg"),
                new MimeInfos(".m2t", "video/vnd.dlna.mpeg-tts"),
                new MimeInfos(".m2ts", "video/vnd.dlna.mpeg-tts"),
                new MimeInfos(".m2v", "video/mpeg"),
                new MimeInfos(".m3u", "audio/x-mpegurl"),
                new MimeInfos(".m3u8", "audio/x-mpegurl"),
                new MimeInfos(".m4a", "audio/m4a"),
                new MimeInfos(".m4b", "audio/m4b"),
                new MimeInfos(".m4p", "audio/m4p"),
                new MimeInfos(".m4r", "audio/x-m4r"),
                new MimeInfos(".m4v", "video/x-m4v"),
                new MimeInfos(".mac", "image/x-macpaint"),
                new MimeInfos(".mak", "text/plain"),
                new MimeInfos(".man", "application/x-troff-man"),
                new MimeInfos(".manifest", "application/x-ms-manifest"),
                new MimeInfos(".map", "text/plain"),
                new MimeInfos(".master", "application/xml"),
                new MimeInfos(".mda", "application/msaccess"),
                new MimeInfos(".mdb", "application/x-msaccess"),
                new MimeInfos(".mde", "application/msaccess"),
                new MimeInfos(".mdp", "application/octet-stream"),
                new MimeInfos(".me", "application/x-troff-me"),
                new MimeInfos(".mfp", "application/x-shockwave-flash"),
                new MimeInfos(".mht", "message/rfc822"),
                new MimeInfos(".mhtml", "message/rfc822"),
                new MimeInfos(".mid", "audio/mid"),
                new MimeInfos(".midi", "audio/mid"),
                new MimeInfos(".mix", "application/octet-stream"),
                new MimeInfos(".mk", "text/plain"),
                new MimeInfos(".mmf", "application/x-smaf"),
                new MimeInfos(".mno", "text/xml"),
                new MimeInfos(".mny", "application/x-msmoney"),
                new MimeInfos(".mod", "video/mpeg"),
                new MimeInfos(".mov", "video/quicktime"),
                new MimeInfos(".movie", "video/x-sgi-movie"),
                new MimeInfos(".mp2", "video/mpeg"),
                new MimeInfos(".mp2v", "video/mpeg"),
                new MimeInfos(".mp3", "audio/mpeg"),
                new MimeInfos(".mp4", "video/mp4"),
                new MimeInfos(".mp4v", "video/mp4"),
                new MimeInfos(".mpa", "video/mpeg"),
                new MimeInfos(".mpe", "video/mpeg"),
                new MimeInfos(".mpeg", "video/mpeg"),
                new MimeInfos(".mpf", "application/vnd.ms-mediapackage"),
                new MimeInfos(".mpg", "video/mpeg"),
                new MimeInfos(".mpp", "application/vnd.ms-project"),
                new MimeInfos(".mpv2", "video/mpeg"),
                new MimeInfos(".mqv", "video/quicktime"),
                new MimeInfos(".ms", "application/x-troff-ms"),
                new MimeInfos(".msi", "application/octet-stream"),
                new MimeInfos(".mso", "application/octet-stream"),
                new MimeInfos(".mts", "video/vnd.dlna.mpeg-tts"),
                new MimeInfos(".mtx", "application/xml"),
                new MimeInfos(".mvb", "application/x-msmediaview"),
                new MimeInfos(".mvc", "application/x-miva-compiled"),
                new MimeInfos(".mxp", "application/x-mmxp"),
                new MimeInfos(".nc", "application/x-netcdf"),
                new MimeInfos(".nsc", "video/x-ms-asf"),
                new MimeInfos(".nws", "message/rfc822"),
                new MimeInfos(".ocx", "application/octet-stream"),
                new MimeInfos(".oda", "application/oda"),
                new MimeInfos(".odc", "text/x-ms-odc"),
                new MimeInfos(".odh", "text/plain"),
                new MimeInfos(".odl", "text/plain"),
                new MimeInfos(".odp", "application/vnd.oasis.opendocument.presentation"),
                new MimeInfos(".ods", "application/oleobject"),
                new MimeInfos(".odt", "application/vnd.oasis.opendocument.text"),
                new MimeInfos(".one", "application/onenote"),
                new MimeInfos(".onea", "application/onenote"),
                new MimeInfos(".onepkg", "application/onenote"),
                new MimeInfos(".onetmp", "application/onenote"),
                new MimeInfos(".onetoc", "application/onenote"),
                new MimeInfos(".onetoc2", "application/onenote"),
                new MimeInfos(".orderedtest", "application/xml"),
                new MimeInfos(".osdx", "application/opensearchdescription+xml"),
                new MimeInfos(".p10", "application/pkcs10"),
                new MimeInfos(".p12", "application/x-pkcs12"),
                new MimeInfos(".p7b", "application/x-pkcs7-certificates"),
                new MimeInfos(".p7c", "application/pkcs7-mime"),
                new MimeInfos(".p7m", "application/pkcs7-mime"),
                new MimeInfos(".p7r", "application/x-pkcs7-certreqresp"),
                new MimeInfos(".p7s", "application/pkcs7-signature"),
                new MimeInfos(".pbm", "image/x-portable-bitmap"),
                new MimeInfos(".pcast", "application/x-podcast"),
                new MimeInfos(".pct", "image/pict"),
                new MimeInfos(".pcx", "application/octet-stream"),
                new MimeInfos(".pcz", "application/octet-stream"),
                new MimeInfos(".pdf", "application/pdf"),
                new MimeInfos(".pfb", "application/octet-stream"),
                new MimeInfos(".pfm", "application/octet-stream"),
                new MimeInfos(".pfx", "application/x-pkcs12"),
                new MimeInfos(".pgm", "image/x-portable-graymap"),
                new MimeInfos(".pic", "image/pict"),
                new MimeInfos(".pict", "image/pict"),
                new MimeInfos(".pkgdef", "text/plain"),
                new MimeInfos(".pkgundef", "text/plain"),
                new MimeInfos(".pko", "application/vnd.ms-pki.pko"),
                new MimeInfos(".pls", "audio/scpls"),
                new MimeInfos(".pma", "application/x-perfmon"),
                new MimeInfos(".pmc", "application/x-perfmon"),
                new MimeInfos(".pml", "application/x-perfmon"),
                new MimeInfos(".pmr", "application/x-perfmon"),
                new MimeInfos(".pmw", "application/x-perfmon"),
                new MimeInfos(".png", "image/png"),
                new MimeInfos(".pnm", "image/x-portable-anymap"),
                new MimeInfos(".pnt", "image/x-macpaint"),
                new MimeInfos(".pntg", "image/x-macpaint"),
                new MimeInfos(".pnz", "image/png"),
                new MimeInfos(".pot", "application/vnd.ms-powerpoint"),
                new MimeInfos(".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"),
                new MimeInfos(".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"),
                new MimeInfos(".ppa", "application/vnd.ms-powerpoint"),
                new MimeInfos(".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"),
                new MimeInfos(".ppm", "image/x-portable-pixmap"),
                new MimeInfos(".pps", "application/vnd.ms-powerpoint"),
                new MimeInfos(".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"),
                new MimeInfos(".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"),
                new MimeInfos(".ppt", "application/vnd.ms-powerpoint"),
                new MimeInfos(".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"),
                new MimeInfos(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"),
                new MimeInfos(".prf", "application/pics-rules"),
                new MimeInfos(".prm", "application/octet-stream"),
                new MimeInfos(".prx", "application/octet-stream"),
                new MimeInfos(".ps", "application/postscript"),
                new MimeInfos(".psc1", "application/PowerShell"),
                new MimeInfos(".psd", "application/octet-stream"),
                new MimeInfos(".psess", "application/xml"),
                new MimeInfos(".psm", "application/octet-stream"),
                new MimeInfos(".psp", "application/octet-stream"),
                new MimeInfos(".pub", "application/x-mspublisher"),
                new MimeInfos(".pwz", "application/vnd.ms-powerpoint"),
                new MimeInfos(".qht", "text/x-html-insertion"),
                new MimeInfos(".qhtm", "text/x-html-insertion"),
                new MimeInfos(".qt", "video/quicktime"),
                new MimeInfos(".qti", "image/x-quicktime"),
                new MimeInfos(".qtif", "image/x-quicktime"),
                new MimeInfos(".qtl", "application/x-quicktimeplayer"),
                new MimeInfos(".qxd", "application/octet-stream"),
                new MimeInfos(".ra", "audio/x-pn-realaudio"),
                new MimeInfos(".ram", "audio/x-pn-realaudio"),
                new MimeInfos(".rar", "application/octet-stream"),
                new MimeInfos(".ras", "image/x-cmu-raster"),
                new MimeInfos(".rat", "application/rat-file"),
                new MimeInfos(".rc", "text/plain"),
                new MimeInfos(".rc2", "text/plain"),
                new MimeInfos(".rct", "text/plain"),
                new MimeInfos(".rdlc", "application/xml"),
                new MimeInfos(".resx", "application/xml"),
                new MimeInfos(".rf", "image/vnd.rn-realflash"),
                new MimeInfos(".rgb", "image/x-rgb"),
                new MimeInfos(".rgs", "text/plain"),
                new MimeInfos(".rm", "application/vnd.rn-realmedia"),
                new MimeInfos(".rmi", "audio/mid"),
                new MimeInfos(".rmp", "application/vnd.rn-rn_music_package"),
                new MimeInfos(".roff", "application/x-troff"),
                new MimeInfos(".rpm", "audio/x-pn-realaudio-plugin"),
                new MimeInfos(".rqy", "text/x-ms-rqy"),
                new MimeInfos(".rtf", "application/rtf"),
                new MimeInfos(".rtx", "text/richtext"),
                new MimeInfos(".ruleset", "application/xml"),
                new MimeInfos(".s", "text/plain"),
                new MimeInfos(".safariextz", "application/x-safari-safariextz"),
                new MimeInfos(".scd", "application/x-msschedule"),
                new MimeInfos(".sct", "text/scriptlet"),
                new MimeInfos(".sd2", "audio/x-sd2"),
                new MimeInfos(".sdp", "application/sdp"),
                new MimeInfos(".sea", "application/octet-stream"),
                new MimeInfos(".searchConnector-ms", "application/windows-search-connector+xml"),
                new MimeInfos(".setpay", "application/set-payment-initiation"),
                new MimeInfos(".setreg", "application/set-registration-initiation"),
                new MimeInfos(".settings", "application/xml"),
                new MimeInfos(".sgimb", "application/x-sgimb"),
                new MimeInfos(".sgml", "text/sgml"),
                new MimeInfos(".sh", "application/x-sh"),
                new MimeInfos(".shar", "application/x-shar"),
                new MimeInfos(".shtml", "text/html"),
                new MimeInfos(".sit", "application/x-stuffit"),
                new MimeInfos(".sitemap", "application/xml"),
                new MimeInfos(".skin", "application/xml"),
                new MimeInfos(".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"),
                new MimeInfos(".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"),
                new MimeInfos(".slk", "application/vnd.ms-excel"),
                new MimeInfos(".sln", "text/plain"),
                new MimeInfos(".slupkg-ms", "application/x-ms-license"),
                new MimeInfos(".smd", "audio/x-smd"),
                new MimeInfos(".smi", "application/octet-stream"),
                new MimeInfos(".smx", "audio/x-smd"),
                new MimeInfos(".smz", "audio/x-smd"),
                new MimeInfos(".snd", "audio/basic"),
                new MimeInfos(".snippet", "application/xml"),
                new MimeInfos(".snp", "application/octet-stream"),
                new MimeInfos(".sol", "text/plain"),
                new MimeInfos(".sor", "text/plain"),
                new MimeInfos(".spc", "application/x-pkcs7-certificates"),
                new MimeInfos(".spl", "application/futuresplash"),
                new MimeInfos(".src", "application/x-wais-source"),
                new MimeInfos(".srf", "text/plain"),
                new MimeInfos(".SSISDeploymentManifest", "text/xml"),
                new MimeInfos(".ssm", "application/streamingmedia"),
                new MimeInfos(".sst", "application/vnd.ms-pki.certstore"),
                new MimeInfos(".stl", "application/vnd.ms-pki.stl"),
                new MimeInfos(".sv4cpio", "application/x-sv4cpio"),
                new MimeInfos(".sv4crc", "application/x-sv4crc"),
                new MimeInfos(".svc", "application/xml"),
                new MimeInfos(".swf", "application/x-shockwave-flash"),
                new MimeInfos(".t", "application/x-troff"),
                new MimeInfos(".tar", "application/x-tar"),
                new MimeInfos(".tcl", "application/x-tcl"),
                new MimeInfos(".testrunconfig", "application/xml"),
                new MimeInfos(".testsettings", "application/xml"),
                new MimeInfos(".tex", "application/x-tex"),
                new MimeInfos(".texi", "application/x-texinfo"),
                new MimeInfos(".texinfo", "application/x-texinfo"),
                new MimeInfos(".tgz", "application/x-compressed"),
                new MimeInfos(".thmx", "application/vnd.ms-officetheme"),
                new MimeInfos(".thn", "application/octet-stream"),
                new MimeInfos(".tif", "image/tiff"),
                new MimeInfos(".tiff", "image/tiff"),
                new MimeInfos(".tlh", "text/plain"),
                new MimeInfos(".tli", "text/plain"),
                new MimeInfos(".toc", "application/octet-stream"),
                new MimeInfos(".tr", "application/x-troff"),
                new MimeInfos(".trm", "application/x-msterminal"),
                new MimeInfos(".trx", "application/xml"),
                new MimeInfos(".ts", "video/vnd.dlna.mpeg-tts"),
                new MimeInfos(".tsv", "text/tab-separated-values"),
                new MimeInfos(".ttf", "application/octet-stream"),
                new MimeInfos(".tts", "video/vnd.dlna.mpeg-tts"),
                new MimeInfos(".txt", "text/plain"),
                new MimeInfos(".u32", "application/octet-stream"),
                new MimeInfos(".uls", "text/iuls"),
                new MimeInfos(".user", "text/plain"),
                new MimeInfos(".ustar", "application/x-ustar"),
                new MimeInfos(".vb", "text/plain"),
                new MimeInfos(".vbdproj", "text/plain"),
                new MimeInfos(".vbk", "video/mpeg"),
                new MimeInfos(".vbproj", "text/plain"),
                new MimeInfos(".vbs", "text/vbscript"),
                new MimeInfos(".vcf", "text/x-vcard"),
                new MimeInfos(".vcproj", "Application/xml"),
                new MimeInfos(".vcs", "text/plain"),
                new MimeInfos(".vcxproj", "Application/xml"),
                new MimeInfos(".vddproj", "text/plain"),
                new MimeInfos(".vdp", "text/plain"),
                new MimeInfos(".vdproj", "text/plain"),
                new MimeInfos(".vdx", "application/vnd.ms-visio.viewer"),
                new MimeInfos(".vml", "text/xml"),
                new MimeInfos(".vscontent", "application/xml"),
                new MimeInfos(".vsct", "text/xml"),
                new MimeInfos(".vsd", "application/vnd.visio"),
                new MimeInfos(".vsi", "application/ms-vsi"),
                new MimeInfos(".vsix", "application/vsix"),
                new MimeInfos(".vsixlangpack", "text/xml"),
                new MimeInfos(".vsixmanifest", "text/xml"),
                new MimeInfos(".vsmdi", "application/xml"),
                new MimeInfos(".vspscc", "text/plain"),
                new MimeInfos(".vss", "application/vnd.visio"),
                new MimeInfos(".vsscc", "text/plain"),
                new MimeInfos(".vssettings", "text/xml"),
                new MimeInfos(".vssscc", "text/plain"),
                new MimeInfos(".vst", "application/vnd.visio"),
                new MimeInfos(".vstemplate", "text/xml"),
                new MimeInfos(".vsto", "application/x-ms-vsto"),
                new MimeInfos(".vsw", "application/vnd.visio"),
                new MimeInfos(".vsx", "application/vnd.visio"),
                new MimeInfos(".vtx", "application/vnd.visio"),
                new MimeInfos(".wav", "audio/wav"),
                new MimeInfos(".wave", "audio/wav"),
                new MimeInfos(".wax", "audio/x-ms-wax"),
                new MimeInfos(".wbk", "application/msword"),
                new MimeInfos(".wbmp", "image/vnd.wap.wbmp"),
                new MimeInfos(".wcm", "application/vnd.ms-works"),
                new MimeInfos(".wdb", "application/vnd.ms-works"),
                new MimeInfos(".wdp", "image/vnd.ms-photo"),
                new MimeInfos(".webarchive", "application/x-safari-webarchive"),
                new MimeInfos(".webtest", "application/xml"),
                new MimeInfos(".wiq", "application/xml"),
                new MimeInfos(".wiz", "application/msword"),
                new MimeInfos(".wks", "application/vnd.ms-works"),
                new MimeInfos(".WLMP", "application/wlmoviemaker"),
                new MimeInfos(".wlpginstall", "application/x-wlpg-detect"),
                new MimeInfos(".wlpginstall3", "application/x-wlpg3-detect"),
                new MimeInfos(".wm", "video/x-ms-wm"),
                new MimeInfos(".wma", "audio/x-ms-wma"),
                new MimeInfos(".wmd", "application/x-ms-wmd"),
                new MimeInfos(".wmf", "application/x-msmetafile"),
                new MimeInfos(".wml", "text/vnd.wap.wml"),
                new MimeInfos(".wmlc", "application/vnd.wap.wmlc"),
                new MimeInfos(".wmls", "text/vnd.wap.wmlscript"),
                new MimeInfos(".wmlsc", "application/vnd.wap.wmlscriptc"),
                new MimeInfos(".wmp", "video/x-ms-wmp"),
                new MimeInfos(".wmv", "video/x-ms-wmv"),
                new MimeInfos(".wmx", "video/x-ms-wmx"),
                new MimeInfos(".wmz", "application/x-ms-wmz"),
                new MimeInfos(".wpl", "application/vnd.ms-wpl"),
                new MimeInfos(".wps", "application/vnd.ms-works"),
                new MimeInfos(".wri", "application/x-mswrite"),
                new MimeInfos(".wrl", "x-world/x-vrml"),
                new MimeInfos(".wrz", "x-world/x-vrml"),
                new MimeInfos(".wsc", "text/scriptlet"),
                new MimeInfos(".wsdl", "text/xml"),
                new MimeInfos(".wvx", "video/x-ms-wvx"),
                new MimeInfos(".x", "application/directx"),
                new MimeInfos(".xaf", "x-world/x-vrml"),
                new MimeInfos(".xaml", "application/xaml+xml"),
                new MimeInfos(".xap", "application/x-silverlight-app"),
                new MimeInfos(".xbap", "application/x-ms-xbap"),
                new MimeInfos(".xbm", "image/x-xbitmap"),
                new MimeInfos(".xdr", "text/plain"),
                new MimeInfos(".xht", "application/xhtml+xml"),
                new MimeInfos(".xhtml", "application/xhtml+xml"),
                new MimeInfos(".xla", "application/vnd.ms-excel"),
                new MimeInfos(".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"),
                new MimeInfos(".xlc", "application/vnd.ms-excel"),
                new MimeInfos(".xld", "application/vnd.ms-excel"),
                new MimeInfos(".xlk", "application/vnd.ms-excel"),
                new MimeInfos(".xll", "application/vnd.ms-excel"),
                new MimeInfos(".xlm", "application/vnd.ms-excel"),
                new MimeInfos(".xls", "application/vnd.ms-excel"),
                new MimeInfos(".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"),
                new MimeInfos(".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"),
                new MimeInfos(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
                new MimeInfos(".xlt", "application/vnd.ms-excel"),
                new MimeInfos(".xltm", "application/vnd.ms-excel.template.macroEnabled.12"),
                new MimeInfos(".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"),
                new MimeInfos(".xlw", "application/vnd.ms-excel"),
                new MimeInfos(".xml", "text/xml"),
                new MimeInfos(".xmta", "application/xml"),
                new MimeInfos(".xof", "x-world/x-vrml"),
                new MimeInfos(".XOML", "text/plain"),
                new MimeInfos(".xpm", "image/x-xpixmap"),
                new MimeInfos(".xps", "application/vnd.ms-xpsdocument"),
                new MimeInfos(".xrm-ms", "text/xml"),
                new MimeInfos(".xsc", "application/xml"),
                new MimeInfos(".xsd", "text/xml"),
                new MimeInfos(".xsf", "text/xml"),
                new MimeInfos(".xsl", "text/xml"),
                new MimeInfos(".xslt", "text/xml"),
                new MimeInfos(".xsn", "application/octet-stream"),
                new MimeInfos(".xss", "application/xml"),
                new MimeInfos(".xtp", "application/octet-stream"),
                new MimeInfos(".xwd", "image/x-xwindowdump"),
                new MimeInfos(".z", "application/x-compress"),
                new MimeInfos(".zip", "application/x-zip-compressed"),

                #endregion
            };

        #region 得到mime类型

        /// <summary>
        /// 得到mime类型
        /// </summary>
        /// <param name="extension">扩展名</param>
        /// <param name="errCode">错误码</param>
        /// <param name="defaultMime">默认mime application/octet-stream</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public static string GetMimeType(string extension, int? errCode = null,
            string defaultMime = "application/octet-stream")
        {
            if (extension == null)
            {
                throw new BusinessException("extension", errCode ?? HttpStatus.Err.Id);
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime = Mappings.Where(x => x.Ext == extension).Select(x => x.MimeType).FirstOrDefault();
            return ObjectCommon.SafeObject(!mime.IsNullOrWhiteSpace(), () => ValueTuple.Create(mime, defaultMime));
        }

        #endregion
    }
}