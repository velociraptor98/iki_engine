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
   public class Character : GameEntity
    {
        public Vector2 velocity;
        //deacc high the quicker the entity will come to a halt
        protected float deacc = 1.2f;
        protected float accelerate = 0.70f;
        protected float maxSpeed = 5.0f;
        const float gravity = 1.0f;
        const float jumpVel = 16.0f;
        const float maxFallVel = 32.0f;
        protected bool isJumping;
        //applygravity is set to true when making platformers
        public static bool applyGravity = true;
        public override void Initialize()
        {
            velocity = Vector2.Zero;
            isJumping = false;
            base.Initialize(); 
        }
        public override void Update(List<GameEntity> entity, Map map)
        {
            UpdateMovement(entity, map);
            base.Update(entity, map);
        }
        protected virtual bool CheckCollision(Map map,List<GameEntity> entity,bool xAxis)
        {
            Rectangle futureBoundingBox = BoundingBox;
            int maxX = (int)maxSpeed;
            int maxY = (int)maxSpeed;
            if(applyGravity == true)
            {
                maxY = (int)jumpVel;
            }
            if(applyGravity==true && velocity.X!=0)
            {
                if (velocity.X > 0)
                    futureBoundingBox.X += maxX;
                else
                    futureBoundingBox.X -= maxX;
            }
            else if(xAxis ==false && velocity.Y!=0 && applyGravity == false)
            {
                if (velocity.Y > 0)
                    futureBoundingBox.Y += maxY;
                else
                    futureBoundingBox.Y -= maxY;
            }
            else if (xAxis == false && velocity.Y != gravity && applyGravity == true)
            {
                if (velocity.Y > 0)
                    futureBoundingBox.Y += maxY;
                else
                    futureBoundingBox.Y -= maxY;
            }
            Rectangle wallCollision = map.CheckCollision(futureBoundingBox);
            if (wallCollision!=Rectangle.Empty)
            {
                if (applyGravity == true && velocity.Y >= gravity && (futureBoundingBox.Bottom > wallCollision.Top - maxSpeed) && (futureBoundingBox.Bottom <= wallCollision.Top + velocity.Y))
                {
                    //character has landed
                    LandResponse(wallCollision);
                    return true;
                }
                else
                    return true;
            }
            //check for objects
            for (int i=0;i<entity.Count;i++ )
            {
                if(entity[i]!=this && entity[i].active == true && entity[i].canCollide==true && entity[i].CheckCollision(futureBoundingBox)==true)
                {
                    return true;
                }
            }
            return false;
        }
        public void LandResponse(Rectangle wallCollision)
        {
            position.Y = wallCollision.Top - (boundingHeight + boundingOffset.Y);
            velocity.Y = 0;
            isJumping = false;
        }
        //function from michael hick's toolbox
        protected Rectangle OnGround(Map map)
        {
            Rectangle futureBoundingBox = new Rectangle((int)(position.X + boundingOffset.X), (int)(position.Y + boundingOffset.Y + (velocity.Y + gravity)), boundingWidth, boundingHeight);

            return map.CheckCollision(futureBoundingBox);
        }
        //function from michael hick's toolbox
        protected float TendToZero(float val, float amount)
        {
            if (val > 0f && (val -= amount) < 0f) return 0f;
            if (val < 0f && (val += amount) > 0f) return 0f;
            return val;
        }
        private void ApplyGravity(Map map)
        {
            if(isJumping== true || OnGround(map) == Rectangle.Empty)
            {
                velocity.Y += gravity;
            }
            if(velocity.Y>maxFallVel)
            {
                //capping fall velocity
                velocity.Y = maxFallVel;
            }
        }
        private void UpdateMovement(List<GameEntity> entity,Map map)
        {
            if(velocity.X!=0 && CheckCollision(map,entity,true)==true)
            {
                velocity.X = 0;
            }
            position.X += velocity.X;
            if (velocity.Y != 0 && CheckCollision(map, entity, false) == true)
            {
                velocity.Y = 0;
            }
            position.Y += velocity.Y;
            if (applyGravity == true)
                ApplyGravity(map);
            velocity.X = TendToZero(velocity.X, deacc);
            if (applyGravity == false)
                velocity.Y = TendToZero(velocity.Y, deacc); 

        }
        protected void MoveRight()
        {
            velocity.X += accelerate + deacc;
            if (velocity.X > maxSpeed)
                velocity.X = maxSpeed;
            direction.X = 1;
        }
        protected void MoveLeft()
        {
            velocity.X -= accelerate + deacc;
            if (velocity.X < -maxSpeed)
                velocity.X = -maxSpeed;
            direction.X = -1;
        }
        protected void MoveUp()
        {
            velocity.Y -= accelerate + deacc;
            if (velocity.Y < -maxSpeed)
                velocity.Y = -maxSpeed;
            direction.X = -1;
        }
        protected void MoveDown()
        {
            velocity.Y += accelerate + deacc;
            if (velocity.Y > maxSpeed)
                velocity.Y = maxSpeed;
            direction.Y = 1;
        }
        protected bool Jump(Map map)
        {
            if (isJumping==true)
            {
                return false;
            }
            if(velocity.Y==0 && OnGround(map)!=Rectangle.Empty)
            {
                velocity.Y -= jumpVel;
                isJumping = true;
                return true;
            }
            return false;
        }
    }
    
}
