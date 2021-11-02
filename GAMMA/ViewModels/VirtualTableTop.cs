using GAMMA.Models;
using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMMA.ViewModels
{
    public class VirtualTableTop : BaseModel
    {
        // Constructors
        public VirtualTableTop()
        {

        }

        // Databound Properties
        #region VttClient
        private TcpClient _VttClient;
        public TcpClient VttClient
        {
            get
            {
                return _VttClient;
            }
            set
            {
                _VttClient = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region VttListener
        private TcpListener _VttListener;
        public TcpListener VttListener
        {
            get
            {
                return _VttListener;
            }
            set
            {
                _VttListener = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region JoinIp
        private string _JoinIp;
        public string JoinIp
        {
            get
            {
                return _JoinIp;
            }
            set
            {
                _JoinIp = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SentData
        private string _SentData;
        public string SentData
        {
            get
            {
                return _SentData;
            }
            set
            {
                _SentData = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ReceivedData
        private string _ReceivedData;
        public string ReceivedData
        {
            get
            {
                return _ReceivedData;
            }
            set
            {
                _ReceivedData = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region StartSession
        private RelayCommand _StartSession;
        public ICommand StartSession
        {
            get
            {
                if (_StartSession == null)
                {
                    _StartSession = new RelayCommand(param => DoStartSession());
                }
                return _StartSession;
            }
        }
        private void DoStartSession()
        {
            //VttClient = new TcpClient();
            //VttClient.Connect(IPAddress.Parse("127.0.0.1"), 8085);
            if (VttListener != null)
            {
                return;
            }
            IPAddress address = IPAddress.Parse("127.0.0.1");
            VttListener = new TcpListener(address, 8085);
            VttListener.Start();
        }
        #endregion
        #region JoinSession
        private RelayCommand _JoinSession;
        public ICommand JoinSession
        {
            get
            {
                if (_JoinSession == null)
                {
                    _JoinSession = new RelayCommand(param => DoJoinSession());
                }
                return _JoinSession;
            }
        }
        private void DoJoinSession()
        {
            VttClient = new TcpClient();
            VttClient.ConnectAsync(IPAddress.Parse(JoinIp), 8085);
        }
        #endregion
        #region TransmitData
        private RelayCommand _TransmitData;
        public ICommand TransmitData
        {
            get
            {
                if (_TransmitData == null)
                {
                    _TransmitData = new RelayCommand(param => DoTransmitData());
                }
                return _TransmitData;
            }
        }
        private void DoTransmitData()
        {
            //NetworkStream stream = VttClient.GetStream();
            //byte[] msg = Encoding.ASCII.GetBytes(SentData);

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                //Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                //TcpClient client = server.AcceptTcpClient();
                //Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = VttClient.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    //Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    //Console.WriteLine("Sent: {0}", data);
                }
                VttClient.Close();
            }
        }
        #endregion

    }
}
