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
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using MathNet.Numerics;

namespace FT_Simulation
{
    public partial class Form1 : Form
    {
        int numSamples = 2000;
        int samplingRate = 4000;
        double[] samples;


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

        void PlotInputGraph(float dcOffset, float amp, float freq, float phaseShift, int endpoint)
        {
            this.Controls.Add(pv_input);
            pv_input.Model = new OxyPlot.PlotModel { Title = "Input Waveform" };

            FunctionSeries fs = new FunctionSeries();

            //numSamples = samplingRate * endpoint;
            samples = Generate.Sinusoidal(numSamples, samplingRate, freq, amp, dcOffset, phaseShift);

            for (int i = 0; i < samples.Length; i++)
            {
                double time = ((i + 1.0) / numSamples);
                fs.Points.Add(new OxyPlot.DataPoint(time, samples[i]));
            }

            
            pv_input.Model.Series.Clear();
            pv_input.Model.Series.Add(fs);

            PlotFTGraph();
        }

        void PlotFTGraph()
        {
            this.Controls.Add(pv_ft);
            pv_ft.Model = new OxyPlot.PlotModel { Title = "Fourier Transform" };


            Complex[] c_samples = new Complex[samples.Length];

            double numSamples = c_samples.Length;

            for (int i = 0; i < numSamples; i++)
            {
                c_samples[i] = new Complex(samples[i], 0);
            }
            Fourier.Forward(c_samples, FourierOptions.NoScaling);

            FunctionSeries fs = new FunctionSeries();

            for (int i = 0; i < numSamples; i++)
            {
                fs.Points.Add(new OxyPlot.DataPoint((samplingRate / numSamples) * i, c_samples[i].Magnitude));
            }
            pv_ft.Model.Series.Clear();
            pv_ft.Model.Series.Add(fs);
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
            int endpoint = int.Parse(textBox5.Text);
            PlotInputGraph(dcOffset, amp, freq, phaseShift, endpoint);
        }
    }
}
