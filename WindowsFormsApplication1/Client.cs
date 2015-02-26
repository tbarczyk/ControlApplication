using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WindowsFormsApplication1
{
    class Client
    {
       
        private WebClient _wc;
        private Robot _robotSmaller;
        private Robot _robotBigger;

        public Client(Robot smaller, Robot bigger)
        {
            _robotSmaller = smaller;
            _robotBigger = bigger;
            _wc = new WebClient();
            _wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        }

        public void SendPost(string s)
        {
            try
            {
                _wc.UploadString(s, "");
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
            
        }

        private string SendGet(string s)
        {
            try
            {
                return(_wc.DownloadString(s));
            }
            catch
            {
                Console.WriteLine("ERROR");
                return "";
            }
        }

        public void SendThetaToRobots()
        {
            _robotSmaller.ComputeRealTheta();
            _robotBigger.ComputeRealTheta();
           
     /*       SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/integer/" + ((int)_robotSmaller.realTheta[0]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/integer/" + ((int)_robotSmaller.realTheta[1]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/integer/" + ((int)_robotSmaller.realTheta[2]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/integer/" + ((int)_robotSmaller.realTheta[3]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/integer/" + ((int)_robotSmaller.realTheta[4]).ToString());
           
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/integer/" + ((int)_robotBigger.realTheta[0]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/integer/" + ((int)_robotBigger.realTheta[1]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/integer/" + ((int)_robotBigger.realTheta[2]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/integer/" + ((int)_robotBigger.realTheta[3]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/integer/" + ((int)_robotBigger.realTheta[4]).ToString());*/
           
        }

        public void SendReal()
        {
            _robotBigger.GetActualTheta("a");
            _robotSmaller.GetActualTheta("a");

     /*      SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/integer/" + ((int)_robotSmaller.deltaTheta[0]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/integer/" + ((int)_robotSmaller.deltaTheta[1]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/integer/" + ((int)_robotSmaller.deltaTheta[2]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/integer/" + ((int)_robotSmaller.deltaTheta[3]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/integer/" + ((int)_robotSmaller.deltaTheta[4]).ToString());

            SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/integer/" + ((int)_robotBigger.deltaTheta[0]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/integer/" + ((int)_robotBigger.deltaTheta[1]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/integer/" + ((int)_robotBigger.deltaTheta[2]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/integer/" + ((int)_robotBigger.deltaTheta[3]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/integer/" + ((int)_robotBigger.deltaTheta[4]).ToString());*/
           


        }

        public void SendStartReal()
        {
      /*      SendPost("http://192.168.0.111:8000/manipulator/0/servo/1/integer/" + ((int)_robotSmaller.realTheta0[0]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/3/integer/" + ((int)_robotSmaller.realTheta0[1]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/0/integer/" + ((int)_robotSmaller.realTheta0[2]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/4/integer/" + ((int)_robotSmaller.realTheta0[3]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/0/servo/2/integer/" + ((int)_robotSmaller.realTheta0[4]).ToString());

            SendPost("http://192.168.0.111:8000/manipulator/1/servo/5/integer/" + ((int)_robotBigger.realTheta0[0]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/2/integer/" + ((int)_robotBigger.realTheta0[1]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/3/integer/" + ((int)_robotBigger.realTheta0[2]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/4/integer/" + ((int)_robotBigger.realTheta0[3]).ToString());
            SendPost("http://192.168.0.111:8000/manipulator/1/servo/0/integer/" + ((int)_robotBigger.realTheta0[4]).ToString());*/
        }
        
    }

    
}
