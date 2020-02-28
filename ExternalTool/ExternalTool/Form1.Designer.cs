namespace ExternalTool
{
    partial class Form1
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
            this.loadMap = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.createMap = new System.Windows.Forms.Button();
            this.heightText = new System.Windows.Forms.TextBox();
            this.widthText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newDungeon = new System.Windows.Forms.Button();
            this.instructions = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadMap
            // 
            this.loadMap.Location = new System.Drawing.Point(30, 346);
            this.loadMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadMap.Name = "loadMap";
            this.loadMap.Size = new System.Drawing.Size(435, 259);
            this.loadMap.TabIndex = 0;
            this.loadMap.Text = "Load Room";
            this.loadMap.UseVisualStyleBackColor = true;
            this.loadMap.Click += new System.EventHandler(this.loadMap_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createMap);
            this.groupBox1.Controls.Add(this.heightText);
            this.groupBox1.Controls.Add(this.widthText);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(575, 225);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(435, 380);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Map";
            // 
            // createMap
            // 
            this.createMap.Location = new System.Drawing.Point(9, 223);
            this.createMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createMap.Name = "createMap";
            this.createMap.Size = new System.Drawing.Size(417, 148);
            this.createMap.TabIndex = 4;
            this.createMap.Text = "Create Room";
            this.createMap.UseVisualStyleBackColor = true;
            this.createMap.Click += new System.EventHandler(this.createMap_Click);
            // 
            // heightText
            // 
            this.heightText.Location = new System.Drawing.Point(214, 151);
            this.heightText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.heightText.Name = "heightText";
            this.heightText.Size = new System.Drawing.Size(148, 26);
            this.heightText.TabIndex = 3;
            // 
            // widthText
            // 
            this.widthText.Location = new System.Drawing.Point(214, 86);
            this.widthText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.widthText.Name = "widthText";
            this.widthText.Size = new System.Drawing.Size(148, 26);
            this.widthText.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height (In Tiles)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width (In Tiles)";
            // 
            // newDungeon
            // 
            this.newDungeon.Location = new System.Drawing.Point(575, 45);
            this.newDungeon.Name = "newDungeon";
            this.newDungeon.Size = new System.Drawing.Size(435, 172);
            this.newDungeon.TabIndex = 2;
            this.newDungeon.Text = "Create Dungeon";
            this.newDungeon.UseVisualStyleBackColor = true;
            this.newDungeon.Click += new System.EventHandler(this.NewDungeon_Click);
            // 
            // instructions
            // 
            this.instructions.Location = new System.Drawing.Point(30, 45);
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(435, 242);
            this.instructions.TabIndex = 3;
            this.instructions.Text = "Instructions";
            this.instructions.UseVisualStyleBackColor = true;
            this.instructions.Click += new System.EventHandler(this.Instructions_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 657);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.newDungeon);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loadMap);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadMap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createMap;
        private System.Windows.Forms.TextBox heightText;
        private System.Windows.Forms.TextBox widthText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newDungeon;
        private System.Windows.Forms.Button instructions;
    }
}

