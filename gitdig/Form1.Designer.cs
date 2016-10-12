namespace gitdig
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonComm = new System.Windows.Forms.Button();
            this.linkLabelParent = new System.Windows.Forms.LinkLabel();
            this.linkLabelChild = new System.Windows.Forms.LinkLabel();
            this.linkLabelTree = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelRoot = new System.Windows.Forms.LinkLabel();
            this.buttonSplit = new System.Windows.Forms.Button();
            this.buttonFindBlob = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(16, 68);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(534, 246);
            this.textBox1.TabIndex = 1;
            // 
            // buttonComm
            // 
            this.buttonComm.Location = new System.Drawing.Point(104, 13);
            this.buttonComm.Name = "buttonComm";
            this.buttonComm.Size = new System.Drawing.Size(102, 23);
            this.buttonComm.TabIndex = 2;
            this.buttonComm.Text = "Select commit";
            this.buttonComm.UseVisualStyleBackColor = true;
            this.buttonComm.Click += new System.EventHandler(this.buttonComm_Click);
            // 
            // linkLabelParent
            // 
            this.linkLabelParent.AutoSize = true;
            this.linkLabelParent.Location = new System.Drawing.Point(16, 43);
            this.linkLabelParent.Name = "linkLabelParent";
            this.linkLabelParent.Size = new System.Drawing.Size(54, 13);
            this.linkLabelParent.TabIndex = 3;
            this.linkLabelParent.TabStop = true;
            this.linkLabelParent.Text = "Go parent";
            this.linkLabelParent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelParent_LinkClicked);
            // 
            // linkLabelChild
            // 
            this.linkLabelChild.AutoSize = true;
            this.linkLabelChild.Location = new System.Drawing.Point(77, 43);
            this.linkLabelChild.Name = "linkLabelChild";
            this.linkLabelChild.Size = new System.Drawing.Size(46, 13);
            this.linkLabelChild.TabIndex = 4;
            this.linkLabelChild.TabStop = true;
            this.linkLabelChild.Text = "Go child";
            this.linkLabelChild.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelChild_LinkClicked);
            // 
            // linkLabelTree
            // 
            this.linkLabelTree.AutoSize = true;
            this.linkLabelTree.Location = new System.Drawing.Point(130, 43);
            this.linkLabelTree.Name = "linkLabelTree";
            this.linkLabelTree.Size = new System.Drawing.Size(42, 13);
            this.linkLabelTree.TabIndex = 5;
            this.linkLabelTree.TabStop = true;
            this.linkLabelTree.Text = "Go tree";
            this.linkLabelTree.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTree_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(478, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // linkLabelRoot
            // 
            this.linkLabelRoot.AutoSize = true;
            this.linkLabelRoot.Location = new System.Drawing.Point(179, 43);
            this.linkLabelRoot.Name = "linkLabelRoot";
            this.linkLabelRoot.Size = new System.Drawing.Size(42, 13);
            this.linkLabelRoot.TabIndex = 7;
            this.linkLabelRoot.TabStop = true;
            this.linkLabelRoot.Text = "Go root";
            this.linkLabelRoot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRoot_LinkClicked);
            // 
            // buttonSplit
            // 
            this.buttonSplit.Location = new System.Drawing.Point(223, 13);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(98, 23);
            this.buttonSplit.TabIndex = 8;
            this.buttonSplit.Text = "Select split";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.buttonSplit_Click);
            // 
            // buttonFindBlob
            // 
            this.buttonFindBlob.Location = new System.Drawing.Point(338, 13);
            this.buttonFindBlob.Name = "buttonFindBlob";
            this.buttonFindBlob.Size = new System.Drawing.Size(75, 23);
            this.buttonFindBlob.TabIndex = 9;
            this.buttonFindBlob.Text = "Find blob";
            this.buttonFindBlob.UseVisualStyleBackColor = true;
            this.buttonFindBlob.Click += new System.EventHandler(this.buttonFindBlob_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 326);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonFindBlob);
            this.Controls.Add(this.buttonSplit);
            this.Controls.Add(this.linkLabelRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabelTree);
            this.Controls.Add(this.linkLabelChild);
            this.Controls.Add(this.linkLabelParent);
            this.Controls.Add(this.buttonComm);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonComm;
        private System.Windows.Forms.LinkLabel linkLabelParent;
        private System.Windows.Forms.LinkLabel linkLabelChild;
        private System.Windows.Forms.LinkLabel linkLabelTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelRoot;
        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.Button buttonFindBlob;
        private System.Windows.Forms.Button button2;
    }
}

