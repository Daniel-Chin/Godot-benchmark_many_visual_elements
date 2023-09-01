using Godot;
using System;

public partial class HUD : CanvasLayer
{
    public override void _Ready()
    {
        FPSLabel fpsLabel = new FPSLabel();
        AddChild(fpsLabel);        
    }
}
