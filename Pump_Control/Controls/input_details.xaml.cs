using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Pump_Control.Controls
{
    /// <summary>
    /// Interaction logic for input_details.xaml
    /// </summary>
    public partial class input_details : UserControl
    {
        private readonly pumpControl mainInput;
       


        public input_details(pumpControl parent)
        {
            InitializeComponent();
            
            mainInput = parent;
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            int[] data = new int[6];
            data[0] = Int32.Parse(cycle_time_start.Text.ToString()); data[1] = Int32.Parse(cycle_time_on.Text.ToString()); data[2] = Int32.Parse(cycle_time_off.Text.ToString());
            data[3] = Int32.Parse(txt_flow.Text.ToString()); data[4] = Int32.Parse(txt_iter.Text.ToString()); data[5]= Int32.Parse(txt_port.Text.ToString());


            mainInput.Pump_name = txt_name.Text.ToString();
            mainInput.Cycle_data = data;
                        
        }
    }
}
