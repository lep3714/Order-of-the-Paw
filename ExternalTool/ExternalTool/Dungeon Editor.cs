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
//Tyler Lynch
//This tool is used to place rooms together into a dungeon floor.
namespace ExternalTool
{
    public partial class Dungeon_Editor : Form
    {
        int locX = 11;
        int locY = 484;
        Button[,] dungeon = new Button[3, 3];//Holds the layout fo the dungeon.
        string currentRoom;//Holds the current room file name. Used for writing to the buttons

        public Dungeon_Editor()
        {
            InitializeComponent();
            PictureBox[,] previewBoxes = new PictureBox[60, 61];
            //Preview(previewBoxes);
            GenerateEmptyDungeon();
            
            //Creates a string collection property so that the file listBox persists between runs of the program without the use of fileIO.
            if (Properties.Settings.Default.Setting != null)
            {
                foreach (string file in Properties.Settings.Default.Setting)
                {
                    roomSel.Items.Add(file);
                }
            }
            else
            {
                Properties.Settings.Default.Setting = new System.Collections.Specialized.StringCollection();
            }
            

        }

        //generates a preview of the room you are placing;
       public void Preview(PictureBox[,] previewBoxes)
        {
            for (int row = 0; row < previewBoxes.GetLength(0); row++)
            {
                for (int col = 0; col < previewBoxes.GetLength(1); col++)
                {
                    previewBoxes[row, col] = new PictureBox();
                    previewBoxes[row, col].Location = new Point(locX, locY);                    
                    previewBoxes[row, col].Size = new Size(4, 4);
                    locX += 4;
                    if (col % 2 == 0)
                    {
                        previewBoxes[row, col].BackColor = Color.Black;
                        
                    }
                    else
                    {
                        previewBoxes[row, col].BackColor = Color.White;
                    }

                    Controls.Add(previewBoxes[row, col]);
                }
                locX = 11;
                locY += 4;
            }
        }

        //Sets the size and location of the buttons for each dungeon room
        public void GenerateEmptyDungeon()
        {
            for(int row = 0; row < dungeon.GetLength(1); row++)
            {
                for (int col = 0; col < dungeon.GetLength(0); col++)
                {
                    dungeon[row, col] = new Button();
                    dungeon[row, col].Size = new Size(50, 50);
                    dungeon[row, col].Location = new Point((col * 55) + 261, (row *55) + 8); //changes the relative distance of each button
                    Controls.Add(dungeon[row, col]);
                    dungeon[row, col].Text = "0";
                    dungeon[row, col].Click += new System.EventHandler(this.DungeonRoomClicked);
                }
            }
        }

        //Changes the text of the button so that it can be saved to the dungeon file.
        private void DungeonRoomClicked(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            temp.Text = currentRoom;
        }

        //Saves the new file added to the properties so that they persist between editing sessions.
        private void AddRoom_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();
            x.Filter = "Room File | *.rm";

            try
            {
                if(x.ShowDialog() == DialogResult.OK)
                {
                    roomSel.Items.Add(Path.GetFileName(x.FileName));
                    Properties.Settings.Default.Setting.Add(Path.GetFileName(x.FileName));
                    Properties.Settings.Default.Save();
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        //Saves the current dungeon configuration.
        private void SaveBt_Click(object sender, EventArgs e)
        {
            SaveFileDialog x = new SaveFileDialog();
            x.Filter = "Dungeon File | *.dn";

            try
            {
                if (x.ShowDialog() == DialogResult.OK)
                {
                    Stream write = File.OpenWrite(x.FileName);
                    BinaryWriter binWrite = new BinaryWriter(write);
                    binWrite.Write(dungeon.GetLength(0));
                    binWrite.Write(dungeon.GetLength(1));
                    //Each files name will be used in the main project to decide what room to show.
                    foreach(Button b in dungeon)
                    {
                        binWrite.Write(b.Text);
                    }

                    binWrite.Close();
                    MessageBox.Show("File Saved");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        
        //Handles switching between each file on the listbox
        private void RoomSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox temp = (ListBox)sender;
            if(temp.SelectedIndex != -1)
            {
                currentRoom = temp.SelectedItem.ToString();
            }
            
            
        }

        //Loads a dungeon from the DN file.
        private void LoadDun_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();
            x.Filter = "Dungeon File | *.dn";

            try
            {
                if(x.ShowDialog() == DialogResult.OK)
                {
                    Stream reader = File.OpenRead(x.FileName);
                    BinaryReader binRead = new BinaryReader(reader);
                    binRead.ReadString(); //Throws out the size values. Used because the dungeon size is fixed.
                    binRead.ReadString(); //Throws out the size values. Used because the dungeon size is fixed.
                    for (int row = 0; row < dungeon.GetLength(1); row++)
                    {
                        for(int col = 0; col < dungeon.GetLength(0); col++)
                        {

                            dungeon[row, col].Text = binRead.ReadString();                          
                            
                        }
                    }
                    binRead.Close();
                    MessageBox.Show("File Loaded");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void RemoveRoom_Click(object sender, EventArgs e)
        {
            
            if (roomSel.SelectedIndex != -1)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to remove this room?", "", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                
                    int index = roomSel.SelectedIndex;
                    roomSel.Items.RemoveAt(index);
                    Properties.Settings.Default.Setting.RemoveAt(index);
                    Properties.Settings.Default.Save();
                    roomSel.ClearSelected();
                }
            }
        }
    }
}
