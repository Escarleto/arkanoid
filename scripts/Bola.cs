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
        RotationDegrees = (float)GD.RandRange(30f, 120f);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 Movement = Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
        KinematicCollision2D Collision = MoveAndCollide(Movement);

        if (Collision == null) return; // Ve se tem colisao senao nao avança 

        if (Collision.GetCollider() is Jogador) // se a bola bateu no jogador, muda a direçao para onde se esta a dirigir
        {
            Node2D Jog = Collision.GetCollider() as Node2D;
            Rotation = Jog.GlobalPosition.DirectionTo(GlobalPosition).Angle();
        }  
        else
        {
            Rotation = Vector2.FromAngle(Rotation).Bounce(Collision.GetNormal()).Angle();    // se for uma parede, calcula o novo angulo
            if (Collision.GetCollider() is Bloco)// se bater num bloco, tira vida ao bloco
            {
                Bloco BlocoAcertado = Collision.GetCollider() as Bloco;
                BlocoAcertado.Health--;
            }
        }
    }

    private void Respawn() // Quando a bola da respawn, vai para o ponto inicial e dirigi se para baixo num angulo aleatorio
    {
        Position = SpawnPos;
        RotationDegrees = (float)GD.RandRange(30f, 120f);
    }
}
