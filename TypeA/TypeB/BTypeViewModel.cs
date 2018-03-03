using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Collections.ObjectModel;

namespace TypeB
{
    public class BTypeViewModel : INotifyPropertyChanged
    {
        public struct DataItemsPortAmount { int port; int amount;
            private int v1;
            private int v2;

            public DataItemsPortAmount(int v1, int v2) : this()
            {
                this.v1 = v1;
                this.v2 = v2;
            }
        }
        public string SelectedPort { get; set; }

        private ObservableCollection<DataItemsPortAmount> m_ItemPort;
        const int INIT_PORT = 9229;
        private int m_PortCounter = 0;
        public System.Windows.Input.ICommand StartCommand { get; private set; }
        public System.Windows.Input.ICommand StopCommand { get; private set; }

        public ObservableCollection<DataItemsPortAmount> ItemPort { get { return m_ItemPort; } set { m_ItemPort = value; OnPropertyChanged("ItemPort"); } }
        private string m_NewPortEntered;
        public string NewPortEntered { get { return m_NewPortEntered; } set { m_NewPortEntered = value;OnPropertyChanged("NewPortEntered"); } }

        private ConcurrentDictionary<int, BackgroundWorker> m_Dicinternal = new ConcurrentDictionary<int, BackgroundWorker>();
        private ConcurrentDictionary<int, int> m_DicProgres = new ConcurrentDictionary<int, int>();
        public BTypeViewModel()
        {
            ItemPort = new ObservableCollection<DataItemsPortAmount>();
            NewPortEntered = INIT_PORT.ToString();
            StartCommand = new RelayCommand(StartCommandExe);
            StopCommand = new RelayCommand(StopCommandExe);
        }

        private void StopCommandExe()
        {
            if (SelectedPort != null)
            {
                int tempSelecterPort = int.Parse(SelectedPort);

                BackgroundWorker bw;
                int i;
                m_Dicinternal.TryRemove(tempSelecterPort, out bw);
                m_DicProgres.TryRemove(tempSelecterPort, out i);

                //ItemPort.Remove(tempSelecterPort);
                //int location = ItemPort.FindIndex(t=>t.port== tempSelecterPort);
                //if (location < ItemPort.Count && location >= 0)
                //{
                //    ItemPort.Remove(tempSelecterPort);
                //    OnPropertyChanged("ItemPort");
                //}
            }
        }

        private void StartCommandExe()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation =true;
            bw.DoWork += new DoWorkEventHandler(bwOnStartSet);
            bw.ProgressChanged += new ProgressChangedEventHandler(bwProgChange);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwComplite);
            NewPortEntered = (INIT_PORT + m_PortCounter).ToString();
            m_Dicinternal.TryAdd(INIT_PORT + m_PortCounter, bw);
            m_DicProgres.TryAdd(INIT_PORT + m_PortCounter, 0);
            ItemPort.Add(new DataItemsPortAmount((INIT_PORT + m_PortCounter++),0));
            OnPropertyChanged("ItemPort");
        }

        private void bwComplite(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void bwProgChange(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void bwOnStartSet(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            UdpClient udp = new UdpClient(INIT_PORT);
            byte[] msg = Encoding.ASCII.GetBytes("CONNECT\r\n");
            udp.Connect("Matan-NB", INIT_PORT);
            // do do client send
            try
            {
                udp.Send(msg, msg.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }




        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
