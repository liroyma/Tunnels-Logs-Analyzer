using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Model
{
    class Detector
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Detector(string p1)
        {
            string[] result = p1.Split(':');
            if(result.Length<2)
            {
                Name = result[0];
            }
            else
            {
            Name = result[0];
            Value = result[1];
        }
        }
    }
}
