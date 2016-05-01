#define testing

using Pump_Contorl;
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
        private int cyc_count;
        private bool pump_on;
        private int[] cycle_data = new int[9];
        private string pump_name;
        private Coms.Serial control;
        private Timer timer;
        private readonly MainWindow mainForm;
        Window window = new Window();

#region gets and sets
        public int COM_Port
        {
            get { return cycle_data[8]; }
        }

        public int Flow_Rate
        {
            get { return cycle_data[6]; }
        }
        public int Iterations
        {
            get { return cycle_data[7]; }
        }

        public string Pump_name
        {
            get
            {
                return pump_name;
            }
            set
            {
                pump_name = value;
            }
        }
        public bool Pump_On
        {
            get
            {
                return pump_on;
            }
            set
            {
                pump_on = value;
                Dispatcher.Invoke(new Action(UpdateUI));
            }
        }

        public int[] Cycle_data
        {
            get
            {
                return cycle_data;
            }
            set
            {
                cycle_data = value;
                close();
            }
        }

        private void close()
        {
            window.Close();
        }



#endregion


        public pumpControl(MainWindow parent, int num)
        {

            InitializeComponent();

            window.Content = new input_details(this);
            window.Height = 207;
            window.Width = 210;
            window.ShowDialog();
            this.mainForm = parent;
            this.cyc_count = 0;


            txt_name.Text = pump_name;
            lbl_flow_rate.Text = Flow_Rate.ToString();
            lbl_Cycle_on.Text = cycle_data[1].ToString() + " min " + cycle_data[4].ToString() + "hrs";
            lbl_Cycle_off.Text = cycle_data[2].ToString() + " min " + cycle_data[5].ToString() + "hrs";

            Coms.Serial intil = new Coms.Serial(COM_Port, Flow_Rate, num);
            this.control = intil;

            this.timer = new Timer();
        }

        private void UpdateUI()
        {
            if (pump_on)
            {
                status_red.Fill = new SolidColorBrush(Colors.Green);
                lbl_status.Text = "Pump On";
            }

            else
            {
                status_red.Fill = new SolidColorBrush(Colors.Red);
                lbl_status.Text = "Pump Off";
            }

            lbl_iterations.Text = cyc_count.ToString();

        }

#region start and cycles
        public void Start()
        {
            cyc_count = 0;
            this.timer.Stop();
            this.lbl_status.Text = "Waiting to start";

            int start = start_time_interval();

            this.timer = new Timer
            {
                Enabled = true,
                Interval = start
            };

            timer.Elapsed += delegate { cycle_on(); };
            
            this.timer.Start();
        }


        private void cycle_on()
        {
            this.timer.Stop();
            
            if (cyc_count != this.Cycle_data[7])
            {
                this.Pump_On = true;
                int start = cycle_on_interval();
                this.timer = new Timer
                {
                    Enabled = true,
                    Interval = start
                };

                timer.Elapsed += delegate { cycle_off(); };
                control.pump_on();
                cyc_count++;
                this.timer.Start();

            }
            else { control.pump_off(); MessageBox.Show(pump_name + " has completed given iterations"); }
        }


        private void cycle_off()
        {
            this.timer.Stop();
            this.Pump_On = false;
            int start = cycle_off_interval();
            this.timer = new Timer
            {
                Enabled = true,
                Interval = start
            };

            timer.Elapsed += delegate { cycle_on(); };
            control.pump_off();
            this.timer.Start();
        }
#endregion


#region getTimes Intervals
        private int cycle_on_interval()
        {


            int time = (cycle_data[4] * 60) + cycle_data[1];  //hrs * 60 + min
            time *= 60; //to seconds
            time *= 1000; //to mili
#if testing
            time = 1000;
#endif
            return time;
        }
        private int cycle_off_interval()
        {
            int time = (cycle_data[5] * 60) + cycle_data[2];  //hrs * 60 + min
            time *= 60; //to seconds
            time *= 1000; //to mili
#if testing
            time = 1500;
#endif
            return time;
        }

        private int start_time_interval()
        {
            int time = (cycle_data[3] * 60) + cycle_data[0];  //hrs * 60 + min
            time *= 60; //to seconds
            time *= 1000; //to mili
#if testing
            time = 2000;
#endif
            return time;
        }
#endregion
    }
}

