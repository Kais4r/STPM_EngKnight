public class EnglishWord
{
    public string Id { get; set; }
    public string WordName { get; set; }
    public string EnglishDescription { get; set; }
    public string VietMeaning { get; set; }
    public string VietDescription { get; set; }
    public string TranslationSource { get; set; }


    public EnglishWord()
    {

    }
    public EnglishWord(string id, string wordName, string englishDescription, string vietMeaning, string vietDescription, string translationSource)
    {
        Id = id;
        WordName = wordName;
        EnglishDescription = englishDescription;
        VietMeaning = vietMeaning;
        VietDescription = vietDescription;
        TranslationSource = translationSource;
    }
}
