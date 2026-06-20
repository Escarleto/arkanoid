using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
	private int Vida = 2;
	private float H_Direction = 0;
	[Export] private float Speed = 1f;
	[Export] private float SpeedFallofRate = 1f;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		H_Direction = Mathf.MoveToward(H_Direction, Input.GetAxis("ui_left", "ui_right"), SpeedFallofRate * (float)delta);

		Velocity = new Vector2(H_Direction * Speed, 0f);
		MoveAndSlide();
	}
}
