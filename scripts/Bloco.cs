using Godot;
using System;
// Assuming you have an AtlasTexture assigned to a Sprite2D
public enum ColorAtlas { White, Orange, Blue, Green, Red, Pink, Yellow, Iron, Gold } 
public partial class Bloco : StaticBody2D
{
    [Export] public Texture2D[] StateTextures; // indexed to match enum order
    [Export] public ColorAtlas CurrentState = ColorAtlas.White;

    private TextureRect _textureRect;

    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("TextureRect");
        ApplyState();
    }

    private void ApplyState()
    {
        _textureRect.Texture = StateTextures[(int)CurrentState];
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
