using Godot;
using System;

public partial class FPSLabel : Label
{
    public FPSLabel()
    {

    }

    public override void _Process(float delta)
    {
        Text = $"FPS={Engine.GetFramesPerSecond()}";
    }
}
