using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ExternalTool
{
    public partial class Form1 : Form
    {

        //Tile Size
        private const int TILE_SIZE = 30;
        private int width;
        private int height;
        private PictureBox[,] tileMap;
        private MapEdit form2;
        private Dungeon_Editor dedit;
        BinaryReader binRead;



        public Form1()
        {
            InitializeComponent();

        }

        private void createMap_Click(object sender, EventArgs e)
        {
            int.TryParse(widthText.Text, out width);
            int.TryParse(heightText.Text, out height);
            tileMap = new PictureBox[height, width];


            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    tileMap[row, col] = new PictureBox();
                    tileMap[row, col].Size = new Size(TILE_SIZE, TILE_SIZE);
                }


            }
            form2 = new MapEdit(this, tileMap);


            form2.Text = "Editor";
            form2.Location = new Point(500, 500);
            form2.ShowDialog();

            //Print the picture box starting a x= 278. Approx y=9


        }

        //handles the logic of reading in a room file.
        private void loadMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();
            x.Filter = "Room File |*.rm";

            try
            {
                if (x.ShowDialog() == DialogResult.OK)
                {
                    Stream loader = File.OpenRead(x.FileName);
                    binRead = new BinaryReader(loader);
                    height = binRead.ReadInt32();
                    width = binRead.ReadInt32();

                    tileMap = new PictureBox[height, width];

                    for (int row = 0; row < tileMap.GetLength(0); row++)
                    {
                        for (int col = 0; col < tileMap.GetLength(1); col++)
                        {
                            tileMap[row, col] = new PictureBox();
                            tileMap[row, col].Size = new Size(TILE_SIZE, TILE_SIZE);
                        }
                    }




                    form2 = new MapEdit(this, binRead, tileMap);
                    form2.Text = "Level Editor - " + x.SafeFileName;
                    form2.Location = new Point(300, 300);
                    MessageBox.Show("File Loaded");
                    form2.ShowDialog();
                    binRead.Close();

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }



        }

        private void NewDungeon_Click(object sender, EventArgs e)
        {
            dedit = new Dungeon_Editor();
            dedit.Text= "Dungeon Editor";
            dedit.Width = 1280;
            dedit.Height = 720;
            dedit.ShowDialog();
        }

        private void Instructions_Click(object sender, EventArgs e)
        {
            Process.Start(@"..\..\..\..\README.txt");
        }
    }
}

