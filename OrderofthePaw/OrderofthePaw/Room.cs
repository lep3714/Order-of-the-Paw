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

//Tyler Lynch
//This static class handles room reading and drawing. used in the dungeon class.
namespace OrderofthePaw
{
    class Room
    {

        const int ROOM_WIDTH = 800;
        const int ROOM_HEIGHT = 480;
        //const int ROOM_WIDTH = 100;
        //const int ROOM_HEIGHT = 100;
        const int T2D_WIDTH = 80;
        const int T2D_HEIGHT = 48;
        //const int T2D_WIDTH = 10;
        //const int T2D_HEIGHT = 10;
        public string file;
        Tile[,] tMap;
        Rectangle roomRect;

        public Rectangle RoomRect
        {
            get { return roomRect; }
            set { roomRect = value; }
            
        }
        public int X
        {
            get { return roomRect.X; }
            set { roomRect.X = value; }
        }

        public int Y
        {
            get { return roomRect.Y; }
            set { roomRect.Y = value; }
        }
        public Room(string f)
        {
            file = f;
        }

        public Tile[,] Map
        {
            get { return tMap; }
        }

        public void CreateRoom(ContentManager Content, int dRow, int dCol)
        {
            if (file != "0")
            {
                try
                {
                    
                    Stream stream = File.OpenRead(@"..\..\..\..\Content\rooms\" + file);
                    BinaryReader reader = new BinaryReader(stream);
                    int cols = reader.ReadInt32();
                    int rows = reader.ReadInt32();
                    roomRect = new Rectangle(dCol * ROOM_WIDTH, dRow *ROOM_HEIGHT, T2D_WIDTH * cols, T2D_HEIGHT * rows);
                    tMap = new Tile[rows, cols];
                    

                    for (int r = 0; r < rows; r++)
                    {
                        for(int c = 0; c < cols; c++)
                        {
                            string temp = reader.ReadString();//Reads in the name of the texture from a file.
                            Texture2D temp2 = Content.Load<Texture2D>(temp);//Loads the texture from the content pipeline.
                            tMap[r, c] = new Tile(temp2);
                            tMap[r, c].Rect = new Rectangle(roomRect.Location.X + (c * T2D_WIDTH), roomRect.Location.Y + (r * T2D_HEIGHT),T2D_WIDTH, T2D_HEIGHT);
                            UpdateTiles(r, c);
                            
                        }
                    }

                    reader.Close();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (file != "0")
            {
                for(int row = 0; row < tMap.GetLength(1); row++)
                {
                    for(int col = 0; col < tMap.GetLength(0); col++)
                    {
                        UpdateTiles(row, col);
                        sb.Draw(tMap[row, col].Texture, tMap[row, col].Rect, Color.White);
                    }
                    
                }
            }
            
        }

        //Updates the tiles to make sure that there position changes when moving through the dungeon.
        public void UpdateTiles(int thisRow, int thisCol)
        {
            tMap[thisRow, thisCol].Rect = new Rectangle(roomRect.Location.X + (thisCol * T2D_WIDTH), roomRect.Location.Y + (thisRow * T2D_HEIGHT), T2D_WIDTH, T2D_HEIGHT);
            GenerateRectangle(tMap[thisRow, thisCol]);
        }

        //Size of top and bottom walls: 80 x 15
        //Generates a rectangle based on the Tiles texture.
        public static void GenerateRectangle(Tile t)
        {
            if(t != null)
            {

            

            if(t.TileType == TileType.BottomWall)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location.X, t.Rect.Location.Y + 33, 80, 15);
            }
            else if(t.TileType == TileType.TopWall)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location, new Point(80, 15));
            }
            else if(t.TileType == TileType.RightWall)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location.X + 64, t.Rect.Location.Y,16, 48);
            }
            else if (t.TileType == TileType.LeftWall)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location, new Point(16, 48));
            }
            else if (t.TileType == TileType.BLCorner)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location.X, t.Rect.Location.Y + 33, 80, 15);
                t.Collision2.Position = new Rectangle(t.Rect.Location, new Point(16, 48));
            }
            else if(t.TileType == TileType.BRCorner)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location.X, t.Rect.Location.Y + 33, 80, 15);
                t.Collision2.Position = new Rectangle(t.Rect.Location.X + 64, t.Rect.Location.Y, 16, 48);
            }
            else if(t.TileType == TileType.TRCorner)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location, new Point(80, 15));
                t.Collision2.Position = new Rectangle(t.Rect.Location.X + 64, t.Rect.Location.Y, 16, 48);
            }
            else if(t.TileType == TileType.TLCorner)
            {
                t.Collision.Position = new Rectangle(t.Rect.Location, new Point(80, 15));
                t.Collision2.Position = new Rectangle(t.Rect.Location, new Point(16, 48));

            }
            }
        }
    }
}
