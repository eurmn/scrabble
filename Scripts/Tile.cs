using Godot;
using System;

public class Tile : Node2D
{
    // Declare member variables here. Examples:
    public Vector2 position;
    protected Color color;
    protected string text;
    private DynamicFont font = new DynamicFont();

    protected void SetupTile()
    {
        font.FontData = GD.Load<DynamicFontData>("res://Fonts/UbuntuMono/UbuntuMonoFontData.tres");
        font.Size = 20;

        var tile = new Sprite();
        var tileTexture = new GradientTexture2D();
        var tileGradient = new Gradient();

        tileTexture.Width = tileTexture.Height = 40;
        tileTexture.Gradient = tileGradient;

        tileGradient.RemovePoint(1);
        tileGradient.SetColor(0, color);

        tile.Texture = tileTexture;
        tile.Centered = false;
        tile.Position = position * new Vector2(40, 40);
        tile.Name = position.x.ToString() + "x" + position.y.ToString();

        if (text != "")
        {
            var label = new RichTextLabel();

            var theme = new Theme();
            theme.DefaultFont = font;
            label.AddColorOverride("default_color", new Color("1e1e1e"));

            label.Theme = theme;
            label.RectSize = new Vector2(40, 40);
            label.Text = text;
            label.RectPosition = new Vector2(10, 10);

            tile.AddChild(label);
        }

        AddChild(tile);
    }
}
