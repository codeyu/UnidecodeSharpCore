# UnidecodeSharpCore
UnidecodeSharpCore is .NET Core library, written in C#.

[![Build status](https://ci.appveyor.com/api/projects/status/qr3dhvf8aw1jq1ks?svg=true)](https://ci.appveyor.com/project/codeyu/unidecodesharpcore)

Nuget: [![NuGet](https://img.shields.io/nuget/dt/UnidecodeSharpCore.svg)](https://www.nuget.org/packages/UnidecodeSharpCore/)

It fork from [unidecodesharpfork](https://bitbucket.org/DimaStefantsov/unidecodesharpfork).
It reference from [Gist](https://gist.github.com/neremin/6db39a951c7c8032ef5a).

It provides `string` or `char` extension method `Unidecode()` that returns ASCII transliterations of unicode text. It supports almost all unicode letters, including Chinese, Cyrillic, Umlauts and etc. For more details please look at [Perl description](http://search.cpan.org/~sburke/Text-Unidecode-0.04/lib/Text/Unidecode.pm#DESCRIPTION)

Generally, idea is:
```cs
("北京").Unidecode() == "Bei Jing "
```

## Usage
```cs
        [Fact]
        public void PythonTest()
        {
            Assert.Equal("Hello, World!", "Hello, World!".Unidecode());
            Assert.Equal("Ni Hao ", "你好".Unidecode());
            Assert.Equal("Xian Zai Shi Fan Ti Zi ", "現在是繁體字".Unidecode());
            Assert.Equal("'\"\r\n", "'\"\r\n".Unidecode());
            Assert.Equal("CZSczs", "ČŽŠčžš".Unidecode());
            Assert.Equal("a", "ア".Unidecode());
            Assert.Equal("a", "α".Unidecode());
            Assert.Equal("a", "а".Unidecode());
            Assert.Equal("chateau", "ch\u00e2teau".Unidecode());
            Assert.Equal("vinedos", "vi\u00f1edos".Unidecode());
        }
```

## Python code
[unidecode](https://github.com/avian2/unidecode)
