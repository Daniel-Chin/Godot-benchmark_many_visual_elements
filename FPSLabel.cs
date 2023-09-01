using Godot;
using System;

public partial class FPSLabel : Label
{
    public FPSLabel()
    {

    }

    public override void _Process(double delta)
    {
        Text = $"FPS={Engine.GetFramesPerSecond()}";
    }
}
