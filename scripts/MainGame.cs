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
    public int LevelBlocks // Conta quantos blocos tem na fase
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

    public void GoNext(int levelIndex) // Vai para o proximo lvl 
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

    private void ChangeLevel(int Index) // Da trigger a mudança de lvl
    {
        _currentLevelInstance?.QueueFree();
        CurrentLevel = Index;
        GD.Print(levels[CurrentLevel].ResourceName);
        _currentLevelInstance = levels[CurrentLevel].Instantiate();
        GetTree().Root.AddChild(_currentLevelInstance);
    }

    private void StartGame() // Começa o jogo - para a primeira scene do jogo
    {
        EmitSignal("StartLevel");
        GetTree().Paused = true;
        StartTime.Start();
    }

    public void Respawn(Node2D Body) // Da trigger quando a bola sai do jogo 
    {
        if (Body is Bola) 
            StartGame();
    }

    public void OnStartTimeout() => GetTree().Paused = false; // Cria uma pausa quando da respawn 

    public void GameOver() // Caso perca o jogo, vai para o nivel 0 
    {
        ChangeLevel(0);
        GetTree().Paused = true;
    }
    #endregion
}
