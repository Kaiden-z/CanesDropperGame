using Godot;
using Godot.Collections;

public partial class ItemBase : RigidBody2D
{
	[Export]
	public int ItemID;
	private bool Matched = false;
	public override void _Ready()
	{
		ContactMonitor = true;
	}

	public override void _Process(double delta)
	{
		CheckOutOfBounds();
	}

	/*
		Delete item if out fallen out of window view
	*/
	private void CheckOutOfBounds() {
		if (Position.Y > GetWindow().Size.Y + 100) {
			GD.Print("Item fell out of bounds. Destroying this object...");
			QueueFree();
		}
	}

	/*
		Checks if collision is detected between two items of
		the same id. Spawn next item of next level if match found.
	*/
	private void _on_body_entered(Node body) {
		ItemBase colliding_item = body as ItemBase;
			if (colliding_item != null && !colliding_item.Matched) {
				Matched = true;
				colliding_item.Matched = true;
				Vector2 avg_pos = (Position + colliding_item.Position) / 2;
				
			}
	}
}
