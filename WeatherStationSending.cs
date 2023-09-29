using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace WeatherStationSending
{

    static class MainModule
    {

        public static System.Timers.Timer timer1;
        public static System.Timers.Timer timer2;
        public static System.Timers.Timer timer3;


        static MainModule()
        {
            timer1 = new System.Timers.Timer();
            timer2 = new System.Timers.Timer();
            timer3 = new System.Timers.Timer();

            notifyIcon1 = new NotifyIcon();
            menu2 = new ContextMenu();
        }

        public static NotifyIcon notifyIcon1;
        private static ContextMenu menu2;
        private const string toolstripSeparator = "-";

        public static long urlTimeout = 20;
        public static int overdueTimeInMinutes = 0;
        public static int howOftenToCheckInMinutes = 0;
        public static int countIcon = 0;
        public static int downIcon = 0;
        public static bool resultLog = false;
        public static bool displayResult = false;
        public static bool optionsSaved = false;
        public static bool gotPage = true;
        public static bool pageIsLoaded = false;
        public static string refreshInterval = "30000";
        public static string urlString = "";
        public static string weatherUndergroundStationId = "";
        public static string weatherUndergroundApi = "";
        public static string timeDifference = "";

        private static Icon icon0 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.Icheckdown2.ico"));
        private static Icon icon1 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.Icheckdown.ico"));
        private static Icon icon2 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.ICheckUp.ico"));

        private static Icon icon12 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU1.ico"));
        private static Icon icon13 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU2.ico"));
        private static Icon icon14 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU3.ico"));
        private static Icon icon15 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU4.ico"));
        private static Icon icon16 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU5.ico"));
        private static Icon icon17 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU6.ico"));
        private static Icon icon18 = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherStationSending.IU7.ico"));

        public static void Main()
        {
            try
            {
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    MessageBox.Show("Another Instance of this process is already running", "Multiple Instances Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Environment.Exit(0);
                }

                notifyIcon1.Icon = icon2;
                menu2.MenuItems.Add("Options", MenuItem1_Click);
                menu2.MenuItems.Add(toolstripSeparator);
                menu2.MenuItems.Add("View Error Log", MenuItem2_Click);
                menu2.MenuItems.Add("View Result File", MenuItem3_Click);
                menu2.MenuItems.Add("Open EXE folder", MenuItem4_Click);
                menu2.MenuItems.Add(toolstripSeparator);
                menu2.MenuItems.Add("Check Now", MenuItem5_Click);
                menu2.MenuItems.Add(toolstripSeparator);
                menu2.MenuItems.Add("Exit", MenuItem6_Click);

                notifyIcon1.ContextMenu = menu2;
                notifyIcon1.Visible = true;
                GetSettings();
                timer1.Enabled = true;
                timer1.AutoReset = true;
                timer1.Elapsed += new ElapsedEventHandler(Timer1_Elapsed);
                timer2.Enabled = false;
                timer2.AutoReset = true;
                timer2.Interval = 100;
                timer2.Elapsed += new ElapsedEventHandler(Timer2_Elapsed);
                timer3.Enabled = false;
                timer3.AutoReset = true;
                timer3.Interval = 500;
                timer3.Elapsed += new ElapsedEventHandler(Timer3_Elapsed);
                SystemEvents.SessionEnding += ShutDown.OnShuttingdown;
                CheckConnection();
                Application.Run();
            }

            catch (Exception err)
            {
                LogInfo("Sub Main " + err.Message + err.StackTrace.ToString());
                while (err.InnerException is not null)
                {
                    err = err.InnerException;
                    LogInfo("Sub Main " + err.Message + err.StackTrace.ToString());
                }
            }


        }
        public static void LogInfo(string LogString)
        {
            var sw = new StreamWriter(Application.StartupPath + @"\WeatherStationSendingError.log", true);
            sw.WriteLine(Conversions.ToString(DateTime.Now) + " == " + LogString);
            sw.Close();
        }

        public static void LogResults(string LogString)
        {
            if (resultLog == true)
            {
                var sw = new StreamWriter(Application.StartupPath + @"\WeatherStationSendingResults.log", true);
                sw.WriteLine(Conversions.ToString(DateTime.Now) + Conversions.ToString('\t') + " == " + LogString);
                sw.Close();
            }
        }

        public static int ConvertMillisecondsToMinutes(int milliseconds)
        {
            return milliseconds / 60000;
        }

        public static int ConvertMinutesToMilliseconds(int minutes)
        {
            return minutes * 60000;
        }

        private static void MenuItem1_Click(object sender, EventArgs e)
        {
            // Options screen displayed here
            var fOptions = new frmOptions();
            fOptions.weatherUndergroundStationId.Text = weatherUndergroundStationId;
            fOptions.weatherUndergroundApi.Text = weatherUndergroundApi;
            fOptions.checkBox1.Checked = resultLog;
            fOptions.checkBox2.Checked = displayResult;
            fOptions.numericUpDown1.Value = ConvertMillisecondsToMinutes(Convert.ToInt32(timer1.Interval));
            fOptions.numericUpDown2.Value = overdueTimeInMinutes;                   
            fOptions.ShowDialog();
        }

        private static void MenuItem2_Click(object sender, EventArgs e)
        {
            OpenFile(Application.StartupPath + @"\WeatherStationSendingError.log");
        }

        private static void MenuItem3_Click(object sender, EventArgs e)
        {
            OpenFile(Application.StartupPath + @"\WeatherStationSendingResults.log");
        }

        private static void MenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Application.StartupPath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = Application.StartupPath,
                        FileName = "explorer.exe"
                    };
                    Process.Start(startInfo);
                }
                else
                {
                    MessageBox.Show(string.Format("{0} Directory does not exist!", Application.StartupPath));
                }
            } catch (Exception err)
            {
                MessageBox.Show("Error trying to open path: " + Application.StartupPath + " Error: " + err.Message);
            }
        }

        private static void MenuItem5_Click(object sender, EventArgs e)
        {
            // Call the API...
            CheckConnection(); 
        }

        private static void MenuItem6_Click(object sender, EventArgs e)
        {
            // Exit program
            notifyIcon1.Dispose();
            Application.Exit();
        }


        private static void OpenFile(string filename)
        {
            if (File.Exists(filename))
            {
                using Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + filename + "\"";
                fileopener.Start();
            }
            else
            {
                MessageBox.Show(filename + " was not found.");
            }
        }

        public static void GetSettings()
        {
            // Read in the xml file for the various settings...
            try
            {
                if (File.Exists(Application.StartupPath + @"\Settings.xml"))
                {

                    XmlDocument ORead;

                    ORead = new XmlDocument();
                    ORead.Load(Application.StartupPath + @"\Settings.xml");

                    XmlNode OList2;

                    OList2 = ORead.SelectSingleNode("SettingsXML/WeatherUndergroundStationID");
                    weatherUndergroundStationId = OList2.InnerText;

                    OList2 = ORead.SelectSingleNode("SettingsXML/WeatherUndergroundAPI");
                    weatherUndergroundApi = OList2.InnerText;

                    try
                    {
                        OList2 = ORead.SelectSingleNode("SettingsXML/ResultLog");
                        resultLog = Conversions.ToBoolean(OList2.InnerText);
                    }
                    catch
                    {
                        resultLog = false;
                    }

                    try
                    {
                        OList2 = ORead.SelectSingleNode("SettingsXML/DisplayResult");
                        displayResult = Conversions.ToBoolean(OList2.InnerText);
                    }
                    catch
                    {
                        displayResult = false;
                    }

                    try
                    {
                        OList2 = ORead.SelectSingleNode("SettingsXML/OverdueTimeInMinutes");
                        overdueTimeInMinutes = Conversions.ToInteger(OList2.InnerText);
                    }
                    catch
                    {
                        overdueTimeInMinutes = 10;
                    }
                    
                    try
                    {
                        OList2 = ORead.SelectSingleNode("SettingsXML/HowOftenToCheckInMinutes");
                        howOftenToCheckInMinutes = Conversions.ToInteger(OList2.InnerText);
                        timer1.Interval = ConvertMinutesToMilliseconds(howOftenToCheckInMinutes);
                    }
                    catch
                    {
                        howOftenToCheckInMinutes = 10;
                        timer1.Interval = ConvertMinutesToMilliseconds(howOftenToCheckInMinutes);
                    }

                }

                else
                {
                    // Create the XML file since we cannot seem to find it...
                    // The station ID and API can be obtained from WeatherUnderground.
                    // Login and click on MyProfile. Select the Devices.  You will see any devices with the station ID listed.  The next tab at the top
                    // is the API keys you will need.  
                    var sw = new StreamWriter(Application.StartupPath + @"\Settings.xml");
                    sw.WriteLine("<?xml version=\"1.0\"?>");
                    sw.WriteLine("<SettingsXML xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    sw.WriteLine("  <WeatherUndergroundStationID>MYSTATIONID</WeatherUndergroundStationID>");
                    sw.WriteLine("  <WeatherUndergroundAPI>MYAPI</WeatherUndergroundAPI>");
                    sw.WriteLine("  <OverdueTimeInMinutes>10</OverdueTimeInMinutes>");                    
                    sw.WriteLine("  <ResultLog>0</ResultLog>");
                    sw.WriteLine("  <DisplayResult>0</DisplayResult>");                    
                    sw.WriteLine("  <HowOftenToCheckInMinutes>10</HowOftenToCheckInMinutes>");
                    sw.WriteLine("</SettingsXML>");
                    sw.Close();
                    weatherUndergroundStationId = "";
                    weatherUndergroundApi = "";
                    howOftenToCheckInMinutes = 10;
                    resultLog = false;
                    displayResult = false;
                    timer1.Interval = ConvertMinutesToMilliseconds(howOftenToCheckInMinutes);
                    overdueTimeInMinutes = 10;
                }
            }

            catch (Exception err)
            {
                LogInfo("Get Settings " + err.Message + err.StackTrace.ToString());
                while (err.InnerException is not null)
                {
                    err = err.InnerException;
                    LogInfo("Get Settings " + err.Message + err.StackTrace.ToString());
                }
            }
        }

        private static void CheckConnection()
        {
            try
            {
                timer3.Enabled = false;
                timer2.Enabled = true;
                bool SomeFound = false;

                if (!string.IsNullOrEmpty(weatherUndergroundStationId))
                {
                    if (GetWeather() == true)
                    {
                        SomeFound = true;
                    }
                    else 
                    {
                        SomeFound = false;
                        LogInfo("The report is overdue.  Check the Weather Underground Website last observed date and time.");
                    }
                }
                                             
                timer2.Enabled = false;

                if (SomeFound == true)
                {
                    if (resultLog == true)
                    {
                        LogResults("The weather station is currently sending.");
                    }
                    notifyIcon1.Icon = icon2;
                }
                else
                {
                    if (resultLog == true)
                    {
                        LogResults("The weather station is NOT currently sending.");
                    }
                    timer3.Enabled = true;
                }

                if (displayResult == true)
                {
                    notifyIcon1.BalloonTipText = timeDifference;
                    notifyIcon1.ShowBalloonTip(5000);  // This is now deprecated and can no longer be set. It is controlled by Windows accessibility
                }
            }

            catch (Exception err)
            {
                LogInfo("Check Connection " + err.Message + err.StackTrace.ToString());
                while (err.InnerException is not null)
                {
                    err = err.InnerException;
                    LogInfo("Check Connection " + err.Message + err.StackTrace.ToString());
                }
            }

        }

        public static Boolean GetWeather()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            try
            {
                // Weather Underground
                string url = "https://api.weather.com/v2/pws/observations/current?stationId=" + weatherUndergroundStationId + "&format=json&units=e&apiKey=" + weatherUndergroundApi;
                string json = new WebClient().DownloadString(url);
                json = json.Replace(Conversions.ToString('"'), "").Replace("{", "").Replace("}", "");
                //obsTimeLocal:2023-08-24 20:10:17
                var firstIndex = json.IndexOf("obsTimeLocal:") + 13;
                var localDateTime = Convert.ToDateTime(json.Substring(firstIndex, 19));
                TimeSpan diffResult = DateTime.Now.Subtract(localDateTime);
                timeDifference = "Diff in mins: " + diffResult.Minutes.ToString() + ". Last date: " + localDateTime.ToString();
                LogResults(timeDifference);
                if (diffResult.Minutes > overdueTimeInMinutes)
                {
                    return false;  //Report is overdue so flag it...
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                LogInfo("Unable to retrieve the report.  This may be due to problems with internet connectivity or the remote server cannot be reached at this time.");
                return false; //Some error getting the json report...
            }
        }

        private static void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Go and try to get the image file...
            timer1.Enabled = false;
            CheckConnection();
            timer1.Enabled = true;
        }

        private static void Timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            countIcon++;
            if (countIcon > 6)
                countIcon = 1;
            switch (countIcon)
            {
                case 1:
                    {
                        notifyIcon1.Icon = icon12;
                        break;
                    }
                case 2:
                    {
                        notifyIcon1.Icon = icon13;
                        break;
                    }
                case 3:
                    {
                        notifyIcon1.Icon = icon14;
                        break;
                    }
                case 4:
                    {
                        notifyIcon1.Icon = icon15;
                        break;
                    }
                case 5:
                    {
                        notifyIcon1.Icon = icon16;
                        break;
                    }
                case 6:
                    {
                        notifyIcon1.Icon = icon17;
                        break;
                    }
                case 7:
                    {
                        notifyIcon1.Icon = icon18;
                        break;
                    }
            }
        }

        private static void Timer3_Elapsed(object sender, ElapsedEventArgs e)
        {
            downIcon++;
            if (downIcon > 1)
                downIcon = 0;
            switch (downIcon)
            {
                case 0:
                    {
                        notifyIcon1.Icon = icon0;
                        break;
                    }
                case 1:
                    {
                        notifyIcon1.Icon = icon1;
                        break;
                    }
            }
        }

    }

    public partial class ShutDown
    {
        public ShutDown()
        {
            InitializeComponent();
        }
        public static void OnShuttingdown(object sender, SessionEndingEventArgs e)


        {
            e.Cancel = false;   // only set to true if you turn down the shutdown request
                                // Console.WriteLine("Shutting down - Reason is " & e.Reason)

            // your code here to clean up before exiting

            MainModule.notifyIcon1.Dispose();
            Application.Exit();

        }
    }
}