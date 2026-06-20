using Godot;
using System;

public partial class MainGame : Node2D
{
    public static MainGame Instance { get; private set; }
    private int CurrentLevelBlocks = 0;
    private int CurrentLevel = 0;
    public int LevelBlocks
    {
        get => CurrentLevelBlocks;
        set
        {
            CurrentLevelBlocks = value;
            if (value <= 0)
            {
                GoNext(CurrentLevel+1);
                GD.Print("Level completo");
            }
        }
    }

    [Export] public Hud hud;
    [Export] private Timer RestartTime;

    public override void _Ready()
    {
        Instance = this;
        Label lifeValue = hud.life;
        Label scoreValue = hud.score;
    }

    [Export] public PackedScene[] levels;
    private Node _currentLevelInstance;
    private void GoNext(int levelIndex)
    {
        _currentLevelInstance?.QueueFree();
        CurrentLevel = levelIndex;
        
        if (CurrentLevel >= levels.Length)
        {
            GD.Print("All levels complete!");
            return;
        }
        _currentLevelInstance = levels[CurrentLevel].Instantiate();
        GetTree().Root.AddChild(_currentLevelInstance);
    }

    [Signal] public delegate void RestartLevelEventHandler();
    public void Restart(Node2D Body)
    {
        if (Body is Bola)
        {
            EmitSignal("RestartLevel");
            GetTree().Paused = true;
            RestartTime.Start();
        }
    }

    public void OnRestartTimeout() => GetTree().Paused = false;

    public void GameOver()
    {
        GD.Print("Game ovi");
    }
}
