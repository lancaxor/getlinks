namespace GetLinks
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
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblRootSite = new System.Windows.Forms.Label();
            this.tbRootSite = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbSites = new System.Windows.Forms.ListBox();
            this.lblMaxDepth = new System.Windows.Forms.Label();
            this.tbMaxDepth = new System.Windows.Forms.TextBox();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(447, 346);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(85, 25);
            this.btnAnalyze.TabIndex = 0;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblRootSite
            // 
            this.lblRootSite.AutoSize = true;
            this.lblRootSite.Location = new System.Drawing.Point(9, 7);
            this.lblRootSite.Name = "lblRootSite";
            this.lblRootSite.Size = new System.Drawing.Size(64, 17);
            this.lblRootSite.TabIndex = 1;
            this.lblRootSite.Text = "Root site";
            // 
            // tbRootSite
            // 
            this.tbRootSite.Location = new System.Drawing.Point(82, 4);
            this.tbRootSite.Name = "tbRootSite";
            this.tbRootSite.Size = new System.Drawing.Size(541, 22);
            this.tbRootSite.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(538, 346);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 25);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbSites
            // 
            this.lbSites.FormattingEnabled = true;
            this.lbSites.HorizontalScrollbar = true;
            this.lbSites.ItemHeight = 16;
            this.lbSites.Location = new System.Drawing.Point(12, 32);
            this.lbSites.Name = "lbSites";
            this.lbSites.Size = new System.Drawing.Size(611, 276);
            this.lbSites.TabIndex = 4;
            // 
            // lblMaxDepth
            // 
            this.lblMaxDepth.AutoSize = true;
            this.lblMaxDepth.Location = new System.Drawing.Point(12, 350);
            this.lblMaxDepth.Name = "lblMaxDepth";
            this.lblMaxDepth.Size = new System.Drawing.Size(73, 17);
            this.lblMaxDepth.TabIndex = 5;
            this.lblMaxDepth.Text = "Max depth";
            // 
            // tbMaxDepth
            // 
            this.tbMaxDepth.Location = new System.Drawing.Point(91, 347);
            this.tbMaxDepth.Name = "tbMaxDepth";
            this.tbMaxDepth.Size = new System.Drawing.Size(122, 22);
            this.tbMaxDepth.TabIndex = 6;
            this.tbMaxDepth.Text = "3";
            this.tbMaxDepth.Leave += new System.EventHandler(this.tbMaxDepth_Leave);
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(356, 346);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(85, 25);
            this.btnSaveXML.TabIndex = 7;
            this.btnSaveXML.Text = "Save";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(12, 321);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(48, 17);
            this.lblState.TabIndex = 8;
            this.lblState.Text = "Status";
            // 
            // lblStatus
            // 
            this.lblStatus.Enabled = false;
            this.lblStatus.Location = new System.Drawing.Point(91, 318);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(532, 22);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 383);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnSaveXML);
            this.Controls.Add(this.tbMaxDepth);
            this.Controls.Add(this.lblMaxDepth);
            this.Controls.Add(this.lbSites);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbRootSite);
            this.Controls.Add(this.lblRootSite);
            this.Controls.Add(this.btnAnalyze);
            this.Name = "Form1";
            this.Text = "Links Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.ResizeForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label lblRootSite;
        private System.Windows.Forms.TextBox tbRootSite;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox lbSites;
        private System.Windows.Forms.Label lblMaxDepth;
        private System.Windows.Forms.TextBox tbMaxDepth;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox lblStatus;
    }
}

