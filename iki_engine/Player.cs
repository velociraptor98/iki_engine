using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
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
    class Player : GameEntity 
    {
        public Player()
        {

        }

        public Player(Vector2 initPosition)
        {
            this.position = initPosition;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            image = TextureLoader.Load("sprite", content);
            base.Load(content);
        }

        public override void Update(List<GameEntity> entity)
        {
            CheckInput();
            base.Update(entity);
        }

        private void CheckInput()
        {
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
        }
    }
}
