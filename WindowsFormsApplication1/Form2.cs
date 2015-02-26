using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Robot rob1;
        public Robot rob2;
        public Form2(Robot r1, Robot r2)
        {
            rob1 = r1;
            rob2 = r2;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rob1.theta[3] = (Convert.ToDouble(textBox1.Text) * Constants.TORAD);
            rob2.theta[3] = (Convert.ToDouble(textBox4.Text) * Constants.TORAD);
            rob1.theta[4] = (Convert.ToDouble(textBox2.Text) * Constants.TORAD);
            rob2.theta[4] = (Convert.ToDouble(textBox3.Text) * Constants.TORAD);
            
            
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            rob1.realTheta[0] = rob1.realTheta0[0];
            rob1.realTheta[1] = rob1.realTheta0[1];
            rob1.realTheta[3] = rob1.realTheta0[3];
            rob1.realTheta[4] = rob1.realTheta0[4];

            rob1.realTheta0[2] = rob1.realTheta[2] = 433;

            rob1.theta[0] = rob1.theta0[0] = 180 * Constants.TORAD;
            rob1.theta[1] = rob1.theta0[1] = 90 * Constants.TORAD;
            rob1.theta[2] = rob1.theta0[2] = 180 * Constants.TORAD;
            rob1.theta[3] = rob1.theta0[3] = 0 * Constants.TORAD;
            rob1.theta[4] = rob1.theta0[4] = 0 * Constants.TORAD;


            rob2.realTheta[0] = rob2.realTheta0[0];
            rob2.realTheta[1] = rob2.realTheta0[1];
            rob2.realTheta[3] = rob2.realTheta0[3];
            rob2.realTheta[4] = rob2.realTheta0[4];

            rob2.realTheta0[2] = rob2.realTheta[2] = 291;

            rob2.theta[0] = rob2.theta0[0] = 180 * Constants.TORAD;
            rob2.theta[1] = rob2.theta0[1] = 90 * Constants.TORAD;
            rob2.theta[2] = rob2.theta0[2] = 180 * Constants.TORAD;
            rob2.theta[3] = rob2.theta0[3] = 0 * Constants.TORAD;
            rob2.theta[4] = rob2.theta0[4] = 0 * Constants.TORAD;

            rob1.SolveDirectKinematics();
            rob2.SolveDirectKinematics();

            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rob1.realTheta[0] = rob1.realTheta0[0];
            rob1.realTheta[1] = rob1.realTheta0[1];
            rob1.realTheta[3] = rob1.realTheta0[3];
            rob1.realTheta[2] = rob1.realTheta0[2];

            rob1.realTheta0[4] = rob1.realTheta[4] = 215;

            rob1.theta[0] = rob1.theta0[0] = 180 * Constants.TORAD;
            rob1.theta[1] = rob1.theta0[1] = 90 * Constants.TORAD;
            rob1.theta[2] = rob1.theta0[2] = 90 * Constants.TORAD;
            rob1.theta[3] = rob1.theta0[3] = 0 * Constants.TORAD;
            rob1.theta[4] = rob1.theta0[4] = 90 * Constants.TORAD;


            rob2.realTheta[0] = rob2.realTheta0[0];
            rob2.realTheta[1] = rob2.realTheta0[1];
            rob2.realTheta[3] = rob2.realTheta0[3];
            rob2.realTheta[2] = rob2.realTheta0[2];

            rob2.realTheta0[4] = rob2.realTheta[4] = 495;

            rob2.theta[0] = rob2.theta0[0] = 180 * Constants.TORAD;
            rob2.theta[1] = rob2.theta0[1] = 90 * Constants.TORAD;
            rob2.theta[2] = rob2.theta0[2] = 90 * Constants.TORAD;
            rob2.theta[3] = rob2.theta0[3] = 0 * Constants.TORAD;
            rob2.theta[4] = rob2.theta0[4] = 90 * Constants.TORAD;

            rob1.SolveDirectKinematics();
            rob2.SolveDirectKinematics();

            Close();
        }
    }
}
