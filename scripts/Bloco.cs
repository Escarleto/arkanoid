using Godot;
using System;
using System.Diagnostics;

public enum ColorAtlas { White, Orange, Blue, Green, Red, Pink, Yellow, Iron, Gold } // Todos as texturas de blocos que temos 
public partial class Bloco : StaticBody2D
{
    [Export] public Texture2D[] StateTextures; 
    private TextureRect _textureRect;
    [Export] public ColorAtlas CurrentState = ColorAtlas.White;
    [Export] private int CurrentHealth = 1;
    
    public int Health // Vida do bloco - tbm apaga o bloco caso a vida for 0 
    {
        get => CurrentHealth;
        set
        {
            CurrentHealth = value;
            if (value == 0)
            {
                MainGame.Instance.LevelBlocks--;
                PowerUp.TrySpawn(GetParent(), GlobalPosition);
                QueueFree();
            }
        }
    }
    private int Points = 50;
    
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
