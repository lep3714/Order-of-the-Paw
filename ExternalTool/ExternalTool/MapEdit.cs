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
using System.Windows.Input;
using System.Resources;
using System.Collections;

//Tyler Lynch
//Main logic for the map editor. Creates the maps and saves/reads maps.
namespace ExternalTool
{
    public partial class MapEdit : Form
    {
        #region CONSTANTS
        //Tile Size
        private const int TILE_SIZE = 800;
        #endregion

        private Form1 first;
        private Image holder;
        private const string resX = @"..\..\Resource1.resx";
        private PictureBox[,] saveBox; //Picture Box used in the save method;
        BinaryReader forLoading; //Gets the binary reader from the first form and loads it.
        string imgLoc; //Holds the name of the image w/o extension. This is used to change each of the SaveBox's Picturboxes.Tag tag to its name for writing to the save file.

        #region properties
        public PictureBox[,] SaveBox
        {
            get { return saveBox; }
            set { saveBox = value; }
        }
        #endregion



        #region constructors

        public MapEdit(Form1 f, PictureBox[,] pb)
        {
            InitializeComponent();
            first = f;
            SaveBox = pb;
            CreateTileBox(saveBox);
            saveButton.Click += saveButton_Click;
            ResXResourceSet resSet = new ResXResourceSet(resX);
            //Adds all the textures to the list.
            foreach (DictionaryEntry d in resSet)
            {
                pictureList.Items.Add(d.Key.ToString());
            }

        }

        //Used for when the map is loaded instead of made new/
        public MapEdit(Form1 f, BinaryReader fi, PictureBox[,] pb)
        {
            InitializeComponent();
            forLoading = fi;
            SaveBox = pb;
            CreateTileBox(fi);
            saveButton.Click += saveButton_Click;
            forLoading.Close();
            fi.Close();
            ResXResourceSet resSet = new ResXResourceSet(resX);
            pictureList.Items.Add("Balcony_9");
            //Adds all the textures to the list.
            foreach (DictionaryEntry d in resSet)
            {
                pictureList.Items.Add(d.Key.ToString());
            }
        }

        

        #endregion

        #region events

        public void PaintBrushEdit(object sender, EventArgs e)
        {
            PictureBox temp = (PictureBox)sender;
            holder = temp.Image;
            currentImg.Image = holder;
        }
        //Handles changing the file by the name in the list box
        private void PictureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ResXResourceSet resSet = new ResXResourceSet(resX);
                ListBox temp = (ListBox)sender;
                string temp2 = (string)temp.SelectedItem;
                holder = (System.Drawing.Image)resSet.GetObject(temp2);
                currentImg.Image = holder;
                imgLoc = temp2;
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        #region tile editing
        private void MapEdit_MouseClick(object sender, MouseEventArgs e)
        {
            //"Erase" tiles with right click.
            if (MouseButtons == MouseButtons.Right)
            {
                PictureBox orig = (PictureBox)sender;
                orig.Tag = "blackpaint";
                orig.Capture = false;
                ResXResourceSet resSet = new ResXResourceSet(resX);
                orig.Image = (System.Drawing.Image)resSet.GetObject("blackpaint");
            }
            else if(MouseButtons == MouseButtons.Left)
            {
                PictureBox orig = (PictureBox)sender;
                orig.Tag = imgLoc;
                orig.Capture = false;
                orig.Image = holder;
            } 
        }

        //Logic for clicking and dragging to paint.
        private void MapEdit_MouseDrag(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                PictureBox orig = (PictureBox)sender;
                orig.Image = holder;
                orig.Tag = imgLoc;
            }
            else if (MouseButtons == MouseButtons.Right)
            {
                PictureBox orig = (PictureBox)sender;
                ResXResourceSet resSet = new ResXResourceSet(resX);
                orig.Image = (System.Drawing.Image)resSet.GetObject("blackpaint");
                orig.Tag = imgLoc;

            }
        }
        #endregion

