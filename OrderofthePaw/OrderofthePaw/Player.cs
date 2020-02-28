using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Editors: Ben Wetzel, Lauren Paige
/// Purpose: To track the player's movement, health, and other attributes specific to the player
/// Problems:
/// </summary>
namespace OrderofthePaw
{
    //enum to check the direction of the player
    enum Direction
    {
        FaceRight,
        FaceLeft,
        FaceForward,
        FaceBehind
    }

    class Player : Entity
    {
        ///Fields

        //track current keyboard state
        private KeyboardState kbState;
        private KeyboardState prevKBState;

        //gets the source position of the player sprites
        private Rectangle playerStandingRec;

        //gets the source position of the health sprites
        private Rectangle threeHealth;
        private Rectangle twoHealth;
        private Rectangle oneHealth;

        //gets the source position of the different ammos
        private Rectangle affectionAmmo;
        private Rectangle treatAmmo;

        private int health; //Tracks the player's health
        private int frame; //current animation frame
        private double timeCounter; //amt of time that has passed
        private double fps; //speed of animation
        private double timePerFrame; //amt of time per frame
        private const int WalkFrameCount = 3;
        int lastX;
        int lastY;

        private double spf;
        private int currentFrame;
        private int widthOfFrame;
        private int heightOfFrame;
        private double timer;

        //track which projectile is being used| True is affection, false is treat
        private bool currentProj;

        /// Properties
        
        //health property to be used in Game1 class
        public int Health
        {
            get { return this.health; }
        }

