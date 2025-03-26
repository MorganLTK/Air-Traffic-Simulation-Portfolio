using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    internal class StormCloud
    {
        private Point location;
        private Point destination;
        private Point start;
        private Point area;

        private Size cloudSize;
       
        private bool inFlight;

        public void setSize(int w, int h)
        {
            cloudSize.Width = w;
            cloudSize.Height = h;
        }

        public Size getSize()
        {
            return cloudSize;
        }
        public void setFlightStatus(bool f)
        {
            inFlight = f;
        }
        public bool getFlightStatus()
        {
            return inFlight;
        }

        public void setArea(int x, int y)
        {
            area.X = x;
            area.Y = y;
        }

        public Point getArea()
        { 
            return area; 
        }

        public void setCoords(int x, int y)
        {
            location.X = x;
            location.Y = y;
            setArea(x + getSize().Width, y + getSize().Height);
        }

        public Point getCoords()
        {
            return location;
        }

        public void setDestination(int x, int y)
        {
            destination.X = x;
            destination.Y = y;
        }

        public Point getDestination()
        {
            return destination;
        }

        public void setStart(int x, int y)
        {
            start.X = x;
            start.Y = y;
            setCoords(x, y);
        }

        public Point getStart()
        {
            return start;
        }

        public void moveCloud()
        {
            int run = (getDestination().X - getStart().X) / 100;
            int rise = (getStart().Y - getDestination().Y) / 100;
            if (getDestination().X - getStart().X < 100 && getDestination().X - getStart().X > 0)
            {
                run = 1;
            }
            if (getDestination().X - getStart().X < 0 && getDestination().X - getStart().X > -100)
            {
                run = -1;
            }
            if (getStart().Y - getDestination().Y < 100 && getStart().Y - getDestination().Y > 0)
            {
                rise = 1;
            }
            if (getStart().Y - getDestination().Y < 0 && getStart().Y - getDestination().Y > -100)
            {
                rise = -1;
            }
            if (run > 0)
            {
                if (getCoords().X < getDestination().X)
                {
                    setCoords(getCoords().X + run, getCoords().Y);
                    setFlightStatus(true);
                }
                else
                {
                    setFlightStatus(false);
                }
            }
            if (run < 0)
            {
                if (getCoords().X > getDestination().X)
                {
                    setCoords(getCoords().X + run, getCoords().Y);
                    setFlightStatus(true);
                }
                else
                {
                    setFlightStatus(false);
                }
            }
            if (rise < 0)
            {
                if (getCoords().Y < getDestination().Y)
                {
                    setCoords(getCoords().X, getCoords().Y - rise);
                    setFlightStatus(true);
                }
                else
                {
                    setFlightStatus(false);
                }
            }
            if (rise > 0)
            {
                if (getCoords().Y > getDestination().Y)
                {
                    setCoords(getCoords().X, getCoords().Y - rise);
                    setFlightStatus(true);
                }
                else
                {
                    setFlightStatus(false);
                }
            }

        }
    }
}

