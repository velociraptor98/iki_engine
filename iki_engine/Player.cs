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
    class Player : Character 
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

        public override void Update(List<GameEntity> entity,Map map)
        {
            CheckInput(entity,map);
            base.Update(entity,map);
        }

        private void CheckInput(List<GameEntity> entity, Map map)
        {
            if (applyGravity == false)
            {
                if (Input.IsKeyDown(Keys.D))
                {
                    MoveRight();
                }
                else if (Input.IsKeyDown(Keys.A))
                {
                    MoveLeft();
                }
                else if (Input.IsKeyDown(Keys.W))
                {
                    MoveUp();
                }
                else if (Input.IsKeyDown(Keys.S))
                {
                    MoveDown();
                }
            }
            else
            {
                if(Input.IsKeyDown(Keys.D))
                {
                    MoveRight();
                }
                else if (Input.IsKeyDown(Keys.A))
                {
                    MoveLeft();
                }
                else if(Input.IsKeyDown(Keys.W))
                {
                    Jump(map);
                }
                else if (Input.IsKeyDown(Keys.S))
                {
                    MoveDown();
                }
            }
        }
    }
}
