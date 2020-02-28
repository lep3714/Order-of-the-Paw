namespace ExternalTool
{
    partial class Dungeon_Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.roomSel = new System.Windows.Forms.ListBox();
            this.addRoom = new System.Windows.Forms.Button();
            this.saveBt = new System.Windows.Forms.Button();
            this.loadDun = new System.Windows.Forms.Button();
            this.removeRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // roomSel
            // 
            this.roomSel.FormattingEnabled = true;
            this.roomSel.ItemHeight = 20;
            this.roomSel.Location = new System.Drawing.Point(12, 12);
            this.roomSel.Name = "roomSel";
            this.roomSel.Size = new System.Drawing.Size(306, 484);
            this.roomSel.TabIndex = 0;
            this.roomSel.SelectedIndexChanged += new System.EventHandler(this.RoomSel_SelectedIndexChanged);
            // 
            // addRoom
            // 
            this.addRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addRoom.Location = new System.Drawing.Point(264, 502);
            this.addRoom.Name = "addRoom";
            this.addRoom.Size = new System.Drawing.Size(50, 50);
            this.addRoom.TabIndex = 1;
            this.addRoom.Text = "+";
            this.addRoom.UseVisualStyleBackColor = true;
            this.addRoom.Click += new System.EventHandler(this.AddRoom_Click);
            // 
            // saveBt
            // 
            this.saveBt.Location = new System.Drawing.Point(71, 636);
            this.saveBt.Name = "saveBt";
            this.saveBt.Size = new System.Drawing.Size(189, 97);
            this.saveBt.TabIndex = 2;
            this.saveBt.Text = "Save Dungeon";
            this.saveBt.UseVisualStyleBackColor = true;
            this.saveBt.Click += new System.EventHandler(this.SaveBt_Click);
            // 
            // loadDun
            // 
            this.loadDun.Location = new System.Drawing.Point(71, 754);
            this.loadDun.Name = "loadDun";
            this.loadDun.Size = new System.Drawing.Size(189, 85);
            this.loadDun.TabIndex = 3;
            this.loadDun.Text = "Load Dungeon";
            this.loadDun.UseVisualStyleBackColor = true;
            this.loadDun.Click += new System.EventHandler(this.LoadDun_Click);
            // 
            // removeRoom
            // 
            this.removeRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeRoom.Location = new System.Drawing.Point(205, 502);
            this.removeRoom.Name = "removeRoom";
            this.removeRoom.Size = new System.Drawing.Size(50, 50);
            this.removeRoom.TabIndex = 4;
            this.removeRoom.Text = "-";
            this.removeRoom.UseVisualStyleBackColor = true;
            this.removeRoom.Click += new System.EventHandler(this.RemoveRoom_Click);
            // 
            // Dungeon_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1896, 1048);
            this.Controls.Add(this.removeRoom);
            this.Controls.Add(this.loadDun);
            this.Controls.Add(this.saveBt);
            this.Controls.Add(this.addRoom);
            this.Controls.Add(this.roomSel);
            this.Name = "Dungeon_Editor";
            this.Text = "Dungeon_Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox roomSel;
        private System.Windows.Forms.Button addRoom;
        private System.Windows.Forms.Button saveBt;
        private System.Windows.Forms.Button loadDun;
        private System.Windows.Forms.Button removeRoom;
    }
}