using Godot;
using System;

public partial class Hud : Node2D
{   
    private int Score = 0;
    [Export]public Label scoreTxt;
    [Export]public Label life;
    
    private void UpdateLifes(int NewLife) // atualiza o text de Life
    {   
        life.Text = NewLife.ToString().PadLeft(2, '0');;
    }

    public void UpdateScore(int NewScore) // atualiza o text de Score
    {
        scoreTxt.Text = NewScore.ToString().PadLeft(5, '0');
    }
}
