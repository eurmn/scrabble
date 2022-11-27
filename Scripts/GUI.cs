using Godot;

public partial class GUI : Node2D
{
    // Declare member variables here. Examples:
    private Panel panel;
    public GameDeck Deck = new GameDeck();

    public void DrawAllCardsBack()
    {
        foreach (var letter in panel.GetChildren())
        {
            (letter as PlayableTile).DrawBack();
        }
    }

    private void GenerateTile(string letter, int index)
    {
        var tile = new PlayableTile();
        tile.Points = Deck.LetterDistribution[letter];
        tile.Letter = letter;
        tile.Index = index;

        panel.AddChild(tile);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        panel = GetChild<Panel>(0);

        for (int i = 0; i < 7; i++)
        {
            GenerateTile(Deck.DrawRandomLetter(), i);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
