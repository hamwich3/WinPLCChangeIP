using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCFinder
{
	public class PLCDiscovery
	{
		public List<DetectedPLC> PLCList = new List<DetectedPLC>();
		public BindingSource PLCbs = new BindingSource();
		int maxmillis = 300;
		IPEndPoint thisEndPoint;
		int plcUDPPort = 28784;

		public PLCDiscovery()
		{
			PLCbs.DataSource = PLCList;
		}

		public void DiscoverPLCs()
		{
			pollNetwork();
			populatePLCList();
			pollNetwork();
			populatePLCList();
			getPLCInfo();
		}

		public DetectedPLC DiscoverNew()
		{
			pollNetwork();
			return getNewPLC();
		}

		void pollNetwork()
		{
			UdpClient udp = NewBroadcastUDP();
			byte[] packet = PacketBuilder.BuildHeader(new byte[] { 0x05 });
			udp.Send(packet, packet.Length);
			udp.EnableBroadcast = false;
			udp.Close();
		}

		void populatePLCList()
		{
			UdpClient udp = RecieveUDP();
			IPEndPoint PLCEndPoint = new IPEndPoint(IPAddress.Any, thisEndPoint.Port);
			Stopwatch UDPReceiveTimeout = new Stopwatch();
			bool timeout = false;
			while (!timeout)
			{
				if (!UDPReceiveTimeout.IsRunning) UDPReceiveTimeout.Start();
				else
				{
					UDPReceiveTimeout.Stop();
					UDPReceiveTimeout.Reset();
					UDPReceiveTimeout.Start();
				}
				bool dataready = false;
				while (UDPReceiveTimeout.ElapsedMilliseconds < maxmillis && !dataready)
				{
					if (udp.Available > 0)
					{
						dataready = true;
					}
				}

				if (dataready)
				{
					try
					{
						byte[] received = udp.Receive(ref PLCEndPoint);
						if (PacketBuilder.CheckData(received) && received[9] == 0x55 && received[10] == 0xaa)
						{
							DetectedPLC plc = new DetectedPLC(PLCEndPoint.Address);
							Array.Copy(received, 11, plc.MAC, 0, plc.MAC.Length);
							bool newplc = true;
							foreach (DetectedPLC old in PLCList)
							{
								if (plc.IP.ToString() == old.IP.ToString()) newplc = false;
							}
							if (newplc)
							{
								PLCbs.Add(plc);
							}
						}
					}
					catch
					{
						timeout = true;
					}
				}
				else timeout = true;
			}
			udp.Close();
		}

		DetectedPLC getNewPLC()
		{
			UdpClient udp = RecieveUDP();
			IPEndPoint PLCEndPoint = new IPEndPoint(IPAddress.Any, thisEndPoint.Port);
			Stopwatch UDPReceiveTimeout = new Stopwatch();
			bool timeout = false;
			UDPReceiveTimeout.Reset();
			while (!timeout)
			{
				if (UDPReceiveTimeout.IsRunning)
				{
					UDPReceiveTimeout.Stop();
					UDPReceiveTimeout.Reset();
					UDPReceiveTimeout.Start();
				}
				else UDPReceiveTimeout.Start();
				bool dataready = false;
				while (UDPReceiveTimeout.ElapsedMilliseconds < maxmillis && !dataready)
				{
					if (udp.Available > 0)
					{
						dataready = true;
					}
				}
				if (dataready)
				{
					try
					{
						byte[] received = udp.Receive(ref PLCEndPoint);
						if (PacketBuilder.CheckData(received))
						{
							if (received[9] == 0x55 && received[10] == 0xaa)
							{ 
								DetectedPLC plc = new DetectedPLC(PLCEndPoint.Address);
								bool newplc = true;
								foreach (DetectedPLC old in PLCList)
								{
									if (old.IP.ToString() == plc.IP.ToString())
									{
										newplc = false;
									}
								}
								if (newplc)
								{
									Console.WriteLine("new plc detected! IP: " + plc.IP);
									Array.Copy(received, 11, plc.MAC, 0, plc.MAC.Length);
									getPLCInfo(plc);
									udp.Close();
									return plc;
								}
							}
						}
					}
					catch
					{
						timeout = true;
						Console.WriteLine("timeout");
					}
				}
				else
				{
					timeout = true;
				}
			}
			udp.Close();
			return null;
		}

		public void changeIP(byte[] MAC, byte[] ip)
		{
			if (ip.Length != 4) throw new Exception("IP must contain exactly four bytes!");
			if (MAC.Length != 6) throw new Exception("MAC must contain exactly six bytes!");
			UdpClient udp = NewBroadcastUDP();
			byte[] data = { 0x15, MAC[0], MAC[1], MAC[2], MAC[3], MAC[4], MAC[5], 0x0c, 0x00, 0x10, 0x00, ip[0], ip[1], ip[2], ip[3] };
			byte[] packet = PacketBuilder.BuildHeader(data);
			udp.Send(packet, packet.Length);
			udp.EnableBroadcast = false;
			udp.Close();
		}

		public void changeIP(DetectedPLC plc, IPAddress IP)
		{
			byte[] ip = IP.GetAddressBytes();
			UdpClient udp = NewBroadcastUDP();
			byte[] data = { 0x15, plc.MAC[0], plc.MAC[1], plc.MAC[2], plc.MAC[3], plc.MAC[4], plc.MAC[5], 0x0c, 0x00, 0x10, 0x00, ip[0], ip[1], ip[2], ip[3] };
			byte[] packet = PacketBuilder.BuildHeader(data);
			udp.Send(packet, packet.Length);
			udp.EnableBroadcast = false;
			udp.Close();
		}

		public void getPLCInfo(DetectedPLC PLC = null)
		{
			if (PLC == null)
			{
				foreach (DetectedPLC plc in PLCList)
				{
					requestName(plc);
					receiveName(plc);
					requestIP(plc);
					receiveIP(plc);
					Console.WriteLine(plc.IP + ": " + BitConverter.ToString(plc.MAC));
					Console.WriteLine("\t" + plc.Name + " " + plc.InternalIP);
				}
			}
			else
			{
				requestName(PLC);
				receiveName(PLC);
				requestIP(PLC);
				receiveIP(PLC);
				Console.WriteLine(PLC.IP + ": " + BitConverter.ToString(PLC.MAC));
				Console.WriteLine("\t" + PLC.Name + " " + PLC.InternalIP);
			}
		}

		void requestName(DetectedPLC plc)
		{
			UdpClient udp = NewBroadcastUDP();
			byte[] data = new byte[11];
			data[0] = 0x15;
			Array.Copy(plc.MAC, 0, data, 1, plc.MAC.Length);
			data[7] = 0x0b;
			data[9] = 0x16;
			byte[] packet = PacketBuilder.BuildHeader(data);
			udp.Send(packet, packet.Length);
			udp.Close();
		}

		void receiveName(DetectedPLC plc)
		{
			UdpClient udp = RecieveUDP();
			IPEndPoint PLCEndPoint = new IPEndPoint(IPAddress.Any, thisEndPoint.Port);
			try
			{
				byte[] received = udp.Receive(ref PLCEndPoint);
				if (PacketBuilder.CheckData(received))
				{
					int length = 0;
					for (length = 0; length < received.Length; length++)
					{
						if (received[length + 11] == 0x00)
						{
							break;
						}
					}
					plc.Name = System.Text.Encoding.UTF8.GetString(received, 11, length);
				}
			}
			catch
			{
				plc.Name = String.Empty;
			}
		}

		void requestIP(DetectedPLC plc)
		{
			UdpClient udp = NewBroadcastUDP();
			byte[] data = new byte[11];
			data[0] = 0x15;
			Array.Copy(plc.MAC, 0, data, 1, plc.MAC.Length);
			data[7] = 0x0b;
			data[9] = 0x10;
			byte[] packet = PacketBuilder.BuildHeader(data);
			udp.Send(packet, packet.Length);
			udp.Close();
		}

		void receiveIP(DetectedPLC plc)
		{
			UdpClient udp = RecieveUDP();
			IPEndPoint PLCEndPoint = new IPEndPoint(IPAddress.Any, thisEndPoint.Port);
			try
			{
				byte[] received = udp.Receive(ref PLCEndPoint);
				if (PacketBuilder.CheckData(received))
				{
					byte[] ip = new byte[4];
					Array.Copy(received, 11, ip, 0, 4);
					plc.InternalIP = new IPAddress(ip);
				}
			}
			catch
			{
				plc.InternalIP = IPAddress.None;
			}
		}

		UdpClient NewBroadcastUDP()
		{
			UdpClient udp = new UdpClient();
			udp.Client.ReceiveTimeout = 500;
			udp.Client.SendTimeout = 500;
			udp.EnableBroadcast = true;
			udp.Connect("255.255.255.255", plcUDPPort);
			thisEndPoint = (IPEndPoint)udp.Client.LocalEndPoint;
			return udp;
		}

		UdpClient RecieveUDP()
		{
			UdpClient udp = new UdpClient(thisEndPoint.Port);
			udp.Client.ReceiveTimeout = 500;
			udp.Client.SendTimeout = 500;
			return udp;
		}

		static class PacketBuilder
		{
			// Define byte indexes
			const byte H1 = 0;
			const byte H2 = 1;
			const byte H3 = 2;
			const byte AV1 = 3;
			const byte AV2 = 4;
			const byte CRC1 = 5;
			const byte CRC2 = 6;
			const byte LEN1 = 7;
			const byte LEN2 = 8;

			// Increment for every packet sent
			static UInt16 AppVal = 1;
			static UInt16 lastAppVal = 0;

			static public byte[] BuildHeader(byte[] data)
			{
				byte[] header = new byte[9];
				UInt16 CRC = GetCRC(data);

				header[H1] = 0x48;
				header[H2] = 0x41;
				header[H3] = 0x50;
				header[AV1] = (byte)AppVal;
				header[AV2] = (byte)(AppVal >> 8);
				header[CRC1] = (byte)CRC;
				header[CRC2] = (byte)(CRC >> 8);
				header[LEN1] = (byte)data.Length;
				header[LEN2] = (byte)(data.Length >> 8);

				byte[] packet = new byte[data.Length + header.Length];
				Array.Copy(header, packet, header.Length);
				Array.Copy(data, 0, packet, header.Length, data.Length);
				lastAppVal = AppVal++;
				return packet;
			}

			static public UInt16 GetCRC(byte[] data)
			{
				UInt16 CRC = 0x0000;
				for (int index = 0; index < data.Length; index++)
				{
					UInt16 d = data[index];
					d <<= 8;
					for (int i = 0; i < 8; i++)
					{
						if (((CRC ^ d) & 0x8000) == 0x8000)
						{
							CRC <<= 1;
							CRC ^= 0x1021;
						}
						else
						{
							CRC <<= 1;
						}
						d <<= 1;
					}
				}
				return CRC;
			}

			static public bool CheckData(byte[] data)
			{
				if (data[H1] == 0x48 && data[H2] == 0x41 && data[H3] == 0x50)
				{
					if (data[AV1] == (byte)lastAppVal && data[AV2] == (byte)(lastAppVal >> 8))
						return true;
				}
				return false;
			}
		}
	}
	public class DetectedPLC
	{
		string name;
		IPAddress internalIP;
		IPAddress ip;
		public byte[] MAC = new byte[6];
		bool connected = true;
		public DetectedPLC(IPAddress ip)
		{
			this.ip = ip;
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public IPAddress IP
		{
			get { return ip; }
			set { ip = value; }
		}

		public IPAddress InternalIP
		{
			get { return internalIP; }
			set { internalIP = value; }
		}
		
		public string MACAddress
		{
			get { return BitConverter.ToString(MAC).Replace("-", " "); }
		}
		
		public bool Connected
		{
			get { return connected; }
			set { connected = value; }
		}
		
	}
}
