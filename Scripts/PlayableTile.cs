using Godot;
using System;

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
    // public 
    public enum States
    {
        AtDeck = 0,
        Placed = 1
    }
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

    private Vector2 getPositionOnBoard()
    {
        var gridPos = (GetGlobalMousePosition() / 40).Floor();
        return gridPos;
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

    private void placeAt(Vector2 position)
    {
        setState(((int)States.Placed));
        GlobalPosition = position * new Vector2(40, 40);
    }

    private void unplace()
    {
        setState(((int)States.AtDeck));
        Position = new Vector2(5 * (Index + 1) + 50 * Index, 5);
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

            if (Input.IsActionJustPressed("place"))
            {
                beingDragged = false;

                var currentTile = getPositionOnBoard();
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
        /*     if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                beingDragged = true;

                setState(((int)States.AtDeck));

                GlobalPosition = GetGlobalMousePosition() - new Vector2(25, 25);
            }
            else if (beingDragged)
            {
                beingDragged = false;

                var currentTile = getPositionOnBoard();
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
        } */
    }
}
