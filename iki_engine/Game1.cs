using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.AccessControl;
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
         public List<GameEntity> entities=new List<GameEntity>();
         public Map map=new Map();

         public Game1()
         {
             graphicsManager=new GraphicsDeviceManager(this);
             Content.RootDirectory = "Assets";
             graphicsManager.PreferredBackBufferWidth = 1280;
             graphicsManager.PreferredBackBufferHeight = 720;
             graphicsManager.IsFullScreen = false;
             graphicsManager.ApplyChanges();
         }

         protected override void Initialize()
         {
             base.Initialize();
         }

         protected override void LoadContent()
         {
             spriteBatch = new SpriteBatch(GraphicsDevice);
             map.Load(Content);
             LoadLevel();
         }

         protected override void Update(GameTime gameTime)
         {
             Input.Update();
             UpdateEntities();
             base.Update(gameTime);
         }

         protected override void Draw(GameTime gameTime)
         {
             GraphicsDevice.Clear(Color.CadetBlue);
             spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend);
             DrawEntities();
             map.drawWalls(spriteBatch);
             spriteBatch.End();
             base.Draw(gameTime);
         }
         public void LoadEntities()
         { 
             foreach (GameEntity entity in entities)
             {
                 entity.Initialize();
                 entity.Load(Content);
             }
         }

         public void UpdateEntities()
         {
             foreach (GameEntity entity in entities)
             {
                 entity.Update(entities);
             }
         }
         public void DrawEntities()
         {
             foreach (GameEntity entity in entities)
             {
                 entity.draw(spriteBatch);
             }
         }

         public void LoadLevel()
         {
             entities.Add(new Player(new Vector2(640,360)));
             map.walls.Add(new Wall(new Rectangle(256,256,256,256)));
             map.walls.Add(new Wall(new Rectangle(0, 650, 1280, 128)));
             LoadEntities();
         }
    }
}
