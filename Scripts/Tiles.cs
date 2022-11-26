using Godot;
using System;

public class Tiles : Node2D
{
    // Declare member variables here.

    private void GenerateCentralTile()
    {
        var tile = new CentralTile();
        AddChild(tile);
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
                var tile = new TripleWordTile();
                tile.position = new Vector2(7 * j, 7 * i);
                AddChild(tile);
            }
        }
    }

    private void GenerateDoubleWordTiles()
    {
        for (int i = 1; i <= 4; i++)
        {
            var tile_1 = new DoubleWordTile();
            tile_1.position = new Vector2(i, i);
            AddChild(tile_1);

            var tile_2 = new DoubleWordTile();
            tile_2.position = new Vector2(14 - i, i);
            AddChild(tile_2);

            var tile_3 = new DoubleWordTile();
            tile_3.position = new Vector2(i, 14 - i);
            AddChild(tile_3);

            var tile_4 = new DoubleWordTile();
            tile_4.position = new Vector2(14 - i, 14 - i);
            AddChild(tile_4);
        }
    }

    private void GenerateDoubleLetterTiles()
    {
        int[] xArray = { 0, 2, 3, 6, 6 };
        int[] yArray = { 3, 6, 0, 2, 6 };

        for (int i = 0; i < xArray.Length; i++)
        {
            var tile_1 = new DoubleLetterTile();
            tile_1.position = new Vector2(xArray[i], yArray[i]);
            AddChild(tile_1);

            var tile_2 = new DoubleLetterTile();
            tile_2.position = new Vector2(14 - xArray[i], yArray[i]);
            AddChild(tile_2);

            var tile_3 = new DoubleLetterTile();
            tile_3.position = new Vector2(xArray[i], 14 - yArray[i]);
            AddChild(tile_3);

            var tile_4 = new DoubleLetterTile();
            tile_4.position = new Vector2(14 - xArray[i], 14 - yArray[i]);
            AddChild(tile_4);
        }

        var tile_5 = new DoubleLetterTile();
        tile_5.position = new Vector2(7, 3);
        AddChild(tile_5);

        var tile_6 = new DoubleLetterTile();
        tile_6.position = new Vector2(7, 14 - 3);
        AddChild(tile_6);

        var tile_7 = new DoubleLetterTile();
        tile_7.position = new Vector2(14 - 3, 7);
        AddChild(tile_7);

        var tile_8 = new DoubleLetterTile();
        tile_8.position = new Vector2(3, 7);
        AddChild(tile_8);
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

                var tile = new TripleLetterTile();
                tile.position = new Vector2(1 + i * 4, 1 + j * 4);
                AddChild(tile);
            }
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GenerateCentralTile();
        GenerateDoubleWordTiles();
        GenerateTripleWordTiles();
        GenerateDoubleLetterTiles();
        GenerateTripleLetterTiles();
    }
}
