using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace OrderofthePaw
{
    abstract class Entity
    {
        //Fields
        protected Rectangle position;
        protected Rectangle sheetPos;
        protected Texture2D texture;
        protected int speed;
        protected bool isActive;
        protected Direction direction;
        protected GraphicsDeviceManager graphics;

        public int X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public int Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        //Methods

        /// <summary>
        /// This will handle the movement of any object in the game that doesn't override it
        /// </summary>
        public virtual void Move(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            int yOffset = 0;
            int xOffset = 0;
            switch (direction)
            {
                case Direction.FaceBehind:
                    yOffset -= this.speed;
                    break;
                case Direction.FaceForward:
                    yOffset += this.speed;
                    break;
                case Direction.FaceLeft:
                    xOffset -= this.speed;
                    break;
                case Direction.FaceRight:
                    xOffset += this.speed;
                    break;
            }

            this.position = new Rectangle(this.position.X + xOffset, this.position.Y + yOffset, this.position.Height, this.position.Height);
        }

        //This will inherit and complete the draw for the entity
        // Lauren will work on this.
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Checks to see if there are any collisions within the 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool CheckCollisions(Entity e)
        {
            if (this.position.Intersects(e.position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Reset()
        {

        }
    }
}
