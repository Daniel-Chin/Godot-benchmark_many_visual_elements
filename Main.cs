using Godot;
using System;

public partial class Main : Node2D
{
	public const int N_BODIES = 10000;
	public const float SCREEN_SIZE = 500f;
	public const float TRIANGLE_SIZE = 0.1f;
	public const float ACCELERATION = 0.3f;

	static public Random rand = new Random();
	static public Main Singleton;
	public Triangle[] Triangles;

	public partial class Triangle {
		public Vector2 Location;
		public Vector2 Velocity;
		public Vector2[] Vertices = new Vector2[3];
		public Color Color;
		public Vector2[] GlobalVertices() {
			Vector2[] g = new Vector2[3];
			for (int j = 0; j < 3; j++)
			{
				g[j] = (Location + Vertices[j]) * SCREEN_SIZE;
			}
			return g;
		}
	}

	public Main() : base()
	{
		Singleton = this;
		Triangles = new Triangle[N_BODIES];
		for (int i = 0; i < N_BODIES; i++)
		{
			Triangle t = new Triangle();
			Triangles[i] = t;
			t.Location = new Vector2(
				(float) rand.NextDouble(), 
				(float) rand.NextDouble()
			);
			for (int j = 0; j < 3; j++)
			{
				t.Vertices[j] = new Vector2(
					(float) rand.NextDouble() - .5f, 
					(float) rand.NextDouble() - .5f
				) * TRIANGLE_SIZE;
			}
			t.Velocity = Vector2.Zero;
			t.Color = Color.FromHsv(
				(float) rand.NextDouble(), 1, (float) rand.NextDouble()
			);
		}
	}

	private static Vector2 FLIP_X = new Vector2(-1, 1);
	private static Vector2 FLIP_Y = new Vector2(1, -1);
	public override void _Process(float delta)
	{
		for (int i = 0; i < N_BODIES; i++)
		{
			Triangle t = Triangles[i];
			t.Velocity += new Vector2(
				(float) rand.NextDouble() - .5f, 
				(float) rand.NextDouble() - .5f
			) * ACCELERATION * delta;
			t.Location += t.Velocity * delta * .5f;
			if (t.Location.x < 0.0f || t.Location.x > 1.0f) {
				t.Velocity *= FLIP_X;
			}
			if (t.Location.y < 0.0f || t.Location.y > 1.0f) {
				t.Velocity *= FLIP_Y;
			}
			t.Location += t.Velocity * delta * .5f;
		}
	}

	public static void QFreeChildren(Node node)
	{
		foreach (Node x in node.GetChildren())
		{
			x.QueueFree();
		}
	}
}
