using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Model
{
    class VMSGMessagesItem : TunnelListItem
    {
        public string VMSGroupID { get; set; }
        public int VMSMsgID { get; set; }
        public string GroupName { get; set; }

        public string HebrewText { get; set; }
        public string EnglishText { get; set; }
        public string Emergency { get; set; }


        public VMSGMessagesItem(string p1, string p2, string p3)
        {
            string[] x = p1.Split();
            string[] temp = x[0].Split('/');
            string time = temp[1] + "/" + temp[0] + "/" + temp[2] + " " + x[1] + " " + x[2];
            _time = DateTime.Parse(time);
            _newtime = _time;

            x = p2.Split(',');
            VMSGroupID = x[0].Split(':')[1];
            VMSMsgID = int.Parse(x[1].Split(':')[1]);
            SetData();
        }

        private void SetData()
        {
            switch (VMSGroupID.Trim())
            {
                case "HS":
                    GroupName = "Haifa South";
                    setHSMessage();
                    break;
                case "K":
                    GroupName = "Krayot";
                    setKMessage();
                    break;
                case "NS-HS":
                    GroupName = "Neve Shaanan to Haifa South";
                    setNSHSMessage();
                    break;
                case "NS-K-1":
                    GroupName = "Neve Shaanan To Krayot – Ramp 1";
                    setNSK1Message();
                    break;
                case "NS-K-2":
                    GroupName = "Neve Shaanan To Krayot – Ramp 2";
                    setNSK2Message();
                    break;
                case "NS-K-3":
                    GroupName = "Neve Shaanan To Krayot – Ramp 3";
                    setNSK3Message();
                    break;
            }
        }

        private void setKMessage()
        {
            switch (VMSMsgID)
            {
                case 0115:
                    HebrewText = "מנהרה:התנועה זורמת";
                    EnglishText = "Tunnel:Traffic Flowing";
                    Emergency = "No";
                    break;
                case 2115:
                    HebrewText = "מנהרה:עומס ביציאת חיפה דר'";
                    EnglishText = "Tunnel:Congestion in KR Exit";
                    Emergency = "No";
                    break;
                case 2215:
                    HebrewText = "מנהרה:עומס ביציאת נש";
                    EnglishText = "Tunnel:Congestion in NS Exit";
                    Emergency = "No";
                    break;
                case 2315:
                    HebrewText = "מנהרה:עומס בשתי היציאות";
                    EnglishText = "Tunnel:Congestion in Both Exits";
                    Emergency = "No";
                    break;
                case 2415:
                    HebrewText = "מנהרה:עומס אחרי נ.שאנן";
                    EnglishText = "Tunnel:Congestion After NS";
                    Emergency = "No";
                    break;
                case 2515:
                    HebrewText = "מנהרה:עומס בכניסה";
                    EnglishText = "Tunnel:Congestion in Entrance";
                    Emergency = "No";
                    break;
                case 2815:
                    HebrewText = "מנהרה:עומס עד נ.שאנן";
                    EnglishText = "Tunnel:Congestion Up to NS";
                    Emergency = "No";
                    break;
                case 2915:
                    HebrewText = "מנהרה:עומס עד חיפה דר'";
                    EnglishText = "Tunnel:Congestion Up to HS";
                    Emergency = "No";
                    break;
                case 3215:
                    HebrewText = "מנהרה:אין יציאה לנ.שאנן";
                    EnglishText = "Tunnel:No Exit to NS";
                    Emergency = "Yes";
                    break;
                case 3415:
                    HebrewText = "מנהרה:חסום אחרי נ.שאנן";
                    EnglishText = "Tunnel:Closed After NS";
                    Emergency = "Yes";
                    break;
                case 3815:
                    HebrewText = "המנהרה חסומה";
                    EnglishText = "Tunnel Closed";
                    Emergency = "Yes";
                    break;
                case 3915:
                    HebrewText = "המנהרה חסומה";
                    EnglishText = "Tunnels: Closed";
                    Emergency = "Yes";
                    break;
            }
        }

        private void setHSMessage()
        {
            switch (VMSMsgID)
            {
                case 0110:
                    HebrewText = "מנהרה:התנועה זורמת";
                    EnglishText =  "Tunnel:Traffic Flowing";
                    Emergency = "No";
                    break;
                case 2110:
                    HebrewText = "מנהרה:עומס ביציאת קר'";
                    EnglishText =  "Tunnel:Congestion in KR Exit";
                    Emergency = "N";
                    break;

                case 2210:
                    HebrewText = "מנהרה:עומס ביציאת נש";
                    EnglishText =  "Tunnel:Congestion in NS Exit";
                    Emergency = "No";
                    break;
                case 2310:
                    HebrewText = "מנהרה:עומס בשתי היציאות";
                    EnglishText = "Tunnel:Congestion in Both Exits";
                    Emergency = "No";
                    break;
                case 2410:
                    HebrewText = "מנהרה:עומס אחרי נ.שאנן";
                    EnglishText = "Tunnel:Congestion After NS";
                    Emergency = "No";
                    break;
                case 2510:
                    HebrewText = "מנהרה:עומס בכניסה";
                    EnglishText = "Tunnel:Congestion in Entrance";
                    Emergency = "No";
                    break;
                case 2810:
                 HebrewText = "מנהרה:עומס עד נ.שאנן";
                EnglishText = "Tunnel:Congestion Up to NS";
                Emergency = "No";
                 break;
                                case 2910:
                 HebrewText = "מנהרה:עומס עד מ.קריות";
                EnglishText = "Tunnel:Congestion Up to KR";
                Emergency = "No";
                 break;
                                case 3210:
                 HebrewText = "מנהרה:אין יציאה לנ.שאנן";
                EnglishText = "Tunnel:No Exit to NS";
                Emergency = "Yes";
                 break;
                                case 3410:
                 HebrewText = "מנהרה:חסום אחרי נ.שאנן";
                EnglishText = "Tunnel:Closed After NS";
                Emergency = "Yes";
                 break;
                                case 3810:
                 HebrewText = "המנהרה חסומה";
                EnglishText = "Tunnel Closed";
                Emergency = "Yes";
                 break;
                case 3910:
                 HebrewText = "המנהרה חסומה";
                EnglishText = "Tunnels: Closed";
                Emergency = "Yes";
                break;
            }
        }

        private void setNSHSMessage()
        {
            switch (VMSMsgID)
            {
                case 0114:
                    HebrewText = "מנהרה לחיפה דר': זורם";
                    EnglishText = "Tunnel to HS: Traffic Flowing";
                    Emergency = "No";
                    break;
                case 2114:
                    HebrewText = "מנהרה לחיפה דר': עומס ביציאה";
                    EnglishText = "Tunnel to HS:Congestion in Exit";
                    Emergency = "No";
                    break;
                case 2414:
                    HebrewText = "מנהרה לחיפה דר': עומס";
                    EnglishText = "Tunnel to HS:Congested";
                    Emergency = "No";
                    break;
                case 2514:
                    HebrewText = "מנהרה לחיפה דר': עומס בכניסה";
                    EnglishText = "Tunnel to HS Congestion in Entrance";
                    Emergency = "No";
                    break;
                case 3014:
                    HebrewText = "מנהרה לחיפה דר': חסום";
                    EnglishText = "Tunnel to HS: Closed";
                    Emergency = "Yes";
                    break;
                case 3414:
                    HebrewText = "מנהרה לחיפה דר': חסום";
                    EnglishText = "Tunnel to HS: Closed";
                    Emergency = "Yes";
                    break;
            }
        }

        private void setNSK1Message()
        {
            switch (VMSMsgID)
            {
                case 0111:
                    HebrewText = "מנהרה לקריות: זורם";
                    EnglishText = "Tunnel to KR: Traffic Flowing";
                    Emergency = "No";
                    break;
                case 2111:
                    HebrewText = "מנהרה לקר':עומס ביציאה";
                    EnglishText = "Tunnel to KR: Congestion in Exit";
                    Emergency = "No";
                    break;
                case 2411:
                    HebrewText = "מנהרה לקריות: עומס";
                    EnglishText = "Tunnel to KR: Congested";
                    Emergency = "No";
                    break;
                case 2511:
                    HebrewText = "מנהרה לקר': עומס בכניסה";
                    EnglishText = "Tunnel to KR:Congestion in Entrance";
                    Emergency = "No";
                    break;
                case 3011:
                    HebrewText = "מנהרה לקריות: חסום";
                    EnglishText = "Tunnel to KR:Closed";
                    Emergency = "Yes";
                    break;
                case 3411:
                    HebrewText = "מנהרה לקריות: חסום";
                    EnglishText = "Tunnel to KR:Closed";
                    Emergency = "Yes";
                    break;
            }
        }

        private void setNSK2Message()
        {
            switch (VMSMsgID)
            {
                case 0112:
                    HebrewText = "מנהרה לקריות: זורם";
                    EnglishText = "Tunnel to KR: Traffic Flowing";
                    Emergency = "No";
                    break;
                case 2112:
                    HebrewText = "מנהרה לקר':עומס ביציאה";
                    EnglishText = "Tunnel to KR:Congestion in Exit";
                    Emergency = "No";
                    break;
                case 2412:
                    HebrewText = "מנהרה לקריות: עומס";
                    EnglishText = "Tunnel to KR:Congested";
                    Emergency = "No";
                    break;
                case 2512:
                    HebrewText = "מנהרה לקר': עומס בכניסה";
                    EnglishText = "Tunnel to KR:Congestion in Entrance";
                    Emergency = "No";
                    break;
                case 3012:
                    HebrewText = "מנהרה לקריות: חסום";
                    EnglishText = "Tunnel to KR:Closed";
                    Emergency = "Yes";
                    break;
                case 3412:
                    HebrewText = "מנהרה לקריות: חסום";
                    EnglishText = "Tunnel to KR:Closed";
                    Emergency = "Yes";
                    break;
            }
        }

        private void setNSK3Message()
        {
            switch (VMSMsgID)
            {
                case 0113:
                    HebrewText = "מנהרה לקריות: זורם";
                    EnglishText = "Tunnel to KR: Traffic Flowing";
                    Emergency = "No";
                    break;
                case 2113:
                    HebrewText = "מנהרה לקר': עומס ביציאה";
                    EnglishText = "Tunnel to KR:Congestion in Exit";
                    Emergency = "No";
                    break;
                case 2413:
                    HebrewText = "מנהרה לקריות: עומס";
                    EnglishText = "Tunnel to KR:Congested";
                    Emergency = "No";
                    break;
                case 2513:
                    HebrewText = "מנהרה לקר': עומס בכניסה";
                    EnglishText = "Tunnel to KR:Congestion in Entrance";
                    Emergency = "No";
                    break;
                case 3013:
                    HebrewText = "מנהרה לקריות: חסום";
                    EnglishText = "Tunnel to KR:Closed";
                    Emergency = "Yes";
                    break;
                case 3413:
                    HebrewText = "מנהרה לקריות: חסום";
                    EnglishText = "Tunnel to KR:Closed";
                    Emergency = "Yes";
                    break;
            }
        }
    }
}
