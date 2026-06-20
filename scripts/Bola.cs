using Godot;
using System;

public partial class Bola : CharacterBody2D
{
    [Export] private float Speed = 1f;
    private Vector2 SpawnPos;

    public override void _Ready()
    {
        base._Ready();
        SpawnPos = Position;
        RotationDegrees = (float)GD.RandRange(45f, 135f);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 Movement = Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
        KinematicCollision2D Collision = MoveAndCollide(Movement);

        if (Collision == null) return;

        if (Collision.GetCollider() is Jogador)
        {
            Node2D Jog = Collision.GetCollider() as Node2D;
            Rotation = Jog.GlobalPosition.DirectionTo(GlobalPosition).Angle();
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

    private void Respawn() => Position = SpawnPos;
}
