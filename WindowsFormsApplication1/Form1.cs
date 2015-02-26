using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Net;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public double Smaller_x, Smaller_y, Smaller_z;
        public double Bigger_x, Bigger_y, Bigger_z;
        
        private double[] result;
       
        
        public Robot _RobotBigger;
        public Robot _RobotSmaller;
        public Graphics graphics;
        private Drawer myDrawer;
        private Client myClient;
        private CollisionDetector myCollisionDetector;
        private Task DetectionTask;
       

        public Form1()
        {   
            InitializeComponent();
            InitializeKinematics();
            //label3.Visible = false;
            DetectionTask = new Task(DetectCollision);
            DetectionTask.Start();
                 
        }

        public void DetectCollision()
        {
            while (true)
            {
                Thread.Sleep(100);
                //ShowCollisionInfo(myCollisionDetector.collisionDetected);
            }
        }

        public void ShowCollisionInfo(bool isCollision)
        {
            
            if (isCollision)
            {
                //myClient.SendStartReal();
                label3.Visible = true;
                label3.Refresh();
                //Thread.Sleep(1000);
            }
            else
            {
                myClient.SendReal();
                label3.Visible = false;
                label3.Refresh();
            }
        }

        public void InitializeKinematics()
        {
            myDrawer = new Drawer(pictureBox1.CreateGraphics(), pictureBox1.Width, pictureBox1.Height, pictureBox2.CreateGraphics(), pictureBox2.Width,pictureBox2.Height);
            myDrawer.WithHistory = checkBox3.Checked;
            
            _RobotBigger = new Robot("big",
                49, 59, 205, 127, 58, 164, 
                new double[5] { 426, 314, 291, 346, 300 },
                new double[6] { 
                    180 * Constants.TORAD, 
                    90 * Constants.TORAD, 
                    180 * Constants.TORAD, 
                    0 * Constants.TORAD, 
                    0 * Constants.TORAD, 
                    0 }
                ); 
            _RobotSmaller = new Robot("small",
                49, 59, 143, 98, 58, 164, 
                new double[5] { 390, 226, 433, 305, 364 },
                new double[6] { 
                    180 * Constants.TORAD, 
                    90 * Constants.TORAD, 
                    180 * Constants.TORAD, 
                    0 * Constants.TORAD, 
                    0 * Constants.TORAD, 
                    0 }
                );

            myClient = new Client(_RobotSmaller, _RobotBigger);

            myCollisionDetector = new CollisionDetector(myDrawer, _RobotSmaller, _RobotBigger);

            //PREPARE TO HAVE TCP MATRIX
            _RobotBigger.SolveDirectKinematics();
            _RobotSmaller.SolveDirectKinematics();

            //Initial values to inverse kinematics
            Bigger_x = _RobotBigger.MainTCP[0, 3];
            Bigger_y = _RobotBigger.MainTCP[1, 3];
            Bigger_z = _RobotBigger.MainTCP[2, 3];

            Smaller_x = _RobotSmaller.MainTCP[0, 3];
            Smaller_y = _RobotSmaller.MainTCP[1, 3];
            Smaller_z = _RobotSmaller.MainTCP[2, 3];

            _RobotSmaller.ComputeRealTheta();
            myClient.SendThetaToRobots();
        }
        
        private void XUPButton_Pressed(object sender, MouseEventArgs e)
        {
            Bigger_x += 4;
            _RobotBigger.MainTCP[0, 3] = Bigger_x++;
            result = _RobotBigger.SolveInverseKinematics();           
        }
        private void XDOWNButton_Pressed(object sender, MouseEventArgs e)
        {
            Bigger_x -= 4;
            _RobotBigger.MainTCP[0, 3] = Bigger_x--;
            result = _RobotBigger.SolveInverseKinematics();
        }
        private void YUPButton_Pressed(object sender, MouseEventArgs e)
        {
            Bigger_y += 4;
            _RobotBigger.MainTCP[1, 3] = Bigger_y++;
            result = _RobotBigger.SolveInverseKinematics();
        }
        private void YDOWNButton_Pressed(object sender, MouseEventArgs e)
        {
            Bigger_y -= 4;
            _RobotBigger.MainTCP[1, 3] = Bigger_y--;
            result = _RobotBigger.SolveInverseKinematics();
        }
        private void ZUPButton_Pressed(object sender, MouseEventArgs e)
        {
            Bigger_z += 4;
            _RobotBigger.MainTCP[2, 3] = Bigger_z++;
            result = _RobotBigger.SolveInverseKinematics();
        }
        private void ZDOWNButton_Pressed(object sender, MouseEventArgs e)
        {
            Bigger_z -= 4;
            _RobotBigger.MainTCP[2, 3] = Bigger_z--;
            result = _RobotBigger.SolveInverseKinematics();
        }

        private void checkBox9_MouseDown(object sender, MouseEventArgs e)
        {
            Smaller_x += 4;
            _RobotSmaller.MainTCP[0, 3] = Smaller_x++;
            result = _RobotSmaller.SolveInverseKinematics();
        }
        private void checkBox7_MouseDown(object sender, MouseEventArgs e)
        {
            Smaller_x -= 4;
            _RobotSmaller.MainTCP[0, 3] = Smaller_x--;
            result = _RobotSmaller.SolveInverseKinematics();
        }
        private void checkBox5_MouseDown(object sender, MouseEventArgs e)
        {
            Smaller_y += 4;
            _RobotSmaller.MainTCP[1, 3] = Smaller_y++;
            result = _RobotSmaller.SolveInverseKinematics();
        }
        private void checkBox4_MouseDown(object sender, MouseEventArgs e)
        {
            Smaller_y -= 4;
            _RobotSmaller.MainTCP[1, 3] = Smaller_y--;
            result = _RobotSmaller.SolveInverseKinematics();
        }
        private void checkBox8_MouseDown(object sender, MouseEventArgs e)
        {
            Smaller_z += 4;
            _RobotSmaller.MainTCP[2, 3] = Smaller_z++;
            result = _RobotSmaller.SolveInverseKinematics();
        }
        private void checkBox6_MouseDown(object sender, MouseEventArgs e)
        {
            Smaller_z -= 4;
            _RobotSmaller.MainTCP[2, 3] = Smaller_z--;
            result = _RobotSmaller.SolveInverseKinematics();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            myDrawer.WithHistory = checkBox3.Checked;
            //myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            myClient.SendThetaToRobots();
            //ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }





        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _RobotBigger.theta[0] = (270+trackBar1.Value) * Constants.TORAD;
            textBox1.Text = (_RobotBigger.theta[0] * Constants.TODEG-180).ToString();
            textBox1.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);

           
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            _RobotBigger.theta[1] = (90+trackBar2.Value) * Constants.TORAD;
            textBox2.Text = (_RobotBigger.theta[1] * Constants.TODEG-90).ToString();
            textBox2.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            _RobotBigger.theta[2] = (180+trackBar3.Value) * Constants.TORAD;
            textBox3.Text = (_RobotBigger.theta[2] * Constants.TODEG-90).ToString();
            textBox3.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            _RobotBigger.theta[3] = (0+trackBar4.Value) * Constants.TORAD;
            textBox4.Text = (_RobotBigger.theta[3] * Constants.TODEG).ToString();
            textBox4.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);

        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            _RobotBigger.theta[4] = trackBar5.Value * Constants.TORAD;
            textBox5.Text = (_RobotBigger.theta[4] * Constants.TODEG).ToString();
            textBox5.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            _RobotSmaller.theta[0] = (270+trackBar6.Value) * Constants.TORAD;
            textBox6.Text = (_RobotSmaller.theta[0] * Constants.TODEG-180).ToString();
            textBox6.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            _RobotSmaller.theta[1] = (90+trackBar7.Value)*Constants.TORAD;
            textBox7.Text = (_RobotSmaller.theta[1] * Constants.TODEG-90).ToString();
            textBox7.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            _RobotSmaller.theta[2] = (180+trackBar8.Value) * Constants.TORAD;
            textBox8.Text = (_RobotSmaller.theta[2] * Constants.TODEG-90).ToString();
            textBox8.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            _RobotSmaller.theta[3] = (0+trackBar9.Value) * Constants.TORAD;
            textBox9.Text = (_RobotSmaller.theta[3] * Constants.TODEG).ToString();
            textBox9.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }
        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            _RobotSmaller.theta[4] = trackBar10.Value * Constants.TORAD;
            textBox10.Text = (_RobotSmaller.theta[4] * Constants.TODEG).ToString();
            textBox10.Refresh();
            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
            ShowCollisionInfo(myCollisionDetector.collisionDetected);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(_RobotSmaller,_RobotBigger);
            form2.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myClient.SendStartReal();
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;
            trackBar5.Value = 0;
            trackBar6.Value = 0;
            trackBar7.Value = 0;
            trackBar8.Value = 0;
            trackBar9.Value = 0;
            trackBar10.Value = 0;

            _RobotBigger.theta[0] = (270 + trackBar1.Value) * Constants.TORAD;
            textBox1.Text = (_RobotBigger.theta[0] * Constants.TODEG - 180).ToString();
            textBox1.Refresh();

            _RobotBigger.theta[1] = (90 + trackBar2.Value) * Constants.TORAD;
            textBox2.Text = (_RobotBigger.theta[1] * Constants.TODEG - 90).ToString();
            textBox2.Refresh();

            _RobotBigger.theta[2] = (180 + trackBar3.Value) * Constants.TORAD;
            textBox3.Text = (_RobotBigger.theta[2] * Constants.TODEG - 90).ToString();
            textBox3.Refresh();

            _RobotBigger.theta[3] = (0 + trackBar4.Value) * Constants.TORAD;
            textBox4.Text = (_RobotBigger.theta[3] * Constants.TODEG).ToString();
            textBox4.Refresh();

            _RobotBigger.theta[4] = trackBar5.Value * Constants.TORAD;
            textBox5.Text = (_RobotBigger.theta[4] * Constants.TODEG).ToString();
            textBox5.Refresh();

            _RobotSmaller.theta[0] = (270 + trackBar6.Value) * Constants.TORAD;
            textBox6.Text = (_RobotSmaller.theta[0] * Constants.TODEG - 180).ToString();
            textBox6.Refresh();

            _RobotSmaller.theta[1] = (90 + trackBar7.Value) * Constants.TORAD;
            textBox7.Text = (_RobotSmaller.theta[1] * Constants.TODEG - 90).ToString();
            textBox7.Refresh();

            _RobotSmaller.theta[2] = (180 + trackBar8.Value) * Constants.TORAD;
            textBox8.Text = (_RobotSmaller.theta[2] * Constants.TODEG - 90).ToString();
            textBox8.Refresh();

            _RobotSmaller.theta[3] = (0 + trackBar9.Value) * Constants.TORAD;
            textBox9.Text = (_RobotSmaller.theta[3] * Constants.TODEG).ToString();
            textBox9.Refresh();

            _RobotSmaller.theta[4] = trackBar10.Value * Constants.TORAD;
            textBox10.Text = (_RobotSmaller.theta[4] * Constants.TODEG).ToString();
            textBox10.Refresh();

            myCollisionDetector.DrawArms(_RobotSmaller, _RobotBigger);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        

    }
}
