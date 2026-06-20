using Godot;
using System;

public partial class Bola : CharacterBody2D
{
    [Export] private float Speed = 1f;
    private Jogador Jog;

    public override void _Ready()
    {
        base._Ready();
        RotationDegrees = (float)GD.RandRange(45f, 135f);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 Movement = Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
        KinematicCollision2D Collision = MoveAndCollide(Movement);

        if (Collision != null)
        {
            if (Collision.GetCollider() is Jogador)
            {
                Jog = Collision.GetCollider() as Jogador;
                Rotation = Jog.GlobalPosition.DirectionTo(GlobalPosition).Angle();
                Jog = null;
            }
            else
            {
                Rotation = Vector2.FromAngle(Rotation).Bounce(Collision.GetNormal()).Angle();   
                if (Collision.GetCollider() is Bloco)
                {
                    Bloco BlocoAcertado = Collision.GetCollider() as Bloco;
                    BlocoAcertado.Health--;
                }
            }
                         
        }

    }

}
