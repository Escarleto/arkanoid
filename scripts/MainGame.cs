using Godot;
using System;

public partial class MainGame : Node2D
{
    [Export] public Hud hud;
        public override void _Ready()
    {
        Label lifeValue = hud.life;
        Label scoreValue = hud.score;
    }
}
