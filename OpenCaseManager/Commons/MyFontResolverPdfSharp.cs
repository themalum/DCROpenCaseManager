using PdfSharp.Fonts;
using System;
using System.IO;

namespace OpenCaseManager.Commons
{
    internal class MyFontResolverPdfSharp : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Ignore case of font names.
            var name = "OpenSans";
            // Deal with the fonts we know.
            switch (name)
            {
                case "OpenSans":
                    if (isBold)
                    {
                        if (isItalic)
                        {
                            return new FontResolverInfo("OpenSans#bi");
                        }

                        return new FontResolverInfo("OpenSans#b");
                    }
                    if (isItalic)
                    {
                        return new FontResolverInfo("OpenSans#i");
                    }

                    return new FontResolverInfo("OpenSans#");
            }

            // We pass all other font requests to the default handler.
            // When running on a web server without sufficient permission, you can return a default font at this stage.
            return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
        }

        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "OpenSans#":
                    return LoadFontData(AppDomain.CurrentDomain.BaseDirectory + "Fonts\\OpenSans-Regular.ttf"); ;

                case "OpenSans#b":
                    return LoadFontData(AppDomain.CurrentDomain.BaseDirectory + "Fonts\\OpenSans-Bold.ttf"); ;

                case "OpenSans#i":
                    return LoadFontData(AppDomain.CurrentDomain.BaseDirectory + "Fonts\\OpenSans-Italic.ttf");

                case "OpenSans#bi":
                    return LoadFontData(AppDomain.CurrentDomain.BaseDirectory + "Fonts\\OpenSans-BoldItalic.ttf");
            }

            return null;
        }

        /// <summary>
        /// Returns the specified font from an embedded resource.
        /// </summary>
        private byte[] LoadFontData(string name)
        {
            return File.ReadAllBytes(name);
        }

        internal static MyFontResolverPdfSharp OurGlobalFontResolver = null;

        /// <summary>
        /// Ensure the font resolver is only applied once (or an exception is thrown)
        /// </summary>
        internal static void Apply()
        {
            if (OurGlobalFontResolver == null || GlobalFontSettings.FontResolver == null)
            {
                if (OurGlobalFontResolver == null)
                {
                    OurGlobalFontResolver = new MyFontResolverPdfSharp();
                }

                GlobalFontSettings.FontResolver = OurGlobalFontResolver;
            }
        }
    }
}