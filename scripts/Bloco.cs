using Godot;
using System;
using System.Diagnostics;

// Todos as texturas de blocos que temos 
public enum ColorAtlas { White = 1, Orange = 2, Blue = 3, Green = 4, Red = 5, Pink = 6, Yellow = 7, Iron = 8, Gold = 9 } 
public partial class Bloco : StaticBody2D
{
    [Export] public Texture2D[] StateTextures; 
    private TextureRect _textureRect;
    [Export] public ColorAtlas CurrentState = ColorAtlas.White;
    [Export] private int CurrentHealth = 1;
    private int Score = 50;

    public int Health // Vida do bloco - tbm apaga o bloco caso a vida for 0 
    {
        get => CurrentHealth;
        set
        {
            CurrentHealth = value;
            if (value == 0)
            {
                MainGame.Instance.LevelBlocks--;
                MainGame.Instance.CurrentScore += Score * ((int)CurrentState + 1);
                PowerUp.TrySpawn(GetParent(), GlobalPosition);
                QueueFree();
            }
        }
    }    
    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("TextureRect");
        MainGame.Instance.LevelBlocks++;

        ApplyState();
    }

    private void ApplyState() // Aplica a textura
    {
        _textureRect.Texture = StateTextures[(int)CurrentState];

    }
}
