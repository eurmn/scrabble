using Godot;
using System;

public class Tiles : Node2D
{
    // Declare member variables here.
    private const string TW_COLOR = "cc0099";
    private const string DW_COLOR = "ff80df";
    private const string CT_COLOR = "ffff4d";
    private const string DL_COLOR = "8080ff";
    private const string TL_COLOR = "4dff88";
    private DynamicFont font = new DynamicFont();

    private void GenerateCentralTile()
    {
        GenerateTile(new Vector2(7, 7), new Color(CT_COLOR));
    }

    private void GenerateTripleWordTiles()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1)
                {
                    continue;
                }
                GenerateTile(new Vector2(7 * j, 7 * i), new Color(TW_COLOR), "3W");
            }
        }
    }

    private void GenerateDoubleWordTiles()
    {
        for (int i = 1; i <= 4; i++)
        {
            GenerateTile(new Vector2(i, i), new Color(DW_COLOR), "2W");
            GenerateTile(new Vector2(14 - i, i), new Color(DW_COLOR), "2W");
            GenerateTile(new Vector2(i, 14 - i), new Color(DW_COLOR), "2W");
            GenerateTile(new Vector2(14 - i, 14 - i), new Color(DW_COLOR), "2W");
        }
    }

    private void GenerateDoubleLetterTiles()
    {
        int[] xArray = { 0, 2, 3, 6, 6 };
        int[] yArray = { 3, 6, 0, 2, 6 };

        for (int i = 0; i < xArray.Length; i++)
        {
            GenerateTile(new Vector2(xArray[i], yArray[i]), new Color(DL_COLOR), "2L");
            GenerateTile(new Vector2(14 - xArray[i], yArray[i]), new Color(DL_COLOR), "2L");
            GenerateTile(new Vector2(xArray[i], 14 - yArray[i]), new Color(DL_COLOR), "2L");
            GenerateTile(new Vector2(14 - xArray[i], 14 - yArray[i]), new Color(DL_COLOR), "2L");
        }

        GenerateTile(new Vector2(7, 3), new Color(DL_COLOR), "2L");
        GenerateTile(new Vector2(7, 14 - 3), new Color(DL_COLOR), "2L");
        GenerateTile(new Vector2(14 - 3, 7), new Color(DL_COLOR), "2L");
        GenerateTile(new Vector2(3, 7), new Color(DL_COLOR), "2L");
    }

    private void GenerateTripleLetterTiles()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((i == 0 && j == 0) || (i == 3 && j == 0) || (i == 0 && j == 3) || (i == 3 && j == 3))
                {
                    continue;
                }
                GenerateTile(new Vector2(1 + i * 4, 1 + j * 4), new Color(TL_COLOR), "3L");
            }
        }
    }

    private void GenerateTile(Vector2 position, Color color, String text = "")
    {
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

        if (text != "") {
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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        font.FontData = GD.Load<DynamicFontData>("res://Fonts/UbuntuMono/UbuntuMonoFontData.tres");
        font.Size = 20;

        GenerateCentralTile();
        GenerateDoubleWordTiles();
        GenerateTripleWordTiles();
        GenerateDoubleLetterTiles();
        GenerateTripleLetterTiles();
    }
}
