using GTA6Game.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTA6Game.Languages
{
    public static class LanguageManager
    {
        public static List<CultureInfo> AvailableCultures = new List<CultureInfo>()
        {
            CultureInfo.GetCultureInfo("hu-HU"),
            CultureInfo.GetCultureInfo("en-US")
        };

        /// <summary>
        /// <para>The language explicitly selected by the user. </para>
        /// <para>You can modify this value directly.</para>
        /// <para>Accepted values:</para> 
        /// <para>- full culture codes of cultures in <see cref="AvailableCultures"/> (e.g "en-US") or short language names (e.g "en")</para>
        /// <para>- empty string or null: indicates that the system language will be used</para>
        /// </summary>
        public static string PreferredLanguage
        {
            get => Settings.Default.Language;
            set
            {
                bool isAcceptable = string.IsNullOrEmpty(value) || AvailableCultures.Any(x =>
                 {
                     bool isTagMatches = x.IetfLanguageTag == value;
                     bool isLanguageNameMatches = x.TwoLetterISOLanguageName == value;
                     return isTagMatches || isLanguageNameMatches;
                 });
                if (!isAcceptable)
                {
                    return;
                }
                Settings.Default.Language = value;
                Settings.Default.Save();
            }
        }

        public static void SetThreadCulture()
        {
            if (!string.IsNullOrEmpty(PreferredLanguage))
            {
                CultureInfo preferred = AvailableCultures.Find(x =>
                {
                    bool isTagMatches = x.IetfLanguageTag == PreferredLanguage;
                    bool isLanguageNameMatches = x.TwoLetterISOLanguageName == PreferredLanguage;
                    return isTagMatches || isLanguageNameMatches;
                });

                if (preferred != null)
                {
                    Thread.CurrentThread.CurrentCulture = preferred;
                    Thread.CurrentThread.CurrentUICulture = preferred;
                    return;
                }
                PreferredLanguage = null;
            }

            var systemCulture = Thread.CurrentThread.CurrentCulture;
            var acceptedCulture = AvailableCultures.Find(x =>
            {
                bool isTagMatches = x.IetfLanguageTag == systemCulture.IetfLanguageTag;
                bool isLanguageNameMatches = x.TwoLetterISOLanguageName == systemCulture.TwoLetterISOLanguageName;
                return isTagMatches || isLanguageNameMatches;
            });

            if (acceptedCulture != null)
            {
                Thread.CurrentThread.CurrentCulture = acceptedCulture;
                Thread.CurrentThread.CurrentUICulture = acceptedCulture;
                return;
            }

            CultureInfo fallback = AvailableCultures[0];
            Thread.CurrentThread.CurrentCulture = fallback;
            Thread.CurrentThread.CurrentUICulture = fallback;
        }
    }
}
