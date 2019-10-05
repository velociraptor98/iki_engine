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
    class GameEntity
    {
        protected Texture2D image;
        protected Vector2 center;
        public Vector2 position;
        public Color colorDraw = Color.White;
        public float scale = 1.0f;
        public float rotation = 0.0f;
        public float layerDepth = 0.5f;
        public bool active = true;
        public bool canCollide = true;
        protected int boundingWidth, boundingHeight;
        protected Vector2 boundingOffset;
        private Texture2D boundingTexture;
        private const bool drawBoundingBoxes = true;

        //position can change each frame so better to use properties
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)(position.X+boundingOffset.X),(int)(position.Y+boundingOffset.Y),boundingWidth,boundingHeight); }
        }

        public GameEntity()
        {
            ;
        }

        public virtual void Initialize()
        {
            ;
        }

        public virtual void Load(ContentManager content)
        {
            boundingTexture = TextureLoader.Load("pixel", content);
            CalculateCenter();
            if (image != null)
            {
                boundingWidth = image.Width;
                boundingHeight = image.Height;
            }
        }

        public virtual void Update(List<GameEntity> entity)
        {
            ;
        }

        public virtual void draw(SpriteBatch spritesBatch)
        {
            if(boundingTexture!=null && active==true && canCollide==true)
                spritesBatch.Draw(boundingTexture, new Vector2(BoundingBox.X,BoundingBox.Y), BoundingBox, new Color(128,128,128,128),0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
            if (image!=null && active == true)
            spritesBatch.Draw(image,position,null,colorDraw,rotation,Vector2.Zero,scale,SpriteEffects.None,layerDepth);
        }

        private void CalculateCenter()
        {
            if (image == null)
            {
                center.X = this.image.Width / 2;
                center.Y = this.image.Height / 2;
            }
        }
    }
}
