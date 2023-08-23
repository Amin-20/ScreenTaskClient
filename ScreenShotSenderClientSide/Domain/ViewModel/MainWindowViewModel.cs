using ScreenShotSenderClientSide.Command;
using ScreenShotSenderClientSide.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ScreenShotSenderClientSide.Domain.ViewModel
{
    public class MainWindowViewModel:BaseViewModel
    {
        public RelayCommand ConnectCommand { get; set; }

        [Obsolete]
        public MainWindowViewModel()
        {
            var port = 27001;
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            ConnectCommand = new RelayCommand((c) =>
            {
                try
                {
                    Thread thread = new Thread(() =>
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            NetworkHelper.Start(myIP, port);
                        });
                    });
                    thread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        //public void OnWindowClosing(object sender, CancelEventArgs e)
        //{
        //    NetworkHelper.WriteDataToServer(NetworkHelper.ExitCommand);
        //}
    }
}
