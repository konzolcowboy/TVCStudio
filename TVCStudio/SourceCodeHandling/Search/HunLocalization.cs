using ICSharpCode.AvalonEdit.Search;

namespace TVCStudio.SourceCodeHandling.Search
{
    public class HunLocalization : Localization
    {
	    public override string FindNextText => @"Következő (F3)";

	    public override string FindPreviousText => @"Előző (SHIFT+F3)";

	    public override string MatchCaseText => @"Kis- és nagybetűk megkülönböztetése";

	    public override string MatchWholeWordsText => @"Csak teljes szóval megenyező találatok";

	    public override string UseRegexText => @"Keresés reguláris kifejezés használatával";

	    public override string ErrorText => @"Hiba:";

	    public override string NoMatchesFoundText => @"Nincs találat!";
    }
}
