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
    private double lakeModifier = 0.01;
    private Random random;
    public enum GenType {PERLIN, TERRAIN};
    public Dictionary<double, TileType> tileMap;

    public Generator()
    {
      random = new Random(DateTime.UtcNow.Millisecond);
    }

    public void Generate(Grid grid, GenType type, Random rand = null, PerlinOptions opt = null)
    {
      switch(type){
        case GenType.PERLIN:
          grid.grid = GeneratePerlin(grid.grid.GetLength(0), grid.grid.GetLength(1), grid.tileMap, rand, opt);
          break;
        case GenType.TERRAIN:
          GenerateTerrain(grid.grid);
          break;
      }
    }

    private TileType[,] GeneratePerlin(int x, int y, Dictionary<double, TileType> tileMap, Random rand = null, PerlinOptions opt = null)
    {
      if (rand == null)
      {
        rand = new Random();
      }
      var perlin = new Perlin(rand);
      //var perlinGrid = perlin.GeneratePerlinArray(x, y, octaves:5, frequency: 1f,
        //                                                amplitude: 0.5f, persistence: 0.5f, emphasis: 0.5f);

      var perlinGrid = perlin.GeneratePerlinArray(x, y, opt);

      var grid = new TileType[x, y];

      //map the float values to tiles
      for (int i = 0; i < grid.GetLength(0); i++){
        for (int j = 0; j < grid.GetLength(1); j++){
          grid[i, j] = GetTile(perlinGrid[i,j], tileMap);
        }
      }
      return grid;
    }

    private void GenerateTerrain(TileType[,] grid)
    {
      for (int i = 0; i < grid.GetLength(0); i++){
        for (int j = 0; j < grid.GetLength(1); j++){
          if (!(grid[i, j] != TileType.GRASS)){
            grid[i, j] = TileType.GRASS;
          }
          var rand = random.NextDouble();
          if (rand < lakeModifier){
            GenerateLake(grid, i, j);
          }
        }
      }
    }

    public void GenerateLake(TileType[,] grid, int x, int y)
    {
      //just a 3X3 rectangle for now
      for (int i = x; i < x+3; i++){
        for (int j = y; j < y+3; j++){
          if (j < grid.GetLength(1) && i < grid.GetLength(0)){
            grid[i, j] = TileType.WATER;
          }
        }
      }
    }

    //finds tiletype from the tilemap dictionary with a floating point value
    private TileType GetTile(double value, Dictionary<double, TileType> tileMap)
    {
      //FIXME: better mapping function
      var ordered = tileMap.OrderByDescending(x => x.Key);
      foreach (var tile in ordered){
        if (tile.Key < Math.Abs(value)){
          return tile.Value;
        }
      }
      return TileType.PERLIN1;
    }
  }
}
