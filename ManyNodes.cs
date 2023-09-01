using Godot;
using System;

public partial class ManyNodes : Node2D
{
    private readonly Polygon2D[] polys;

    public ManyNodes() : base()
    {
        Main.QFreeChildren(this);
        polys = new Polygon2D[Main.N_BODIES];
        for (int i = 0; i < Main.N_BODIES; i++)
        {
            polys[i] = new Polygon2D();
            AddChild(polys[i]);
            polys[i].Color = Main.Singleton.Triangles[i].Color3[0];
        }
    }

    public override void _Ready()
    {
        // GetNode("ExamplePolygon2D").QueueFree();
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < Main.N_BODIES; i++)
        {
            Main.Triangle t = Main.Singleton.Triangles[i];
            polys[i].Polygon = t.GlobalVertices();
        }
    }
}
