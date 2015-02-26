

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;



namespace WindowsFormsApplication1
{
    public class Robot
    {
        public Matrix<double> MainTCP;

      
         
        public const double REALTHETACONVERT = 2.222222;
        private double l0;
        private double l1;
        private double l2;
        private double l3;
        private double l4;
        private double l5;

        public double[] theta;
        public double[] theta0;
        public double[] realTheta0;
        public double[] realTheta;
        public double[] deltaTheta;
        private double[] alfa;
        private double[] a;
        private double[] d;
        private string name;

        public Robot(string n, int x0, int x1, int x2, int x3, int x4, int x5, double[] realTh0, double[] th)
        {
            name = n;
            l0 = x0;
            l1 = x1;
            l2 = x2;
            l3 = x3;
            l4 = x4;
            l5 = x5;
            
            a=new double[6]{0,0,l2,0,0,0};
	        d=new double[6]{l0+l1,0,0,l3+l4,0,l5};
	        alfa=new double[6]{-90*Constants.TORAD,90*Constants.TORAD,0,90*Constants.TORAD,-90*Constants.TORAD,90*Constants.TORAD};
            theta = new double[6];
            theta0 = new double[6];
            realTheta0 = new double[5];
            realTheta = new double[5];
            deltaTheta = new double[5];
            
            th.CopyTo(theta,0);
            theta.CopyTo(theta0,0);
            realTh0.CopyTo(realTheta, 0) ;
            realTh0.CopyTo(realTheta0, 0);

            deltaTheta[0] = theta[0] * Constants.TODEG - 270;
            deltaTheta[1] = theta[1] * Constants.TODEG - 90;
            deltaTheta[2] = theta[2] * Constants.TODEG - theta0[2]*Constants.TODEG;
            deltaTheta[3] = theta[3] * Constants.TODEG;
            deltaTheta[4] = theta[4] * Constants.TODEG;

        }


        public void GetActualTheta(string title)
        {
            if (name == "big")
            {
                deltaTheta[0] = realTheta0[0] - (theta[0] * Constants.TODEG - 270) * REALTHETACONVERT;
                deltaTheta[1] = realTheta0[1] - (theta[1] * Constants.TODEG - 90) * REALTHETACONVERT;
                deltaTheta[2] = realTheta0[2] - (theta[2] * Constants.TODEG - theta0[2] * Constants.TODEG) * REALTHETACONVERT;
                deltaTheta[3] = realTheta0[3] + (theta[3] * Constants.TODEG) * REALTHETACONVERT;
                deltaTheta[4] = realTheta0[4] + (theta[4] * Constants.TODEG) * REALTHETACONVERT;
            }
            if (name == "small")
            {
                deltaTheta[0] = realTheta0[0] - (theta[0] * Constants.TODEG - 270) * REALTHETACONVERT;
                deltaTheta[1] = realTheta0[1] + (theta[1] * Constants.TODEG - 90) * REALTHETACONVERT;
                deltaTheta[2] = realTheta0[2] + (theta[2] * Constants.TODEG - theta0[2] * Constants.TODEG) * REALTHETACONVERT;
                deltaTheta[3] = realTheta0[3] + (theta[3] * Constants.TODEG) * REALTHETACONVERT;
                deltaTheta[4] = realTheta0[4] - (theta[4] * Constants.TODEG) * REALTHETACONVERT;
            }

        }
        public void ComputeRealTheta()
        {
            if (name == "big")
            {
                int i = 0;
                realTheta[i] = realTheta0[i] - ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 1;
                realTheta[i] = realTheta0[i] - ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 2;
                realTheta[i] = realTheta0[i] - ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 3;
                realTheta[i] = realTheta0[i] + ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 4;
                realTheta[i] = realTheta0[i] - ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
            }
            if (name == "small")
            {
                int i = 0;
                realTheta[i] = realTheta0[i] - ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 1;
                realTheta[i] = realTheta0[i] + ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 2;
                realTheta[i] = realTheta0[i] + ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 3;
                realTheta[i] = realTheta0[i] + ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
                i = 4;
                realTheta[i] = realTheta0[i] - ((theta[i] * Constants.TODEG - theta0[i] * Constants.TODEG)) * REALTHETACONVERT;
            }
            
        }



        public Matrix<double> GetTransformationMatrix(int StartIndex, int EndIndex)
        {
            Matrix<double> T = Matrix<double>.Build.DenseIdentity(4, 4);
            Matrix<double> m = Matrix<double>.Build.DenseIdentity(4, 4);

	        for (int i = StartIndex; i <EndIndex; i++)
	        {
		        m[0,0]=Math.Cos(theta[i]);
		        m[0,1]=-Math.Sin(theta[i]);
		        m[0,2]=0;
		        m[0,3]=a[i];
		        
		        m[1,0]=Math.Cos(alfa[i])*Math.Sin(theta[i]);
		        m[1,1]=Math.Cos(alfa[i])*Math.Cos(theta[i]);
		        m[1,2]=-Math.Sin(alfa[i]);
		        m[1,3]=-d[i]*Math.Sin(alfa[i]);

		        m[2,0]=Math.Sin(alfa[i])*Math.Sin(theta[i]);
		        m[2,1]=Math.Sin(alfa[i])*Math.Cos(theta[i]);
		        m[2,2]=Math.Cos(alfa[i]);
		        m[2,3]=d[i]*Math.Cos(alfa[i]);

		        m[3,0]=0;
		        m[3,1]=0;
		        m[3,2]=0;
		        m[3,3]=1;

                T = T * m;
	        }
	        return T;
        }

