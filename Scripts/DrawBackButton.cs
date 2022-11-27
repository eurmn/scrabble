using Godot;
using System;

public partial class DrawBackButton : ScrabbleActionButton
{
    public void onPressed()
    {
        GetParent<Panel>().GetParent<GUI>().DrawAllCardsBack();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("pressed",new Callable(this,nameof(onPressed)));
        Setup();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    /* public override void _Process(float delta)
    {
    } */
}
