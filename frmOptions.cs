using System;
using System.IO;
using System.Windows.Forms;

namespace WeatherStationSending
{

    public partial class frmOptions
    {
        public frmOptions()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Create the XML file...
            var sw = new StreamWriter(Application.StartupPath + @"\Settings.xml");
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine("<SettingsXML xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            sw.WriteLine("  <WeatherUndergroundStationID>" + weatherUndergroundStationId.Text + "</WeatherUndergroundStationID>");
            sw.WriteLine("  <WeatherUndergroundAPI>" + weatherUndergroundApi.Text + "</WeatherUndergroundAPI>");
            sw.WriteLine("  <OverdueTimeInMinutes>" + numericUpDown2.Value.ToString() + "</OverdueTimeInMinutes>");
            sw.WriteLine("  <ResultLog>" + checkBox1.Checked.ToString() + "</ResultLog>");
            sw.WriteLine("  <DisplayResult>" + checkBox2.Checked.ToString() + "</DisplayResult>");
            sw.WriteLine("  <HowOftenToCheckInMinutes>" + numericUpDown1.Value.ToString() + "</HowOftenToCheckInMinutes>");
            sw.WriteLine("</SettingsXML>");
            sw.Close();

            // Set the values to the new settings...
            MainModule.weatherUndergroundStationId = weatherUndergroundStationId.Text;
            MainModule.weatherUndergroundApi = weatherUndergroundApi.Text;
            MainModule.resultLog = checkBox1.Checked;
            MainModule.displayResult = checkBox2.Checked;
            MainModule.overdueTimeInMinutes = (int)Math.Round(numericUpDown2.Value);

            //Reset the timer to the new value.
            MainModule.howOftenToCheckInMinutes = (int)Math.Round(numericUpDown1.Value);
            MainModule.timer1.Stop();
            MainModule.timer1.Enabled = false;
            MainModule.timer1.Interval = MainModule.ConvertMinutesToMilliseconds(MainModule.howOftenToCheckInMinutes);
            MainModule.timer1.Enabled = true;
            MainModule.timer1.Start();


            //Close the options window...
            this.Close(); 
        }
    }
}