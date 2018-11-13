
/*
COPYRIGHT
Character transliteration tables:
Copyright 2001, Sean M. Burke <sburke@cpan.org>, all rights reserved.
Python code:
Copyright 2009, Tomaz Solc <tomaz@zemanta.com>
CSharp code:
Copyright 2010, Oleg Usanov <oleg@usanov.net>
Refactorings (2015) - Nikolay Eremin <neremin@gmail.com>
The programs and documentation in this dist are distributed in the
hope that they will be useful, but without any warranty; without even
the implied warranty of merchantability or fitness for a particular
purpose.
This library is free software; you can redistribute it and/or modify
it under the same terms as Perl.
*/

using System;
using System.Text;
namespace UnidecodeSharpCore
{
    public static partial class Unidecoder
{
    /// <summary>
    /// Transliterate an Unicode object into an ASCII string
    /// </summary>
    /// <remarks>
    /// unidecode(u"\u5317\u4EB0") == "Bei Jing "
    /// </remarks>
    /// <param name="input">The input.</param>
    /// <param name="options"></param>
    /// <returns>ASCII encoded string.</returns>
    public static string Unidecode(this string input, UnidecodeOptions options = UnidecodeOptions.Default)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }

        var output = new StringBuilder(input.Length * 2);

        foreach (var symbol in input)
        {

            var result = Unidecode(symbol);
            if (string.IsNullOrEmpty(result))
            {
                continue;
            }
            output.Append(result);

        }

        switch (options)
        {
            case UnidecodeOptions.ToLower:
                return output.ToString().Trim(' ').ToLower();
            case UnidecodeOptions.ToUpper:
                return output.ToString().Trim(' ').ToUpper();
            case UnidecodeOptions.RemoveSpace:
                return output.ToString().Trim(' ').Replace(" ", "");
            case UnidecodeOptions.RemoveSpaceAndToLower:
                return output.ToString().Trim(' ').Replace(" ", "").ToLower();
            case UnidecodeOptions.RemoveSpaceAndToUpper:
                return output.ToString().Trim(' ').Replace(" ", "").ToUpper();
            case UnidecodeOptions.Default:
                return output.ToString().Trim(' ');
            default:
                return output.ToString().Trim(' ');
        }
        
    }
    /// <summary>
        /// Transliterate Unicode character to ASCII string.
        /// </summary>
        /// <param name="c">Character you want to transliterate into ASCII</param>
        /// <returns>
        ///     ASCII string. Unknown(?) unicode characters will return [?] (3 characters).
        ///     It is this way in Python code as well.
        /// </returns>
        public static string Unidecode(this char c)
        {
            string result;
            if (c < 0x80)
            {
                result = new string(c, 1);
            }
            else
            {
                var high = c >> 8;
                var low = c & 0xff;
                result = Characters.Value.TryGetValue(high, out var values) ? values.Value[low] : "";
            }

            return result;
        }
}
}
