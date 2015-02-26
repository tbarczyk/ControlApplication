using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
    
        private WebClient _wc;
        private string _HtmlResult;
        private Robot _robot;

        public Form3()
        {
            InitializeComponent();
            
            _wc = new WebClient();
            _wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            
            
        }

        private void SendPost(string s)
        {
            try
            {
                _HtmlResult = _wc.UploadString(s, "");
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        private void SendGet(string s)
        {
            try
            {
                Console.WriteLine(_wc.DownloadString(s));
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            e.SuppressKeyPress = true;
            this.KeyDown -= new KeyEventHandler(Form1_KeyDown);
            switch (e.KeyCode)
            {
                case Keys.Up:
                    SendPost("http://192.168.0.111:8000/motor/pwm/0.5");
                    SendPost("http://192.168.0.111:8000/motor/*/dir/0,0,0,0");
                    break;
                case Keys.Down:
                    SendPost("http://192.168.0.111:8000/motor/pwm/0.5");
                    SendPost("http://192.168.0.111:8000/motor/*/dir/1,1,1,1");
                    break;
                case Keys.Right:
                    SendPost("http://192.168.0.111:8000/motor/pwm/0.5");
                    SendPost("http://192.168.0.111:8000/motor/*/dir/1,1,0,0");
                    break;
                case Keys.Left:
                    SendPost("http://192.168.0.111:8000/motor/pwm/0.5");
                    SendPost("http://192.168.0.111:8000/motor/*/dir/0,0,1,1");
                    break;
                    
                case Keys.Q:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/up");
                    break;
                case Keys.W:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/down");
                    break;
                case Keys.D:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/up");
                    break;
                case Keys.F:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/down");
                    break;
                case Keys.A:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/up");
                    break;
                case Keys.S:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/down");
                    break;
                case Keys.E:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/up");
                    break;
                case Keys.R:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/down");
                    break;
                case Keys.C:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/5/up");
                    break;
                case Keys.V:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/5/down");
                    break;
                case Keys.Z:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/up");
                    break;
                case Keys.X:
                    SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/down");
                    break;

                case Keys.B:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/1/up");
                    break;
                case Keys.N:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/1/down");
                    break;
                case Keys.L:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/up");
                    break;
                case Keys.K:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/down");
                    break;
                case Keys.Oemcomma:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/up");
                    break;
                case Keys.M:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/down");
                    break;
                case Keys.I:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/up");
                    break;
                case Keys.U:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/down");
                    break;
                case Keys.P:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/up");
                    break;
                case Keys.O:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/down");
                    break;
                case Keys.H:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/up");
                    break;
                case Keys.J:
                    SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/down");
                    break;
                   
                default:
                    Console.WriteLine("Unknown2");
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
                {
                    this.KeyDown += new KeyEventHandler(Form1_KeyDown);

                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            SendPost("http://192.168.0.111:8000/motor/pwm/0");
                            break;
                        case Keys.Down:
                            SendPost("http://192.168.0.111:8000/motor/pwm/0");
                            break;
                        case Keys.Left:
                            SendPost("http://192.168.0.111:8000/motor/pwm/0");
                            break;
                        case Keys.Right:
                            SendPost("http://192.168.0.111:8000/motor/pwm/0");
                            break;

                        case Keys.Q:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/1/position");
                            break;
                        case Keys.W:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/1/position");
                            break;
                        case Keys.D:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/2/position");
                            break;
                        case Keys.F:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/2/position");
                            break;
                        case Keys.A:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/3/position");
                            break;
                        case Keys.S:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/3/position");
                            break;
                        case Keys.E:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/4/position");
                            break;
                        case Keys.R:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/4/position");
                            break;
                        case Keys.C:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/5/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/5/position");
                            break;
                        case Keys.V:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/5/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/5/position");
                            break;
                        case Keys.Z:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/0/position");
                            break;
                        case Keys.X:
                            SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/0/servo/0/position");
                            break;

                        case Keys.P:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/5/position");
                            break;
                        case Keys.O:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/5/position");
                            break;
                        case Keys.L:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/2/position");
                            break;
                        case Keys.K:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/2/position");
                            break;
                        case Keys.Oemcomma:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/3/position");
                            break;
                        case Keys.M:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/3/position");
                            break;
                        case Keys.I:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/4/position");
                            break;
                        case Keys.U:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/4/position");
                            break;
                        case Keys.B:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/1/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/1/position");
                            break;
                        case Keys.N:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/1/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/1/position");
                            break;
                        case Keys.J:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/0/position");
                            break;
                        case Keys.H:
                            SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/stop");
                            SendGet("http://192.168.0.111:8000/manipulator/1/servo/0/position");
                            break;


                        default:
                            Console.WriteLine("Unknown");
                            break;

                            
                    }
                }

        

        //private void Form1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    trackBar1.Enabled=true;
           
        //}

        //private void Form1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    trackBar1.Enabled = false;
           
        //}



    }
    }

