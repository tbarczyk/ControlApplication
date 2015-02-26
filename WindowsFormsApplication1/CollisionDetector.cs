using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using System.Windows.Media.Media3D;
using System.Media;

namespace WindowsFormsApplication1
{
    class CollisionDetector
    {
        public bool collisionDetected;
        public Drawer armsDrawer;
        private Robot[] robot;
        private List<Point3D>[] robotPoints;
        private List<Point3D>[] mainPoints;
        private List<Point>[] robotYintervals;
        private double[] Ymax;
        private List<Point3D> foundPoints1;
        private List<Point3D> foundPoints2;
        private SoundPlayer player; 
        
        private List<int>[] intervalsWithY;
        public CollisionDetector(Drawer d, Robot r1, Robot r2)
        {
            collisionDetected = true;
            armsDrawer = d;
            robot = new Robot[2] { r1, r2 };
            robotPoints = new List<Point3D>[2];
            robotYintervals = new List<Point>[2];
            mainPoints = new List<Point3D>[2];
            intervalsWithY = new List<int>[2];
            foundPoints1 = new List<Point3D>();
            foundPoints2 = new List<Point3D>();
            player = new SoundPlayer();
            player.SoundLocation = "C:/Users/Admin/Desktop/inv - przerobione formy/beep17.wav";
            
            player.Play();
            Ymax = new double[2];
            for(int i=0;i<2;i++)
            {
                robotPoints[i]=new List<Point3D>();
                robotYintervals[i]=new List<Point>();
                intervalsWithY[i] = new List<int>();
                mainPoints[i] = new List<Point3D>();
            }
           
        }


        void PlaySound()
        {
            player.Play();
        }

        public void ComputePoints(Robot r1, Robot r2)
        {
            for(int i=0;i<2;i++)
            {
                robotPoints[i].Clear();
                mainPoints[i].Clear();
            }
            for (int i = 0; i < 7; i++)
            {
                var T2 = r2.GetTransformationMatrix(0, i);
                var T1 = r1.GetTransformationMatrix(0, i);
                var p2 = new Point3D((int)T2[0, 3] + 200, (int)T2[1, 3], (int)T2[2, 3]);
                var p1 = new Point3D((int)T1[0, 3], (int)T1[1, 3], (int)T1[2, 3]);
                robotPoints[1].Add(p2);
                robotPoints[0].Add(p1);
            }
            for(int i=0;i<2;i++)
            {
                mainPoints[i].Add(robotPoints[i][0]);
                mainPoints[i].Add(robotPoints[i][1]);
                mainPoints[i].Add(robotPoints[i][3]);
                mainPoints[i].Add(robotPoints[i][4]);
                mainPoints[i].Add(robotPoints[i][6]);
            }
        }

        public void DrawArms(Robot r1, Robot r2)
        {
            r1.GetActualTheta("Robot Smaller");
            r2.GetActualTheta("Robot Bigger");
            
            ComputePoints(r1, r2);
            if (!armsDrawer.WithHistory)
            {
                armsDrawer.bufferG.Clear(Color.White);
                armsDrawer.bufferG2.Clear(Color.White);
            }
            for (int i = 0; i < 6; i++)
            {
                armsDrawer.Draw(robotPoints[1][i], robotPoints[1][i + 1]); 
                armsDrawer.Draw(robotPoints[0][i], robotPoints[0][i + 1]);
            }
            armsDrawer.graphics.DrawImage(armsDrawer.bufforBmp, 0, 0);
            armsDrawer.graphics2.DrawImage(armsDrawer.bufforBmp2, 0, 0);
            
            ComputeFunctions();
        }


        public void ComputeFunctions()
        {
            robotYintervals[0].Clear();
            robotYintervals[1].Clear();

            Ymax[0] = Ymax[1] = 0; 
            //Y max for each robot
            for(int i=0;i<robotPoints[0].Count();i++)
            {
                if (robotPoints[0][i].Y >= Ymax[0]) Ymax[0] = robotPoints[0][i].Y; 
                if (robotPoints[1][i].Y >= Ymax[1]) Ymax[1] = robotPoints[1][i].Y; 
            }
            //Intervals (start;end)
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < mainPoints[i].Count()-1; j++)
                {
                    if (mainPoints[i][j].Y > mainPoints[i][j + 1].Y)
                        robotYintervals[i].Add(new Point((int)mainPoints[i][j + 1].Y, (int)mainPoints[i][j].Y));
                    else
                        robotYintervals[i].Add(new Point((int)mainPoints[i][j].Y, (int)mainPoints[i][j + 1].Y));
                }
            }

