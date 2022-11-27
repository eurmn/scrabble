using Godot;
using System;

public partial class Tile : Node2D
{
    // Declare member variables here. Examples:
    public Vector2 position;
    protected Color color;
    protected string text;

    protected void SetupTile()
    {
        var tile = new Sprite2D();
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
            var label = new Label();

            label.AddThemeColorOverride("font_color", new Color("1e1e1e"));
            label.AddThemeFontOverride("font", ResourceLoader.Load<Font>("res://Fonts/UbuntuMono/UbuntuMono-Bold.ttf"));
            label.AddThemeFontSizeOverride("font_size", 20);

            label.Size = new Vector2(40, 40);
            label.Text = text;

            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;

            tile.AddChild(label);
        }

        AddChild(tile);
    }
}
