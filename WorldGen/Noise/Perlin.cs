using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen.Noise
{
  /// <summary>
  /// Class for storing parameters for the perlin generator
  /// Handy for saving good parameter combinations
  /// </summary>
  public class PerlinOptions
  {
    public int octaves;
    public float amplitude;
    public float frequency;
    public float persistence;
    public float emphasis;
  }

  public class Perlin
  {
    private Random random;
    private int[] permutation;
    private Vector2[] gradients;

    public Perlin(Random rand = null)
    {
      if (rand != null){
        random = rand;
      }else{
        random = new Random();
      }
      permutation = CalculatePermutation();
      gradients = CalculateGradients();
    }

    /// <summary>
    /// Generates a 2D array of floats using the improved Perlin noise algorithm
    /// </summary>
    /// <param name="width">Width of output array</param>
    /// <param name="height">Height of output array</param>
    /// <param name="octaves">Number of arrays blended together</param>
    /// <param name="frequency">Determines the frequency the pattern changes</param>
    /// <param name="amplitude">Determines how drastically the values change</param>
    /// <param name="persistence">Determines how much the frequency changes between octaves</param>
    /// <param name="emphasis">Number towards which the values are weighted</param>
    /// <returns></returns>
    public float[,] GeneratePerlinArray(int width, int height, int octaves = 1, float frequency = 1f,
                                        float amplitude = 1f, float? persistence = null, float emphasis = 0.5f)
    {
      var perlinArray = new float[width,height];
      if (persistence == null){
        persistence = amplitude;
      }

      //TODO: implement emphasis
      for (var octave = 0; octave < octaves; octave++){
        Parallel.For(0, width * height, (index) => {
            var x = index % width;
            var y = index / width;
            var noise = this.Noise(x * frequency / width, y * frequency / height);
          noise *= amplitude;// + (emphasis - noise);
            perlinArray[x,y] += noise;
          }
        );
        frequency *= 2;
        amplitude *= (float)persistence;
      }
      return perlinArray;
    }

    public float[,] GeneratePerlinArray(int width, int height, PerlinOptions opt)
    {
      return GeneratePerlinArray(
          width, height, opt.octaves, opt.frequency,
          opt.amplitude, opt.persistence, opt.emphasis
        );
    }

    /// <summary>
    /// Generates new Perlin array that continues the pattern of other Perlin array.
    /// </summary>
    /// <returns></returns>
    public float[,] GenerateWithSeedArray()
    {
      //TODO: this method
      throw new NotImplementedException();
    }
    
    public void Reseed()
    {
      this.permutation = CalculatePermutation();
    }
    
    /// <summary>
    /// Calculates the noise for a point
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public float Noise(float x, float y)
    {
      var cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));
      var total = 0f;
      var corners = new[] { new Vector2(0, 0) + cell, new Vector2(0, 1) + cell,
                            new Vector2(1, 0) + cell, new Vector2(1, 1) + cell };

      foreach (var corner in corners)
      {
        var substract = new Vector2(x - corner.X, y - corner.Y);
        var index = permutation[(int)corner.X % permutation.Length];
        index = permutation[(index + (int)corner.Y) % permutation.Length];

        var grad = gradients[index % gradients.Length];

        total += FadeVector(substract) * Vector2.Dot(grad, substract);
      }

      return total;
    }

    #region Private

    private int[] CalculatePermutation()
    {
      var perm = Enumerable.Range(0, 256).ToArray();
      return perm.OrderBy(x => this.random.Next()).ToArray();
    }

    private Vector2[] CalculateGradients()
    {
      var grad = new Vector2[256];

      for (var i = 0; i < grad.Length; i++){
        var gradient = new Vector2((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1));

        gradient.Normalize();
        grad[i] = gradient;
      }
      return grad;
    }

    private static float FadeVector(Vector2 vect)
    {
      return Fade(vect.X) * Fade(vect.Y);
    }

    //Ken Perlin's fade function
    private static float Fade(float t)
    {
      t = Math.Abs(t);
      return 1f - t * t * t * (t * (t * 6 - 15) + 10);
    }

    #endregion
  }
}
