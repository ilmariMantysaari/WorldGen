using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

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

    public WorldGen()
    {
      graphics = new GraphicsDeviceManager(this);
      camera = new Camera();
      Content.RootDirectory = "Content";
      grid = new Grid();
      generator = new Generator();
      generator.GenerateTerrain(grid.grid);
      
    }

    protected override void Initialize()
    {
      base.Initialize();
      viewport = GraphicsDevice.Viewport;
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      var textures = new Dictionary<TileType, Texture2D>
      {
        { TileType.GRASS, Content.Load<Texture2D>("green_tile") },
        { TileType.WATER, Content.Load<Texture2D>("blue_tile") },
        { TileType.SAND, Content.Load<Texture2D>("yellow_tile") },
        { TileType.ROCK, Content.Load<Texture2D>("grey_tile") }
      };
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

    private double lastPress = 0;
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      //regenarate terrain
      //
      if (Keyboard.GetState().IsKeyDown(Keys.Space) && (gameTime.TotalGameTime.TotalMilliseconds - lastPress) > 500)
      {
        Debug.WriteLine(lastPress + " " + gameTime.TotalGameTime.TotalMilliseconds);
        lastPress = gameTime.TotalGameTime.TotalMilliseconds;

        grid.grid = new TileType[500, 500];
        generator.GenerateTerrain(grid.grid);
      }

      camera.Update();

      base.Update(gameTime);
    }
   
  }
}
