using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Model
{
    class LogFileItem : TunnelListItem
    {
        public string _url {get; set;}
        public LogFileItemType _type { get; set; }

        public LogFileItem(string p1, string p2, string p3, string p4)
        {
            _url = p2.Split('?')[1];
            string[] x = p3.Split();
            string[] temp = x[0].Split('/');

            string time = temp[1] + "/" + temp[0] + "/" + temp[2] + " " + x[1] + " " + x[2];
            _time = DateTime.Parse(time);
            _type = GetLogFileItemType();
            _newtime = _time;

        }

        private LogFileItemType GetLogFileItemType()
        {
            if (_url.Contains("TravelTime"))
                return LogFileItemType.TravelTime;
            if (_url.Contains("LCS"))
                return LogFileItemType.LCS;
            if (_url.Contains("K=") || _url.Contains("NS_HS") || _url.Contains("NS_K") || _url.Contains("HS"))
                return LogFileItemType.VMSG;
            if (_url.Contains("TunnelStatus"))
                return LogFileItemType.TunnelStatus;
            return LogFileItemType.DetectorData;
        }
    }

    enum LogFileItemType
    {
        TunnelStatus,
        LCS,
        TravelTime,
        DetectorData,
        VMSG

    }
}
