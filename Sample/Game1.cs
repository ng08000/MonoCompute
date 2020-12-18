using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoCompute;
using System.IO;

namespace Sample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var cs = new ComputeShader(GraphicsDevice, CSCompiler.Compile("sample.compute"));
            var surface = new ComputeTexture2D(GraphicsDevice, 1024, 1024, false, SurfaceFormat.Color);
            cs.SetRWTexture(0, surface);
            cs.Dispatch("Main", 32, 32, 1);
            surface.Texture.SaveAsJpeg(File.Create("file.jpg"), 1024, 1024);

            cs.Dispatch("Main2", 32, 32, 1);
            surface.Texture.SaveAsJpeg(File.Create("file1.jpg"), 1024, 1024);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
