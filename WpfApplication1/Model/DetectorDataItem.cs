using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Model
{
    class DetectorDataItem : TunnelListItem
    {
        public List<Detector> DetectorList { get; set; }

        public DetectorDataItem(List<string> templist)
        {
            DetectorList = new List<Detector>();
            string[] x = templist[0].Split();
            string[] temp = x[0].Split('/');
            string time = temp[1] + "/" + temp[0] + "/" + temp[2] + " " + x[1] + " " + x[2];
            _time = DateTime.Parse(time);
            _newtime = _time;

            for (int i = 1; i < templist.Count - 1; i++)
            {
                DetectorList.Add(new Detector(templist[i]));
            }
        }
    }
}
