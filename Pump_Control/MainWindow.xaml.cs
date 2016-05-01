using Pump_Control.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pump_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<pumpControl> pumpList;
        private int num_o_pumps = 0;
        private Timer timer;
        DateTime start;
        TimeSpan elapsed;

        public MainWindow()
        {
            InitializeComponent();

            this.pumpList = new List<pumpControl>();
        }

        private void btn_Add_Pump_Click(object sender, RoutedEventArgs e)
        {
            num_o_pumps++;
            var pump = new pumpControl(this,num_o_pumps);
            this.pumpList.Add(pump);
            this.pump_stack.Children.Add(pump);
        }

        private void btn_timeline_start_Click(object sender, RoutedEventArgs e)
        {
            stck_time.Visibility = System.Windows.Visibility.Visible;
            start = DateTime.Now;
            string time = start.ToString("hh:mm:ss"); // includes leading zeros
            lbl_start_time.Text = time;
            lbl_running.Background = new SolidColorBrush(Colors.Green);
            lbl_running.Text = "Running";
            foreach (pumpControl p in pumpList)
            {
                p.Start();
            }
            this.timer = new Timer
            {
                Enabled = true,
                Interval = 5000
            };

            timer.Elapsed += delegate { updateUI(); };

            this.timer.Start();
        }
        private void updateUI()
        {
            this.timer.Stop();
            DateTime elapse = DateTime.Now;
            elapsed = elapse.Subtract(start);
            
            this.timer = new Timer
            {
                Enabled = true,
                Interval = 5000
            };

            timer.Elapsed += delegate { updateUI(); };
            Dispatcher.Invoke(new Action(label_update));
            this.timer.Start();
        }
        private void label_update()
        {
            lbl_elapsed.Text = elapsed.ToString(@"hh\:mm\:ss");
        }
    }
}
