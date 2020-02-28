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
    enum DogType//Enum currently doesnt do anything aside from determining health. Later, this will determine what texture the enemy uses automatically.
    {
        Small,
        Medium,
        Large
    }
    class Dog : Entity
    {
        ///I just went through and switched some of the fields the class was creating to their inherited counterparts - Ben
        //Fields
        private DogType enemysize;
        private int health;       
        private bool isHurt;//Used for drawing if the dog gets hit by a projectile.
        private int width;
        private Random rng;

        //Constants
        //Store the various textures for the different kinds of dogs
        private const int spriteHeight = 780;
        protected const int corgiLocX = 1770;
        private const int corgiLocY = 1775;
        private const int pomLocX = 2670;
        private const int pomLocY = 2270;

        //sheetLocX and Y are not actual variables yet, but will be entered once we have a firm location of the sprites on the sheet
        private Rectangle smallDogBasePos1 = new Rectangle(corgiLocX, corgiLocY, spriteHeight,spriteHeight);
        private Rectangle smallDogBasePos2 = new Rectangle(pomLocX, pomLocY, spriteHeight,spriteHeight);

        //Properties
        public bool Alive
        {
            get { return this.isActive; }
        }

        public int Width 
        {
            get{return this.width;}
        }

        //Constructor
        public Dog(Texture2D dgT, Rectangle pos, DogType size, Random rng)
        {
            this.texture = dgT;
            this.position = pos;
            this.rng = rng;

            this.enemysize = size;
            this.isActive = true;
            this.isHurt = false;

            //As dogs get larger, they get slower, but have more initial health.
            if (enemysize == DogType.Small)
            {
                this.health = 5;
                this.speed = 3;
                this.width = 50;
                switch(this.rng.Next(0,2))
                {
                    case 0:
                        this.sheetPos = this.smallDogBasePos2;
                        break;
                    case 1:
                        this.sheetPos = this.smallDogBasePos1;
                        break;
                }
            }
            if(enemysize == DogType.Medium)
            {
                this.health = 10;
                this.speed = 3;
                this.width = 75;

                /*switch(this.rng.Next(0,2))
                {
                    case 0:
                        this.sheetPos = this.mediumDogBasePos2;
                        break;
                    case 1:
                        this.sheetPos = this.mediumDogBasePos1;
                        break;
                }*/
            }
            if(enemysize == DogType.Large)
            {
                this.health = 15;
                this.speed = 2;
                this.width = 100;

                /*switch(this.rng.Next(0,2))
                {
                    case 0:
                        this.sheetPos = this.largeDogBasePos2;
                        break;
                    case 1:
                        this.sheetPos = this.largeDogBasePos1;
                        break;
                }*/
            }
        }

        ///Methods

        //Draw method. 
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive && !isHurt)
            {
                spriteBatch.Draw(this.texture, this.position, this.sheetPos, Color.White);
            }
            else if(isActive && isHurt)
            {
                this.sheetPos = new Rectangle(this.sheetPos.X + this.sheetPos.Width, this.sheetPos.Y, this.sheetPos.Width, this.sheetPos.Height);//offsets the sprite anchor by the width to grab the next one over (the damaged image)
                spriteBatch.Draw(this.texture, this.position, this.sheetPos, Color.White);
                this.sheetPos = new Rectangle(this.sheetPos.X - this.sheetPos.Width, this.sheetPos.Y, this.sheetPos.Width, this.sheetPos.Height);//Sets the picture back
            }
            else
            {
                return;
            }
            
        }

        //for now, this moves the dog in a straight line and shecks if it still has health left.
        public override void Move(GraphicsDeviceManager graphics)
        {
            //If the dog moves off the screen
            if(this.position.X > graphics.GraphicsDevice.Viewport.Width || this.position.Y > graphics.GraphicsDevice.Viewport.Height || this.position.X < 0 || this.position.Y < 0)
            {
                //reverses the dog direction
                switch(this.direction)
                {
                    case Direction.FaceBehind:
                        this.direction = Direction.FaceForward;
                        break;
                    case Direction.FaceForward:
                        this.direction = Direction.FaceBehind;
                        break;
                    case Direction.FaceLeft:
                        this.direction = Direction.FaceRight;
                        break;
                    case Direction.FaceRight:
                        this.direction = Direction.FaceLeft;
                        break;
                }
            }
            
            base.Move(graphics);

            if(this.health <= 0)
            {
                this.isActive = false;
            }
        }

        //Checks for collison with other entities.
        public override bool CheckCollisions(Entity e)
        {
            if (base.CheckCollisions(e) && e.GetType().Equals(typeof(Projectile)))
            {
                this.health --;
                return true;
            }
            else
            {
                return base.CheckCollisions(e);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public void SwitchRoom(Rectangle r)
        {

        }
    }
}
