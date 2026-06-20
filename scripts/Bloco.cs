using Godot;
using System;
using System.Diagnostics;
// Assuming you have an AtlasTexture assigned to a Sprite2D
public enum ColorAtlas { White, Orange, Blue, Green, Red, Pink, Yellow, Iron, Gold } 
public partial class Bloco : StaticBody2D
{
    [Export] public Texture2D[] StateTextures; // indexed to match enum order
    private TextureRect _textureRect;
    [Export] public ColorAtlas CurrentState = ColorAtlas.White;
    [Export] private int CurrentHealth = 1;
    public int Health
    {
        get => CurrentHealth;
        set
        {
            CurrentHealth = value;
            if (value == 0)
            {
                MainGame.Instance.LevelBlocks--;
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

    private void ApplyState()
    {
        _textureRect.Texture = StateTextures[(int)CurrentState];

    }
}
