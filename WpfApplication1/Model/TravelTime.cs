using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Model
{
    class TravelTime : TunnelListItem
    {
        public int SectionID { get; set; }
        public int SectionTravelTime { get; set; }
        public string Direction { get; set; }
        public string SectionDescription { get; set; }
        public string StatusDescription { get; set; }


        public TravelTime(string p1, string p2, string p3)
        {
            string[] x = p1.Split();
            string[] temp = x[0].Split('/');
            string time = temp[1] + "/" + temp[0] + "/" + temp[2] + " " + x[1] + " " + x[2];
            _time = DateTime.Parse(time);
            _newtime = _time;

            x = p2.Split(',');
            SectionID = int.Parse(x[0].Split(':')[1]);
            SectionTravelTime = int.Parse(x[1].Split(':')[1]);
            SetData();
        }

        private void SetData()
        {
            switch(SectionID)
            {
                case 1:
                    Direction = "Eastbound";
                    SectionDescription = "Hof Carmel entrance ramp to tunnel T1";
                    break;
                case 2:
                    Direction = "Eastbound";
                    SectionDescription = "Tunnel T1";
                    break;
                case 3:
                    Direction = "Eastbound";
                    SectionDescription = "Exit ramp from tunnel T1 to Neve Sha'anan";
                    break;
                case 4:
                    Direction = "Eastbound";
                    SectionDescription = "Neve Sha'anan entrance ramp 1 (6) to tunnel T3";
                    break;
                case 5:
                    Direction = "Eastbound";
                    SectionDescription = "Neve Sha'anan entrance ramp 2 (15) to tunnel T3";
                    break;
                case 6:
                    Direction = "Eastbound";
                    SectionDescription = "Neve Sha'anan entrance ramp 3 (5) to tunnel T3";
                    break;
                case 7:
                    Direction = "Eastbound";
                    SectionDescription = "Tunnel T3 + connection between T1 and T3";
                    break;
                case 8:
                    Direction = "Eastbound";
                    SectionDescription = "Exit ramp from tunnel T3 to Krayot";
                    break;
                case 9:
                    Direction = "Westbound";
                    SectionDescription = "Krayot entrance ramp to tunnel T4";
                    break;
                case 10:
                    Direction = "Westbound";
                    SectionDescription = "Tunnel T4";
                    break;
                case 11:
                    Direction = "Westbound";
                    SectionDescription = "Exit ramp from tunnel T4 to Neve Sha'anan";
                    break;
                case 12:
                    Direction = "Westbound";
                    SectionDescription = "Neve Sha'anan entrance ramp to tunnel T2";
                    break;
                case 13:
                    Direction = "Westbound";
                    SectionDescription = "Tunnel T2 + connection between T4 and T2";
                    break;
                case 14:
                    Direction = "Westbound";
                    SectionDescription = "Exit ramp from tunnel T2 to Hof Carmel";
                    break;
            }
            switch (SectionTravelTime)
            {
                case 0:
                    StatusDescription = "free flow";
                    break;
                case 1:
                    StatusDescription = "slow traffic 1";
                    break;
                case 2:
                    StatusDescription = "slow traffic 2";
                    break;
                case 3:
                    StatusDescription = "congestion 1";
                    break;
                case 4:
                    StatusDescription = "congestion 2";
                    break;
                case 5:
                    StatusDescription = "congestion 3";
                    break;
                case 6:
                    StatusDescription = "section closed";
                    break;
                case 7:
                    StatusDescription = "section evacuated";
                    break;
            }
        }
       
    }
}
