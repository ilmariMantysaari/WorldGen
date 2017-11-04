﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WorldGen.Noise;

namespace WorldGen
{
  /// <summary>
  /// 
  /// </summary>
  public class WorldGen : Game
  {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public Grid grid;
    public Camera camera;
    public static Viewport viewport;
    private Generator generator;
    private Random rand;
    private PerlinOptions options;

    public WorldGen()
    {
      graphics = new GraphicsDeviceManager(this);
      camera = new Camera();
      Content.RootDirectory = "Content";
      grid = new Grid();
      generator = new Generator();
      options = new PerlinOptions
      {
        amplitude = 0.5f,
        emphasis = 0.5f,
        frequency = 3,
        octaves = 5,
        persistence = 0.25f
      };
      rand = new Random(1);
      generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      
      
    }

    protected override void Initialize()
    {
      base.Initialize();
      viewport = GraphicsDevice.Viewport;
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      var textures = new Dictionary<TileType, Texture2D>();
      textures.Add(TileType.GRASS, Content.Load<Texture2D>("green_tile"));
      textures.Add(TileType.WATER, Content.Load<Texture2D>("blue_tile"));
      textures.Add(TileType.SAND, Content.Load<Texture2D>("yellow_tile"));
      textures.Add(TileType.ROCK, Content.Load<Texture2D>("grey_tile"));

      //testing tiles for perlin noise
      textures.Add(TileType.PERLIN1, Content.Load<Texture2D>("perlin_tile1"));
      textures.Add(TileType.PERLIN2, Content.Load<Texture2D>("perlin_tile2"));
      textures.Add(TileType.PERLIN3, Content.Load<Texture2D>("perlin_tile3"));
      textures.Add(TileType.PERLIN4, Content.Load<Texture2D>("perlin_tile4"));
      textures.Add(TileType.PERLIN5, Content.Load<Texture2D>("perlin_tile5"));
      textures.Add(TileType.PERLIN6, Content.Load<Texture2D>("perlin_tile6"));
      textures.Add(TileType.PERLIN7, Content.Load<Texture2D>("perlin_tile7"));
      textures.Add(TileType.PERLIN8, Content.Load<Texture2D>("perlin_tile8"));
      textures.Add(TileType.PERLIN9, Content.Load<Texture2D>("perlin_tile9"));
      grid.tileTextures = textures;
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);
      spriteBatch.Begin(transformMatrix: camera.Transform);

      grid.Draw(spriteBatch);

      base.Draw(gameTime);
      spriteBatch.End();
    }
    
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      //regenarate terrain
      //
      if (Keyboard.GetState().IsKeyDown(Keys.Space))
      {
        //rand = new Random(1);
        grid.grid = new TileType[100, 100];
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.W))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.amplitude += 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.S))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.amplitude -= 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.E))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.frequency += 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.frequency -= 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.R))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.persistence += 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.F))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.persistence -= 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.T))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.emphasis += 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.G))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.emphasis -= 0.01f;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.Y))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.octaves += 1;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.H))
      {
        rand = new Random(1);
        grid.grid = new TileType[100, 100];
        options.octaves -= 1;
        generator.Generate(grid, Generator.GenType.PERLIN, rand, options);
      }

      //TODO: print the current values to UI
      camera.Update();

      base.Update(gameTime);
    }
   
  }
}
