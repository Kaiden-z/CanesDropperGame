using Godot;
using System;

public partial class HeightLimit : Area2D
{
	[Signal]
	public delegate void HeighLimitReachedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Monitoring = true;
		HeighLimitReached += GameManager.Instance.OnGameLost;
	}

	public void OnBodyEntered(Node2D body) 
	{
		EmitSignal(SignalName.HeighLimitReached);
	}
}