        #region save
        //Logic for the saving of the tile map
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Room File |*.rm";



            try
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    Stream write = File.OpenWrite(save.FileName);
                    BinaryWriter binWrite = new BinaryWriter(write);
                    binWrite.Write(SaveBox.GetLength(0));
                    binWrite.Write(SaveBox.GetLength(1));

                    
                    for (int row = 0; row < SaveBox.GetLength(0); row++)
                    {
                        for (int col = 0; col < SaveBox.GetLength(1); col++)
                        {
                            binWrite.Write(Path.GetFileNameWithoutExtension((string)SaveBox[row, col].Tag));
                        }
                    }
                    binWrite.Close();
                    MessageBox.Show("File Saved");
                }
            }
            catch (Exception errrs)
            {
                MessageBox.Show(errrs.Message);
            }

        }
       
        #endregion
        #endregion
        //

        #region methods
        //Draws the array of pictureboxes in the desired place
        private void CreateTileBox(PictureBox[,] pb)
        {
            ResXResourceSet resSet = new ResXResourceSet(resX);
            for (int row = 0; row < SaveBox.GetLength(0); row++)
            {
                for (int col = 0; col < SaveBox.GetLength(1); col++)
                {
                    SaveBox[row, col].Size = new Size(TILE_SIZE / (SaveBox.GetLength(1)/2), TILE_SIZE / (SaveBox.GetLength(1)/2));
                    if(SaveBox[row, col].Size.Width > 80)
                    {
                        SaveBox[row, col].Size = new Size(50, 50);
                    }
                    SaveBox[row, col].Location = new Point(279 + (SaveBox[row,col].Size.Width * col), 9 + (SaveBox[row, col].Size.Height * row));
                    SaveBox[row, col].Image = (System.Drawing.Image)resSet.GetObject("blackpaint");//makes the beginning canvas black
                    SaveBox[row, col].Tag = "blackpaint";
                    SaveBox[row, col].SizeMode = PictureBoxSizeMode.StretchImage;
                    Controls.Add(SaveBox[row, col]);

                    SaveBox[row, col].MouseDown += delegate(object sender, MouseEventArgs e){ MapEdit_MouseClick(sender,  e); };
                    SaveBox[row, col].MouseEnter += delegate (object sender, EventArgs e) { MapEdit_MouseDrag(sender, e); };

                }
            }

            
        }

        //Creates the tile map from a file.
        private void CreateTileBox(BinaryReader bi)
        {
            
            ResXResourceSet resSet = new ResXResourceSet(resX); //Holds all the images to paint with.
            try
            {

                for (int row = 0; row < SaveBox.GetLength(0); row++)
                {
                    for (int col = 0; col < SaveBox.GetLength(1); col++)
                    {
                        SaveBox[row, col].Size = new Size(TILE_SIZE / (SaveBox.GetLength(1) / 2), TILE_SIZE / (SaveBox.GetLength(1) / 2));
                        if (SaveBox[row, col].Size.Width > 80)
                        {
                            SaveBox[row, col].Size = new Size(50, 50);
                        }
                        SaveBox[row, col].Location = new Point(279 + (SaveBox[row, col].Size.Width * col), 9 + (SaveBox[row, col].Size.Height * row));
                        string temp = bi.ReadString();
                        SaveBox[row, col].Image = (System.Drawing.Image)resSet.GetObject(temp);
                        SaveBox[row, col].SizeMode = PictureBoxSizeMode.StretchImage;
                        Controls.Add(SaveBox[row, col]);
                        SaveBox[row, col].Tag = temp;


                        SaveBox[row, col].MouseDown += delegate (object sender, MouseEventArgs e) { MapEdit_MouseClick(sender, e); };
                        SaveBox[row, col].MouseEnter += delegate (object sender, EventArgs e) { MapEdit_MouseDrag(sender, e); };
                    }
                           
                }
                
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
                
        }

        
        #endregion

       
    }
}
