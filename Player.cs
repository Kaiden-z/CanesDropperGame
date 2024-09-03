using Godot;
using System;
using System.Reflection;

public partial class Player : CharacterBody2D
{
	[Export]
	public ItemSpawner itemSpawner;
	[Export]
	public CollisionShape2D collisionShape2D;
	public const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		UpdateMovement(delta);
		CheckDropInput();
	}

	private void UpdateMovement(double delta) {

		Vector2 position = Position;

		if (Input.IsActionPressed("left"))
		{
			position.X -= Speed * (float) delta;
		}
		else if (Input.IsActionPressed("right"))
		{
			position.X += Speed * (float) delta;
		}

		Position = position;
		MoveAndSlide();
	}

	private void CheckDropInput() {
		if (Input.IsActionJustPressed("drop")) {
			GD.Print("Drop pressed");
			itemSpawner.DropItem();
		}
	}

}
