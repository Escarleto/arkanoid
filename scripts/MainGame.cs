using Godot;
using System;

public partial class MainGame : Node2D
{
    public static MainGame Instance {get; private set;}
    //private int CurrentLevelBlocks

    [Export] public Hud hud;
    public override void _Ready()
    {
        Instance = this;
        Label lifeValue = hud.life;
        Label scoreValue = hud.score;
    }

    public void GameOver()
    {
        GD.Print("Game ovi");
    }
}
