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
    private bool canDrop = true;
	private List<PackedScene> ItemList = new List<PackedScene>();
    private ItemBase HeldItem;
    private const float spawnCooldown = 0.5f;
    private const string spawnPoolPath = "res://items/itemRBs/spawnPool/";
    
	
	public override void _Ready()
	{
		ItemList = GetItemListFromDir(spawnPoolPath);
		GD.Print("Item list size: " + ItemList.Count);
        SpawnItem();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	/*
        Spawn a randomly chosen item from item list
    */
	public void SpawnItem()
	{
        GD.Print("Spawning item...");

        int i = rng.Next(0, ItemList.Count);
        HeldItem = ItemList[i].Instantiate<ItemBase>();
        AddChild(HeldItem);
        HeldItem.GlobalPosition = GlobalPosition;
        HeldItem.DisableMode = CollisionObject2D.DisableModeEnum.MakeStatic;
        HeldItem.ProcessMode = ProcessModeEnum.Disabled;
        HeldItem.SetCollisionLayerValue(2, false);
        HeldItem.SetCollisionMaskValue(2, false);
        
        GD.Print("Done spawning");
        
	}

    /*
        Release item and re-enable physics simulation
    */
    public void DropItem()
    {
        if (canDrop) {
            canDrop = false;
            RemoveChild(HeldItem);
            GetTree().Root.AddChild(HeldItem);
            HeldItem.ProcessMode = ProcessModeEnum.Always;
            HeldItem.GlobalPosition = GlobalPosition;
            HeldItem.SetCollisionLayerValue(2, true);
            HeldItem.SetCollisionMaskValue(2, true);
            GetTree().CreateTimer(spawnCooldown).Timeout += OnTimerOut;
        }
        
    }

    /*
        Spawn a new item after drop cooldown timer has completed
    */
    private void OnTimerOut() 
    {
        SpawnItem();
        canDrop = true;
    }

	/*
        Get list of items to spawn from directory path
    */
	private List<PackedScene> GetItemListFromDir(string path) 
    {

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
