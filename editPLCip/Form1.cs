using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using PLCFinder;
using System.Threading;
using System.Timers;
using System.Net.NetworkInformation;
using System.Net;

namespace editPLCip
{
	public partial class Form1 : Form
	{
		PLCDiscovery DiscoverPLC;
		int lastSelectedRow = -1;

		public Form1()
		{
			InitializeComponent();
			DiscoverPLC = new PLCDiscovery();
			PLCGrid.DataSource = DiscoverPLC.PLCbs;
			PLCGrid.AutoGenerateColumns = true;
			string ip = GetIP();
			if (ip != String.Empty)
			{
				IPLabel.Text = "Local IP: " + ip;
				int lastOctetIndex = ip.LastIndexOf(".") + 1;
				ip = ip.Substring(0, lastOctetIndex);
				ip += "245";
				plcIPAddressBox.IPAddress = IPAddress.Parse(ip);
			}
			else
			{
				plcIPAddressBox.IPAddress = new IPAddress(new byte[] { 192, 168, 0, 245 });
			}
		}

		private string GetIP()
		{
			IPHostEntry host;
			string localIP = String.Empty;
			host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					localIP = ip.ToString();
				}
			}
			return localIP;
		}

		private void FindPLCs()
		{
			Thread t = new Thread(delegate()
			{
				this.BeginInvoke(new Action(delegate() 
					{
						PingTimer.Stop();
						changeCursor(Cursors.WaitCursor);
						EnableButtons(false);
						DiscoverPLC.PLCbs.Clear();
						DiscoverPLC.DiscoverPLCs();
						changeCursor(Cursors.Default);
						EnableButtons(true);
						DiscoverPLC.PLCbs.ResetBindings(false);
						PLCGrid.ClearSelection();
						PingTimer.Start(); 
					}));
				lastSelectedRow = -1;
			});
			t.Start();
			this.ActiveControl = Discoverbtn;
			this.ActiveControl = null;
		}

		private void ChangeIP_Click(object sender, EventArgs e)
		{
			PingTimer.Stop();
			Ping ping = new Ping();
			PingReply reply = ping.Send(plcIPAddressBox.IPAddress, 1500);
			Console.WriteLine(reply.Status.ToString() + " " + plcIPAddressBox.IPAddress.ToString());
			if (reply.Status == IPStatus.Success)
			{
				DialogResult result = MessageBox.Show("Detected another device with this IP address. Use this address anyway?",
					"IP Address in Use!", MessageBoxButtons.OKCancel);
				if (result == DialogResult.Cancel) return;
			}
			if (PLCGrid.SelectedRows.Count <= 0)
			{
				MessageBox.Show("Select PLC from List.", "Error!");
				return;
			}
			DetectedPLC plc = (DetectedPLC)DiscoverPLC.PLCbs.Current;
			if (plc == null)
			{
				MessageBox.Show("Null PLC Exception!", "Error!");
				return;
			}
			DiscoverPLC.changeIP(plc, plcIPAddressBox.IPAddress);
			FindPLCs();
		}

		private bool PingIP(IPAddress ip)
		{
			Ping ping = new Ping();
			int timeout = 500;
			PingReply reply = ping.Send(ip, timeout);
			if (reply.Status == IPStatus.Success) return true;
			else return false;
		}

		private void EnableButtons(bool enable)
		{
			foreach (Control c in Controls)
			{
				if (c is Button)
				{
					Button b = c as Button;
					if (enable) b.Enabled = true;
					else b.Enabled = false;
				}
			}
		}

		private void changeCursor(Cursor c)
		{
			this.Invoke(new Action(() => Cursor = c));
		}

		private void Discoverbtn_Click(object sender, EventArgs e)
		{
			FindPLCs();
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (lastSelectedRow == e.RowIndex)
			{
				PLCGrid.ClearSelection();
				lastSelectedRow = -1;
			}
			else
			{
				lastSelectedRow = e.RowIndex;
			}
		}

		private void Form1_Click(object sender, EventArgs e)
		{
			this.ActiveControl = Discoverbtn;
			this.ActiveControl = null;
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			FindPLCs();
			this.ActiveControl = null;
		}

		// Set row color
		private void PLCGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataGridViewRow row = PLCGrid.Rows[e.RowIndex];
			DataGridViewCell cell = row.Cells[e.ColumnIndex];
			DetectedPLC plc = (DetectedPLC)row.DataBoundItem;
			try
			{
				if (!plc.Connected)
				{
					row.DefaultCellStyle.BackColor = Color.Red;
					cell.ToolTipText = "PLC Disconnected!";
				}
				else if (plc.IP.ToString() != plc.InternalIP.ToString())
				{
					row.DefaultCellStyle.BackColor = Color.LightPink;
					row.DefaultCellStyle.SelectionForeColor = Color.LightPink;
					cell.ToolTipText = "Cycle PLC power to change IP Address!";
				}
				else cell.ToolTipText = null;
			}
			catch (Exception)
			{
				MessageBox.Show("Row not bound to PLC!", "Error!");
			}
		}

		private void PingTimer_Tick(object sender, EventArgs e)
		{
			foreach (DetectedPLC plc in DiscoverPLC.PLCList)
			{
				Console.WriteLine(plc.Name);
				if (plc.Connected)
				{
					Ping ping = new Ping();
					PingReply reply = ping.Send(plc.IP, 500);
					if (reply.Status != IPStatus.Success)
					{
						PingReply reply2 = ping.Send(plc.IP, 500);
						if (reply2.Status != IPStatus.Success)
						{
							plc.Connected = false;
							plcDisconnected(plc);
						}
					}
				}
				else
				{
					Ping ping = new Ping();
					PingReply reply = ping.Send(plc.InternalIP, 500);
					if (reply.Status == IPStatus.Success)
					{
						Console.WriteLine(reply.Status.ToString() + " " + plc.InternalIP.ToString());
						plcReconnected(plc, true);
						plc.Connected = true;
					}
					else
					{
						PingReply reply2 = ping.Send(plc.IP, 500);
						if (reply2.Status == IPStatus.Success)
						{
							Console.WriteLine(reply2.Status.ToString() + " " + plc.IP.ToString());
							plcReconnected(plc, false);
							plc.Connected = true;
						}
					}
				}
			}
			Console.WriteLine("");
		}

		void plcDisconnected(DetectedPLC plc)
		{
			DiscoverPLC.PLCbs.ResetBindings(false);
			PLCGrid.ClearSelection();
		}

		void plcReconnected(DetectedPLC plc, bool newIP)
		{
			if (newIP)
			{
				plc.IP = plc.InternalIP;
			}
			DiscoverPLC.PLCbs.ResetBindings(false);
			PLCGrid.ClearSelection();
		}

		private void catchbtn_Click(object sender, EventArgs e)
		{
			PingTimer.Stop();
			Catch catch1 = new Catch(plcIPAddressBox.IPAddress, DiscoverPLC);
			catch1.ShowDialog();
			//DiscoverPLC.DiscoverPLCs();
			PingTimer.Start();
		}

	}
}
