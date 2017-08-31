namespace editPLCip
{
	partial class Catch
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Catch));
			this.label1 = new System.Windows.Forms.Label();
			this.plcAddressBox = new NullFX.Controls.IPAddressControl();
			this.OKbtn = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(151, 190);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "60.00";
			// 
			// plcAddressBox
			// 
			this.plcAddressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.plcAddressBox.IPAddress = ((System.Net.IPAddress)(resources.GetObject("plcAddressBox.IPAddress")));
			this.plcAddressBox.Location = new System.Drawing.Point(23, 117);
			this.plcAddressBox.Margin = new System.Windows.Forms.Padding(4);
			this.plcAddressBox.Name = "plcAddressBox";
			this.plcAddressBox.Size = new System.Drawing.Size(163, 22);
			this.plcAddressBox.TabIndex = 4;
			this.plcAddressBox.Text = "192.168.0.245";
			this.plcAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// OKbtn
			// 
			this.OKbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OKbtn.Location = new System.Drawing.Point(221, 94);
			this.OKbtn.Name = "OKbtn";
			this.OKbtn.Size = new System.Drawing.Size(78, 28);
			this.OKbtn.TabIndex = 5;
			this.OKbtn.Text = "OK";
			this.OKbtn.UseVisualStyleBackColor = true;
			this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(221, 128);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(78, 28);
			this.button1.TabIndex = 5;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.DarkRed;
			this.label2.Location = new System.Drawing.Point(59, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 20);
			this.label2.TabIndex = 6;
			this.label2.Text = "Unplug and replug PLC to set IP!";
			this.label2.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.DarkRed;
			this.label3.Location = new System.Drawing.Point(2, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(336, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "PLC IP changed! Cycle PLC power to complete change.";
			this.label3.Visible = false;
			// 
			// Catch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(336, 236);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.OKbtn);
			this.Controls.Add(this.plcAddressBox);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Catch";
			this.Text = "Catch";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private NullFX.Controls.IPAddressControl plcAddressBox;
		private System.Windows.Forms.Button OKbtn;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}