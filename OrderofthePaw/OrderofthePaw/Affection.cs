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
    class Affection : Projectile
    {
        public Affection(Direction direction, Rectangle position, Texture2D texture) 
            :base(direction,position,texture)
        {
            this.sheetPos = new Rectangle(675, 565, 400, 300);
            this.speed = 1;
            this.isActive = true;
        }
    }
}
