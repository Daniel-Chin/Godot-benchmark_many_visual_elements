# Benchmark Godot 4: Many Nodes VS Custom Draw VS Rendering Server
When you have a large number of visual elements to render, Godot officially recommends Custom Draw and Rendering Server over using a thousand nodes. See:  
- [Custom drawing in 2D - Godot Docs](https://docs.godotengine.org/en/stable/tutorials/2d/custom_drawing_in_2d.html)  
- [Optimization using Servers - Godot Docs](https://docs.godotengine.org/en/stable/tutorials/performance/using_servers.html)  

But exactly how much improvement do they offer? Which one is the fastest? This repo is an experiment to benchmark that.  

## FPS Results
| # of triangles | Many Nodes | Custom Draw | Rendering Server | Rendering Server but rid not reused |
|----------------|-----------:|------------:|-----------------:|---:|
|      100       |   60 FPS   |    60 FPS   |       60 FPS     | 60 FPS |
|      1000      |   40 FPS   |    60 FPS   |       60 FPS     | 59 FPS |
|      10000     |    3 FPS   |     5 FPS   |       60 FPS     | 4 FPS |
|      100000    |   <1 FPS   |    <1 FPS   |        8 FPS     | <0.1 FPS (multi-thread catastrophe?) |

rid can be re-used when the only thing changing through time is the transformation matrix of objects. If object vertices need to be computed every frame, then you cannot re-use rid.  

## Static RAM Results
| # of triangles | Many Nodes | Custom Draw | Rendering Server |
|----------------|-----------:|------------:|-----------------:|
|      10000     |   120 MiB  |     86 MiB  |       87 MiB     |

However this comparison may be unfair since my implementation heavily uses dynamic (heap) memory. Todo: implement object pooling and test again.  

## Reproduction
In file "Main.cs", change  
- line 6: set # of triangles.  
- line 60: toggle rendering strategy.  

In file "DirectServer.cs", change  
- line 6: whether to reuse rid.  
