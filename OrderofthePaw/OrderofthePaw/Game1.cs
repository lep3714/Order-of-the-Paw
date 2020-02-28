using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
/// <summary>
/// Editors: Lauren Paige, Ben Wetzel
/// Purpose: Main class for functionality for our game.
/// Problems: Optimization is an issue and there are code snippets that can be simplified with variables to make readibility cleaner.
/// </summary>
namespace OrderofthePaw
{
    //This will be the Enum that lets us keep track of the game state.
    enum GameState
    {
        MainMenu, //goes to PlayGame, Instructions
        PauseMenu, //goes to PlayGame, MainMenu
        Instructions, //goes to MainMenu
        PlayGame, //goes to PauseMenu, WinScreen, GameOverScreen
        WinScreen, //goes to MainMenu
        GameOverScreen //goes to MainMenu
    };
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        ///Fields

                //Track visuals
            GraphicsDeviceManager graphics;
            SpriteBatch spriteBatch;
            Texture2D gameTextures; //Track the sprite sheet that contains all of the assets we will be using
            SpriteFont arial14;
            SpriteFont gameFont; //Track the font we will be using for the game
            Dictionary<Rectangle, Room> roomD;//Used for checking wether a player inhabits a room.

            //Track Objects
            Player player; //Create player object
            Dungeon testDungeon; //Used to test the dungeon map maker.
            EntityManager manager; //Keeps track of all of the various enemies and projectiles
            Stopwatch sw;
            Random rng;
            Room currentRoom;
            bool firstLoad;

                //establish global variables
            int difficulty;
            int cooldown; //Holds the cooldown for the player getting hurt.
            bool projFired;
            bool playerDamaged;
            bool newLevel;
            double deltaTimeProj;
            double lastDeltaProj;
            double deltaTimeDog;
            double lastDeltaDog;
            const int coolTime =  500; //Holds the cooldown between the players shots

            private Rectangle startingPlayerPos = new Rectangle(50, 50, 32, 64); //starting position for player

            //Track the controllers for the game in their current and previous states.
            private GameState gameState;
            private KeyboardState kbState;
            private KeyboardState prevKBState;
            private MouseState mState;
            private MouseState prevMState;

            //declare the different screens from the sprite sheet
            private Rectangle mainMenu = new Rectangle(4790, 0, 1928, 1080);
            private Rectangle pauseMenu = new Rectangle(4798, 1080, 1917, 1080);
            private Rectangle instructions = new Rectangle(2870, 0, 1917, 1080);
            private Rectangle winScreen = new Rectangle(2870, 1080, 1917, 1080);
            private Rectangle gameOverScreen = new Rectangle(2870, 0, 1917, 1080); //gameover screen and instructions screen currently the same

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef; //addresses bug
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            cooldown = 0;
            roomD = new Dictionary<Rectangle, Room>();
            firstLoad = true;
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            manager = new EntityManager();

            // TODO: use this.Content to load your game content here
            //Content for the dungeon graphics
            testDungeon = new Dungeon(@"..\..\..\..\Content\Dungeon\dungeon.dn", Content);
            testDungeon.Initialize();

            foreach (Room r in testDungeon.Rooms)
            {
                if(r.RoomRect != null)
                roomD.Add(r.RoomRect, r);
            }

            
            ///
            ///Add the spriteSheets for all of the textures
            gameTextures = Content.Load<Texture2D>("Ultimate Spritesheet");

            ///add the texture fonts for Germanica (currently Arial until we can get it working), the font that we will be using
            gameFont = Content.Load<SpriteFont>("arial");
            arial14 = Content.Load<SpriteFont>("arial14");

            ///Set the starting Game State to main Menu
            gameState = GameState.MainMenu;

            ///Instantiate the player object
            player = new Player(startingPlayerPos, gameTextures, 3);

            //create random object to handle any randomness in the game
            rng = new Random();

            //Establishes the game difficulty at the start. This can be changed manually in the menu and will automatically increment 
            //when a boss enemy is defeated
            difficulty = 1;

            newLevel = true;
            projFired = false;
            playerDamaged = false;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevKBState = kbState;
            kbState = Keyboard.GetState();
            prevMState = mState;
            mState = Mouse.GetState();

