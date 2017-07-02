
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
    /// <returns>ASCII encoded string.</returns>
    public static string Unidecode(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }

        var inputLength = input.Length;
        var output = new StringBuilder(inputLength * 2);
        var chars = Characters.Value;
            
        foreach (var symbol in input)
        {
            if (symbol < 0x80)
            {
                output.Append(symbol);
                continue;
            }

            var high = symbol >> 8;
            var low  = symbol & 0xff;

            WeakLazy<string[]> values;
            if (!chars.TryGetValue(high, out values))
            {
                continue;
            }
            output.Append(values.Value[low]);

        }
        return output.ToString();
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
                int high = c >> 8;
                int low = c & 0xff;
                WeakLazy<string[]> values;
                result = Characters.Value.TryGetValue(high, out values) ? values.Value[low] : "";
            }

            return result;
        }
}
}
