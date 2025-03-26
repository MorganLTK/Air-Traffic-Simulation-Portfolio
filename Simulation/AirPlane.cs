using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Simulation
{
    internal class AirPlane
    {
      
        private Point location;
        private Point destination;       
        private Point start;

        private int direction;
        private int tag;

        private string startName;
        private string destName;

        private bool inFlight;
        private bool isDelayed;
        private bool flightPending;
        private bool destroyed;
        private bool selected;


        public bool getSelected()
        {
            return selected;
        }
        public void setSelected(bool f)
        {
            selected = f;
        }

        public void setTag(int t)
        {
            tag = t;
        }

        public int getTag() 
        { 
            return tag; 
        }
        public void destroyPlane(bool d)
        {            
            destroyed = d;                        
        }
        public bool getDestroyed()
        {
            return destroyed;
        }
        public void setPending(bool f)
        {
            flightPending = f;
        }
        public bool getPending()
        {
            return flightPending;
        }
        public void setName(string s, string d)
        {
            startName = s;
            destName = d;
        }
        public string getStartName()
        {
            return startName;
        }
        public string getDestName()
        {
            return destName;
        }
        public void setDelay(bool f)
        {
            isDelayed = f;
        }
        public bool getDelay()
        {
            return isDelayed;
        }
        public void setFlightStatus(bool f)
        {
            inFlight = f;
        }
        public bool getFlightStatus()
        {
            return inFlight;
        }        
        public void setCoords(int x, int y)
        {
            location.X = x;
            location.Y = y;            
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
        public int getDirection()
        {
            return direction;
        }
        public void setDirection(int d)
        {
            direction = d;
        }
        public void movePlane()
        {
            
            int run = (getDestination().X - getStart().X) / 100;
            int rise = (getStart().Y - getDestination().Y) / 100;
            int currentRun = (getDestination().X - getCoords().X);
            int currentRise = (getCoords().Y - getDestination().Y);
            bool movingX = true;
            bool movingY = true;
            setFlightStatus(true);
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


                    if (rise >= 2)
                    {
                        setDirection(2);
                    }
                    else if (rise <= -2)
                    {
                        setDirection(3);
                    }
                    else if (rise > -2 && rise < 2)
                    {
                        setDirection(6);
                    }
                    if (currentRise <= 20 && currentRise >= -20)
                    {
                        setDirection(6);
                    }

                }
                else 
                {
                    movingX = false;
                }
            }
            if (run < 0)
            {
                if (getCoords().X > getDestination().X)
                {
                    setCoords(getCoords().X + run, getCoords().Y);

                    if (rise >= 2)
                    {
                        setDirection(0);
                    }
                    else if (rise <= -2)
                    {
                        setDirection(7);
                    }
                    else if (rise > -2 && rise < 2)
                    {
                        setDirection(4);
                    }
                    else if (currentRise <= 20 && currentRise >= -20)
                    {
                        setDirection(4);
                    }

                }
                else
                {
                    movingX = false;
                }
            }
            if (rise < 0)
            {
                if (getCoords().Y < getDestination().Y)
                {
                    setCoords(getCoords().X, getCoords().Y - rise);

                    if (run >= 2)
                    {
                        setDirection(6);
                    }
                    else if (run <= -2)
                    {
                        setDirection(7);
                    }
                    else if (run > -2 && run < 2)
                    {
                        setDirection(5);
                    }
                    if (currentRun <= 20 && currentRun >= -20)
                    {
                        setDirection(5);
                    }
                }
                else
                {
                    movingY = false;
                }
            }
            if (rise > 0)
            {
                if (getCoords().Y > getDestination().Y)
                {
                    setCoords(getCoords().X, getCoords().Y - rise);

                    if (run >= 2)
                    {
                        setDirection(3);
                    }
                    else if (run <= -2)
                    {
                        setDirection(0);
                    }
                    else if (run > -2 && run < 2)
                    {
                        setDirection(1);
                    }
                    if (currentRun <= 20 && currentRun >= -20)
                    {
                        setDirection(1);
                    }
                }
                else
                {
                    movingY = false;                   
                }               
            }
            if (movingX == false && movingY == false)
            {
                setFlightStatus(false);
            }
        }
    }
}
