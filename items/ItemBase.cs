using Godot;
using System;

public partial class ItemBase : Node2D
{
	public RigidBody2D body;
	public override void _Ready()
	{
		GetChild<Sprite2D>(0).Hide();
	}

	public override void _Process(double delta)
	{
		if (body.Position.Y > GetWindow().Size.Y + 100) {
			GD.Print("Item fell out of bounds. Destroying this object...");
			QueueFree();
		}
	}

	public ItemBase WithItem(RigidBody2D itemRB) {
		body = itemRB;
		AddChild(body);

		return this;
	}
}
