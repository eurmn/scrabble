using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using NHunspell;
using WeCantSpell.Hunspell;

public partial class GameDeck
{
    private List<string> letterDeck = new List<string>();
    private RandomNumberGenerator random = new RandomNumberGenerator();
    private WordList dictionary;
    public Dictionary<string, int> LetterDistribution = new Dictionary<string, int>();
    // public Hunspell spellChecker = new Hunspell("Dicts/pt_BR.aff", "Dicts/pt_BR.dic");

    private void InitializeLetterDistributionDictionary()
    {
        LetterDistribution["a"] = LetterDistribution["e"] = LetterDistribution["i"] =
            LetterDistribution["o"] = LetterDistribution["s"] = LetterDistribution["u"] =
            LetterDistribution["m"] = LetterDistribution["r"] = LetterDistribution["t"] = 1;
        LetterDistribution["d"] = LetterDistribution["l"] = LetterDistribution["c"] = LetterDistribution["p"] = 2;
        LetterDistribution["n"] = LetterDistribution["b"] = LetterDistribution["ç"] = 3;
        LetterDistribution["f"] = LetterDistribution["g"] = LetterDistribution["h"] = LetterDistribution["v"] = 4;
        LetterDistribution["j"] = 5;
        LetterDistribution["q"] = 6;
        LetterDistribution["x"] = LetterDistribution["z"] = 8;
        LetterDistribution[""] = 0;
    }

    private void InitializeLetterDeck()
    {
        // Spaghetti!
        string[] letters = { "a", "e", "i", "o", "s", "u", "m", "r", "t", "d", "l", "c", "p", "n", "b", "ç", "f", "g", "h", "v", "j", "q", "x", "z" };
        int[] amounts = { 14, 11, 10, 10, 8, 7, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 2, 2, 2, 2, 2, 2, 1, 1, 1 };

        for (int i = 0; i < letters.Count(); i++)
        {
            for (int j = 0; j < amounts[i]; j++)
            {
                letterDeck.Add(letters[i]);
            }
        }
    }

    public string DrawRandomLetter()
    {
        random.Randomize();
        var index = random.RandiRange(0, letterDeck.Count() - 1);

        var letter = letterDeck[index];
        letterDeck.RemoveAt(index);

        return letter;
    }

    public GameDeck()
    {
        var dictionary = WordList.CreateFromFiles(@"Dicts/pt_BR.dic");
        GD.Print(dictionary.Check("aiosudhiuashd"));

        InitializeLetterDeck();
        InitializeLetterDistributionDictionary();
    }
}
