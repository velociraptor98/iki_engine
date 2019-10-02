using System;
using System.Collections.Generic;
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
    public class Map
    {
        public List<Wall> walls=new List<Wall>();
        private Texture2D wallImage;
        public int MapHeight = 9;
        public int MapWidth = 15;
        public const  int TileSize = 128;

        public void Load(ContentManager content)
        {
            wallImage = TextureLoader.Load("pixel", content);
        }

        public Rectangle CheckCollision(Rectangle input)
        {
            foreach (Wall wall in walls)
            {
                if (wall != null && wall.wall.Intersects(input) == true)
                {
                    return wall.wall;
                }
            }
            return Rectangle.Empty;
        }

        public void drawWalls(SpriteBatch spriteBatch)
        {
            foreach (Wall wall in walls)
            {
                if (wall != null && wall.active == true)
                {
                    spriteBatch.Draw(wallImage,new Vector2(wall.wall.X,wall.wall.Y),wall.wall, Color.Black,0.0f,Vector2.Zero,1.0f,SpriteEffects.None,0.7f);
                }
            }
        }
    }

public class Wall
{
    public Rectangle wall;
    public bool active = true;

    public Wall()
    {

    }

    public Wall(Rectangle initRectangle)
    {
        wall = initRectangle;
    }
}
}
