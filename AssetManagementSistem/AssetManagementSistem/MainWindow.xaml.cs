using Contracts;
using LocalControler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssetManagementSistem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

       

        public MainWindow()
        {
            InitializeComponent();
            OpenServer();
            OpenServerFromDevice();
        }

        void OpenServer()
        {

            ServiceHost svc = new ServiceHost(typeof(WriteAms));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(IWriteAms), binding, new Uri(String.Format("net.tcp://localhost:10100/WriteAms")));
            svc.Open();

        }

        void OpenServerFromDevice()
        {
            ServiceHost svc = new ServiceHost(typeof(WriteAms));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(IWriteAms), binding, new Uri(String.Format("net.tcp://localhost:10102/WriteAms")));
            svc.Open();

        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
