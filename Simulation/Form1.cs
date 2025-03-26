using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public partial class Form1 : Form
    {
        Bitmap graphicBits = null;
        Bitmap clouds = null;
        Bitmap thunder = null;
        Bitmap thunderCloud = null;
        Bitmap rain = null;

        Random random = new Random();

        int imageIndex = 1;
        int toggle;
        int totalFlights;
        int planesDestroyed;
        int flightsDelayed;
        int totalStorms;

        List<AirPort> airPorts = new List<AirPort>();
        List<AirPlane> airPlanes = new List<AirPlane>();
        List<StormCloud> storms = new List<StormCloud>();

        string status = "";
        

        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();            
            graphicBits = new Bitmap(typeof(Form1), "Plane Final.png");
            clouds = new Bitmap(typeof(Form1), "BackgroundClouds.png");
            thunder = new Bitmap(typeof(Form1), "ThunderBackground.png");
            thunderCloud = new Bitmap(typeof(Form1), "LargeThunderCloud.png");
            rain = new Bitmap(typeof(Form1), "Rain.png");                                   
        }

        private void Form1_Load(object sender, EventArgs e)
        {                       
            for(int i = 0; i < 15; i++)
            {
                airPorts.Add(new AirPort());
            }
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label1.Text = "Total Flights: " + totalFlights;
            label2.Text = "Planes Destroyed: " + planesDestroyed;
            label3.Text = "Flights Delayed: " + flightsDelayed;
            label4.Text = "Total Storms: " + totalStorms; 
            airPorts[0].setCoords(65, 417);
            airPorts[0].setName("LAX"); //LA
            airPorts[1].setCoords(544, 573);
            airPorts[1].setName("IAH"); // Houston
            airPorts[2].setCoords(1014, 686);
            airPorts[2].setName("MCO"); //orlando
            airPorts[3].setCoords(745, 418);
            airPorts[3].setName("BNA"); //nashville
            airPorts[4].setCoords(1100, 240);
            airPorts[4].setName("LGA"); //NYC
            airPorts[5].setCoords(328, 444);
            airPorts[5].setName("ABQ"); //albuquerque
            airPorts[6].setCoords(95, 174);
            airPorts[6].setName("SEA"); //seattle
            airPorts[7].setCoords(900, 230);
            airPorts[7].setName("YYZ"); //toronto
            airPorts[8].setCoords(575, 115);
            airPorts[8].setName("YWG"); // winnipeg
            airPorts[9].setCoords(1192, 130);
            airPorts[9].setName("YHZ"); //Halifax
            airPorts[10].setCoords(1051, 130);
            airPorts[10].setName("YQB"); // Quebec City
            airPorts[11].setCoords(377, 220);
            airPorts[11].setName("BIL"); //billings
            airPorts[12].setCoords(541, 348);
            airPorts[12].setName("LNK"); //lincoln
            airPorts[13].setCoords(714, 251);
            airPorts[13].setName("MKE"); //milwaukee
            airPorts[14].setCoords(703, 601);
            airPorts[14].setName("MSY"); // new orleans
            
            
            Invalidate();           
        }

        public void createFlightPlan()
        {

            airPlanes.Add(new AirPlane());
            airPlanes[airPlanes.Count - 1].setPending(true);
            airPlanes[airPlanes.Count - 1].setDelay(false);
            airPlanes[airPlanes.Count - 1].setTag(airPlanes.Count);
            int ran1 = random.Next(0, 15);
            airPlanes[airPlanes.Count - 1].setStart(airPorts[ran1].getCoords().X, airPorts[ran1].getCoords().Y);
            int ran2 = random.Next(0, 15);
            while (ran2 == ran1)
            {
                ran2 = random.Next(0, 15);
            }
            airPlanes[airPlanes.Count - 1].setDestination(airPorts[ran2].getCoords().X, airPorts[ran2].getCoords().Y);
            airPlanes[airPlanes.Count - 1].setName(airPorts[ran1].getName(), airPorts[ran2].getName());

            status = "ON TIME";
            ListViewItem item = new ListViewItem();
            item.Text = airPorts[ran1].getName() + " to " + airPorts[ran2].getName() + " " + status;
            item.Tag = airPlanes.Count;
            item.ForeColor = Color.Green;
            listView1.Items.Add(item);
            totalFlights++;
        }

        public void createStormCloud()
        {
            storms.Add(new StormCloud());
            storms[storms.Count - 1].setSize(trackBar4.Value, trackBar4.Value);

            storms[storms.Count - 1].setStart(random.Next(20, 1200), random.Next(20, 650));

            storms[storms.Count - 1].setDestination(random.Next(20, 1200), random.Next(20, 650));

            storms[storms.Count - 1].setSize(trackBar3.Value, trackBar3.Value);
        }

        public void checkForStorm()
        {
            for (int i = 0; i < airPorts.Count; i++)
            {
                for(int j = 0; j < storms.Count; j++)
                {
                    if (airPorts[i].getCoords().X < storms[j].getArea().X && airPorts[i].getCoords().X > storms[j].getCoords().X && airPorts[i].getCoords().Y < storms[j].getArea().Y && airPorts[i].getCoords().Y > storms[j].getCoords().Y)
                    {                       
                        for(int k = 0; k < airPlanes.Count; k++)
                        {
                            if (airPlanes[k].getDestination() == airPorts[i].getCoords() && airPlanes[i].getPending()|| airPlanes[k].getStart() == airPorts[i].getCoords() && airPlanes[i].getPending())
                            {
                                airPlanes[k].setDelay(true);
                                flightsDelayed++;
                            }
                        }                                                   
                    }
                    else
                    {
                        for (int k = 0; k < airPlanes.Count; k++)
                        {
                            if (airPlanes[k].getDestination() == airPorts[i].getCoords() || airPlanes[k].getStart() == airPorts[i].getCoords())
                            {
                                if (airPlanes[k].getDelay() == true)
                                {
                                    airPlanes[k].setDelay(false);
                                }                                
                            }
                        }
                    }
                }              
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle srcRect = new Rectangle();
            Rectangle destRect = new Rectangle();
            
            for (int i = 0; i < airPorts.Count(); i++)
            {
                srcRect = new Rectangle(1080, 0, 120, 120);
                destRect = new Rectangle(airPorts[i].getCoords().X +15, airPorts[i].getCoords().Y + 15, 30, 30);
                e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < airPlanes.Count(); i++)
            {               
                if (airPlanes[i].getFlightStatus() && airPlanes[i].getPending() == false && airPlanes[i].getSelected() == false)
                {                    
                    srcRect = new Rectangle(0 + (airPlanes[i].getDirection() * 120), 0, 120, 120);
                    destRect = new Rectangle(airPlanes[i].getCoords().X, airPlanes[i].getCoords().Y, 25, 25);       // x y draws the location, (scales the card), (scales the card)
                    e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);

                    if (airPlanes[i].getDirection() == 8)
                    {
                        airPlanes[i].destroyPlane(true);
                    }
                }
                if (airPlanes[i].getFlightStatus() && airPlanes[i].getPending() == false && airPlanes[i].getSelected() == true)
                {                    
                    srcRect = new Rectangle(0 + (airPlanes[i].getDirection() * 120), 120, 120, 120);
                    destRect = new Rectangle(airPlanes[i].getCoords().X, airPlanes[i].getCoords().Y, 25, 25);       // x y draws the location, (scales the card), (scales the card)
                    e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);

                    if (airPlanes[i].getDirection() == 8)
                    {
                        airPlanes[i].destroyPlane(true);
                    }
                }
            }
            
            for (int i = 0; i < storms.Count(); i++)
            {
                srcRect = new Rectangle(0, 0, 600, 600);
                destRect = new Rectangle(storms[i].getCoords().X, storms[i].getCoords().Y, storms[i].getSize().Width, storms[i].getSize().Height);       // x y draws the location, (scales the card), (scales the card)
                e.Graphics.DrawImage(thunderCloud, destRect, srcRect, GraphicsUnit.Pixel);                
            }
            srcRect = new Rectangle(0,0, 1231, 729);
            destRect = new Rectangle(0, 0, 1500, 800);
            
            e.Graphics.DrawImage(clouds, destRect, srcRect, GraphicsUnit.Pixel);
            if(storms.Count > 0)
            {
                e.Graphics.DrawImage(thunder, destRect, srcRect, GraphicsUnit.Pixel);

                srcRect = new Rectangle(0, 0, 1920, 1080 * imageIndex);
                destRect = new Rectangle(0, 0, 1500, 800);
                e.Graphics.DrawImage(rain, destRect, srcRect, GraphicsUnit.Pixel);

                imageIndex++;
                if(imageIndex > 3)
                {
                    imageIndex = 1;
                }
            }                       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (airPlanes.Count < trackBar6.Value)
            {
                createFlightPlan();

            }
            for (int i = 0; i < airPlanes.Count(); i++)
            {
                
                if (i < trackBar2.Value)
                {
                    if (airPlanes[i].getDelay() == false)
                    {
                        airPlanes[i].setPending(false);
                    }                                      
                    if (listView1.Items[i].Text.Contains("DEPARTED"))
                    {
                       
                    }
                    else
                    {
                        status = "DEPARTED";
                        ListViewItem item = new ListViewItem();
                        item.Text = airPlanes[i].getStartName() + " to " + airPlanes[i].getDestName() + " " + status;
                        item.Tag = listView1.Items[i].Tag;
                        item.ForeColor = Color.Black;
                        listView1.Items.RemoveAt(i);
                        listView1.Items.Insert(i, item);
                    }
                    if (airPlanes[i].getPending() == false)
                    {
                        airPlanes[i].movePlane();
                    }
                    if (airPlanes[i].getFlightStatus() == false|| airPlanes[i].getDestroyed() == true)
                    {
                        airPlanes.RemoveAt(i);
                        listView1.Items.RemoveAt(i);
                    }
                }                
                else if (airPlanes[i].getDelay())
                {
                    if (listView1.Items[i].Text.Contains("DELAYED"))
                    {
                        
                    }
                    else
                    {
                        status = "DELAYED";
                        ListViewItem item = new ListViewItem();
                        item.Text = airPlanes[i].getStartName() + " to " + airPlanes[i].getDestName() + " " + status;                       
                        item.Tag = listView1.Items[i].Tag;
                        item.ForeColor = Color.Red;
                        listView1.Items.RemoveAt(i);
                        listView1.Items.Insert(i, item);
                    }
                }                                
            }
            for (int i = 0; i < storms.Count(); i++)
            {      
                storms[i].setSize(trackBar3.Value, trackBar3.Value);                         
                storms[i].moveCloud();
                if (storms[i].getFlightStatus() == false)
                {
                    storms.RemoveAt(i);
                }                              
            }    
            if(storms.Count() > 0)
            {
                checkForStorm();
            }
            label1.Text = "Total Flights: " + totalFlights;
            label2.Text = "Planes Destroyed: " + planesDestroyed;
            label3.Text = "Flights Delayed: " + flightsDelayed;
            label4.Text = "Total Storms: " + totalStorms;
            Invalidate();           
        }        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 400 - trackBar1.Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {           
            timer1.Start();
            timer2.Start();
        }       
        private void timer2_Tick(object sender, EventArgs e)
        {
            int ran = random.Next(0, 50);
            if(ran < trackBar4.Value)
            {
                createStormCloud();
                totalStorms++;
            }
            ran = random.Next(0, trackBar2.Value);
            if (ran < trackBar5.Value)
            {
                airPlanes[ran].setDirection(8);
                planesDestroyed++;
                Invalidate();                
            }
        }
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            for(int i = 0; i < airPlanes.Count; i++)
            {
                if (airPlanes[i].getSelected())
                {
                    airPlanes[i].setSelected(false);
                }
            }
            
           for (int j = 0; j < airPlanes.Count; j++)
           {
               if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag.ToString() == airPlanes[j].getTag().ToString())
               {
                   airPlanes[j].setSelected(true);      
                   
               }
           }                      
        }         
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Location.ToString());
        }  
        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            trackBar3.Value = 40;
            trackBar4.Value = 30;
            trackBar2.Value = 18;
            trackBar5.Value = 2;
            trackBar6.Value = 20;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar3.Value = 150;
            trackBar4.Value = 15;
            trackBar2.Value = 22;
            trackBar5.Value = 22;
            trackBar6.Value = 25;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trackBar3.Value = 100;
            trackBar4.Value = 5;
            trackBar2.Value = 50;
            trackBar5.Value = 10;
            trackBar6.Value = 50;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trackBar3.Value = 400;
            trackBar4.Value = 20;
            trackBar2.Value = 15;
            trackBar5.Value = 0;
            trackBar6.Value = 30;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {       
            if(toggle == 1)
            {
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
                toggle = 0; 
            }
            else
            {
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                toggle = 1;
            }                      
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
