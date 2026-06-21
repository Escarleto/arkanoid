using Godot;
using System;

public partial class Menu : Control
{
    private void OnButtonPress() // é o trigger do botao 
    {
        MainGame.Instance.GoNext(1);
        MainGame.Instance.Reset();
        QueueFree();
    }
}
