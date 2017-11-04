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
    public Dictionary<double, TileType> tileMap;

    public Grid()
    {
      grid = new TileType[100,100];
      tileSize = 64;
      tileMap = new Dictionary<double, TileType>();
      /*
      tileMap.Add( 0.0, TileType.PERLIN1);
      tileMap.Add( 0.1, TileType.PERLIN2);
      tileMap.Add( 0.2, TileType.PERLIN3);
      tileMap.Add( 0.3, TileType.PERLIN4);
      tileMap.Add( 0.45, TileType.PERLIN5);
      tileMap.Add( 0.6, TileType.PERLIN6);
      tileMap.Add( 0.7, TileType.PERLIN7);
      tileMap.Add( 0.8, TileType.PERLIN8);
      tileMap.Add( 0.9, TileType.PERLIN9);*/
      
      tileMap.Add(0.0, TileType.WATER);
      tileMap.Add(0.25, TileType.SAND);
      tileMap.Add(0.50, TileType.GRASS);
      tileMap.Add(0.75, TileType.ROCK);

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