            ///Update depending on the current state
            switch(gameState)
            {
                
                ///If the state is Main Menu
                case GameState.MainMenu:

                    //if the user clicks the play game button, switch to playing the game
                    if (mState.LeftButton == ButtonState.Pressed && mState.X > 280 && mState.X < 726 && mState.Y > 260 && mState.Y < 335 && mState != prevMState)
                    {
                        Reset();
                        gameState = GameState.PlayGame;
                    }

                    //if the user clicks the instructions button, switch to the instructions menu
                    if (mState.LeftButton == ButtonState.Pressed && mState.X > 280 && mState.X < 726 && mState.Y > 310 && mState.Y < 385 && mState != prevMState)
                    {
                        gameState = GameState.Instructions;
                    }

                    //if the user clicks the quit button, exit program
                    if(mState.LeftButton == ButtonState.Pressed && mState.X > 280 && mState.X < 726 && mState.Y > 360 && mState.Y < 435 && mState != prevMState)
                    {
                        Exit();
                    }
                    break;

                ///If the state is Play
                case GameState.PlayGame:


                    cooldown = 200;
                    lastDeltaProj = deltaTimeProj;
                    deltaTimeProj = (double)gameTime.ElapsedGameTime.TotalMilliseconds; // gets the elapsed time at the begining of the update for the Play Game state used for the projectile
                    lastDeltaDog = deltaTimeDog;
                    deltaTimeDog = (double)gameTime.ElapsedGameTime.TotalMilliseconds;

                    //accumulate the timer for the Player's firing cooldown
                    if (deltaTimeProj + lastDeltaProj <= cooldown+20 && projFired)
                    {
                        deltaTimeProj += lastDeltaProj;
                    }
                    else
                    {
                        deltaTimeProj = 0;
                    }

                    //accumulate the timer for the player's damage cooldown
                    if (deltaTimeDog + lastDeltaDog <= cooldown + 20 && playerDamaged)
                    {            
                        deltaTimeDog += lastDeltaDog;
                    }
                    else
                    {
                        deltaTimeDog = 0;
                    }

                    //If the level hasn't been loaded yet, load the level
                    if (newLevel)
                    {
                        LoadLevel(difficulty);
                        currentRoom = OnRoomSwitch();
                        newLevel = false;
                    }

                    //If the player is walking into a wall, stop them from moving that direction.
                    //for each of the wall rectangles present in the room, check for a collision - ***will need to be able to access the Dungeon's current rooms and the walls of those rooms***
                    //handle collisions in the player object
                    Point lastPosition;
                    for(int y = 0; y < currentRoom.Map.GetLength(0); y++)
                    {
                        for(int x = 0; x < currentRoom.Map.GetLength(1); x++)
                        {
                            if (player.CheckCollisions(currentRoom.Map[x,y].Collision))
                            {
                                player.X = player.X - player.LastPosition.X;
                                player.Y = player.Y - player.LastPosition.Y;
                            }
                        }
                    }

                    
                    player.Move(kbState,graphics);
                    

                    //allows the player to shoot and also adds any of the projectiles to the manager 
                    
                    if(projFired && deltaTimeProj > cooldown)
                    {
                        projFired = false;
                    }
                    else if(!projFired)
                    {
                        manager.AddEntity(player.Shoot());
                        projFired = true;
                        deltaTimeProj = 0;
                    }
                    
                    
                    //Attempting to handle player collision with the dogs
                    foreach(Dog corgi in manager.CycleByType(""))
                    {
                        if (deltaTimeDog > cooldown && playerDamaged)
                        {
                            playerDamaged = false;                                                           
                        }
                        else if (!playerDamaged)
                        {
                            if(player.CheckCollisions(corgi))
                            {
                                deltaTimeDog = 0;
                                playerDamaged = true;
                            }
                        }
                    }

                    //handle dog collision with projectiles currently on the screen
                    //Checks each existing projectile with each existing dog
                    foreach (Projectile newProj in manager.CycleByType())
                    {
                        if (newProj != null)
                        {
                            foreach (Dog corgi in manager.CycleByType(""))
                            {
                                if(corgi.CheckCollisions(newProj))
                                {
                                    manager.RemoveEntity(corgi);
                                    manager.RemoveEntity(newProj);
                                }
                            }
                        }
                    }      
                    manager.MoveEntities(graphics);
                    
                    //if the player hits the p key go to the pause menu
                    if(kbState.IsKeyDown(Keys.P))
                    {
                        gameState = GameState.PauseMenu;
                    }

                    //if there are no more dogs left in the list, then the player wins and gamestate switches to the win screen
                    if(manager.CycleByType("").Count == 0)
                    {
                        gameState = GameState.WinScreen;
                    }
                    
                    // Ends the game if the player has no health
                    if(player.Health < 0)
                    {
                        gameState = GameState.GameOverScreen;
                    }

                    //Y bounds: 0, 476
                    //XBounds Left: -12, Right: 792.
                    if(player.Position.X > 792)
                    {
                        Camera.Right(testDungeon, manager);
                        player.X = -12;
                        currentRoom = OnRoomSwitch();
                   
                    }
                    if(player.Position.X < -12)
                    {
                        Camera.Left(testDungeon, manager);
                        player.X = 792;
                        currentRoom = OnRoomSwitch();
                    }
                    if(player.Position.Y > 476)
                    {
                        Camera.Down(testDungeon, manager);
                        player.Y = -12;
                        currentRoom = OnRoomSwitch();
                    }
                    if(player.Position.Y < -12)
                    {
                        Camera.Up(testDungeon, manager);
                        player.Y = 476;
                        currentRoom = OnRoomSwitch();
                    }

                    break;

                //If the state is Pause Menu
                case GameState.PauseMenu:

                    //if the user clicks the play game button, go back to the game
                    if (mState.LeftButton == ButtonState.Pressed && mState.X > 250 && mState.X < 696 && mState.Y > 260 && mState.Y < 335 && mState != prevMState)
                    {
                        gameState = GameState.PlayGame;

                    }

                    //if the user clicks the main menu button, go to the main menu
                    if (mState.LeftButton == ButtonState.Pressed && mState.X > 250 && mState.X < 696 && mState.Y > 310 && mState.Y < 385 && mState != prevMState)
                    {
                        gameState = GameState.MainMenu;
                    }

                    break;

                ///If in the Instructions menu
                case GameState.Instructions:

                    //if the user clicks the main menu button, go to the main menu
                    if(mState.LeftButton == ButtonState.Pressed && mState.X > 260 && mState.X < 706 && mState.Y > 370 && mState.Y < 445 && mState != prevMState)
                    {
                        gameState = GameState.MainMenu;
                    }

                    break;

                //If the player loses
                case GameState.GameOverScreen:

                    //if the user clicks the main menu button, go to the main menu
                    if (mState.LeftButton == ButtonState.Pressed && mState.X > 260 && mState.X < 706 && mState.Y > 310 && mState.Y < 385 && mState != prevMState)
                    {
                        gameState = GameState.MainMenu;
                        player = new Player(startingPlayerPos, gameTextures, 2); //reset player position
                        testDungeon.Reset(); //Resets dungeon position
                    }

                    break;

                //If the player wins
                case GameState.WinScreen:

                    //if the user clicks the main menu button, go to the main menu
                    if (mState.LeftButton == ButtonState.Pressed && mState.X > 260 && mState.X < 706 && mState.Y > 310 && mState.Y < 385 && mState != prevMState)
                    {
                        gameState = GameState.MainMenu;
                    }
                    break;
                    
            }
            base.Update(gameTime);

