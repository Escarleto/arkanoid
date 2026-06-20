using Godot;
using System;

public partial class MainGame : Node2D
{
    public static MainGame Instance { get; private set; }
    private int CurrentLevelBlocks = 0;
    public int LevelBlocks
    {
        get => CurrentLevelBlocks;
        set
        {
            CurrentLevelBlocks = value;
            if (value <= 0)
            {
                GD.Print("Level completo");
            }
            GD.Print(CurrentLevelBlocks);
        }
    }

    [Export] public Hud hud;
    [Export] private Timer RestartTime;

    public Action RestartLevel;
    public override void _Ready()
    {
        Instance = this;
        RestartLevel += Restart;
        Label lifeValue = hud.life;
        Label scoreValue = hud.score;
    }

    private void Restart()
    {
        GetTree().Paused = true;
        RestartTime.Start();
    }

    public void GameOver()
    {
        GD.Print("Game ovi");
    }

    
}
