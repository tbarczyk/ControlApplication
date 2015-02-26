using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace WindowsFormsApplication1
{

    public class Drawer : EventArgs
    {
        public Graphics graphics;
        public Graphics graphics2;
        public Bitmap bufforBmp;
        public Bitmap bufforBmp2;
        public Graphics bufferG;
        public Graphics bufferG2;
        public bool WithHistory;
        private int boxWidth;
        private int boxWidth2;
        private int boxHeight;
        private int boxHeight2;
        public Drawer(Graphics g, int width, int height, Graphics g2, int width2, int height2)
        {
            graphics = g;
            graphics2 = g2;
            bufforBmp = new System.Drawing.Bitmap(width, height);
            bufforBmp2 = new System.Drawing.Bitmap(width2, height2);
            boxHeight = height;
            boxHeight2 = height2;
            boxWidth = width;
            boxWidth2 = width2;
            bufferG = Graphics.FromImage(bufforBmp);
            bufferG2 = Graphics.FromImage(bufforBmp2);

        }

        public void Draw(Point3D p1, Point3D p2)
        {
            bufferG.DrawLine(System.Drawing.Pens.Red, (int)p1.X+100, boxHeight - (int)p1.Y, (int)p2.X+100, boxHeight - (int)p2.Y);
            bufferG.FillRectangle(new SolidBrush(Color.Black), (int)p1.X+100, boxHeight - (int)p1.Y, 5, 5);
           
            bufferG2.DrawLine(System.Drawing.Pens.Red, (int)p1.Z + 300, boxHeight2 - (int)p1.Y, (int)p2.Z + 300, boxHeight2 - (int)p2.Y);
            bufferG2.FillRectangle(new SolidBrush(Color.Black), (int)p1.Z + 300, boxHeight2 - (int)p1.Y, 5, 5);
        
        }

        

       
    }
}