            prevMState = mState; //update the mouse state
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Olive);

            spriteBatch.Begin(); //start drawing

            prevMState = mState;
            mState = Mouse.GetState();
            int x = mState.X;
            int y = mState.Y;

            ///Draw the current State
            switch (gameState)
            {
                ///If the state is Main Menu
                case GameState.MainMenu:

                    //draw the title and the background
                    spriteBatch.Draw(gameTextures, new Rectangle(0,0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), mainMenu, Color.White);
                    spriteBatch.DrawString(gameFont, "Order of the Paw", new Vector2(335, 190), Color.Black);

                    //draw the play button
                    spriteBatch.Draw(gameTextures, new Rectangle(280, 260, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White);
                    spriteBatch.DrawString(gameFont, "Play Game", new Vector2(370, 270), Color.AntiqueWhite);

                    //if the mouse position is within the button, draw the hovered effect
                    if (x >= 280 && x <= 530 && y >= 260 && y <= 300)
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(280, 260, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Play Game", new Vector2(370, 270), Color.AntiqueWhite);
                    }

                    //draw the instructions button
                    spriteBatch.Draw(gameTextures, new Rectangle(280, 310, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White);
                    spriteBatch.DrawString(gameFont, "Instructions", new Vector2(370, 320), Color.AntiqueWhite);

                    //if the mouse position is within the button, draw the hovered effect
                    if (x >= 280 && x <= 530 && y >= 310 && y <= 350)
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(280, 310, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Instructions", new Vector2(370, 320), Color.AntiqueWhite);
                    }

                    //draw the exit button
                    spriteBatch.Draw(gameTextures, new Rectangle(280, 360, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White);
                    spriteBatch.DrawString(gameFont, "Exit", new Vector2(395, 370), Color.AntiqueWhite);

                    //if the mouse position is within the button, draw the hovered effect
                    if (x >= 280 && x <= 530 && y >= 360 && y <= 400)
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(280, 360, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Exit", new Vector2(395, 370), Color.AntiqueWhite);
                    }

                    break;

                ///If the state is Play
                case GameState.PlayGame:
                    
                    testDungeon.Draw(spriteBatch);//Draws the entire dungeon.
                    player.Draw(spriteBatch, gameTime);
                    manager.DrawEntities(spriteBatch, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                    break;

                ///If the state is Pause Menu
                case GameState.PauseMenu:

                    spriteBatch.Draw(gameTextures, new Rectangle(0,0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), pauseMenu, Color.White); //draw the background
                    spriteBatch.DrawString(arial14, "Paused", new Vector2(343, 190), Color.Black);

                    //draw the play game button
                    spriteBatch.Draw(gameTextures, new Rectangle(250, 260, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White);
                    spriteBatch.DrawString(gameFont, "Play Game", new Vector2(340, 270), Color.AntiqueWhite);

                    //if the mouse position is within the button, draw the hovered effect
                    if (x >= 250 && x <= 500 && y >= 260 && y <= 300)
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(250, 260, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Play Game", new Vector2(340, 270), Color.AntiqueWhite);
                    }

                    //draw the main menu button
                    spriteBatch.Draw(gameTextures, new Rectangle(250, 310, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White);
                    spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(340, 320), Color.AntiqueWhite);

                    //if the mouse position is within the button, draw the hovered effect
                    if (x >= 250 && x <= 500 && y >= 310 && y <= 350)
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(250, 310, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(340, 320), Color.AntiqueWhite);
                    }

                    break;

                ///If in the Instructions menu
                case GameState.Instructions:

                    spriteBatch.Draw(gameTextures, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), instructions, Color.White); //draw the background

                    spriteBatch.DrawString(arial14, "Instructions", new Vector2(343, 100), Color.AntiqueWhite);
                    spriteBatch.DrawString(gameFont, "Give the minions in the dungeon affection or treats to move on to face the boss.", new Vector2(140, 130), Color.Black);

                    spriteBatch.DrawString(arial14, "Controls", new Vector2(355, 180), Color.AntiqueWhite);
                    spriteBatch.DrawString(gameFont, "W - Move up", new Vector2(340, 210), Color.Black);
                    spriteBatch.DrawString(gameFont, "A - Move left", new Vector2(340, 230), Color.Black);
                    spriteBatch.DrawString(gameFont, "S - Move down", new Vector2(340, 250), Color.Black);
                    spriteBatch.DrawString(gameFont, "D - Move right", new Vector2(340, 270), Color.Black);
                    spriteBatch.DrawString(gameFont, "P - Pause game", new Vector2(340, 290), Color.Black);
                    spriteBatch.DrawString(gameFont, "Arrow Keys - Shoot", new Vector2(340, 310), Color.Black);
                    spriteBatch.DrawString(gameFont, "Shift - Switch weapons", new Vector2(340, 330), Color.Black);

                    spriteBatch.Draw(gameTextures, new Rectangle(260, 390, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White); //draw main menu button
                    spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(350, 400), Color.AntiqueWhite);
                    
                    if (x >= 260 && x <= 510 && y >= 390 && y <= 440) //if the mouse position is within the button, draw the hovered effect
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(260, 390, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(350, 400), Color.AntiqueWhite);
                    }

                    break;

                //If the player loses
                case GameState.GameOverScreen:

                    spriteBatch.Draw(gameTextures, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), gameOverScreen, Color.White); //draw background
                    spriteBatch.DrawString(arial14, "Game Over", new Vector2(340, 190), Color.Black);
                    spriteBatch.DrawString(gameFont, "You will be remembered... maybe.", new Vector2(270, 220), Color.Black);
                    
                    spriteBatch.Draw(gameTextures, new Rectangle(260, 310, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White); //draw main menu button
                    spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(350, 320), Color.AntiqueWhite);

                    if (x >= 260 && x <= 510 && y >= 310 && y <= 350) //if the mouse position is within the button, draw the hovered effect
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(260, 310, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(350, 320), Color.AntiqueWhite);
                    }

                    break;

                //If the player wins
                case GameState.WinScreen:

                    spriteBatch.Draw(gameTextures, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), winScreen, Color.White); //draw background
                    spriteBatch.DrawString(arial14, "The Order of the Paw welcomes you. Congratulations!", new Vector2(150, 190), Color.Black);

                    //draw main menu button
                    spriteBatch.Draw(gameTextures, new Rectangle(260, 310, 250, 40), new Rectangle(2158, 1351, 446, 75), Color.White);
                    spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(350, 320), Color.AntiqueWhite);

                    //if the mouse position is within the button, draw the hovered effect
                    if (x >= 260 && x <= 510 && y >= 310 && y <= 350)
                    {
                        spriteBatch.Draw(gameTextures, new Rectangle(260, 310, 250, 40), new Rectangle(2158, 1574, 446, 75), Color.White);
                        spriteBatch.DrawString(gameFont, "Main Menu", new Vector2(350, 320), Color.AntiqueWhite);
                    }

                    break;
            }

            spriteBatch.End(); //stop drawing

            base.Draw(gameTime);
        }

        /// <summary>
        /// This will add a random number of enemies to the current room at random positions
        /// </summary>
        protected void LoadLevel(int difficult)
        {
            //randomly determines how many enemies will be generated when the level loads.
            int enemies = difficult * rng.Next(3,5);

            //Stores the desired size of the enemy sprite
            int size;

            List<Room> temp = new List<Room>();//used for randomly adding enemies to the 
            foreach(var v in roomD.Values)
            {
                temp.Add(v);
            }
            //Adds a number of enemies at the correct level of difficulty depending on the passed int value.
            switch(difficult)
            {
                case 1:
                    size = 50;
                    while (temp.Count != 0)
                    {
                        int currentSpawn = rng.Next(temp.Count);//A random index of the rooms array. 

                        for (int i = 0; i < enemies; i++)
                        {
                            
                            manager.AddEntity(
                               new Dog(
                                   gameTextures,
                                   new Rectangle(
                                        rng.Next(temp[currentSpawn].RoomRect.X, temp[currentSpawn].RoomRect.Width + temp[currentSpawn].RoomRect.X), //Randomly generates a x-position within the walls of the room
                                        rng.Next(temp[currentSpawn].RoomRect.Y, temp[currentSpawn].RoomRect.Width + temp[currentSpawn].RoomRect.Y),//randomly generates a y-position within the walls of the room
                                        size, size), //the size of the dog's sprite on the screen
                                   DogType.Small,
                                   rng)
                               );
                        }

                        temp.RemoveAt(currentSpawn);
                    }
                    break;
                case 2:
                    size = 75;
                    for(int i = 0; i < enemies; i++){
                       manager.AddEntity(
                           new Dog(
                               gameTextures, 
                               new Rectangle(
                                   rng.Next(size, graphics.GraphicsDevice.Viewport.Width - size), //Randomly generates a x-position within the walls of the dungeon
                                   rng.Next(size, graphics.GraphicsDevice.Viewport.Height - size),//randomly generates a y-position within the walls of the dungeon
                                   size,size), //the size of the dog's sprite on the screen
                               DogType.Medium, 
                               rng)
                           );
                    }
                    break;
                case 3:
                    size = 100;
                    for(int i = 0; i < enemies; i++){
                       manager.AddEntity(
                           new Dog(
                               gameTextures, 
                               new Rectangle(
                                   rng.Next(size, graphics.GraphicsDevice.Viewport.Width - size), //Randomly generates a x-position within the walls of the dungeon
                                   rng.Next(size, graphics.GraphicsDevice.Viewport.Height - size),//randomly generates a y-position within the walls of the dungeon
                                   size,size), //the size of the dog's sprite on the screen
                               DogType.Large, 
                               rng)
                           );
                    }
                    break;
            }

        }

        //resets the gameState's variables
        protected void Reset()
        {
            difficulty = 1;
            player.Reset();
            manager.Reset();
            newLevel = true;
        }

        private Room OnRoomSwitch()
        {
            foreach(Rectangle rect in roomD.Keys)
            {
                if (rect.Intersects(player.Position)) 
                {
                    return roomD[rect];
                }
            }
            return null;
        }

     
    }
}
