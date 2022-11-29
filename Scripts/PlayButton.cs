using Godot;
using System;
using System.Collections.Generic;
using WeCantSpell.Hunspell;
using static PlayableTile;

public partial class PlayButton : ScrabbleActionButton
{
	private GameDeck deck;
	private WordList dictionary;
	private Panel letterPanel;

	public int currentRound = 0;

	public void onPressed()
	{
		List<String> words = new List<string>();

		foreach (var child in letterPanel.GetChildren())
		{
			var letter = child as PlayableTile;

			if (letter.State == (int)States.AtDeck) continue;

			if (letter.HorizontalConnections.IsFirstLetter)
			{
				var word = "";
				var i = letter;
				var hasPlayedLetter = false;

				while (i.AdjacentTiles.Right.Exists)
				{
					word += i.Letter;
					hasPlayedLetter = i.State == (int)PlayableTile.States.Played;
					i = i.AdjacentTiles.Right.Tile;
				}
				word += i.Letter;

				if ((hasPlayedLetter || currentRound == 0) && word.Length > 1)
				{
					words.Add(word);
				}
			}
			
			if (letter.VerticalConnections.IsFirstLetter)
			{
				var word = "";
				var i = letter;
				var hasPlayedLetter = false;

				while (i.AdjacentTiles.Bottom.Exists)
				{
					word += i.Letter;
					hasPlayedLetter = i.State == (int)PlayableTile.States.Played;
					i = i.AdjacentTiles.Bottom.Tile;
				}
				word += i.Letter;

				if ((hasPlayedLetter || currentRound == 0) && word.Length > 1)
				{
					words.Add(word);
				}
			}
		}

		foreach (var word in words)
		{
			var dictionary = WordList.CreateFromFiles(@"Dicts/pt_BR.dic");

			var valid = dictionary.Check(word);
			GD.Print(word, " ", valid);

			GD.Print("asjas89dj9a8 ", dictionary.Check("asjas89dj9a8"));
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		letterPanel = GetParent().GetParent<GUI>().GetNode<Panel>("LetterPanel");

		Connect("pressed", new Callable(this, nameof(onPressed)));
		Setup();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	/* public override void _Process(float delta)
	{
	} */
}
