using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NuciText.Conversion
{
    /// <summary>
    /// Implements the INuciTextConverter interface to provide functionality for converting text.
    /// </summary>
    public sealed class NuciTextConverter : INuciTextConverter
    {
        readonly ConcurrentDictionary<string, string> windows1252cache;

        readonly Dictionary<char, string> CommonCharacterMappings = new()
        {
            { 'А', "A" },
            { 'Α', "A" },
            { 'Ꭺ', "A" },
            { 'ꓮ', "A" },
            { 'Ά', "Á" },
            { 'Ὰ', "À" }, { 'Ȁ', "À" },
            { 'Ắ', "Ă" }, { 'Ặ', "Ă" },
            { 'Ẩ', "Â" },
            { 'Β', "B" }, { 'Ᏼ', "B" }, { 'ꓐ', "B" }, { 'Ḇ', "B" },
            { 'Χ', "Ch" },
            { 'С', "C" }, { 'Ϲ', "C" }, { 'Ꮯ', "C" }, { 'ꓚ', "C" },
            { 'Ĉ', "C" }, { 'Ц', "C" },
            { 'Ꭰ', "D" },
            { 'ꓓ', "D" },
            { 'Џ', "Dž" },
            { 'Ɖ', "Đ" },
            { 'Е', "E" }, { 'Ε', "E" }, { 'Ꭼ', "E" }, { 'ꓰ', "E" }, { 'Ɛ', "E" }, { 'Э', "E" },
            { 'Ё', "Ë" },
            { 'Έ', "É" },
            { '∃', "Ǝ" },
            { 'ꓝ', "F" }, { 'Ḟ', "F" },
            { 'Ꮐ', "G" }, { 'ꓖ', "G" },
            { 'Ƣ', "Ğ" }, // Untested in the games
            { 'Ȝ', "Gh" }, // Or G
            { 'Ɣ', "Gh" },
            { 'Ю', "Iu" },
            { 'Η', "H" }, { 'Ꮋ', "H" }, { 'ꓧ', "H" }, { 'Ḥ', "H" },
            { 'І', "I" }, { 'Ι', "I" }, { 'Ӏ', "I" }, { 'ӏ', "I" }, { 'Ί', "I" }, { 'Ɨ', "I" },
            { 'Ỉ', "Ì" },
            { 'Ї', "Ï" }, { 'Ϊ', "Ï" }, { 'Ḯ', "Ï" },
            { 'Ǐ', "Ĭ" },
            { 'Ј', "J" }, { 'Ꭻ', "J" }, { 'ꓙ', "J" },
            { 'К', "K" }, { 'Κ', "K" }, { 'Ꮶ', "K" }, { 'ꓗ', "K" },
            { 'Ќ', "Ḱ" },
            { 'Ꮮ', "L" }, { 'ꓡ', "L" }, { 'Լ', "L" },
            { 'М', "M" }, { 'Μ', "M" }, { 'Ꮇ', "M" }, { 'ꓟ', "M" }, { 'Ṁ', "M" },
            { 'Ǌ', "NJ" },
            { 'Н', "N" }, { 'Ν', "N" }, { 'ꓠ', "N" }, { 'Ṉ', "N" },
            { 'Ƞ', "Ŋ" },
            { 'О', "O" }, { 'Ο', "O" }, { 'ꓳ', "O" }, { 'Օ', "O" }, { 'Ɔ', "O" }, { 'Ợ', "O" },
            { 'Ӧ', "Ö" },
            { 'Ớ', "Ó" }, { 'Ό', "Ó" },
            { 'Ỏ', "Ò" },
            { 'Ỗ', "Ô" },
            { 'Ǒ', "Ŏ" },
            { 'Ǭ', "Ǫ" },
            { 'Р', "P" }, { 'Ρ', "P" }, { 'Ꮲ', "P" }, { 'ꓑ', "P" },
            { 'Ƿ', "Uu" }, { 'Ỽ', "Uu" }, // Or W
            { 'Ԛ', "Q" },
            { 'Ꮢ', "R" }, { 'ꓣ', "R" }, { 'Ṟ', "R" },
            { 'Ѕ', "S" }, { 'Ꮪ', "S" }, { 'ꓢ', "S" }, { 'Տ', "S" },
            { 'Ṯ', "Th" }, { 'Θ', "Th" },
            { 'Т', "T" }, { 'Τ', "T" }, { 'Ꭲ', "T" }, { 'ꓔ', "T" },
            { 'Ս', "U" }, { 'ꓴ', "U" }, { 'Ʊ', "U" },
            { 'Ǔ', "Ŭ" },
            { 'Ǚ', "Ŭ" }, // Or Ü
            { 'Ǜ', "Ü" },
            { 'В', "V" }, { 'Ꮩ', "V" }, { 'ꓦ', "V" },
            { 'Ꮃ', "W" }, { 'ꓪ', "W" }, { 'Ԝ', "W" },
            { 'Ẇ', "Ẃ" },
            { 'Х', "X" }, { 'ꓫ', "X" },
            { 'Ү', "Y" }, { 'Υ', "Y" }, { 'ꓬ', "Y" },
            { 'Ύ', "Ý" },
            { 'Ζ', "Z" }, { 'Ꮓ', "Z" }, { 'ꓜ', "Z" }, { 'Ƶ', "Z" },
            { 'Ǯ', "Ž" },

            { 'ә', "æ" },
            { 'α', "a" }, { 'а', "a" },
            { 'ὰ', "à" }, { 'ȁ', "à" },
            { 'ά', "á" }, { 'ȧ', "á" },
            { 'ӑ', "ă" }, { 'ắ', "ă" }, { 'ǎ', "ă" }, { 'ẵ', "ă" }, { 'ặ', "ă" },
            { 'ẩ', "â" },
            { 'ᏼ', "b" }, { 'ḇ', "b" },
            { 'χ', "ch" },
            { 'ĉ', "c" }, { 'ц', "c" },
            { 'ⅾ', "d" },
            { 'џ', "dž" },
            { 'е', "e" }, { 'ε', "e" }, { 'ɛ', "e" }, { 'э', "e" },
            { 'ĕ', "ě" },
            { 'ǝ', "ə" },
            { 'ё', "ë" },
            { 'έ', "é" },
            { 'ḟ', "f" },
            { 'г', "g" },
            { 'ƣ', "ğ" }, // Untested in the games
            { 'ȝ', "gh" }, // Or g
            { 'ɣ', "gh" },
            { 'ḥ', "h" },
            { 'ю', "iu" },
            { 'я', "ia" },
            { 'і', "i" }, { 'ι', "i" }, { 'ɨ', "i" },
            { 'ỉ', "ì" },
            { 'ɩ', "ı" },
            { 'ǐ', "ĭ" },
            { 'ї', "ï" }, { 'ϊ', "ï" }, { 'ΐ', "ï" }, { 'ḯ', "ï" },
            { 'ј', "j" },
            { 'к', "k" }, { 'κ', "k" },
            { 'ќ', "ḱ" },
            { 'ẖ', "kh" },
            { 'л', "l" },
            { 'ɬ', "ł" },
            { 'ƚ', "ł" },
            { 'ṁ', "m" },
            { 'н', "n" }, { 'ṉ', "n" },
            { 'ƞ', "ŋ" },
            { 'о', "o" }, { 'ο', "o" }, { 'օ', "o" }, { 'ɔ', "o" }, { 'ợ', "o" },
            { 'ӧ', "ö" },
            { 'ό', "ó" }, { 'ớ', "ó" },
            { 'ỏ', "ò" },
            { 'ỗ', "ô" },
            { 'ǒ', "ŏ" },
            { 'ǭ', "ǫ" },
            { 'р', "p" }, { 'ṗ', "p" }, { 'ɸ', "p" },
            { 'ԥ', "p" }, // It's actually ṗ but that doesn't work either
            { 'ꮢ', "r" }, { 'ṟ', "r" },
            { 'ṯ', "th" }, { 'θ', "th" },
            { 'т', "t" },
            { '‡', "t" }, // Guessed
            { 'ƿ', "uu" }, { 'ỽ', "uu" }, // Or w
            { 'у', "u" }, { 'ʊ', "u" },
            { 'ǔ', "ŭ" },
            { 'ǚ', "ŭ" }, // Or ü
            { 'ύ', "ú" },
            { 'ǜ', "ü" },
            { 'ẇ', "ẃ" },
            { 'γ', "y" },
            { 'ƶ', "z" }, { 'ᶻ', "z" },
            { 'ǯ', "ž" },

            // Characters with apostrophe that needs to be detached
            { 'ƙ', "k'" },
            { 'Ƙ', "K'" },
            { 'ư', "u'" },
            { 'Ư', "U'" },
            { 'ứ', "ú'" },
            { 'Ứ', "Ú'" },
            { 'ừ', "ù'" },
            { 'Ừ', "Ù'" },
            { 'ử', "ủ'" },
            { 'Ử', "Ủ'" },

            // Secondary accent diacritic
            { 'Ấ', "Â" },
            { 'Ḗ', "Ē" },
            { 'Ế', "Ê" },
            { 'Ṓ', "Ō" },
            { 'Ố', "Ô" },
            { 'ấ', "â" },
            { 'ḗ', "ē" },
            { 'ế', "ê" },
            { 'ṓ', "ō" },
            { 'ố', "ô" },

            // Secondary grave accent diacritic
            { 'Ầ', "Â" },
            { 'Ề', "Ê" },
            { 'Ồ', "Ô" },
            { 'ầ', "â" },
            { 'ề', "ê" },
            { 'ồ', "ô" },

            // Secondary hook diacritic
            { 'Ể', "Ê" },
            { 'Ổ', "Ô" },
            { 'ể', "ê" },
            { 'ổ', "ô" },
        };

        readonly Dictionary<char, string> Windows1252CharacterMappings = new()
        {
            { 'Ǣ', "Æ" },
            { 'Ạ', "A" }, { 'Ə', "A" },
            { 'Ả', "À" },
            { 'Ậ', "Â" },
            { 'Ă', "Ã" }, { 'Ā', "Ã" },
            { 'Ǟ', "Ä" },
            { 'Ḃ', "B" }, { 'Ḅ', "B" },
            { 'Ć', "C" }, { 'Ċ', "C" },
            { 'Č', "Ch" },
            { 'Ḏ', "D" }, { 'Ɗ', "D" }, { 'Ḑ', "D" }, { 'Ď', "D" }, { 'Ḍ', "D" },
            { 'Đ', "Ð" }, { 'Ɖ', "Ð" },
            { 'Ē', "Ë" }, { 'Ẹ', "Ë" }, { 'Ẽ', "Ë" },
            { 'Ė', "É" },
            { 'Ẻ', "È" },
            { 'Ệ', "È" }, { 'Ě', "È" },
            { 'Ę', "E" }, { 'Ǝ', "E" },
            { 'Ĕ', "Ê" },
            { 'Ğ', "G" }, { 'Ĝ', "G" }, { 'Ģ', "G" }, { 'Ǵ', "G" },
            { 'Ĥ', "H" }, { 'Ȟ', "H" }, { 'Ḧ', "H" }, { 'Ḩ', "H" }, { 'Ħ', "H" },
            { 'İ', "I" }, { 'Į', "I" }, { 'Ị', "I" },
            { 'Ĭ', "Ï" }, { 'Ī', "Ï" }, { 'Ĩ', "Ï" },
            { 'Ĵ', "J" }, { 'Ǧ', "J" },
            { 'Ḫ', "Kh" },
            { 'Ḱ', "K" }, { 'Ḳ', "K" }, { 'Ķ', "K" }, { 'Ḵ', "K" }, { 'Ǩ', "K" },
            { 'Ĺ', "L" }, { 'Ł', "L" }, { 'Ľ', "L" }, { 'Ḷ', "L" }, { 'Ļ', "L" },
            { 'Ṃ', "M" }, { 'Ḿ', "M" },
            { 'Ň', "Ñ" },
            { 'Ǹ', "En" },
            { 'Ń', "N" }, { 'Ņ', "N" }, { 'Ṅ', "N" }, { 'Ṇ', "N" }, { 'Ŋ', "N" }, { 'Ɲ', "N" },
            { 'Ơ', "O" }, { 'Ọ', "O" },
            { 'Ȯ', "Ó" },
            { 'Ờ', "Ò" },
            { 'Ỡ', "Õ" }, { 'Ō', "Õ" }, { 'Ȫ', "Õ" },
            { 'Ŏ', "Õ" }, // Maybe replace with "Eo"
            { 'Ő', "Ö" }, { 'Ǫ', "Ö" },
            { 'Ǿ', "Ø" },
            { 'Ộ', "Ô" },
            { 'Ṕ', "P" },
            { 'Ř', "Rz" },
            { 'Ŕ', "R" }, { 'Ṙ', "R" }, { 'Ṛ', "R" }, { 'Ŗ', "R" },
            { 'Ś', "S" }, { 'Ŝ', "S" }, { 'Ş', "S" }, { 'Ș', "S" }, { 'Ṣ', "S" }, { 'Ṡ', "S" },
            { 'Ť', "Ty" },
            { 'Ț', "T" }, { 'Ţ', "T" }, { 'Ṭ', "T" }, { 'Ŧ', "T" },
            { 'Ů', "U" }, { 'Ų', "U" }, { 'Ụ', "U" },
            { 'Ũ', "Ü" }, { 'Ū', "Ü" }, { 'Ŭ', "Ü" }, { 'Ű', "Ü" }, { 'Ṳ', "Ü" },
            { 'Ủ', "Ù" },
            { 'Ṿ', "V" },
            { 'Ẃ', "W" }, { 'Ẅ', "W" }, { 'Ŵ', "W" },
            { 'Ẍ', "X" },
            { 'Ŷ', "Y" },
            { 'Ȳ', "Ÿ" },
            { 'Ỳ', "Ý" }, { 'Ẏ', "Ý" },
            { 'Ź', "Z" }, { 'Ẓ', "Z" },
            { 'Ż', "Ž" },
            { 'ǣ', "æ" },
            { 'ạ', "a" }, { 'ə', "a" }, { 'ą', "a" },
            { 'ả', "à" },
            { 'ậ', "â" },
            { 'ă', "ã" }, { 'ā', "ã" },
            { 'ǟ', "ä" },
            { 'ḃ', "b" }, { 'ḅ', "b" },
            { 'ć', "c" }, { 'ċ', "c" },
            { 'č', "ch" },
            { 'đ', "dž" },
            { 'ḏ', "d" }, { 'ɗ', "d" }, { 'ɖ', "d" }, { 'ḑ', "d" }, { 'ď', "d" }, { 'ḍ', "d" },
            { 'ē', "ë" }, { 'ẽ', "ë" },
            { 'ė', "é" },
            { 'ệ', "ê" }, { 'ě', "ê" },
            { 'ę', "e" }, { 'ẹ', "e" },
            { 'ğ', "g" }, { 'ĝ', "g" }, { 'ģ', "g" }, { 'ǵ', "g" },
            { 'ẻ', "è" },
            { 'ĥ', "h" }, { 'ȟ', "h" }, { 'ḧ', "h" }, { 'ḩ', "h" }, { 'ħ', "h" },
            { 'ı', "i" }, { 'į', "i" }, { 'ị', "i" },
            { 'ĭ', "ï" }, { 'ī', "ï" }, { 'ĩ', "ï" },
            { 'ĵ', "j" }, { 'ǰ', "j" }, { 'ǧ', "j" },
            { 'ḫ', "kh" },
            { 'ḱ', "k" }, { 'ḳ', "k" }, { 'ķ', "k" }, { 'ḵ', "k" }, { 'ǩ', "k" },
            { 'ĺ', "l" }, { 'ł', "l" }, { 'ľ', "l" }, { 'ḷ', "l" }, { 'ļ', "l" },
            { 'ṃ', "m" }, { 'ḿ', "m" },
            { 'ň', "ñ" },
            { 'ǹ', "en" },
            { 'ń', "n" }, { 'ņ', "n" }, { 'ṅ', "n" }, { 'ṇ', "n" }, { 'ŋ', "n" }, { 'ɲ', "n" },
            { 'ơ', "o" }, { 'ọ', "o" },
            { 'ȯ', "ó" },
            { 'ờ', "ò" },
            { 'ỡ', "õ" }, { 'ō', "õ" }, { 'ȫ', "õ" },
            { 'ŏ', "õ" }, // Maybe replace with "eo"
            { 'ő', "ö" }, { 'ǫ', "ö" },
            { 'ǿ', "ø" },
            { 'ộ', "ô" },
            { 'ṕ', "p" },
            { 'ř', "rz" },
            { 'ŕ', "r" }, { 'ṙ', "r" }, { 'ṛ', "r" }, { 'ŗ', "r" },
            { 'ś', "s" }, { 'ŝ', "s" }, { 'ş', "s" }, { 'ș', "s" }, { 'ṣ', "s" }, { 'ṡ', "s" },
            { 'ť', "ty" },
            { 'ț', "t" }, { 'ţ', "t" }, { 'ṭ', "t" }, { 'ŧ', "t" },
            { 'ů', "u" }, { 'ų', "u" }, { 'ụ', "u" },
            { 'ũ', "ü" }, { 'ū', "ü" }, { 'ŭ', "ü" }, { 'ű', "ü" }, { 'ṳ', "ü" },
            { 'ủ', "ù" },
            { 'ṿ', "v" },
            { 'ẅ', "w" }, { 'ŵ', "w" },
            { 'ẍ', "x" },
            { 'ŷ', "y" },
            { 'ȳ', "ÿ" },
            { 'ỳ', "ý" }, { 'ẏ', "ý" },
            { 'ź', "z" }, { 'ẓ', "z" }, { 'ʐ', "z" },
            { 'ż', "ž" },
        };

        public NuciTextConverter()
        {
            windows1252cache = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// Converts the given text to Windows-1252 encoding, replacing characters that are not supported in that encoding with their closest equivalents.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>The converted text.</returns>
        public string ToWindows1252(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            if (windows1252cache.TryGetValue(text, out string value))
            {
                return value;
            }

            string processedName = text
                .Replace("iīẗ", "iyyah")
                .Replace("īẗ", "iyah");

            processedName = ApplyCommonReplacements(processedName);

            // Crusader Kings II
            processedName = processedName.Replace("āẗ", "āh");

            processedName = ReplaceUsingMap(processedName, Windows1252CharacterMappings);

            processedName = Regex.Replace(processedName, "[Ġ]([^h])", "Gh$1");
            processedName = Regex.Replace(processedName, "[a]*[ẗ]", "ah");
            processedName = Regex.Replace(processedName, "[ġ]([^h])", "gh$1");

            processedName = processedName
                .Replace("Ġh", "Gh")
                .Replace("ġh", "gh")
                .Replace("J̌", "J")
                .Replace("Ŏ̤", "Õ") // Maybe replace with "Eo"
                .Replace("T̈", "T")
                .Replace("ŏ̤", "õ"); // Maybe replace with "eo"

            windows1252cache.TryAdd(text, processedName);

            return processedName;
        }

        private string ApplyCommonReplacements(string name)
        {
            string processedName = name;

            processedName = Regex.Replace(processedName, "\\bɸ", "P");

            processedName = ReplaceUsingMap(processedName, CommonCharacterMappings);

            processedName = processedName
                .Replace("D‍", "D")
                .Replace("G‍", "G")
                .Replace("H̱", "Kh")
                .Replace("Ϊ́", "Ï")
                .Replace("K‍", "K")
                .Replace("L‌", "L")
                .Replace("N‌", "N")
                .Replace("Ṉ", "Ņ")
                .Replace("R̥̄", "Ŕu")
                .Replace("R̥", "Ru")
                .Replace("Ṭ‍", "Ṭ");

            processedName = Regex.Replace(processedName, "(𝖠|A‍)", "A");
            processedName = Regex.Replace(processedName, "( ᐋ)", " Â");
            processedName = Regex.Replace(processedName, "(B‍|B‌|پ)", "B");
            processedName = Regex.Replace(processedName, "(M̄|M̐)", "M");
            processedName = Regex.Replace(processedName, "(P‍|П)", "P");
            processedName = Regex.Replace(processedName, "(R‍|R‌)", "R");
            processedName = Regex.Replace(processedName, "(S‍|S‌)", "S");

            processedName = processedName
                .Replace("ḡ", "ğ") // Untested in the games
                .Replace("ڭ", "ġ")
                .Replace("j‌", "j")
                .Replace("k‍", "k")
                .Replace("l‌", "l")
                .Replace("ǌ", "nj")
                .Replace("ⁿ", "n") // Superscript n - nasal sound
                .Replace("n‌", "n")
                .Replace("ṉ", "ņ")
                .Replace("r̥̄", "ŕu")
                .Replace("r̥", "ru")
                .Replace("ṭ‍", "ṭ");

            processedName = Regex.Replace(processedName, "(𝖺|a‍)", "a");
            processedName = Regex.Replace(processedName, "([^ ])ᐋ", "$1â");
            processedName = Regex.Replace(processedName, "(b‍|b‌)", "b");
            processedName = Regex.Replace(processedName, "(𝖽|d‍‌)", "d");
            processedName = Regex.Replace(processedName, "(g‍|g‌)", "g");
            processedName = Regex.Replace(processedName, "(m̄|m̐|m̃)", "m");
            processedName = Regex.Replace(processedName, "(p‍|п)", "p");
            processedName = Regex.Replace(processedName, "(r‍|r‌)", "r");
            processedName = Regex.Replace(processedName, "(s‍|s‌)", "s");

            // Floating vertical lines
            processedName = processedName
                .Replace("a̍", "ȧ")
                .Replace("e̍", "ė")
                .Replace("i̍", "i")
                .Replace("o̍", "ȯ")
                .Replace("u̍", "ú");

            // Floating accents
            processedName = processedName
                .Replace("á", "á")
                .Replace("ć", "ć")
                .Replace("é", "é")
                .Replace("ǵ", "ǵ")
                .Replace("í", "í")
                .Replace("ḿ", "ḿ")
                .Replace("ń", "ń")
                .Replace("ṕ", "ṕ")
                .Replace("ŕ", "ŕ")
                .Replace("ś", "ś")
                .Replace("ú", "ú")
                .Replace("ý", "ý")
                .Replace("ź", "ź");

            // Floating grave accents
            processedName = processedName
                .Replace("ì", "ì")
                .Replace("ǹ", "ǹ")
                .Replace("ò", "ò")
                .Replace("ù", "ù")
                .Replace("ỳ", "ỳ");

            // Floating umlauts
            processedName = processedName
                .Replace("T̈", "T̈")
                .Replace("ä", "ä")
                .Replace("ā̈", "ǟ")
                .Replace("ą̈", "ą̈")
                .Replace("b̈", "b̈")
                .Replace("c̈", "c̈")
                .Replace("ë", "ë")
                .Replace("ɛ̈̈", "ë")
                .Replace("ḧ", "ḧ")
                .Replace("ï", "ï")
                .Replace("j̈", "j̈")
                .Replace("k̈", "k̈")
                .Replace("l̈", "l̈")
                .Replace("m̈", "m̈")
                .Replace("n̈", "n̈")
                .Replace("ö", "ö")
                .Replace("ō̈", "ȫ")
                .Replace("ǫ̈", "ǫ̈")
                .Replace("ɔ̈̈", "ö")
                .Replace("p̈", "p̈")
                .Replace("q̈", "q̈")
                .Replace("q̣̈", "q̣̈")
                .Replace("r̈", "r̈")
                .Replace("s̈", "s̈")
                .Replace("ẗ", "t") // Because ẗ is a
                .Replace("ü", "ü")
                .Replace("v̈", "v̈")
                .Replace("ẅ", "ẅ")
                .Replace("ẍ", "ẍ")
                .Replace("ÿ", "ÿ")
                .Replace("z̈", "z̈");

            // Floating tildas
            processedName = processedName
                .Replace("ã", "ã")
                .Replace("ẽ", "ẽ")
                .Replace("ĩ", "ĩ")
                .Replace("ñ", "ñ")
                .Replace("õ", "õ")
                .Replace("ũ", "ũ")
                .Replace("ṽ", "ṽ")
                .Replace("ỹ", "ỹ");

            // Floating carets
            processedName = processedName.Replace("ṳ̂", "û");

            // Floating commas
            processedName = processedName.Replace("A̓", "Á"); // Or Á?

            // Other floating diacritics
            processedName = Regex.Replace(processedName, "[̧̣̤̦̓́̀̆̂̌̈̋̄̍̃͘᠌̬]", "");
            processedName = Regex.Replace(processedName, "(ॎ|઼|‌ॎ)", ""); // ???
            processedName = Regex.Replace(processedName, "[・̲̥̮̱̇̐͡]", ""); // Diacritics that attach to characters... I guess

            processedName = Regex.Replace(processedName, "[ʔ]", "ʾ");
            processedName = Regex.Replace(processedName, "[ʾʻʼʽʹ′]", "´");
            processedName = Regex.Replace(processedName, "[ʿ]", "`");
            processedName = Regex.Replace(processedName, "[ꞌʿˀʲь]", "'");
            processedName = Regex.Replace(processedName, "[ʺ″]", "\"");
            processedName = Regex.Replace(processedName, "[‌‍]", "");
            processedName = Regex.Replace(processedName, "[–—]", "-");
            processedName = Regex.Replace(processedName, "[꞉]", ":");
            processedName = Regex.Replace(processedName, "[‎·]", "");
            processedName = Regex.Replace(processedName, "[＝̷̯̰̊̒]", "");
            processedName = Regex.Replace(processedName, "[​]", "");
            processedName = Regex.Replace(processedName, "([‎‎])", ""); // Invisible characters

            return processedName;
        }

        private static string ReplaceUsingMap(string input, Dictionary<char, string> map)
        {
            if (input is null)
            {
                return null;
            }

            StringBuilder sb = new(input.Length);

            foreach (char c in input)
            {
                if (map.TryGetValue(c, out string replacement))
                {
                    sb.Append(replacement);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
