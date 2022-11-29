using Godot;
using System;
using System.Collections.Generic;

public partial class PlayableTile : Panel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private FontFile font = new FontFile();
    private FontFile pointsFont = new FontFile();
    private bool beingDragged = false;
    private bool mouseInside = false;
    private int state = ((int)States.AtDeck);
    private Label label = new Label();
    private Label pointsLabel = new Label();
    private StyleBoxFlat styleBox = new StyleBoxFlat();
    private GameDeck deck;

    public enum States
    {
        AtDeck = 0,
        Placed = 1,
        Played = 2
    }
    public Connections HorizontalConnections = new Connections();
    public Connections VerticalConnections = new Connections();
    public string Letter;
    public int Points;
    public int Index;

    private void onMouseEntered()
    {
        mouseInside = true;
    }

    private void onMouseExited()
    {
        mouseInside = false;
    }

    private void setState(int newState)
    {
        if (newState == ((int)States.AtDeck))
        {
            state = ((int)States.Placed);

            styleBox.SetCornerRadiusAll(5);

            Size = new Vector2(50, 50);

            label.Size = new Vector2(50, 50);
            label.AddThemeFontSizeOverride("font_size", 40);

            pointsLabel.Size = new Vector2(45, 45);
            pointsLabel.AddThemeFontSizeOverride("font_size", 15);
        }
        else if (newState == ((int)States.Placed))
        {
            state = ((int)States.Placed);

            styleBox.SetCornerRadiusAll(0);

            Size = new Vector2(40, 40);

            label.Size = new Vector2(40, 40);
            label.AddThemeFontSizeOverride("font_size", 30);

            pointsLabel.Size = new Vector2(37, 37);
            pointsLabel.AddThemeFontSizeOverride("font_size", 12);
        }
    }

    private class AdjacentTiles
    {
        public class Side
        {
            public PlayableTile Tile;
            public bool Exists;

            public Side(bool exists)
            {
                Exists = exists;
            }

            public Side(PlayableTile tile, bool exists)
            {
                Exists = exists;
                Tile = tile;
            }
        }
        public Side Top = new Side(false);
        public Side Right = new Side(false);
        public Side Left = new Side(false);
        public Side Bottom = new Side(false);
    }

    private void placeAt(Vector2 position)
    {
        setState(((int)States.Placed));
        GlobalPosition = position * new Vector2(40, 40);

        // top right bottom left.
        var adjacentTiles = new AdjacentTiles();

        foreach (var child in GetParent().GetChildren())
        {
            if (child == this) continue;

            var letter = child as PlayableTile;
            var letterPosition = letter.GetPositionOnBoard();
            var myPosition = GetPositionOnBoard();

            if (letterPosition == myPosition + new Vector2(0, 1))
            {
                adjacentTiles.Top = new AdjacentTiles.Side(letter, true);
            }
            else if (letterPosition == myPosition + new Vector2(1, 0))
            {
                adjacentTiles.Right = new AdjacentTiles.Side(letter, true);
            }
            else if (letterPosition == myPosition - new Vector2(0, 1))
            {
                adjacentTiles.Bottom = new AdjacentTiles.Side(letter, true);
            }
            else if (letterPosition == myPosition - new Vector2(1, 0))
            {
                adjacentTiles.Left = new AdjacentTiles.Side(letter, true);
            }

            if (adjacentTiles.Left.Exists && !adjacentTiles.Right.Exists)
            {
                HorizontalConnections.IsLastLetter = true;
                adjacentTiles.Left.Tile.HorizontalConnections.IsLastLetter = false;
            }
            else if (adjacentTiles.Right.Exists && !adjacentTiles.Left.Exists)
            {
                HorizontalConnections.IsFirstLetter = true;
                if (!deck.FirstLetters.Contains(this)) deck.FirstLetters.Add(this);
                adjacentTiles.Right.Tile.HorizontalConnections.IsFirstLetter = false;
                deck.FirstLetters.Remove(adjacentTiles.Right.Tile);
            }
            else if (!(adjacentTiles.Right.Exists || adjacentTiles.Left.Exists))
            {
                if (!deck.FirstLetters.Contains(this)) deck.FirstLetters.Add(this);
                HorizontalConnections.IsFirstLetter = true;
                HorizontalConnections.IsLastLetter = true;
            }

            if (adjacentTiles.Top.Exists && !adjacentTiles.Bottom.Exists)
            {
                VerticalConnections.IsLastLetter = true;
                adjacentTiles.Top.Tile.VerticalConnections.IsLastLetter = false;
            }
            else if (adjacentTiles.Bottom.Exists && !adjacentTiles.Top.Exists)
            {
                VerticalConnections.IsFirstLetter = true;
                if (!deck.FirstLetters.Contains(this)) deck.FirstLetters.Add(this);
                adjacentTiles.Bottom.Tile.VerticalConnections.IsFirstLetter = false;
                deck.FirstLetters.Remove(adjacentTiles.Bottom.Tile);
            }
            else if (!(adjacentTiles.Top.Exists || adjacentTiles.Bottom.Exists))
            {
                if (!deck.FirstLetters.Contains(this)) deck.FirstLetters.Add(this);
                VerticalConnections.IsFirstLetter = true;
                VerticalConnections.IsLastLetter = true;
            }
        }
    }

    private void unplace()
    {
        setState(((int)States.AtDeck));
        Position = new Vector2(5 * (Index + 1) + 50 * Index, 5);
    }

    public Vector2 GetPositionOnBoard()
    {
        var gridPos = (GetGlobalMousePosition() / 40).Floor();
        return gridPos;
    }

    public void DrawBack()
    {
        if (state == ((int)States.Placed))
        {
            unplace();
            Position = new Vector2(5 * (Index + 1) + 50 * Index, 5);
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        deck = GetParent().GetParent<GUI>().Deck;

        var font = ResourceLoader.Load<Font>("res://Fonts/UbuntuMono/UbuntuMono-Bold.ttf");
        styleBox.BgColor = new Color("ffffff");
        styleBox.SetCornerRadiusAll(5);

        AddThemeStyleboxOverride("panel", styleBox);
        Position = new Vector2(5 * (Index + 1) + 50 * Index, 5);
        Size = new Vector2(50, 50);

        label.AddThemeFontSizeOverride("font_size", 40);
        label.AddThemeFontOverride("font", font);
        label.AddThemeColorOverride("font_color", new Color("1e1e1e"));
        label.Size = new Vector2(50, 50);
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.Text = Letter.ToUpper();

        pointsLabel.AddThemeFontOverride("font", font);
        pointsLabel.AddThemeColorOverride("font_color", new Color("1e1e1e"));
        pointsLabel.Size = new Vector2(45, 45);
        pointsLabel.HorizontalAlignment = HorizontalAlignment.Right;
        pointsLabel.VerticalAlignment = VerticalAlignment.Top;
        pointsLabel.Text = Points.ToString();

        Connect("mouse_entered", new Callable(this, nameof(onMouseEntered)));
        Connect("mouse_exited", new Callable(this, nameof(onMouseExited)));

        AddChild(label);
        AddChild(pointsLabel);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (beingDragged)
        {
            setState(((int)States.AtDeck));

            GlobalPosition = GetGlobalMousePosition() - new Vector2(25, 25);

            if (Input.IsActionJustReleased("place"))
            {
                beingDragged = false;

                var currentTile = GetPositionOnBoard();
                if (currentTile.x < 15 && currentTile.x >= 0 && currentTile.y >= 0 && currentTile.y < 15)
                {
                    placeAt(currentTile);
                }
                else
                {
                    unplace();
                    Position = new Vector2(5 * (Index + 1) + 50 * Index, 5);
                }
            }
        }
        else
        {
            if (mouseInside)
            {
                if (Input.IsActionJustPressed("place"))
                {
                    beingDragged = true;
                }
            }
        }
    }
}
