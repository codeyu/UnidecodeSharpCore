using System;
using System.Text;
using Xunit;

namespace UnidecodeSharpCore.Test
{
    public class UnidecoderTest
    {
        [Fact]
        public void ChineseTest()
        {
            var value1 = "\u5317\u4EB0".Unidecode();
            var value2 = "你好".Unidecode();
            var value3 = "現在是繁體字".Unidecode();
            Assert.Equal("Bei Jing ", value1);
            Assert.Equal("Ni Hao ", value2);
            Assert.Equal("Xian Zai Shi Fan Ti Zi ", value3);
        }

        [Fact]
        public void MixTest()
        {
            var value1 = "窳荀彧yu".Unidecode();
            var value2 = "你好abc".Unidecode();
            var value3 = "現在是繁體字abc123".Unidecode();
            var value4 = "窳荀彧yu".Unidecode(UnidecodeOptions.RemoveSpace);
            var value5 = "你好abc".Unidecode(UnidecodeOptions.RemoveSpaceAndToLower);
            var value6 = "現在是繁體字abc123".Unidecode(UnidecodeOptions.RemoveSpaceAndToUpper);
            var value7 = "窳荀yu彧".Unidecode(UnidecodeOptions.ToUpper);
            var value8 = "你好abc".Unidecode(UnidecodeOptions.ToLower);
            Assert.Equal("Yu Xun Yu yu", value1);
            Assert.Equal("Ni Hao abc", value2);
            Assert.Equal("Xian Zai Shi Fan Ti Zi abc123", value3);
            Assert.Equal("YuXunYuyu", value4);
            Assert.Equal("nihaoabc", value5);
            Assert.Equal("XIANZAISHIFANTIZIABC123", value6);
            Assert.Equal("YU XUN YUYU", value7);
            Assert.Equal("ni hao abc", value8);
        }

        [Fact]
        public void CustomTest()
        {
            Assert.Equal("Rabota s kirillitsey", "Работа с кириллицей".Unidecode());
            Assert.Equal("aouoAOUO", "äöűőÄÖŨŐ".Unidecode());
        }

        [Fact]
        public void PythonTest()
        {
            Assert.Equal("Hello, World!", "Hello, World!".Unidecode());

            Assert.Equal("'\"\r\n", "'\"\r\n".Unidecode());
            Assert.Equal("CZSczs", "ČŽŠčžš".Unidecode());
            Assert.Equal("a", "ア".Unidecode());
            Assert.Equal("a", "α".Unidecode());
            Assert.Equal("a", "а".Unidecode());
            Assert.Equal("chateau", "ch\u00e2teau".Unidecode());
            Assert.Equal("vinedos", "vi\u00f1edos".Unidecode());
        }

        /// <summary>
        /// According to http://en.wikipedia.org/wiki/Romanization_of_Russian BGN/PCGN.
        /// http://en.wikipedia.org/wiki/BGN/PCGN_romanization_of_Russian
        /// With converting "ё" to "yo".
        /// </summary>
        [Fact]
        public void RussianAlphabetTest()
        {
            string russianAlphabetLowercase = "а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ъ ы ь э ю я".Unidecode();
            string russianAlphabetUppercase = "А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ъ Ы Ь Э Ю Я".Unidecode();

            string expectedLowercase = "a b v g d e yo zh z i y k l m n o p r s t u f kh ts ch sh shch \" y ' e yu ya";
            string expectedUppercase = "A B V G D E Yo Zh Z I Y K L M N O P R S T U F Kh Ts Ch Sh Shch \" Y ' E Yu Ya";

            Assert.Equal(expectedLowercase, russianAlphabetLowercase);
            Assert.Equal(expectedUppercase, russianAlphabetUppercase);
        }

        [Fact]
        public void CharUnidecodeTest()
        {
            string input = "а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ъ ы ь э ю я А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ъ Ы Ь Э Ю Я";
            string expected = "a b v g d e yo zh z i y k l m n o p r s t u f kh ts ch sh shch \" y ' e yu ya A B V G D E Yo Zh Z I Y K L M N O P R S T U F Kh Ts Ch Sh Shch \" Y ' E Yu Ya";

            var sb = new StringBuilder(expected.Length);
            foreach (char c in input)
            {
                sb.Append(c.Unidecode());
            }
            string result = sb.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UnidecodeOnNullShouldReturnEmptyString()
        {
            Assert.Equal("", ((string)null).Unidecode());
        }
    }
}
