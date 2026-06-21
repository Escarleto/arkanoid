using Godot;
using System;

public partial class MainGame : Node2D
{
    #region Setup
    public static MainGame Instance { get; private set; }
    
    [Export] public Hud hud;
    [Export] private Timer StartTime;
    [Export] public PackedScene Transition;
    [Export] public bool DebugMode = false;
    [Export] public PackedScene[] levels;

    private int CurrentLevelBlocks = 0;
    private int CurrentLevel = 0;
    private Node _currentLevelInstance;

    [Signal] public delegate void StartLevelEventHandler();

    public override void _Ready()
    {
        Instance = this;
        Label lifeValue = hud.life;
        Label scoreValue = hud.score;
        
        if (!DebugMode)
            GetTree().Paused = true;
    }
    #endregion

    #region Level managing
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

    public void GoNext(int levelIndex)
    {
        var timer = new Timer { WaitTime = 3.0, OneShot = true };

        if (levelIndex < levels.Length && levelIndex >= 0)
        {
            _currentLevelInstance?.QueueFree();
            CurrentLevel = levelIndex;
            GD.Print(CurrentLevel);
            _currentLevelInstance = levels[CurrentLevel].Instantiate();
            GetTree().Root.AddChild(_currentLevelInstance);

            AddChild(timer);
            timer.Start();
        }

        Node CurrentTransition = Transition.Instantiate();
        GetTree().Root.AddChild(CurrentTransition);

        GetTree().Paused = true;
        timer.Timeout += () =>
        {
            timer.QueueFree();
            CurrentTransition.QueueFree();
            StartTime.Start();
        };
    }

    public void Restart(Node2D Body)
    {
        if (Body is Bola)
        {
            EmitSignal("StartLevel");
            GetTree().Paused = true;
            StartTime.Start();
        }
    }

    public void OnStartTimeout() => GetTree().Paused = false;

    public void GameOver()
    {
        GD.Print("Game ovi");
    }
    #endregion
}
