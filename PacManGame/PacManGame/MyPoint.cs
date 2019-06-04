using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class MyPoint
    {
        private int x;
        private int y;

        public MyPoint(int x=0,int y=0)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public override string ToString()
        {
            return "X:" + x.ToString() + " Y:" + y.ToString();
        }
    }
}
