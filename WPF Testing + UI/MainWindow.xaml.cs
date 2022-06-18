using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using OpenQA.Selenium.Chrome; 

namespace WPF_Testing___UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    
        public MainWindow()
        {
            InitializeComponent();
            
        }

       
        private void Run_TC(object sender, RoutedEventArgs e)
        {
            TCViewModel tcvm = new();
            this.DataContext = tcvm;
        }
    }

    public class TCViewModel
    {
        private ObservableCollection<TC> _TCinfo;
        public ObservableCollection<TC> TCinfo
        {
            get { return _TCinfo; }
            set
            {
                _TCinfo = value;
            }
        }

        public TCViewModel()
        {
            TCinfo = new ObservableCollection<TC>();
            RunTC();
        }

        private async void RunTC()
        {
            //var index = collectionInstance.Select((item, index) => new { Item = item, Index = index }).First(i => i.Item == SomeCondition()).Index;
            //("1", "2", "3");
            //for (int i = 0; i < 5; i++)
            //{

            //    TCinfo.Add(new TC(i.ToString(), (i+1).ToString(), (i+2).ToString()));
            //    //for (int j = 0; j < 5; j++)
            //    //{
            //    //    i=i+2;
            //    //}
            //}
            //TCinfo[3] = new TC("a", "b", "c");
            //await Task.Delay(1000);
            

            long startTime = DateTime.Now.Ticks;
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            ChromeDriver chrome = new ChromeDriver(chromeDriverService, options);
            try
            {
                TCinfo.Add(new TC("TC_GG_SEARCH", "Opening Browser", "Calculating..."));

                //Hide the cmd             
                await Task.Delay(1000);
                TCinfo[0] = new TC("TC_GG_SEARCH", "Excuting", "Calculating...");
                chrome.Navigate().GoToUrl("https://www.google.com/");
                await Task.Delay(1000);

                var searchbox = chrome.FindElement(OpenQA.Selenium.By.XPath("//input[@class='gLFyf gsfi']"));
                searchbox.SendKeys("OpenCommerce Group");
                await Task.Delay(1000); //Should use wait for element appears better

                var btn = chrome.FindElement(OpenQA.Selenium.By.XPath("//input[@class='RNmpXc']"));
                btn.Click();

                if(chrome.Url == "https://www.opencommercegroup.com/vi")
                {
                    double secondsElapsed = new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds;
                    string time = String.Format("{0:0.00}", secondsElapsed) + "s";
                    chrome.Close();
                    TCinfo[0] = new TC("TC_GG_SEARCH", "Pass", time);
                }
            }
            catch (Exception)
            {
                chrome.Close();
                double secondsElapsed = new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds;
                string time = String.Format("{0:0.00}", secondsElapsed) + "s";
                TCinfo[0] = new TC("TC_GG_SEARCH", "Fail", time);

            }
        }
    }
}
