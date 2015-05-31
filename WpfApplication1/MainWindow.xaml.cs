using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WpfApplication1.Model;
using Microsoft.Win32;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Collection collection;
        bool firsttime = true;
        public MainWindow()
        {
            InitializeComponent();
            InitComboboxses();
            collection = new Collection();
            this.DataContext = collection;
        }

        private void InitComboboxses()
        {
            foreach (LogFileItemType item in Enum.GetValues(typeof(LogFileItemType)))
            {
                CheckBox c = new CheckBox();
                c.Content = item.ToString();
                c.IsChecked = true;
                PannelChecker.Children.Add(c);
            }

            List<int> hours = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i);
            }
            List<int> minutes = new List<int>();
            for (int i = 0; i < 60; i=i+5)
            {
                minutes.Add(i);
            }
            cmbhrstart.ItemsSource = hours;
            cmbhrstart.SelectedValue = hours.First();
            cmbhrend.ItemsSource = hours;
            cmbhrend.SelectedValue = hours.Last();
            cmbminstart.ItemsSource = minutes;
            cmbminstart.SelectedValue = minutes.First();
            cmbminend.ItemsSource = minutes;
            cmbminend.SelectedValue = minutes.Last();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            switch (item.Header.ToString())
            {
                case "_Open...":
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; 
                    openFileDialog.Multiselect = true; 
                    if (openFileDialog.ShowDialog() == true)
                    {
                        foreach (string file in openFileDialog.FileNames)
                        {
                            addFile(file);
                        } ;
                    }
                    break;
                case "_New...":
                    collection = new Collection();
                    this.DataContext = collection;
                    loglistbox.Visibility = System.Windows.Visibility.Collapsed;
                    ResetCounters();
                    break;
            }
        }

        private void addFile(string filepth)
        {
            try
            {
                collection.AddFile(filepth);
                if (collection.FilterdLogData.Count > 0)
                    loglistbox.Visibility = System.Windows.Visibility.Visible;
                else
                    loglistbox.Visibility = System.Windows.Visibility.Collapsed;
                AddCounter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            int starthour = (int)cmbhrstart.SelectedValue;
            int startminute = (int)cmbminstart.SelectedValue;
            int endhour = (int)cmbhrend.SelectedValue;
            int endminute = (int)cmbminend.SelectedValue;

            if (collection.Date == "Please Select a log file")
            {
                MessageBox.Show("please load a log file first");
                return;
            }

            DateTime d = DateTime.Parse(collection.Date);
            DateTime s = new DateTime(d.Year, d.Month, d.Day, starthour, startminute, 0);
            DateTime en = new DateTime(d.Year, d.Month, d.Day, endhour, endminute, 59);

            if(s>=en)
            {
                MessageBox.Show("start time is greater then end time");
                return;
            }
            List<LogFileItemType> list = new List<LogFileItemType>();
            foreach (CheckBox item in PannelChecker.Children)
            {
                if(item.IsChecked==true)
                {
                    string text = item.Content.ToString().Split()[0];
                    LogFileItemType MyStatus = (LogFileItemType)Enum.Parse(typeof(LogFileItemType), text);
                    list.Add(MyStatus);
                }
            }
            collection.FilterLog(s, en, list);
        }

        private void loglistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loglistbox.SelectedIndex == -1)
                return;
            LogFileItem log = (LogFileItem)loglistbox.SelectedItem;
            traveltimelistbox.Visibility = System.Windows.Visibility.Collapsed;
            VMSlistbox.Visibility = System.Windows.Visibility.Collapsed;
            Detectorslistbox.Visibility = System.Windows.Visibility.Collapsed;
            traveltimedetailsbox.Visibility = System.Windows.Visibility.Collapsed;
            VMSdetailsbox.Visibility = System.Windows.Visibility.Collapsed;
            Detectorsdetailsbox.Visibility = System.Windows.Visibility.Collapsed;
            switch(log._type)
            {
                case LogFileItemType.TravelTime:
                    showTravelDetails(traveltimedetailsbox, SearchInList(log, traveltimelistbox));
                    break;
                case LogFileItemType.VMSG:
                    showVMSDetails(VMSdetailsbox,SearchInList(log, VMSlistbox));
                    break;
                case LogFileItemType.DetectorData:
                    showDetectorDetails(Detectorsdetailsbox, SearchInList(log, Detectorslistbox));
                    break;
                    
            }
        }

        private void showDetectorDetails(StackPanel Detectorsdetailsbox, TunnelListItem tunnelListItem)
        {
            if (tunnelListItem == null)
                return;
            Detectorsdetailsbox.Visibility = System.Windows.Visibility.Visible;
            DetectorDataItem item = (DetectorDataItem)tunnelListItem;
            detectorslist.ItemsSource = item.DetectorList;
            detectorslist.Items.Refresh();
        }

        private void showVMSDetails(StackPanel VMSdetailsbox, TunnelListItem tunnelListItem)
        {
            if (tunnelListItem == null)
                return;
            VMSdetailsbox.Visibility = System.Windows.Visibility.Visible;
            VMSGMessagesItem item = (VMSGMessagesItem)tunnelListItem;
            VMSGroupID.Text = item.VMSGroupID;
            VMSMsgID.Text = item.VMSMsgID.ToString();
            VMSGroupName.Text = item.GroupName;
            VMSHebrewText.Text = item.HebrewText;
            VMSEnglishText.Text = item.EnglishText;
            VMSEmergency.Text = item.Emergency;

        }

        private void showTravelDetails(StackPanel traveltimedetailsbox, TunnelListItem tunnelListItem)
        {
            if (tunnelListItem == null)
                return;
            traveltimedetailsbox.Visibility = System.Windows.Visibility.Visible; 
            TravelTime item = (TravelTime)tunnelListItem;
            travelSectionID.Text = item.SectionID.ToString();
            travelSectiontime.Text = item.SectionTravelTime.ToString();
            travelDirection.Text = item.Direction;
            travelSectionDescription.Text = item.SectionDescription;
        }

        private TunnelListItem SearchInList(LogFileItem logitem, ListBox list)
        {
            TunnelListItem tempitem = null;
            list.Items.Refresh();
            foreach (TunnelListItem item in list.Items)
            {
                if (logitem._newtime.Ticks == item._newtime.Ticks)
                {
                    tempitem = item;
                    list.SelectedItem = item;
                    list.ScrollIntoView(item);
                    break;
                }
            }
            if(list.Items.Count > 0)
                list.Visibility = System.Windows.Visibility.Visible;

            return tempitem;
        }

        private void AddCounter()
        {
            if (firsttime)
            {
                foreach (CheckBox item in PannelChecker.Children)
	            {
                    LogFileItemType type = (LogFileItemType)Enum.Parse(typeof(LogFileItemType), item.Content.ToString(), true);
                    item.Content = type.ToString() + " (" + collection.LogData.Count(x => x._type == type) + ")";
	            }
                TotalCount.Visibility = System.Windows.Visibility.Visible;
                TotalCount.Text = "------------------------------\nTotal Count: " + collection.LogData.Count;
                firsttime = false;
            }
            TotalVMSCount.Text = "Total VMS items: " + collection.VMSMessagesData.Count;
            TotaTravelTimelCount.Text = "Total Travel Time items: " + collection.TravelTimeData.Count;
            TotalDetectorsCount.Text = "Total Detectors items: " + collection.TunnelDetectorData.Count;
        }

        private void ResetCounters()
        {
            firsttime = true;
            PannelChecker.Children.Clear();
            foreach (LogFileItemType item in Enum.GetValues(typeof(LogFileItemType)))
            {
                CheckBox c = new CheckBox();
                c.Content = item.ToString();
                c.IsChecked = true;
                PannelChecker.Children.Add(c);
            }
            TotalCount.Visibility = System.Windows.Visibility.Collapsed;
            traveltimelistbox.Visibility = System.Windows.Visibility.Collapsed;
            VMSlistbox.Visibility = System.Windows.Visibility.Collapsed;
            Detectorslistbox.Visibility = System.Windows.Visibility.Collapsed;
            traveltimedetailsbox.Visibility = System.Windows.Visibility.Collapsed;
            VMSdetailsbox.Visibility = System.Windows.Visibility.Collapsed;
            Detectorsdetailsbox.Visibility = System.Windows.Visibility.Collapsed;
            TotalCount.Text = "------------------------------\nTotal Count: ";
            TotalVMSCount.Text = "Total VMS items: ";
            TotaTravelTimelCount.Text = "Total Travel Time items: ";
            TotalDetectorsCount.Text = "Total Detectors items: ";
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            TunnelListItem item = listbox.SelectedItem as TunnelListItem;
            switch(listbox.Name)
            {
                case "VMSlistbox":
                    showVMSDetails(VMSdetailsbox, item);
                    break;
                case "traveltimelistbox":
                    showTravelDetails(traveltimedetailsbox, item);
                    break;
                case "Detectorslistbox":
                    showDetectorDetails(Detectorsdetailsbox, item);
                    break;
            }
        }
    }
}
