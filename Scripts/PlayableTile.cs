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
    public int State = ((int)States.AtDeck);
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
    public AdjacentTiles AdjacentTiles = new AdjacentTiles();
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
            State = ((int)States.Placed);

            styleBox.SetCornerRadiusAll(5);

            Size = new Vector2(50, 50);

            label.Size = new Vector2(50, 50);
            label.AddThemeFontSizeOverride("font_size", 40);

            pointsLabel.Size = new Vector2(45, 45);
            pointsLabel.AddThemeFontSizeOverride("font_size", 15);
        }
        else if (newState == ((int)States.Placed))
        {
            State = ((int)States.Placed);

            styleBox.SetCornerRadiusAll(0);

            Size = new Vector2(40, 40);

            label.Size = new Vector2(40, 40);
            label.AddThemeFontSizeOverride("font_size", 30);

            pointsLabel.Size = new Vector2(37, 37);
            pointsLabel.AddThemeFontSizeOverride("font_size", 12);
        }
    }

    private void placeAt(Vector2 position)
    {
        setState(((int)States.Placed));
        GlobalPosition = position * new Vector2(40, 40);

        var myPosition = GetPositionOnBoard();

        foreach (var child in GetParent().GetChildren())
        {
            var letter = child as PlayableTile;
            if (child == this || letter.State == (int)States.AtDeck) continue;

            var letterPosition = letter.GetPositionOnBoard();

            if (letterPosition == myPosition + new Vector2(0, 1))
            {
                AdjacentTiles.Bottom = new AdjacentTiles.Side(letter, true);
            }
            else if (letterPosition == myPosition + new Vector2(1, 0))
            {
                AdjacentTiles.Right = new AdjacentTiles.Side(letter, true);
            }
            else if (letterPosition == myPosition - new Vector2(0, 1))
            {
                AdjacentTiles.Top = new AdjacentTiles.Side(letter, true);
            }
            else if (letterPosition == myPosition - new Vector2(1, 0))
            {
                AdjacentTiles.Left = new AdjacentTiles.Side(letter, true);
            }
        }

        if (!AdjacentTiles.Left.Exists)
        {
            HorizontalConnections.IsFirstLetter = true;
            if (AdjacentTiles.Right.Exists)
            {
                AdjacentTiles.Right.Tile.HorizontalConnections.IsFirstLetter = false;
            }
        } else
        {
            AdjacentTiles.Left.Tile.AdjacentTiles.Right = new AdjacentTiles.Side(this, true);
            HorizontalConnections.IsFirstLetter = false;
        }

        if (!AdjacentTiles.Top.Exists)
        {
            VerticalConnections.IsFirstLetter = true;
            if (AdjacentTiles.Bottom.Exists)
            {
                AdjacentTiles.Bottom.Tile.VerticalConnections.IsFirstLetter = false;
            }
        } else
        {
            AdjacentTiles.Top.Tile.AdjacentTiles.Bottom = new AdjacentTiles.Side(this, true);
            VerticalConnections.IsFirstLetter = false;
        }
    }

    private void unplace()
    {
        setState(((int)States.AtDeck));
        Position = new Vector2(5 * (Index + 1) + 50 * Index, 5);
    }

    public Vector2 GetMousePositionOnBoard()
    {
        var gridPos = (GetGlobalMousePosition() / 40).Floor();
        return gridPos;
    }

    public Vector2 GetPositionOnBoard()
    {
        var gridPos = (GlobalPosition / 40).Floor();
        return gridPos;
    }

    public void DrawBack()
    {
        if (State == ((int)States.Placed))
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

                var currentTile = GetMousePositionOnBoard();
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
