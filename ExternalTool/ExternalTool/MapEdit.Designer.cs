namespace ExternalTool
{
    partial class MapEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.currentImg = new System.Windows.Forms.PictureBox();
            this.pictureList = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.currentImg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 434);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(179, 150);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(45, 590);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // currentImg
            // 
            this.currentImg.Location = new System.Drawing.Point(61, 482);
            this.currentImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.currentImg.Name = "currentImg";
            this.currentImg.Size = new System.Drawing.Size(67, 62);
            this.currentImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentImg.TabIndex = 4;
            this.currentImg.TabStop = false;
            // 
            // pictureList
            // 
            this.pictureList.FormattingEnabled = true;
            this.pictureList.ItemHeight = 16;
            this.pictureList.Location = new System.Drawing.Point(12, 10);
            this.pictureList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureList.Name = "pictureList";
            this.pictureList.Size = new System.Drawing.Size(179, 388);
            this.pictureList.Sorted = true;
            this.pictureList.TabIndex = 41;
            this.pictureList.SelectedIndexChanged += new System.EventHandler(this.PictureList_SelectedIndexChanged);
            // 
            // MapEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 674);
            this.Controls.Add(this.pictureList);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.currentImg);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MapEdit";
            this.Text = "MapEdit";
            ((System.ComponentModel.ISupportInitialize)(this.currentImg)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion
        private System.Windows.Forms.PictureBox currentImg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ListBox pictureList;
    }
}