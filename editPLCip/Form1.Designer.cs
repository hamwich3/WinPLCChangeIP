namespace editPLCip
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PLCGrid = new System.Windows.Forms.DataGridView();
			this.Discoverbtn = new System.Windows.Forms.Button();
			this.ChangeIP = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.PingTimer = new System.Windows.Forms.Timer(this.components);
			this.catchbtn = new System.Windows.Forms.Button();
			this.plcIPAddressBox = new NullFX.Controls.IPAddressControl();
			this.IPLabel = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PLCGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.PLCGrid);
			this.groupBox1.Location = new System.Drawing.Point(0, 43);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.MinimumSize = new System.Drawing.Size(779, 308);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Size = new System.Drawing.Size(779, 308);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// PLCGrid
			// 
			this.PLCGrid.AllowUserToAddRows = false;
			this.PLCGrid.AllowUserToDeleteRows = false;
			this.PLCGrid.AllowUserToResizeColumns = false;
			this.PLCGrid.AllowUserToResizeRows = false;
			this.PLCGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.PLCGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.PLCGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.PLCGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.PLCGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.PLCGrid.DefaultCellStyle = dataGridViewCellStyle2;
			this.PLCGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PLCGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.PLCGrid.Location = new System.Drawing.Point(3, 17);
			this.PLCGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.PLCGrid.MultiSelect = false;
			this.PLCGrid.Name = "PLCGrid";
			this.PLCGrid.ReadOnly = true;
			this.PLCGrid.RowHeadersVisible = false;
			this.PLCGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.PLCGrid.RowTemplate.Height = 30;
			this.PLCGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.PLCGrid.Size = new System.Drawing.Size(773, 289);
			this.PLCGrid.TabIndex = 0;
			this.PLCGrid.TabStop = false;
			this.PLCGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			this.PLCGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.PLCGrid_CellFormatting);
			// 
			// Discoverbtn
			// 
			this.Discoverbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Discoverbtn.Location = new System.Drawing.Point(154, 650);
			this.Discoverbtn.Margin = new System.Windows.Forms.Padding(4);
			this.Discoverbtn.Name = "Discoverbtn";
			this.Discoverbtn.Size = new System.Drawing.Size(131, 28);
			this.Discoverbtn.TabIndex = 1;
			this.Discoverbtn.Text = "Discover PLCs";
			this.Discoverbtn.UseVisualStyleBackColor = true;
			this.Discoverbtn.Click += new System.EventHandler(this.Discoverbtn_Click);
			// 
			// ChangeIP
			// 
			this.ChangeIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ChangeIP.Location = new System.Drawing.Point(431, 507);
			this.ChangeIP.Margin = new System.Windows.Forms.Padding(4);
			this.ChangeIP.Name = "ChangeIP";
			this.ChangeIP.Size = new System.Drawing.Size(100, 28);
			this.ChangeIP.TabIndex = 2;
			this.ChangeIP.Tag = "";
			this.ChangeIP.Text = "Change IP";
			this.ChangeIP.UseVisualStyleBackColor = true;
			this.ChangeIP.Click += new System.EventHandler(this.ChangeIP_Click);
			// 
			// PingTimer
			// 
			this.PingTimer.Interval = 2000;
			this.PingTimer.Tick += new System.EventHandler(this.PingTimer_Tick);
			// 
			// catchbtn
			// 
			this.catchbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.catchbtn.Location = new System.Drawing.Point(524, 650);
			this.catchbtn.Name = "catchbtn";
			this.catchbtn.Size = new System.Drawing.Size(100, 28);
			this.catchbtn.TabIndex = 4;
			this.catchbtn.Text = "Catch PLC";
			this.catchbtn.UseVisualStyleBackColor = true;
			this.catchbtn.Click += new System.EventHandler(this.catchbtn_Click);
			// 
			// plcIPAddressBox
			// 
			this.plcIPAddressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.plcIPAddressBox.IPAddress = ((System.Net.IPAddress)(resources.GetObject("plcIPAddressBox.IPAddress")));
			this.plcIPAddressBox.Location = new System.Drawing.Point(229, 510);
			this.plcIPAddressBox.Margin = new System.Windows.Forms.Padding(4);
			this.plcIPAddressBox.Name = "plcIPAddressBox";
			this.plcIPAddressBox.Size = new System.Drawing.Size(163, 22);
			this.plcIPAddressBox.TabIndex = 3;
			this.plcIPAddressBox.Text = "192.168.0.245";
			this.plcIPAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// IPLabel
			// 
			this.IPLabel.AutoSize = true;
			this.IPLabel.Location = new System.Drawing.Point(72, 723);
			this.IPLabel.Name = "IPLabel";
			this.IPLabel.Size = new System.Drawing.Size(62, 16);
			this.IPLabel.TabIndex = 5;
			this.IPLabel.Text = "Local IP: ";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(781, 763);
			this.Controls.Add(this.IPLabel);
			this.Controls.Add(this.catchbtn);
			this.Controls.Add(this.plcIPAddressBox);
			this.Controls.Add(this.ChangeIP);
			this.Controls.Add(this.Discoverbtn);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.ShowIcon = false;
			this.Text = "WinPLC IP Editor";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.Click += new System.EventHandler(this.Form1_Click);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PLCGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
		  private System.Windows.Forms.DataGridView PLCGrid;
		  private System.Windows.Forms.Button Discoverbtn;
		  private System.Windows.Forms.Button ChangeIP;
		  private NullFX.Controls.IPAddressControl plcIPAddressBox;
		  private System.Windows.Forms.ToolTip toolTip1;
		  private System.Windows.Forms.Timer PingTimer;
		  private System.Windows.Forms.Button catchbtn;
		  private System.Windows.Forms.Label IPLabel;

    }
}

