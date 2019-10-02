using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace iki_engine
{
    class Game1:Microsoft.Xna.Framework.Game
    {
         GraphicsDeviceManager graphicsManager;
         SpriteBatch spriteBatch;
         private Texture2D image;
         private Vector2 position;
            
         public Game1()
         {
             graphicsManager=new GraphicsDeviceManager(this);
             Content.RootDirectory = "Assets";
             graphicsManager.PreferredBackBufferWidth = 1280;
             graphicsManager.PreferredBackBufferHeight = 720;
             graphicsManager.IsFullScreen = false;
             graphicsManager.ApplyChanges();
             position = new Vector2(640,360);
         }

         protected override void Initialize()
         {
             base.Initialize();
         }

         protected override void LoadContent()
         {
             spriteBatch = new SpriteBatch(GraphicsDevice);
             image = TextureLoader.Load("sprite", Content);
         }

         protected override void Update(GameTime gameTime)
         {
             Input.Update();
             if (Input.IsKeyDown(Keys.D))
             {
                 position.X += 1;
             }
             else if (Input.IsKeyDown(Keys.A))
             {
                 position.X -= 1;
             }
             else if (Input.IsKeyDown(Keys.W))
             {
                 position.Y -= 1;
             }
             else if (Input.IsKeyDown(Keys.S))
             {
                 position.Y += 1;
             }
             base.Update(gameTime);
         }

         protected override void Draw(GameTime gameTime)
         {
             GraphicsDevice.Clear(Color.CadetBlue);
             spriteBatch.Begin();
             spriteBatch.Draw(image,position,Color.White);
             spriteBatch.End();
             base.Draw(gameTime);
         }
    }
}
