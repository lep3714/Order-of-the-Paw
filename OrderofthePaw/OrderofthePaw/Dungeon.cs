using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
//Handles the loading of dungeons from the files created by the dungeon maker.
namespace OrderofthePaw
{
    class Dungeon
    {
        //Fields

        const int ROOM_WIDTH = 800;
        const int ROOM_HEIGHT = 480;
        //const int ROOM_WIDTH = 100;
        //const int ROOM_HEIGHT = 100;
        const int ROOM_SIZE = 50;
        const int ORIG_LOCATION_X = 200;
        const int ORIG_LOCATION_Y = 100;
        string filePath; //Holds the path to this floors dungeon file.
        string[,] dnMap; //Holds the values of each dungeon room.
        ContentManager cm;
        Rectangle rect;
        Room[,] dArray; //Holds each room object.

        public Room[,] Rooms
        {
            get { return dArray; }
            set { dArray = value; }
        }

        public Rectangle Rectangle
        {
            get { return rect; }
            set { rect = value; }
        }
        //Constructor
        public Dungeon(string path, ContentManager c)
        {
            filePath = path;
            rect = new Rectangle(ORIG_LOCATION_X, ORIG_LOCATION_Y, ROOM_SIZE, ROOM_SIZE);
            cm = c;
        }

        //Methods

        //Draws the Dungeon floor file.
        public void Initialize()
        {
            try
            {
                Stream read = File.OpenRead(filePath);
                BinaryReader bi = new BinaryReader(read);
                int c = bi.ReadInt32();
                int r = bi.ReadInt32();
                dnMap = new string[r, c];
                dArray = new Room[r, c];

                for (int row = 0; row < dnMap.GetLength(1); row++)
                {
                    for (int col = 0; col < dnMap.GetLength(0); col++)
                    {
                        dArray[row, col] = new Room(bi.ReadString());
                        dArray[row, col].CreateRoom(cm, row, col);
                        //dArray[row, col].RoomRect = new Rectangle(col * ROOM_WIDTH, row * ROOM_HEIGHT, ROOM_WIDTH, ROOM_HEIGHT);//Used to prevent rooms from being drawn on top of eachother.
                    }
                }
                bi.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
      
        }

        //Resets the dungeons position to its initial values
        public void Reset()
        {
            for (int row = 0; row < dnMap.GetLength(1); row++)
            {
                for (int col = 0; col < dnMap.GetLength(0); col++)
                {
                    dArray[row, col].RoomRect = new Rectangle(col * ROOM_WIDTH, row * ROOM_HEIGHT, ROOM_WIDTH, ROOM_HEIGHT);//Used to prevent rooms from being drawn on top of eachother.
                }
            }
        }
        //Draws the dungeon in the background
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < dnMap.GetLength(1); row++)
            {
                //rect.Y = ORIG_LOCATION_Y + (row * 50);
                for (int col = 0; col < dnMap.GetLength(0); col++)
                {
                    dArray[row, col].Draw(spriteBatch);
                }
            }
            //rect.X = ORIG_LOCATION_X;
            //rect.Y = ORIG_LOCATION_Y;
        }
    }
}
