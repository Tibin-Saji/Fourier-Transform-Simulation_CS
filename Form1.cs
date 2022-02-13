using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.WindowsForms;
using OxyPlot.Series;


namespace FT_Simulation
{
    public partial class Form1 : Form
    {
        PlotView pv_input = new PlotView();
        PlotView pv_ft = new PlotView();
        public Form1()
        {
            InitializeComponent();
            pv_input.Location = new Point(0, 0);
            pv_input.Size = new Size(500, 500);
            pv_ft.Location = new Point(0, 500);
            pv_ft.Size = new Size(500, 500);
        }

        void PlotGraph(float dcOffset, float amp, float freq, float phaseShift, float endpoint)
        {
            this.Controls.Add(pv_input);

            pv_input.Model = new OxyPlot.PlotModel { Title = "Input Waveform" };

            FunctionSeries fs = new FunctionSeries();
            for (float t = 0; t < endpoint; t += 0.1f/freq)
            {
                fs.Points.Add(new OxyPlot.DataPoint(t, amp * Math.Sin(t*freq + phaseShift) + dcOffset));
            }

            
            pv_input.Model.Series.Clear();
            pv_input.Model.Series.Add(fs);
        }


        void Form1_Load(object sender, System.EventArgs e)
        {
            
    }

    private void button1_Click(object sender, EventArgs e)
        {
            float dcOffset = float.Parse(textBox1.Text);
            float amp = float.Parse(textBox2.Text);
            float freq = float.Parse(textBox3.Text);
            float phaseShift = float.Parse(textBox4.Text);
            float endpoint = float.Parse(textBox5.Text);
            PlotGraph(dcOffset, amp, freq, phaseShift, endpoint);
        }
    }
}
