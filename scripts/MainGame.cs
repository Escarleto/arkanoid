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
    private int CurrentLevel = 1;
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
            //GD.Print(value);
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
        GetTree().Paused = true;

        Node CurrentTransition = Transition.Instantiate();
        if (levelIndex < levels.Length && levelIndex > 0)
        {
            ChangeLevel(levelIndex);
            AddChild(timer);
            if (levelIndex < 4)
            {
                timer.Start();
                GetTree().Root.AddChild(CurrentTransition);
            }  
        }

        timer.Timeout += () =>
        {
            timer.QueueFree();
            CurrentTransition.QueueFree();
            StartGame();
        };
    }

    private void ChangeLevel(int Index)
    {
        _currentLevelInstance?.QueueFree();
        CurrentLevel = Index;
        GD.Print(levels[CurrentLevel].ResourceName);
        _currentLevelInstance = levels[CurrentLevel].Instantiate();
        GetTree().Root.AddChild(_currentLevelInstance);
    }

    private void StartGame()
    {
        EmitSignal("StartLevel");
        GetTree().Paused = true;
        StartTime.Start();
    }

    public void Respawn(Node2D Body)
    {
        if (Body is Bola) 
            StartGame();
    }

    public void OnStartTimeout() => GetTree().Paused = false;

    public void GameOver()
    {
        ChangeLevel(0);
        GetTree().Paused = true;
    }
    #endregion
}
