using Godot;

public class GUI : Node2D
{
    // Declare member variables here. Examples:
    private Panel panel;
    private DynamicFont font = new DynamicFont();
    public GameDeck deck = new GameDeck();

    private void GenerateTile(string letter, int index) {
        var tile = new PlayableTile();
        tile.Points = deck.LetterDistribution[letter];
        tile.Letter = letter;
        tile.Index = index;

        panel.AddChild(tile);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        font.FontData = GD.Load<DynamicFontData>("res://Fonts/UbuntuMono/UbuntuMonoFontData.tres");
        font.Size = 40;

        panel = GetChild<Panel>(0);

        for (int i = 0; i < 7; i++)
        {
            GenerateTile(deck.DrawRandomLetter(), i);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