        public Rectangle Rect
        {
            get { return playerStandingRec; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }
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

        public Point LastPosition
        {
            get { return new Point(lastX, lastY); }
        }
        ///Constructor
        public Player(Rectangle pos, Texture2D texture, int speed)
        {
            this.position = pos;
            this.texture = texture;
            this.speed = speed;
            this.health = 3;
            this.isActive = true;
            this.currentProj = true;
            this.direction = Direction.FaceForward; // Sets the player's starting direction towards the camera
            lastX = this.X;
            lastY = this.Y;

            //initialize the different player sprites from the sprite sheet
            playerStandingRec = new Rectangle(98, 1153, 317, 848);

            //initialize the amount of health from the sprite sheet
            threeHealth = new Rectangle(78, 81, 500, 200);
            twoHealth = new Rectangle(80, 433, 320, 135);
            oneHealth = new Rectangle(80, 792, 140, 127);

            //initialize ammo from the sprite sheet
            affectionAmmo = new Rectangle(683, 566, 380, 290);
            treatAmmo = new Rectangle(793, 64, 575, 290);

            //animation variables
            spf = .25; //speed per frame
            currentFrame = 0;
            widthOfFrame = 425;
            heightOfFrame = 850;
            timer = 0;
    }

        ///Methods

        //This should draw the character in their current animation state, their health, and then the current weapons on the screen
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //add to the timer the elapsed game time
            timer += gameTime.ElapsedGameTime.TotalSeconds;

            //if the timer is greater than the speed per frame
            if(timer > spf)
            {
                //subtract the spf from the timer
                timer -= spf;
                currentFrame++; //increase amt of frames

                //if the current frame is greater than or equal to 3, cycle back to the beginning
                if(currentFrame >= 3)
                {
                    currentFrame = 0;
                }
            }

            //otherwise don't cycle
            if(direction == Direction.FaceBehind)
            {
                currentFrame = 0;
            }

            //create offsets and a variable to check if the animations need to be flipped
            int yOffset = 0;
            int xOffset = 0;
            SpriteEffects flip = SpriteEffects.None;

            //switch statement to determind offsets and flipping
            switch(direction)
            {
                case Direction.FaceForward:
                    xOffset = 0;
                    yOffset = 2088;
                    break;

                case Direction.FaceLeft:
                    xOffset = 0;
                    yOffset = 1256;
                    flip = SpriteEffects.FlipHorizontally;
                    break;

                case Direction.FaceRight:
                    xOffset = 0;
                    yOffset = 1256;
                    break;

                case Direction.FaceBehind:
                    xOffset = 1350;
                    yOffset = 1256;
                    break;
            }

            //draw the player animation, taking in certain overloads
            spriteBatch.Draw(texture, position, new Rectangle(widthOfFrame * currentFrame + xOffset, yOffset, widthOfFrame, heightOfFrame), Color.White, 0, Vector2.Zero, flip, 0);

            //switch statement to draw the current health, passing in the player's health
            switch(this.health)
            {
                //if the player has three health, draw three hearts
                case 3:
                    spriteBatch.Draw(texture, new Rectangle(0, 0, 130, 60), threeHealth, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;

                //if the player has two health, draw two hearts
                case 2:
                    spriteBatch.Draw(texture, new Rectangle(0, 0, 64, 32), twoHealth, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;

                //if the player has one health, draw one heart
                case 1:
                    spriteBatch.Draw(texture, new Rectangle(0, 0, 32, 32), oneHealth, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;

            }

            //if currentProj is true (affection) draw the affection bigger than the treat
            if (currentProj)
            {
                spriteBatch.Draw(texture, new Rectangle(660, 410, 65, 65), affectionAmmo, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(texture, new Rectangle(740, 430, 50, 45), treatAmmo, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }

            //otherwise if false (treat) draw the treat bigger
            else
            {
                spriteBatch.Draw(texture, new Rectangle(670, 430, 45, 45), affectionAmmo, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(texture, new Rectangle(730, 415, 65, 60), treatAmmo, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// This will compute the movement of the character.
        /// </summary>
        /// <param name="xDis">the change in x position</param>
        /// <param name="yDis">the change in y position</param>
        public void Move(KeyboardState currentKB, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            int xDisplace = 0;
            int yDisplace = 0;

            this.prevKBState = kbState;
            this.kbState = currentKB;

            //Track the overall player input to determine which direction they move.
            //This will allow for all the movement buttons to be pressed, and the 
            //player can still function.
            if(this.kbState.IsKeyDown(Keys.D))
            {
                xDisplace += this.speed;
            }
            if(this.kbState.IsKeyDown(Keys.S))
            {
                yDisplace += this.speed;
            }
            if (this.kbState.IsKeyDown(Keys.A))
            {
                xDisplace -= this.speed;
            }
            if (this.kbState.IsKeyDown(Keys.W))
            {
                yDisplace -= this.speed;
            }

            //Changes the direction to correctly represent the movement
            if(xDisplace > 0 && Math.Abs(xDisplace) > Math.Abs(yDisplace)) //the absolute value will allow the movement to determine a dominant direction for the player to face
            {                                                               //If the player moves at an angle, they will face the direction of the button pressed first.
                direction = Direction.FaceRight;                            //for example, if the player presses A and W, they will move to the top left, facing left.
            }                                                               //But if they press W and then A, they will move to the top left faceing up.
            else if(xDisplace < 0 && Math.Abs(xDisplace) > Math.Abs(yDisplace))
            {
                direction = Direction.FaceLeft;
            }
            else if(yDisplace > 0 && Math.Abs(yDisplace) > Math.Abs(xDisplace))
            {
                direction = Direction.FaceForward;
            }
            else if(yDisplace < 0 && Math.Abs(yDisplace) > Math.Abs(xDisplace))
            {
                direction = Direction.FaceBehind;
            }

            //if there were changes to the position, and the player is inside the window, create a new player rectangle to simulate movement
            if(yDisplace != 0 || xDisplace != 0) 
            {
                lastX = xDisplace;
                lastY = yDisplace;
                this.position = new Rectangle(this.position.X + xDisplace, this.position.Y + yDisplace, this.position.Width, this.position.Height);
            }

            //changes the currently launching projectile based on the move method's keyboard state
            if (kbState.IsKeyDown(Keys.RightShift) && !prevKBState.IsKeyDown(Keys.RightShift))
            {
                if (currentProj)
                {
                    currentProj = false; // makes the projectiles treats
                }
                else
                {
                    currentProj = true;// makes the projectiles hearts
                }
            }

        }

        /// <summary>
        /// Creates a new projectile if the arrow keys are pressed
        /// <summary>
        public Projectile Shoot()
        {
            //shoot the projectile in the correct direction
            if(this.kbState.IsKeyDown(Keys.Up))//&& !this.prevKBState.IsKeyDown(Keys.Up))
            {
                if(currentProj)
                {
                    return new Affection(Direction.FaceBehind, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
                else
                {
                    return new Treat(Direction.FaceBehind, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
            }
            else if (this.kbState.IsKeyDown(Keys.Down))
            {
                if (currentProj)
                {
                    return new Affection(Direction.FaceForward, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
                else
                {
                    return new Treat(Direction.FaceForward, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
            }
            else if (this.kbState.IsKeyDown(Keys.Right) )
            {
                if (currentProj)
                {
                    return new Affection(Direction.FaceRight, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
                else
                {
                    return new Treat(Direction.FaceRight, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
            }
            else if(this.kbState.IsKeyDown(Keys.Left) )
            {
                if (currentProj)
                {
                    return new Affection(Direction.FaceLeft, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
                else
                {
                    return new Treat(Direction.FaceLeft, new Rectangle(this.position.X, this.position.Y, 20, 20), this.texture);
                }
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Checks if the player is colliding with anything, to be implemented with the move method
        /// <summary>
        public override bool CheckCollisions(Entity e)
        {
            if(e is Dog && base.CheckCollisions(e))
            {
                this.health--;
            }

            return base.CheckCollisions(e);
        }

        public override void Reset()
        {
            this.health = 3;
            this.isActive = true;
            this.currentProj = true;
            this.direction = Direction.FaceForward;
        }

        //update animation as needed
        public void UpdateAnimation(GameTime gt)
        {
            //check if we have enough time to advance the frame
            timeCounter += gt.ElapsedGameTime.TotalSeconds;
            if(timeCounter >= timePerFrame)
            {
                frame += 1; //adjust the frame

                if(frame > WalkFrameCount) //check the bounds
                {
                    frame = 1;
                }

                timeCounter -= timePerFrame; //remove the time used
            }
        }

        public void ReverseDirection()
        {
            switch (this.direction)
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
    }
}
