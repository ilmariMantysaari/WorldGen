using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGen.Noise;

namespace WorldGen
{
  public class Generator
  {
    public Random rand;
    public IOrderedEnumerable<KeyValuePair<double, TileType>> tileMap;
    public PerlinOptions options;

    public Generator(Random rand, PerlinOptions opts, Dictionary<double, TileType> tiles)
    {
      options = opts;
      tileMap = tiles.OrderByDescending(x => x.Key);
      this.rand = rand;
    }

    public TileType[,] Generate(Grid grid)
    {
      var x = grid.grid.GetLength(0);
      var y = grid.grid.GetLength(1);
      var perlin = new Perlin(rand);
      var perlinGrid = perlin.GeneratePerlinArray(x, y, options);

      var tiles = new TileType[x, y];

      //maps the float values to tiles
      for (int i = 0; i < tiles.GetLength(0); i++){
        for (int j = 0; j < tiles.GetLength(1); j++){
          tiles[i, j] = GetTile(perlinGrid[i,j], grid.tileMap);
        }
      }
      return tiles;
    }

    //finds tiletype from the tilemap dictionary with a floating point value
    private TileType GetTile(double value, Dictionary<double, TileType> tileMap)
    {
      //FIXME: better mapping function
      foreach (var tile in this.tileMap){
        if (tile.Key < Math.Abs(value)){
          return tile.Value;
        }
      }
      return TileType.PERLIN1;
    }
  }
}
