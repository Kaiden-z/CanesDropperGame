using Godot;
using System;
using System.ComponentModel;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
	private int Score = 0;
	private int NumDrops = 0;
    public override void _Ready()
    {
        Instance = this;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnGameLost() 
	{
		GD.Print("Height limit reached. Ending game...");
	}
	public void AddScore(int sc) 
	{
		Score += sc;
		GD.Print(Score);
	}
}
