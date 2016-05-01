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
        private int[] cycle_data = new int[6];
        private string pump_name;
        
        private Timer timer;
        private readonly MainWindow mainForm;
        Window window = new Window();

        #region gets and sets
        public int COM_Port
        {
            get { return cycle_data[5]; }
        }

        public int Flow_Rate
        {
            get { return cycle_data[3]; }
        }
        public int Iterations
        {
            get { return cycle_data[4]; }
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

       
        public pumpControl(MainWindow parent)
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
            lbl_Cycle_on.Text = cycle_data[1].ToString();
            lbl_Cycle_off.Text = cycle_data[2].ToString();

            this.timer = new Timer();
        }

        private void UpdateUI()
        {
            if (pump_on) status_red.Fill = new SolidColorBrush(Colors.Green);
            else status_red.Fill = new SolidColorBrush(Colors.Red);

            lbl_iterations.Text = cyc_count.ToString();
          
        }

        #region start and cycles
        public void Start()
        {
            this.timer.Stop();
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
            this.cyc_count++;
            if (cyc_count != this.Cycle_data[4])
            {
                this.Pump_On = true;
                int start = cycle_on_interval();
                this.timer = new Timer
                {
                    Enabled = true,
                    Interval = start
                };

                timer.Elapsed += delegate { cycle_off(); };

                this.timer.Start();
            }
            else { MessageBox.Show(pump_name + " has completed given iterations"); }
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

            timer.Elapsed += delegate { cycle_on();};
            
            this.timer.Start();
        }
        #endregion


        #region getTimes
        private int cycle_on_interval()
        {
            int time = cycle_data[1];
       //     time *= 60;
            time *= 1000;
            return time;
        }
        private int cycle_off_interval()
        {
            int time = cycle_data[2];
            //time *= 60;
            time *= 1000;
            return time;
        }

        private int start_time_interval()
        {
            int time = cycle_data[0];
            //time = time * 60; //to seconds
            time = time * 1000; //to miliseconds

            return time;
        }
        #endregion
    }
}

