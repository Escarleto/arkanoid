using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
	private int CurrentHealth = 2;
	[Signal] public delegate int LifeChangedEventHandler(int NewHealth);
	public int Health // vida atual do jogador - se for 0 manda oara game over 
	{
		get => CurrentHealth;
		set
		{
			CurrentHealth = value;
			EmitSignal("LifeChanged", value);
			if (value <= 0) MainGame.Instance.GameOver();
		}
	}
	private float H_Direction = 0;
	private Vector2 SpawnPos;
	[Export] private float Speed = 1f;
	[Export] private float SpeedFallofRate = 1f;

	public override void _Ready()
	{
		base._Ready();
		EmitSignal("LifeChanged", CurrentHealth);
		SpawnPos = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		H_Direction = Mathf.MoveToward(H_Direction, Input.GetAxis("ui_left", "ui_right"), SpeedFallofRate * (float)delta);

		Velocity = new Vector2(H_Direction * Speed, 0f);
		MoveAndSlide();
	}

	private void Respawn() // volta ao ponto inicial quando da restart 
	{
		Position = SpawnPos;
	}

	private void OnLosingBall(Node2D Bola)
	{
		if (Bola is Bola) Health--;
	}
}
