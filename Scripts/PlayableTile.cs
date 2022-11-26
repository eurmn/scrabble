using Godot;
using System;

public class PlayableTile : Panel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private DynamicFont font = new DynamicFont();
    private DynamicFont pointsFont = new DynamicFont();
    public string Letter;
    public int Points;
    public int Index;

    private void GenerateTile()
    {
        var styleBox = new StyleBoxFlat();
        var label = new Label();
        var pointsLabel = new Label();

        styleBox.BgColor = new Color("6666ff");
        styleBox.SetCornerRadiusAll(5);

        AddStyleboxOverride("panel", styleBox);
        RectPosition = new Vector2(5 * (Index + 1) + 50 * Index, 5);
        RectSize = new Vector2(50, 50);

        label.AddFontOverride("font", font);
        label.AddColorOverride("font_color", new Color("1e1e1e"));
        label.RectSize = new Vector2(50, 50);
        label.Align = Label.AlignEnum.Center;
        label.Valign = Label.VAlign.Center;
        label.Text = Letter.ToUpper();

        pointsLabel.AddFontOverride("font", pointsFont);
        pointsLabel.AddColorOverride("font_color", new Color("1e1e1e"));
        pointsLabel.RectSize = new Vector2(45, 45);
        pointsLabel.Align = Label.AlignEnum.Right;
        pointsLabel.Valign = Label.VAlign.Top;
        pointsLabel.Text = Points.ToString();

        AddChild(label);
        AddChild(pointsLabel);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        font.FontData = pointsFont.FontData = GD.Load<DynamicFontData>("res://Fonts/UbuntuMono/UbuntuMonoFontData.tres");
        font.Size = 40;
        pointsFont.Size = 15;

        GenerateTile();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
