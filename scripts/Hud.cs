using Godot;
using System;

public partial class Hud : Node2D
{   
    private int Score = 0;
    [Export]public Label scoreTxt;
    [Export]public Label life;
    
    private void UpdateLifes(int NewLife) // atualiza o text de Life
    {   
        string NewLifeText = NewLife < 10 ? "0" + NewLife.ToString() : 
                             NewLife.ToString();
        life.Text = NewLifeText;
    }

    public void UpdateScore(int NewScore)
    {
        scoreTxt.Text = NewScore.ToString().PadLeft(5, '0');
    }
}
