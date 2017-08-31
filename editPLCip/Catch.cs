using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;
using PLCFinder;

namespace editPLCip
{
	public partial class Catch : Form
	{
		Stopwatch sw;
		PLCDiscovery DiscoverPLC;
		bool stopThread = false;
		bool PLCFound = false;
		public Catch(IPAddress ip, PLCDiscovery plcfinder)
		{
			InitializeComponent();
			label2.Visible = false;
			DiscoverPLC = plcfinder;
			plcAddressBox.IPAddress = ip;
			sw = new Stopwatch();
		}

		void displayTimer()
		{
			Thread timer = new Thread(delegate()
				{
					while (sw.ElapsedMilliseconds <= 60000 && !stopThread)
					{
						try
						{
							this.BeginInvoke(new Action(() => label1.Text = (60 - sw.ElapsedMilliseconds / 1000.00).ToString("0.00")));
							Thread.Sleep(5);
						}
						catch
						{
							break;
						}
					}
				});
			timer.Start();
		}

		void catchPLC()
		{
			Thread c = new Thread(delegate()
				{
					while (!stopThread)
					{
						try
						{
							DetectedPLC plc = DiscoverPLC.DiscoverNew();
							if (plc != null)
							{
								DiscoverPLC.changeIP(plc, plcAddressBox.IPAddress);
								DiscoverPLC.getPLCInfo(plc);
								plc.Connected = false;
								this.BeginInvoke(new Action(delegate()
									{
										DiscoverPLC.PLCbs.Add(plc);
										label3.Visible = true;
										label2.Visible = false;
									}));
								PLCFound = true;
								sw.Reset();
								stopThread = true;
								break;
							}
						}
						catch (Exception e)
						{
							MessageBox.Show(e.ToString());
							break;
						}
					}
				});
			c.Start();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			stopThread = true;
			if (PLCFound)
			{
				this.Close();
				return;
			}
			if (sw.IsRunning)
			{
				sw.Reset();
			}
			else
			{
				this.Close();
			}
		}

		private void OKbtn_Click(object sender, EventArgs e)
		{
			if (PLCFound)
			{
				this.Close();
				return;
			}
			if (!sw.IsRunning)
			{
				label2.Visible = true;
				stopThread = false;
				sw.Start();
				displayTimer();
				catchPLC();
			}
		}

	}
}
