using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Testing___UI
{
    public class TC: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string status;

            public string TestcaseID { get; set; }
            public string Status
            {
                get { return status; }
                set
                {
                    status=value;
                    OnPropertyChanged(Status);
                }
            }
            public string TimeExcute { get; set; }
        public TC(string TestcaseID, string Status, string TimeExcute)
        {
            this.TestcaseID=TestcaseID;
            this.Status=Status;
            this.TimeExcute=TimeExcute;
        }
    }
}
