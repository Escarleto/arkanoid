using Godot;
using System;

public partial class PU_Enlarge : PowerUp
{
    protected override void Effect()
    {
        Jogador player = Player;                       // local, not the field
        Vector2 original = player.Scale;
        player.Scale = new Vector2(original.X * 2, original.Y);

        var timer = new Timer { WaitTime = 5.0, OneShot = true };
        player.AddChild(timer);                         // lives on the paddle, not the power-up
        timer.Timeout += () =>
        {
            if (IsInstanceValid(player))
                player.Scale = original;
            timer.QueueFree();
        };
        timer.Start();
    }
}
