using System.Linq;
using NUnit.Framework;
using NuciText.Conversion;

namespace NuciText.Conversion.UnitTests
{
    public class NameNormaliserTests
    {
        const string StringOfVariousCharacters = "​‌‍*[]`°´·ʹ–‘’”‡′″꞉＝̷̧̲̬̯̣̤̥̦̮̰̓́̀̆̂̌̊̈̋̃̇̄̍̒̐͘͡ᶻźŹžŽżŻẓẒƶƵʐǯþÞƿǷʔˀʼʾꞌʿΆγθΊΐΌΎχаАәёіІїјЈоџьᐋ";
        const string Windows1252Characters = "_-–—,;:!¡?¿.…·'‘’‚‹›\"“”„«»()[]{}§¶@*/\\&#%‰†‡•`´˜^¯¨¸ˆ°©®+±÷×<=>¬|¦~¤¢$£¥€01¹½¼2²3³¾456789aAªáÁàÀâÂåÅäÄãÃæÆbBcCçÇdDðÐeEéÉèÈêÊëËfFƒgGhHiIíÍìÌîÎïÏjJkKlLmMnNñÑoOºóÓòÒôÔöÖõÕøØœŒpPqQrRsSšŠßtT™uUúÚùÙûÛüÜvVwWxXyYýÝÿŸzZžŽþÞµ";

        private NuciTextConverter converter;

        [SetUp]
        public void SetUp() => converter = new();

