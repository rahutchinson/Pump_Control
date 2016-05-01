using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pump_Contorl
{
    class Coms
    {
        public class Serial
        {
            string comport = "COM4";
            int baud = 4800;
            int flow = 0;
            int pump_num;



            string message = ((char)5).ToString();  //ENQ
            string message1 = ((char)2).ToString(); //STK
            string cr = ((char)'\r').ToString();

            public Serial()
            {

            }

            /// <summary>
            /// Constructor for class Serial.. 
            /// Initilizes checks pumps and then setups up for start.
            /// </summary>
            public Serial(int port, int flow, int num)
            {
                comport = port_string(port);
                this.flow = flow;
                pump_num = num;
                try
                {
                    SerialPort port_ = new SerialPort(comport, baud, Parity.Odd, 7, StopBits.One);
                    port_.Open();


                    port_.Write(message + cr);
                    System.Threading.Thread.Sleep(130);
                    port_.DiscardInBuffer();
                    port_.DiscardOutBuffer();
                    port_.Write(string.Format(message1));
                    port_.Write("P0" + pump_num + cr);
                    System.Threading.Thread.Sleep(130);
                    port_.Write(string.Format(message1));
                    port_.Write("P0"+ pump_num + "R" + cr);
                    //end_config


                    port_.Close();
                }
                catch (Exception e)
                {
                    
                }
                
            }



            public void pump_on()
            {

                SerialPort port_ = new SerialPort(comport, baud, Parity.Odd, 7, StopBits.One);
                System.Threading.Thread.Sleep(20);
                port_.Open();
                port_.Write(string.Format(message1));
                port_.Write("P0"+ pump_num + "G0" + cr);
                port_.Write(string.Format(message1));
                port_.WriteLine("P0"+ pump_num + "S+" + flow + cr);

                port_.Close();


            }


            internal void pump_off()
            {
                SerialPort port = new SerialPort(comport, baud, Parity.Odd, 7, StopBits.One);
                port.Open();
                port.Write(string.Format(message1));
                port.WriteLine("P0"+ pump_num+"H" + cr);

                port.Close();

            }
            private string port_string(int port)
            {
                switch (port)
                {
                    case 1: return "COM1";
                    case 2: return "COM2";
                    case 3: return "COM3";
                    case 4: return "COM4";
                    case 5: return "COM5";
                    case 6: return "COM6";
                    case 7: return "COM7";
                    case 8: return "COM8";
                    case 9: return "COM9";
                    case 10: return "COM10";
                    default: return "COM4";
                }
            }
        }
    }
}