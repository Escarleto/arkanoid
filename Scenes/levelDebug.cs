using Godot;
using System;

public partial class levelDebug : Node2D
{
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("ui_down"))
        {
            MainGame.Instance.LevelBlocks = 0;
        }
    }

}
