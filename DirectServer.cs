using Godot;
using System;

public partial class DirectServer : Node2D
{
    private Rid[] items = new Rid[Main.N_BODIES];
    public DirectServer() : base()
    {
        for (int i = 0; i < Main.N_BODIES; i++)
		{
            Main.Triangle t = Main.Singleton.Triangles[i];
            items[i] = RenderingServer.CanvasItemCreate();
            RenderingServer.CanvasItemSetParent(items[i], GetCanvasItem());
            RenderingServer.CanvasItemAddPolygon(items[i], t.Vertices, t.Color3);
        }
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < Main.N_BODIES; i++)
		{
            Main.Triangle t = Main.Singleton.Triangles[i];
            RenderingServer.CanvasItemSetTransform(
                items[i], new Transform2D(0f, t.Location).Scaled(Main.SCREEN_SIZE)
            );
        }
    }
}
