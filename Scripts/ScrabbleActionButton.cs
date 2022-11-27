using Godot;
using System;

public partial class ScrabbleActionButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public void Setup()
    {
        Connect("button_down",new Callable(this,nameof(onButtonDown)));
        Connect("button_up",new Callable(this,nameof(onButtonUp)));
    }

    public void onButtonDown()
    {
        Scale = new Vector2(.95f, .95f);
    }

    public void onButtonUp()
    {
        Scale = new Vector2(1, 1);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Setup();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    /* public override void _Process(float delta)
    {
    } */
}
