using Godot;
using System;

public class CentralTile : Tile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        color = new Color("ffff4d");
        text = "";
        position = new Vector2(7, 7);

        SetupTile();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
