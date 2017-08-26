using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
  /// <summary>
  /// Represents a 2D grid of tiles
  /// </summary>
  public class Grid
  {
    public TileType[,] grid;
    private int tileSize;
    public Dictionary<TileType, Texture2D> tileTextures;
    

    public Grid()
    {
      grid = new TileType[100,100];
      tileSize = 64;
    }
    
    public void Draw(SpriteBatch batch)
    {
      Vector2 position = new Vector2(0, 0);
      for (int i = 0; i < grid.GetLength(0); i++)
      {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
          var texture = tileTextures[grid[i, j]];
          batch.Draw(texture, position);
          position.Y += tileSize;
        }
        position.X += tileSize;
        position.Y = 0;
      }
    }
  }
}
