public class EnglishWord
{
    public string Id { get; set; }
    public string WordName { get; set; }
    public string EnglishDescription { get; set; }
    public string VietMeaning { get; set; }
    public string VietDescription { get; set; }
    
    public EnglishWord()
    {

    }
    public EnglishWord(string id, string wordName, string englishDescription, string vietMeaning, string vietDescription)
    {
        Id = id;
        WordName = wordName;
        EnglishDescription = englishDescription;
        VietMeaning = vietMeaning;
        VietDescription = vietDescription;
        
    }
}
