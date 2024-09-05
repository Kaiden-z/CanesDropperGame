using Godot;
using System;
using System.ComponentModel;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
	private int Score = 0;
    public override void _Ready()
    {
        Instance = this;
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
	public void StartNewGame() {
		Score = 0;
		GetTree().ReloadCurrentScene();
	}
}
