using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WeatherStationSending
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmOptions : Form
    {

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.Label1 = new System.Windows.Forms.Label();
            this.weatherUndergroundStationId = new System.Windows.Forms.TextBox();
            this.weatherUndergroundApi = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.saveSettings = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.DimGray;
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(160, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "WeatherUnderground Station ID";
            // 
            // weatherUndergroundStationId
            // 
            this.weatherUndergroundStationId.Location = new System.Drawing.Point(178, 6);
            this.weatherUndergroundStationId.Name = "weatherUndergroundStationId";
            this.weatherUndergroundStationId.Size = new System.Drawing.Size(187, 20);
            this.weatherUndergroundStationId.TabIndex = 1;
            // 
            // weatherUndergroundApi
            // 
            this.weatherUndergroundApi.Location = new System.Drawing.Point(148, 32);
            this.weatherUndergroundApi.Name = "weatherUndergroundApi";
            this.weatherUndergroundApi.Size = new System.Drawing.Size(217, 20);
            this.weatherUndergroundApi.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(12, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(130, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "WeatherUnderground API";
            // 
            // saveSettings
            // 
            this.saveSettings.BackColor = System.Drawing.Color.DimGray;
            this.saveSettings.ForeColor = System.Drawing.Color.White;
            this.saveSettings.Location = new System.Drawing.Point(275, 58);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(90, 71);
            this.saveSettings.TabIndex = 7;
            this.saveSettings.Text = "Save Settings";
            this.saveSettings.UseVisualStyleBackColor = false;
            this.saveSettings.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(12, 60);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(159, 13);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "How Often To Check in Minutes";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(198, 58);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(159, 112);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Result Logging";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(198, 86);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown2.TabIndex = 14;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Overdue Time in Minutes";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.ForeColor = System.Drawing.Color.White;
            this.checkBox2.Location = new System.Drawing.Point(12, 112);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(93, 17);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "Display Result";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(378, 135);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.saveSettings);
            this.Controls.Add(this.weatherUndergroundApi);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.weatherUndergroundStationId);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOptions";
            this.Text = "    Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal Label Label1;
        internal TextBox weatherUndergroundStationId;
        internal TextBox weatherUndergroundApi;
        internal Label Label2;
        internal Button saveSettings;
        internal Label Label5;
        internal NumericUpDown numericUpDown1;
        internal CheckBox checkBox1;
        internal NumericUpDown numericUpDown2;
        internal Label label6;
        internal CheckBox checkBox2;
    }
}