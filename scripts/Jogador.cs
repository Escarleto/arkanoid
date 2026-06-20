using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
	private int CurrentHealth = 2;
	private int Health
	{
		get => CurrentHealth;
		set
		{
			CurrentHealth = value;
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
		SpawnPos = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		H_Direction = Mathf.MoveToward(H_Direction, Input.GetAxis("ui_left", "ui_right"), SpeedFallofRate * (float)delta);

		Velocity = new Vector2(H_Direction * Speed, 0f);
		MoveAndSlide();
	}

	private void Respawn()
	{
		Position = SpawnPos;
		Health--;
	}
}
