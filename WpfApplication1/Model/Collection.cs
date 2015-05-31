using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Model
{
    class Collection : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<LogFileItem> _lagdata = new List<LogFileItem>();
        private List<TravelTime> _traveltimedata = new List<TravelTime>();
        private List<VMSGMessagesItem> _VMSMEssagesdata = new List<VMSGMessagesItem>();
        private List<LogFileItem> _filterdlagdata = new List<LogFileItem>();
        private List<DetectorDataItem> _tunneldetectordata = new List<DetectorDataItem>();
        private string _date;

        private bool _haveLogFile;
        private bool _haveTravelTimeFile;
        private bool _haveTunnelDetectorFile;
        private bool _haveVMSGMessagesFile;

        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public bool HaveLogFile
        {
            get
            {
                return _haveLogFile;
            }
            set
            {
                _haveLogFile = value;
                OnPropertyChanged("HaveLogFile");
            }
        }
        public bool HaveTravelTimeFile
        {
            get
            {
                return _haveTravelTimeFile;
            }
            set
            {
                _haveTravelTimeFile = value;
                OnPropertyChanged("HaveTravelTimeFile");
            }
        }
        public bool HaveTunnelDetectorFile
        {
            get
            {
                return _haveTunnelDetectorFile;
            }
            set
            {
                _haveTunnelDetectorFile = value;
                OnPropertyChanged("HaveTunnelDetectorFile");
            }
        }
        public bool HaveVMSGMessagesFile
        {
            get
            {
                return _haveVMSGMessagesFile;
            }
            set
            {
                _haveVMSGMessagesFile = value;
                OnPropertyChanged("HaveVMSGMessagesFile");
            }
        }
        public List<LogFileItem> LogData
        {
            get
            {
                return _lagdata;
            }
            set
            {
                _lagdata = value;
                OnPropertyChanged("LogData");
            }
        }
        public List<TravelTime> TravelTimeData
        {
            get
            {
                return _traveltimedata;
            }
            set
            {
                _traveltimedata = value;
                OnPropertyChanged("TravelTimeData");
            }
        }
        public List<VMSGMessagesItem> VMSMessagesData
        {
            get
            {
                return _VMSMEssagesdata;
            }
            set
            {
                _VMSMEssagesdata = value;
                OnPropertyChanged("VMSMessagesData");
            }
        }
        public List<LogFileItem> FilterdLogData
        {
            get
            {
                return _filterdlagdata;
            }
            set
            {
                _filterdlagdata = value;
                OnPropertyChanged("FilterdLogData");
            }
        }
        public List<DetectorDataItem> TunnelDetectorData
        {
            get
            {
                return _tunneldetectordata;
            }
            set
            {
                _tunneldetectordata = value;
                OnPropertyChanged("TunnelDetectorData");
            }
        }
       
        public Collection()
        {
            Date = "Please Select a log file";
            _haveLogFile = false;
            _haveTravelTimeFile = false;
            _haveTunnelDetectorFile = false;
            _haveVMSGMessagesFile = false;
        }

        public void AddFile(string file)
        {
            string[] fullname = file.Split('\\');
            string filename = fullname.Last();
            if (filename.Contains("log"))
            {
                Date = GetDateFromFileName(filename);
                ReadLogFile(file);
            }
            else if (Date == "Please Select a log file")
            {
                throw new Exception("Please selecte a log file first");
            }
            else if (Date != GetDateFromFileName(filename))
            {
                throw new Exception("The selected file date is not the same as the log file");
            }
            else if (filename.Contains("setTravelTime"))
            {
                ReadTraveTimeFile(file);
            }
            else if (filename.Contains("setTunnelDetectorData"))
            {
                 ReadDetectorDataFile(file);
            }
            else if (filename.Contains("VMSGMessages"))
            {
                ReadVMSFile(file);
            }
        }

        public void ReadLogFile(string file)
        {
            LogData = new List<LogFileItem>();
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                LogFileItem log = new LogFileItem(reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                LogData.Add(log);
            }
            reader.Close();
            reader.Dispose();
            if (LogData.Count > 0)
                HaveLogFile = true;
            for (int i = 1; i < LogData.Count; i++)
            {
                if (LogData[i]._time <= LogData[i - 1]._time && LogData[i]._type == LogData[i - 1]._type)
                {
                    LogData[i]._newtime = LogData[i - 1]._newtime.AddSeconds(1);
                }
                else
                {
                    LogData[i]._newtime = LogData[i]._time;
                }
            }
            FilterdLogData = LogData;
        }

        public void ReadTraveTimeFile(string file)
        {
            TravelTimeData = new List<TravelTime>();
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                TravelTime log = new TravelTime(reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                TravelTimeData.Add(log);
            }
            if (TravelTimeData.Count > 0)
                HaveTravelTimeFile = true;
            reader.Close();
            reader.Dispose();
            for (int i = 1; i < TravelTimeData.Count; i++)
            {
                if (TravelTimeData[i]._time <= TravelTimeData[i - 1]._time)
                {
                    TravelTimeData[i]._newtime = TravelTimeData[i - 1]._newtime.AddSeconds(1);
                }
                else
                {
                    TravelTimeData[i]._newtime = TravelTimeData[i]._time;
                }
            }
        }

        public void ReadVMSFile(string file)
        {
            VMSMessagesData = new List<VMSGMessagesItem>();
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                VMSGMessagesItem log = new VMSGMessagesItem(reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                VMSMessagesData.Add(log);
            }
            if (VMSMessagesData.Count > 0)
                HaveVMSGMessagesFile = true;
            reader.Close();
            reader.Dispose();
            for (int i = 1; i < VMSMessagesData.Count; i++)
            {
                if (VMSMessagesData[i]._time <= VMSMessagesData[i - 1]._time)
                {
                    VMSMessagesData[i]._newtime = VMSMessagesData[i - 1]._newtime.AddSeconds(1);
                }
                else
                {
                    VMSMessagesData[i]._newtime = VMSMessagesData[i]._time;
                }
            }
        }

        public void ReadDetectorDataFile(string file)
        {
            TunnelDetectorData = new List<DetectorDataItem>();
            List<string> templist = new List<string>();
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string s = reader.ReadLine();
               
                if (s.Contains("---"))
                {
                    if (templist.Count > 0)
                    {
                        DetectorDataItem log = new DetectorDataItem(templist);
                        if(log.DetectorList.Count > 1)
                            TunnelDetectorData.Add(log);
                        templist = new List<string>();
                    }
                }
                templist.Add(s);
               
            }
            if (TunnelDetectorData.Count > 0)
                HaveTunnelDetectorFile = true;
            reader.Close();
            reader.Dispose();
            for (int i = 1; i < TunnelDetectorData.Count; i++)
            {
                if (TunnelDetectorData[i]._time <= TunnelDetectorData[i - 1]._time)
                {
                    TunnelDetectorData[i]._newtime = TunnelDetectorData[i - 1]._newtime.AddSeconds(1);
                }
                else
                {
                    TunnelDetectorData[i]._newtime = TunnelDetectorData[i]._time;
                }
            }
        }

        private string GetDateFromFileName(string filename)
        {
            string[] temp = filename.Split('_')[1].Split('.')[0].Split('-');
            return temp[1] + "/" + temp[0] + "/" + temp[2];
        }

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void FilterLog(DateTime start, DateTime end, List<LogFileItemType> list)
        {
            FilterdLogData = LogData.Where(x => x._time >= start && x._time <= end).Where(x => list.Contains(x._type)).ToList();
        }

    }

}
