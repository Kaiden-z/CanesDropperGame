using Godot;
using System;

public partial class ItemBase : RigidBody2D
{
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		CheckOutOfBounds();
	}

	// Delete item if out fallen out of view
	private void CheckOutOfBounds() {
		if (Position.Y > GetWindow().Size.Y + 100) {
			GD.Print("Item fell out of bounds. Destroying this object...");
			QueueFree();
		}
	}
}
