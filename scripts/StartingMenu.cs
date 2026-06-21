using Godot;
using System;

public partial class StartingMenu : Control
{
    private void OnStart()
    {
        MainGame.Instance.GoNext(0);
        QueueFree();
    }
}