            CheckCollision();

        }
        public void CheckCollision()
        {
            
            //CHOOSE LESSER Ymax from each robot
            var Y = Math.Min(Ymax[0], Ymax[1]);

            //MAIN LOOP
            collisionDetected = false;
            //Console.WriteLine("");
            for (int y = 0; y < Y + 1; )
            {
                foundPoints1.Clear();
                foundPoints2.Clear();
                y++;
                //if (y >= Y) y = Y;   << to reduce computational complexity

                foreach (List<int> l in intervalsWithY)
                    l.Clear();

                //SEARCHING INTERVALS WITH THE Y
                for (int interval = 0; interval < robotYintervals[0].Count(); interval++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (y >= robotYintervals[i][interval].X && y <= robotYintervals[i][interval].Y)
                        {
                            intervalsWithY[i].Add(interval);
                        }
                    }
                }

                //COMPUTING POINTS IN FOUND INTERVALS WITH Y
                for (int i = 0; i < intervalsWithY[0].Count; i++)
                {
                    var xp1 = mainPoints[0][intervalsWithY[0][i]].X;
                    var xk1 = mainPoints[0][intervalsWithY[0][i] + 1].X;

                    var zp1 = mainPoints[0][intervalsWithY[0][i]].Z;
                    var zk1 = mainPoints[0][intervalsWithY[0][i] + 1].Z;

                    var yp1 = mainPoints[0][intervalsWithY[0][i]].Y;
                    var yk1 = mainPoints[0][intervalsWithY[0][i] + 1].Y;

                    if (yk1 != yp1)
                    {
                        var x1 = ((y - yp1) / (yk1 - yp1)) * (xk1 - xp1) + xp1;
                        var z1 = ((y - yp1) / (yk1 - yp1)) * (zk1 - zp1) + zp1;
                        foundPoints1.Add(new Point3D(x1, y, z1));
                    }
                    else
                    {
                        var xHorizontalLength1 = xk1 - xp1;
                        var zHorizontalLength1 = zk1 - zp1;

                        for (int j = 0; j < 11; j++)
                        {
                            var x1 = xp1 + xHorizontalLength1 / 10 * j;
                            var z1 = zp1 + zHorizontalLength1 / 10 * j;
                            foundPoints1.Add(new Point3D(x1, y, z1));
                        }
                    }
                }


                for (int i = 0; i < intervalsWithY[1].Count; i++)
                {
                    var xp2 = mainPoints[1][intervalsWithY[1][i]].X;
                    var xk2 = mainPoints[1][intervalsWithY[1][i] + 1].X;

                    var zp2 = mainPoints[1][intervalsWithY[1][i]].Z;
                    var zk2 = mainPoints[1][intervalsWithY[1][i] + 1].Z;

                    var yp2 = mainPoints[1][intervalsWithY[1][i]].Y;
                    var yk2 = mainPoints[1][intervalsWithY[1][i] + 1].Y;

                    if (yk2 != yp2)
                    {
                        var x2 = ((y - yp2) / (yk2 - yp2)) * (xk2 - xp2) + xp2;
                        var z2 = ((y - yp2) / (yk2 - yp2)) * (zk2 - zp2) + zp2;
                        foundPoints2.Add(new Point3D(x2, y, z2));
                    }
                    else
                    {
                        var xHorizontalLength1 = xk2 - xp2;
                        var zHorizontalLength1 = zk2 - zp2;

                        for (int j = 0; j < 11; j++)
                        {
                            var x2 = xp2 + xHorizontalLength1 / 10 * j;
                            var z2 = zp2 + zHorizontalLength1 / 10 * j;
                            foundPoints1.Add(new Point3D(x2, y, z2));
                        }
                    }
                }
                //POINTS ARE READY
                double distance = 0;
                //COMPUTE DISTANCE BETWEEN POINTS
                for (int i = 0; i < foundPoints1.Count; i++)
                {
                    for (int j = 0; j < foundPoints2.Count; j++)
                    {
                        distance = Math.Sqrt(Math.Pow(foundPoints1[i].X - foundPoints2[j].X, 2) + Math.Pow(foundPoints1[i].Z - foundPoints2[j].Z, 2));
                        if (distance < 150)
                        {
                            collisionDetected = true;
                            PlaySound();
                            //Console.WriteLine("DIST: " + distance);
                            break;
                        }
                    }
                    if (collisionDetected)
                        break;
                }    
            }
        }
    }
}
    
