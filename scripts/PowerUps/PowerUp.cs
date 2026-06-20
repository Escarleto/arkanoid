using Godot;
using System;
using System.Collections.Generic;

public enum PowerUps { Laser, Enlarge, Catch, Slow, Break, Disruption, Player }

public abstract partial class PowerUp : Area2D
{
    [Export] private Texture2D[] StateTextures; 
    [Export] public PowerUps Type;              
    [Export] private float FallSpeed = 60f;     
    protected Jogador Player;

    // --- lifecycle ---------------------------------------------------------

    public override void _Ready()
    {
        BodyEntered += GetPlayer;               
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Down * FallSpeed * (float)delta;
        
        if (GlobalPosition.Y > 250) QueueFree();
    }

    // --- drop logic (owned by the base script) -----------------------------

    private const float DropChance = 0.3f;      // 30% of destroyed blocks drop

    private static readonly (PowerUps powerUp, float weight)[] _weightedDrops =
    {
        (PowerUps.Laser,      0f),
        (PowerUps.Enlarge,    1.0f),
        (PowerUps.Catch,      0f),
        (PowerUps.Slow,       0f),
        (PowerUps.Break,      0f),
        (PowerUps.Disruption, 0f),
        (PowerUps.Player,     0.3f),
    };

    private static readonly Dictionary<PowerUps, string> _scenePaths = new()
    {
        { PowerUps.Laser,      "res://Scenes/PowerUps/power_Laser.tscn" },
        { PowerUps.Enlarge,    "res://Scenes/PowerUps/power_Enlarge.tscn" },
        { PowerUps.Catch,      "res://Scenes/PowerUps/power_Catch.tscn" },
        { PowerUps.Slow,       "res://Scenes/PowerUps/power_Slow.tscn" },
        { PowerUps.Break,      "res://Scenes/PowerUps/power_Break.tscn" },
        { PowerUps.Disruption, "res://Scenes/PowerUps/power_Disruption.tscn" },
        { PowerUps.Player,     "res://Scenes/PowerUps/power_Player.tscn" },
    };

    
    public static void TrySpawn(Node parent, Vector2 position)
    {
        if (GD.Randf() > DropChance) return;            // no drop
        Spawn(GetRandomWeightedPowerUp(), parent, position);
    }

    
    public static void Spawn(PowerUps type, Node parent, Vector2 position)
    {
        var scene = GD.Load<PackedScene>(_scenePaths[type]);
        var instance = scene.Instantiate<PowerUp>();
        parent.AddChild(instance);
        instance.GlobalPosition = position;
        instance.Type = type;
    }

    public static PowerUps GetRandomWeightedPowerUp()
    {
        float totalWeight = 0f;
        foreach (var (_, weight) in _weightedDrops)
            totalWeight += weight;

        float roll = (float)GD.RandRange(0.0, (double)totalWeight);
        float cumulative = 0f;

        foreach (var (powerUp, weight) in _weightedDrops)
        {
            cumulative += weight;
            if (roll <= cumulative)
                return powerUp;
        }

        return _weightedDrops[0].powerUp;
    }

    // --- collection --------------------------------------------------------

    private void GetPlayer(Node2D Body)
    {
        if (Body is Jogador jogador)
        {
            Player = jogador;
            Effect();
            QueueFree();
        }
    }

    protected abstract void Effect();
}