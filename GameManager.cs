using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnGameLost() {
		GD.Print("Height limit reached. Ending game...");
	}
}
