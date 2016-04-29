using Pump_Control.Controls;
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

namespace Pump_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<pumpControl> pumpList;
        

        public MainWindow()
        {
            InitializeComponent();

            this.pumpList = new List<pumpControl>();
        }

        private void btn_Add_Pump_Click(object sender, RoutedEventArgs e)
        {
            var pump = new pumpControl(this);

            this.pump_stack.Children.Add(pump);
        }
    }
}
