using Godot;
using System;

public partial class PlayButton : ScrabbleActionButton
{
    public void onPressed()
    {
        /* HTTPRequest httpRequest = GetNode<HTTPRequest>("HTTPRequest");
        httpRequest.Request("http://www.mocky.io/v2/5185415ba171ea3a00704eed", new string[] { "user-agent: YourCustomUserAgent" }); */
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
