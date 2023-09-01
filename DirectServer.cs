using Godot;
using System;

public partial class DirectServer : Node2D
{
    public const bool REUSE_ITEMS = true;

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
        Rid this_item = GetCanvasItem();
        for (int i = 0; i < Main.N_BODIES; i++)
		{
            Main.Triangle t = Main.Singleton.Triangles[i];
            if (! REUSE_ITEMS) {
                RenderingServer.FreeRid(items[i]);
                items[i] = RenderingServer.CanvasItemCreate();
                RenderingServer.CanvasItemSetParent(items[i], this_item);
                RenderingServer.CanvasItemAddPolygon(items[i], t.Vertices, t.Color3);
            }
            RenderingServer.CanvasItemSetTransform(
                items[i], new Transform2D(0f, t.Location).Scaled(Main.SCREEN_SIZE)
            );
        }
    }
}
