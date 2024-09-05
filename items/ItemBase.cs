using System;
using Godot;
using Godot.Collections;

public partial class ItemBase : RigidBody2D
{
	[Export]
	public PackedScene NextItem;
	[Export]
	public int ItemID;
	private bool Matched = false;
	public override void _Ready()
	{
		ContactMonitor = true;
		MaxContactsReported = 20;
		SetCollisionLayerValue(1, false);
		SetCollisionLayerValue(1, false);
		SetCollisionLayerValue(2, true);
		SetCollisionMaskValue(2, true);
	}

	public override void _Process(double delta)
	{
		CheckOutOfBounds();
	}

	/*
		Delete item if out fallen out of window view
	*/
	private void CheckOutOfBounds() 
	{
		if (Position.Y > GetWindow().Size.Y + 100) {
			GD.Print("Item fell out of bounds. Destroying this object...");
			QueueFree();
		}
	}

	/*
		Checks if collision is detected between two items of
		the same id. Spawn next item of next level if match found.
	*/
	private void OnBodyEntered(Node body) 
	{
		ItemBase collidingItem = body as ItemBase;

		if (collidingItem != null && NextItem != null && 
			collidingItem.ItemID == ItemID)
		{
			if (Matched) return;

			GD.Print("Match found");

			collidingItem.Matched = true;
			RigidBody2D newItem = NextItem.Instantiate<RigidBody2D>();
			GetTree().Root.CallDeferred("add_child", newItem);
			newItem.Position = (Position + collidingItem.Position) / 2;
			collidingItem.QueueFree();
			QueueFree();
		}

		SetCollisionLayerValue(3, true);
	}
}
