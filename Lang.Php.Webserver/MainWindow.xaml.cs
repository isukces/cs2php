using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Lang.Php.Webserver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            {
                HttpRequest r = HttpRequest.Parse(HttpRequest.Example);
            }
            {
                ServerEngine e = ServerEngine.Instance;
                e.DocumentRoot = Path.Combine(
                     Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "cs2php",
                    "webserver");

                e.OnLog += e_OnLog;
                string[] args = Environment.GetCommandLineArgs();
                foreach (var i in args.Skip(1))
                    e.Load(i);
                e.ListenPort = 11000;
                e.StartListening();
            }
        }

        List<string> loglines = new List<string>();
        void e_OnLog(object sender, ServerEngine.OnLogEventArgs e)
        {
            loglines.Add(e.Text);
            if (loglines.Count > 100)
                loglines.RemoveAt(0);
            string t = string.Join("\r\n", loglines.AsEnumerable().Reverse());

            Dispatcher.Invoke(
            () => log.Text = t
                );

        }


    }
}