        public Matrix<double> SolveDirectKinematics()
        {
            
	        Matrix<double> TCP = Matrix<double>.Build.DenseIdentity(4,4);

	        TCP=GetTransformationMatrix(0,6);
	        double r,p,y;
	        p=Math.Atan2(-TCP[2,0],Math.Sqrt(TCP[0,0]*TCP[0,0]+TCP[1,0]*TCP[1,0]));
	        if(p==Constants.PI/2)
	        {
		        r=0;
		        y=Math.Atan2(TCP[0,1],TCP[1,1]);
	        }
	        else if(p==-Constants.PI/2)
	        {
		        r=0;
		        y=-Math.Atan2(TCP[0,1],TCP[1,1]);
	        }
	        else
	        {
		        r=Math.Atan2(TCP[2,1],TCP[2,2]);
		        y=Math.Atan2(TCP[1,0],TCP[0,0]);

	        }
           
            MainTCP = TCP;
            return TCP;
        }


        public Matrix<double> CheckDirectKinematics(double[] _th)
        {
            var copy = theta;
            theta = _th;
            Matrix<double> _checkTCP = GetTransformationMatrix(0, 6);
            theta = copy;
            return _checkTCP;
        }


        public double[] SolveInverseKinematics()
        {
	        Matrix<double> T06 = Matrix<double>.Build.DenseIdentity(4,4);
	        Matrix<double> angles = Matrix<double>.Build.DenseIdentity(8,8);
	        
	        T06=MainTCP;
	        double px=T06[0,3];
	        double py=T06[1,3];
	        double pz=T06[2,3];
	
	        double pwx=px-(l5)*T06[0,2]; 
	        double pwy=py-(l5)*T06[1,2]; 
	        double pwz=pz-(l5)*T06[3,2]; 	
	
	        if(Math.Abs(pwx)<0.01)
		        pwx=0;
	        if(Math.Abs(pwy)<0.01)
		        pwy=0;
	        if(Math.Abs(pwz)<0.01)
		        pwz=0;

	        theta[0]=Math.Atan2(pwz,pwx)+180*Constants.TORAD;

            Matrix<double> p14 = Matrix<double>.Build.DenseIdentity(3,1);
	        Matrix<double> T01=GetTransformationMatrix(0,1);
	        
	        p14[0,0]=pwx-T01[0,3];
	        p14[1,0]=pwy-T01[1,3];
	        p14[2,0]=pwz-T01[2,3];
	        
	
	        double phi=((Math.Pow(l2,2)+Math.Pow(l3+l4,2)-(Math.Pow(p14[0,0],2)+Math.Pow(p14[1,0],2)+Math.Pow(p14[2,0],2)))/(2*l2*(l3+l4)));
	
	        phi=phi>0 ? phi-0.0000001 : phi + 0.0000001;
	        phi=Math.Acos(phi);


	        theta[2]=((Constants.PI-phi))+90*Constants.TORAD;

            Matrix<double> p14_1 = Matrix<double>.Build.DenseIdentity(3, 1);

            Matrix<double> R01 = T01.SubMatrix(0, 3, 0, 3);
	        p14_1=R01.Transpose()*p14;

	        for (int i = 0; i < 3; i++)
	        {
		        if(Math.Abs(p14_1[i,0])<0.01)
			        p14_1[i,0]=0;
	        }

	        double beta1=Math.Atan2(p14_1[0,0],p14_1[2,0]);

	        double beta2=((Math.Pow(l2,2)+(Math.Pow(p14[0,0],2)+Math.Pow(p14[1,0],2)+Math.Pow(p14[2,0],2))-Math.Pow(l3+l4,2))/(2*l2*Math.Sqrt(Math.Pow(p14[0,0],2)+Math.Pow(p14[1,0],2)+Math.Pow(p14[2,0],2))));
	        beta2=beta2>0 ? beta2-0.0000001 : beta2 + 0.0000001;
	        
	        beta2=Math.Acos(beta2);

            Matrix<double> T03 = GetTransformationMatrix(0, 3);
            Matrix<double> M = T03.Transpose() * T06;
	        for (int i = 0; i < M.RowCount; i++)
	        {
		        for (int j = 0; j < M.ColumnCount; j++)
		        {
			        if(Math.Abs(M[i,j])<0.01)
				        M[i,j]=0;
		        }
	        }
	
	        theta[1]=(-beta1-beta2)+90*Constants.TORAD;

            if (Math.Abs(theta[0] - 180 * Constants.TORAD) < 0.01 || Math.Abs(theta[0] - 360*Constants.TORAD) < 0.01)
                theta[0] = 180*Constants.TORAD;
	        theta[3]= Math.Atan2(M[2,0],M[0,0]);

            if (Math.Abs(theta[3] - 180*Constants.TORAD) < 0.01 || Math.Abs(theta[3]*Constants.TORAD) < 0.01)
                theta[3] = 0;

            theta[4] = Math.Atan2(M[1, 0],-M[1, 2]);
	        theta[5]= 0;

            return theta;

        }
    }
}
