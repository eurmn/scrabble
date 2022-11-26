using Godot;
using System;

public class PlayableTile : Panel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private DynamicFont font = new DynamicFont();
    private DynamicFont pointsFont = new DynamicFont();
    private bool beingDragged = false;
    private bool mouseInside = false;
    private int state = ((int)States.AtDeck);
    private Label label = new Label();
    private Label pointsLabel = new Label();
    private StyleBoxFlat styleBox = new StyleBoxFlat();
    public enum States
    {
        AtDeck = 0,
        Placed = 1
    }
    public string Letter;
    public int Points;
    public int Index;

    private void Setup()
    {
        styleBox.BgColor = new Color("ffffff");
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

        Connect("mouse_entered", this, nameof(onMouseEntered));

        AddChild(label);
        AddChild(pointsLabel);
    }

    private void onMouseEntered()
    {
        Connect("mouse_exited", this, nameof(onMouseExited));
        mouseInside = true;
    }

    private void onMouseExited()
    {
        mouseInside = false;
        beingDragged = false;
    }

    private Vector2 getPositionOnBoard()
    {
        var gridPos = (GetGlobalMousePosition() / 40).Floor();
        return gridPos;
    }

    private void setState(int state)
    {
        if (state == ((int)States.AtDeck))
        {
            state = ((int)States.AtDeck);

            styleBox.SetCornerRadiusAll(5);

            RectSize = new Vector2(50, 50);

            label.RectSize = new Vector2(50, 50);
            font.Size = 40;

            pointsLabel.RectSize = new Vector2(45, 45);
            pointsFont.Size = 15;
        }
        else if (state == ((int)States.Placed))
        {
            state = ((int)States.Placed);

            styleBox.SetCornerRadiusAll(0);

            RectSize = new Vector2(40, 40);

            label.RectSize = new Vector2(40, 40);
            font.Size = 30;

            pointsLabel.RectSize = new Vector2(37, 37);
            pointsFont.Size = 12;
        }
    }

    private void placeAt(Vector2 position)
    {
        setState(((int)States.Placed));
        RectGlobalPosition = position * new Vector2(40, 40);
    }

    private void unplace()
    {
        setState(((int)States.AtDeck));
        RectPosition = new Vector2(5 * (Index + 1) + 50 * Index, 5);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        font.FontData = pointsFont.FontData = GD.Load<DynamicFontData>("res://Fonts/UbuntuMono/UbuntuMonoFontData.tres");
        font.Size = 40;
        pointsFont.Size = 15;

        Setup();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (mouseInside)
        {
            if (Input.IsMouseButtonPressed(((int)ButtonList.Left)))
            {
                beingDragged = true;

                setState(((int)States.AtDeck));

                RectGlobalPosition = GetGlobalMousePosition() - new Vector2(25, 25);
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
                    RectPosition = new Vector2(5 * (Index + 1) + 50 * Index, 5);
                }
            }
        }
    }
}
