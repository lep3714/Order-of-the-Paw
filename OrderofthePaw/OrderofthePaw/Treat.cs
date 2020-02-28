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
    class Treat : Projectile
    {
        public Treat(Direction direction, Rectangle position, Texture2D texture) 
            : base(direction, position, texture)
        {
            this.sheetPos = new Rectangle(790, 60, 590, 293);
            this.speed = 1;
            this.isActive = true;
        }
    }
}
