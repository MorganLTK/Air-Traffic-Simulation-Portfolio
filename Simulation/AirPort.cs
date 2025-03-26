using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    internal class AirPort
    {
        private Point location;
        private string name;
        public void setCoords(int x, int y)
        {
           location.X = x;
           location.Y = y;
        }

        public Point getCoords()
        {
            return location;
        }

        public void setName(string s) 
        {
            name = s;
        }
        public string getName()
        {
            return name;
        }
    }
}
