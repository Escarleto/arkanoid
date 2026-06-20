using Godot;
using System;

public partial class Hud : Node2D
{
    [Export]public Label score;
    [Export]public Label life;
    
    private void UpdateLifes(int NewLife)
    {   
        string NewLifeText = NewLife < 10 ? "0" + NewLife.ToString() : 
                             NewLife.ToString();
        life.Text = NewLifeText;
    }
}
