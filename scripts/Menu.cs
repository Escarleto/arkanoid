using Godot;
using System;

public partial class Menu : Control
{
    private void OnButtonPress()
    {
        MainGame.Instance.GoNext(1);
        QueueFree();
    }
}
