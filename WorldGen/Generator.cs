using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
  public class Generator
  {
    private double lakeModifier = 0.01;
    private Random random;

    public Generator()
    {
      random = new Random(DateTime.UtcNow.Millisecond);
    }

    public void GenerateTerrain(TileType[,] grid)
    {
      for (int i = 0; i < grid.GetLength(0); i++)
      {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
          if (!(grid[i, j] != TileType.GRASS))
          {
            grid[i, j] = TileType.GRASS;
          }
          
          var rand = random.NextDouble();
          if (rand < lakeModifier)
          {
            GenerateLake(grid, i, j);
          }
        }
      }
    }

    public void GenerateLake(TileType[,] grid, int x, int y)
    {
      //just a 3X3 rectangle for now
      for (int i = x; i < x+3; i++)
      {
        for (int j = y; j < y+3; j++)
        {
          if (j < grid.GetLength(1) && i < grid.GetLength(0))
          {
            grid[i, j] = TileType.WATER;
          }
        }
      }
    }
  }
}