        [Test]
        [TestCase("Â-ngì-pî-sṳ̂ sân", "Â-ngì-pî-sû sân")]
        [TestCase("Ab‌khajiyā", "Abkhajiyã")]
        [TestCase("Aǧīm", "Ajïm")]
        [TestCase("Aḫmīm", "Akhmïm")]
        [TestCase("Ais‍lyāṇḍ", "Aislyãnd")]
        [TestCase("Aǩsubaj", "Aksubaj")]
        [TestCase("al-Basīṭ", "al-Basït")]
        [TestCase("al-Ǧubayl", "al-Jubayl")]
        [TestCase("al-Hāmā al-Arāġūn", "al-Hãmã al-Arãghün")]
        [TestCase("al-H̱ānīẗ", "al-Khãniyah")]
        [TestCase("āl-Zāwyẗ", "ãl-Zãwyah")]
        [TestCase("al-ʼAḥsāʼ", "al-´Ahsã´")]
        [TestCase("Aĺbasiete", "Albasiete")]
        [TestCase("Āl‌jāsa", "Ãljãsa")]
        [TestCase("an-Nāṣira", "an-Nãsira")]
        [TestCase("And‍riyā", "Andriyã")]
        [TestCase("Anwākšūṭ", "Anwãkšüt")]
        [TestCase("Aṗsny", "Apsny")]
        [TestCase("Åsele", "Åsele")]
        [TestCase("Bāḇel", "Bãbel")]
        [TestCase("Basileia Rhṓmaiṓn", "Basileia Rhõmaiõn")]
        [TestCase("Bạt Đế Mỗ", "Bat Ðê Mô")]
        [TestCase("Blāsīnṯīā", "Blãsïnthïã")]
        [TestCase("Brægentford", "Brægentford")]
        [TestCase("B‍risṭ‍riṭā", "Bristritã")]
        [TestCase("Br̥̄sels", "Brusels")]
        [TestCase("Budapeşt", "Budapest")]
        [TestCase("Bułgar Wielki", "Bulgar Wielki")]
        [TestCase("Bùyínuòsīàilìsī", "Bùyínuòsïàilìsï")]
        [TestCase("Český Krumlov", "Cheský Krumlov")]
        [TestCase("Cetiǌe", "Cetinje")]
        [TestCase("Chęciny", "Checiny")]
        [TestCase("Đakovo", "Ðakovo")]
        [TestCase("Đặng Khẩu", "Ðãng Khâu")]
        [TestCase("Danmǫrk", "Danmörk")]
        [TestCase("پwyrṭūrīkū", "Bwyrtürïkü")]
        [TestCase("Dasavleti Virǯinia", "Dasavleti Viržinia")]
        [TestCase("Đế quốc Nga", "Ðê quôc Nga")]
        [TestCase("Dobřany", "Dobrzany")]
        [TestCase("Dᶻidᶻəlal̓ič", "Dzidzalalich")]
        [TestCase("Egeyan Kġziner", "Egeyan Kghziner")]
        [TestCase("Enkoriџ", "Enkoridž")]
        [TestCase("Ĕnṭrima", "Êntrima")]
        [TestCase("Ər-Rəqqə", "Ar-Raqqa")]
        [TestCase("Farƣona", "Fargona")]
        [TestCase("Ġhaūdeš", "Ghaüdeš")]
        [TestCase("Glideroχ", "Glideroch")]
        [TestCase("Góðviðra", "Góðviðra")]
        [TestCase("Grɨnlɛɛn", "Grinleen")]
        [TestCase("G‍roseṭō", "Grosetõ")]
        [TestCase("Ḥadīṯẗ", "Hadïthah")]
        [TestCase("Ȟaȟáwakpa", "Haháwakpa")]
        [TestCase("Ḥamāẗ", "Hamãh")]
        [TestCase("H̱rūnīnġn", "Khrünïnghn")]
        [TestCase("Ins Br̥k", "Ins Bruk")]
        [TestCase("Iṉspruk", "Inspruk")]
        [TestCase("Ja઼āgreba", "Jaãgreba")]
        [TestCase("Jālaॎs‌burga", "Jãlasburga")]
        [TestCase("Jémanị", "Jémani")]
        [TestCase("Jhānjāṁ", "Jhãnjãm")]
        [TestCase("K’asablank’a", "K’asablank’a")]
        [TestCase("Kaer Gradaỽc", "Kaer Gradauuc")]
        [TestCase("Kalɩfɔrnii", "Kalifornii")]
        [TestCase("Kašuubimaa", "Kašuubimaa")]
        [TestCase("Kȁzahstān", "Kàzahstãn")]
        [TestCase("Khar‌gōn", "Khargõn")]
        [TestCase("K‍ragujevak", "Kragujevak")]
        [TestCase("Lablaẗ", "Lablah")]
        [TestCase("Lāip‌ॎsiśa", "Lãipsisa")]
        [TestCase("Lėnkėjė", "Lénkéjé")]
        [TestCase("Likṟṟaṉ‌sṟṟaiṉ", "Likrransrrain")]
        [TestCase("Linkøbing", "Linkøbing")]
        [TestCase("Lǐyuērènèilú", "Lïyuërènèilú")]
        [TestCase("Lò̤-mā Dá̤-guók", "Lò-mã Dá-guók")]
        [TestCase("Loṙow", "Lorow")]
        [TestCase("Luật Tước Đàm", "Luât Tu'óc Ðàm")]
        [TestCase("Lǚfádēng", "Lüfádëng")]
        [TestCase("Lúksẹ́mbọ̀rg", "Lúksemborg")]
        [TestCase("Łużyce", "Lužyce")]
        [TestCase("Maďarsko", "Madarsko")]
        [TestCase("Mīdīlbūrẖ", "Mïdïlbürkh")]
        [TestCase("Miniṡoṡeiyoḣdoke Otoƞwe", "Minisoseiyohdoke Otonwe")]
        [TestCase("Miniᐋpulis", "Miniâpulis")]
        [TestCase("Moscoƿ", "Moscouu")]
        [TestCase("Mūrīṭanīẗ al-Ṭinǧīẗ", "Mürïtaniyah al-Tinjiyah")]
        [TestCase("Nam̐si", "Namsi")]
        [TestCase("Nazareḟŭ", "Nazarefü")]
        [TestCase("Ngò-lò-sṳ̂", "Ngò-lò-sû")]
        [TestCase("Nörvêzi", "Nörvêzi")]
        [TestCase("Novyĭ Margelan", "Novyï Margelan")]
        [TestCase("Nowĩ", "Nowï")]
        [TestCase("Nɔɔrɩvɛɛzɩ", "Nooriveezi")]
        [TestCase("Nuorvegėjė", "Nuorvegéjé")]
        [TestCase("Nūrṯāmbtūn", "Nürthãmbtün")]
        [TestCase("Nuwakicɔɔtɩ", "Nuwakicooti")]
        [TestCase("Ɔsɩloo", "Osiloo")]
        [TestCase("Perejäslavľĭ", "Perejäslavlï")]
        [TestCase("Permė", "Permé")]
        [TestCase("Phin‌sṭrām", "Phinstrãm")]
        [TestCase("P‍ṭiyuj", "Ptiyuj")]
        [TestCase("Purūkḷiṉ", "Purüklin")]
        [TestCase("ɸlāryo", "Plãryo")]
        [TestCase("Qart-Ḥadašt", "Qart-Hadašt")]
        [TestCase("Ra‍yājāna", "Rayãjãna")]
        [TestCase("R‍hods", "Rhods")]
        [TestCase("Rừng Bohemia", "Rù'ng Bohemia")]
        [TestCase("Sāg‍rab", "Sãgrab")]
        [TestCase("Sai Ngǭn", "Sai Ngön")]
        [TestCase("Sālj‌barg‌", "Sãljbarg")]
        [TestCase("Šaqūbīẗ", "Šaqübiyah")]
        [TestCase("Saraẖs", "Sarakhs")]
        [TestCase("Semêndria", "Semêndria")]
        [TestCase("Starověký Řím", "Starovêký Rzím")]
        [TestCase("Sveti Đorđe", "Sveti Ðordže")]
        [TestCase("Taɖɛsalam", "Tadesalam")]
        [TestCase("Taϊpéi", "Taïpéi")]
        [TestCase("Test ɸlāryoɸ", "Test Plãryop")]
        [TestCase("Thượng Volta", "Thu'ong Volta")]
        [TestCase("Tibískon", "Tibískon")]
        [TestCase("Tłnáʔəč", "Tlná´ach")]
        [TestCase("Ṭ‍renṭō", "Trentõ")]
        [TestCase("Truǧālẗ", "Trujãlah")]
        [TestCase("Užhorod", "Užhorod")]
        [TestCase("Vialikaja Poĺšča", "Vialikaja Polšcha")]
        [TestCase("Vюrцby’rg", "Viurcby’rg")]
        [TestCase("Ẇel‌ś‌", "Wels")]
        [TestCase("Вуллонгонг", "Vullongong")]
        [TestCase("Эstoniья", "Estoni'ia")]
        [TestCase("Юli’h", "Iuli’h")]
        public void WhenNormalisingForWindow1252_ReturnsTheExpectedNormalisedName(
            string name,
            string expectedResult)
            => Assert.That(converter.ToWindows1252(name), Is.EqualTo(expectedResult));

        [Test]
        [TestCase(StringOfVariousCharacters)]
        public void WhenNormalisingForWindow1252_ReturnsTheNameWithoutCharsOutsideCharset(string name)
            => TestCharsNotOutsideSet(converter.ToWindows1252(name), Windows1252Characters);

        static void TestCharsNotOutsideSet(string str, string charset)
        {
            string actualCharset = charset + " ";
            string charsOutisdeCharset = string.Concat(str.Where(c => !actualCharset.Contains(c)));

            if (string.IsNullOrWhiteSpace(charsOutisdeCharset))
            {
                charsOutisdeCharset = string.Empty;
            }

            Assert.That(charsOutisdeCharset, Is.Empty);
        }
    }
}
