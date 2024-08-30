using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Numerics;

public partial class ItemSpawner : Node2D
{
	[Export]
	public PackedScene itemBase;
	private Random rng = new Random();

	List<PackedScene> ItemList = new List<PackedScene>();
	
	public override void _Ready()
	{
		ItemList = GetItemListFromDir("res://items/itemRBs/");
		GD.Print(ItemList.Count);
		for (int i = 0; i < ItemList.Count; i++) {
			GD.Print(ItemList[i]);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnItem()
	{
		GD.Print("Spawning item...");

		int i = rng.Next(0, ItemList.Count - 1);
		RigidBody2D newItemRB = ItemList[i].Instantiate<RigidBody2D>();
		ItemBase newItem = itemBase.Instantiate<ItemBase>().WithItem(newItemRB);
		GetTree().Root.AddChild(newItem);

		GD.Print(this.Position);

		newItem.GlobalPosition = this.GlobalPosition;
		
		GD.Print("Done spawning");
	}

	private List<PackedScene> GetItemListFromDir(string path) {

		List<PackedScene> scenes = new List<PackedScene>();
        DirAccess directory = DirAccess.Open(path);
		
        // Open the directory
        if (directory != null)
        {
            directory.ListDirBegin();

            string fileName = directory.GetNext();
            while (fileName != "")
            {
                if (fileName.EndsWith(".tscn")) // Only consider .tscn files
                {
                    // Load the scene
                    string scenePath = path + "/" + fileName;
                    PackedScene scene = (PackedScene) ResourceLoader.Load(scenePath);
                    if (scene != null)
                    {
                        scenes.Add(scene);
                    }
                    else
                    {
                        GD.PrintErr($"Failed to load scene: {scenePath}");
                    }
                }

                fileName = directory.GetNext();
            }

            directory.ListDirEnd();
        }
        else
        {
            GD.PrintErr($"Failed to open directory: {path}");
        }

		return scenes;
	}
}
