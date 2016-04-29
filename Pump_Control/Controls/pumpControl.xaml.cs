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

namespace Pump_Control.Controls
{
    /// <summary>
    /// Interaction logic for pumpControl.xaml
    /// </summary>
    public partial class pumpControl : UserControl
    {
        private int count;
        private int[] cycle_data = new int[3];
        private Timer timer;
        private readonly MainWindow mainForm;

        public pumpControl(MainWindow parent)
        {
            InitializeComponent();

            this.mainForm = parent;
            this.count = 0;
            
            this.timer = new Timer();
        }

        public void Start() {
            this.timer.Stop();
            this.timer = new Timer
            {
                Enabled = true,
                Interval = 3000
            };

            this.timer.Elapsed += delegate { };
            this.timer.Start();
        }

       private int start_time_interval()
        {
            int time = cycle_data[0];
            time = time * 60; //to seconds
            time = time * 1000; //to miliseconds

            return time;
        }
    }
}
