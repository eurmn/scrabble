using Godot;
using System;

public partial class Background : Sprite2D
{
	// Declare member variables here. Examples:
	private int currentLine = 0;

	private void GenerateLine() {
		var line = new Line2D();
		line.DefaultColor = Colors.White;
		line.ZAsRelative = false;
		
		line.ZIndex = 10;
		if (currentLine == 0 || currentLine == 15 || currentLine == 16) {
			line.Width = 4;
		} else {
			line.Width = 2f;
		}

		if (currentLine <= 15) {
			line.AddPoint(new Vector2(currentLine * 40, 0));
			line.AddPoint(new Vector2(currentLine * 40, 600));
		} else {
			line.AddPoint(new Vector2(0, (currentLine - 16) * 40));
			line.AddPoint(new Vector2(1024, (currentLine - 16) * 40));
		}

		AddChild(line);

		currentLine++;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Godot.Engine.MaxFps = 60;

		for (int i = 0; i < 32; i++)
		{
			GenerateLine();
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
