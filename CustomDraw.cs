using Godot;
using System;

public partial class CustomDraw : Node2D
{
	public override void _Draw() 
	{
        for (int i = 0; i < Main.N_BODIES; i++)
		{
            Main.Triangle t = Main.Singleton.Triangles[i];
			DrawPolygon(t.GlobalVertices(), t.Color3);
		}
	}
	public override void _Process(double delta)
	{
		QueueRedraw();
	}
}
