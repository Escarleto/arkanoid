using Godot;
using System;
public enum PowerUps { Laser, Enlarge, Catch, Slow, Break, Disruption, Player } 
public abstract partial class PowerUp : Area2D
{
    [Export] private Texture2D[] StateTextures; // indexed to match enum order
    [Export] protected PowerUps Type;
    protected Jogador Player;
    
    private void ApplyState()
    {
        //_textureRect.Texture = StateTextures[(int)CurrentState];
    }

    private void GetPlayer(Node2D Body)
    {
        if (Body is Jogador)
        {
            Player = Body as Jogador;
            Effect();
            QueueFree();
        }
    }

    protected abstract void Effect();
}
