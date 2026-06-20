using Godot;
using System;
public enum PowerUp { Laser, Enlarge, Catch, Slow, Break, Disruption, Player } 
public partial class PowerBase : Node2D
{
    [Export] public Texture2D[] StateTextures; // indexed to match enum order
    [Export] public PowerUp CurrentState = PowerUp.Laser;
    
    private void ApplyState()
    {
        //_textureRect.Texture = StateTextures[(int)CurrentState];
    }
}
