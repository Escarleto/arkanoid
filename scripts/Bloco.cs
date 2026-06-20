using Godot;
using System;
// Assuming you have an AtlasTexture assigned to a Sprite2D
public enum ColorAtlas { White, Orange, Blue, Green, Red, Pink, Yellow, Iron, Gold } 
public partial class Bloco : StaticBody2D
{
    [Export] public Texture2D[] StateTextures; // indexed to match enum order
    [Export] public ColorAtlas CurrentState = ColorAtlas.White;
    private Area2D _detectionArea;

    public CollisionShape2D Collision;
    private TextureRect _textureRect;
    private Bola _bola;

    [Export] private int CurrentHealth = 1;
    public int Health
    {
        get => CurrentHealth;
        set
        {
            CurrentHealth = value;
            if (value == 0) QueueFree();
        }
    }
    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("TextureRect");
        ApplyState();
    }

    private void ApplyState()
    {
        _textureRect.Texture = StateTextures[(int)CurrentState];
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Bola bola)
        {
            DestroyBlock();
        }
    }
    private void DestroyBlock()
    {
        QueueFree();
    }
}
