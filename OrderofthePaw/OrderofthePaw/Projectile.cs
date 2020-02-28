using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OrderofthePaw
{
    class Projectile : Entity
    {
        //Fields

        //Properties
        public Rectangle Position
        {
            get { return this.position; }
        }

        //Constructor
        public Projectile(Direction direction, Rectangle position, Texture2D texture)
        {
            this.isActive = true;
            this.direction = direction;
            this.position = position;
            this.texture = texture;
            this.speed = 1;
            this.sheetPos = new Rectangle();
        }

        //Methods
        
        //Move handled by the Entity parent

        //Draw the projectile in the viewport when called
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(this.position.X < graphics.GraphicsDevice.Viewport.Width && this.position.Y < graphics.GraphicsDevice.Viewport.Height && this.position.X > 0 && this.position.Y > 0)
            {
                spriteBatch.Draw(this.texture, this.position, this.sheetPos, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
        }

        //check to see if the projectile is colliding with anything
        public override bool CheckCollisions(Entity e)
        {
            return base.CheckCollisions(e);
        }
    }
}
