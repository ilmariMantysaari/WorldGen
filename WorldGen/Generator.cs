using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
  public class Generator
  {
    public void GenerateTerrain(TileType[,] grid)
    {
      GenerateGround(grid);
      GenerateLakes(grid);
    }

    public void GenerateGround(TileType[,] grid)
    {
      //just grass for the time being
      for (int i = 0; i < grid.GetLength(0); i++)
      {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
          //TODO: replace with real logic
          grid[i, j] = TileType.GRASS;
        }
      }
    }
    public void GenerateLakes(TileType[,] grid)
    {

    }
  }
}
