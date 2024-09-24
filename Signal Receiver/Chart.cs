using ScottPlot;
using ScottPlot.Palettes;
using ScottPlot.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Signal_Receiver
{
    public partial class Form1 : Form
    {
        private double[] _dataX = new double[0];
        private double[] _dataY = new double[0];
        private List<double> _bufferX = new List<double>();
        private List<double> _bufferY = new List<double>();
        private double _ms      = 0;
        private double _frequency = 0.1;
        private double _xMinX    = 0;
        private int _counter    = 0;
        private float _marker = 5;
        

        private void InitializeChart()
        {
            formsPlot1.Plot.YLabel("Received Data");
            formsPlot1.Plot.XLabel("Display Frequency");
            formsPlot1.Plot.Title("Function");
            formsPlot1.Plot.SetAxisLimitsX(0, 2);
            formsPlot1.Refresh();
        }



        private void UpdateChart(string? text)
        { 
             
            

            _ms += _frequency;
            _counter += 1;


            formsPlot1.Plot.XLabel($"Display Frequency {_frequency.ToString()}x ");
            formsPlot1.Plot.YLabel("Received Data");
            formsPlot1.Plot.Title("Function");

            text = text.Replace("\n", "").Replace("\r", "");
            _number = float.Parse(text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);

            _dataX = _dataX.Append(_ms).ToArray();
            _dataY = _dataY.Append(_number).ToArray();
            
            

            formsPlot1.Plot.AddScatter(_dataX, _dataY, color: Color.Red, markerSize: _marker);
            

            


            if (_IsFilling)
            {
                formsPlot1.Plot.AddFillAboveAndBelow(_dataX, _dataY,
                     baseline: 0,
                     colorAbove: Color.FromArgb(100, Color.LightGreen),
                     colorBelow: Color.FromArgb(100, Color.IndianRed));
            }
            if (!_IsFilling)
            {
                formsPlot1.Plot.AddFillAboveAndBelow(_dataX, _dataY,
                    baseline: 0,
                    colorAbove: Color.Transparent,
                    colorBelow: Color.Transparent);

            }


            if (_IsAutoScale)
            {
                formsPlot1.Plot.AxisAuto();
                _xMinX = _ms > 12 ? _ms - 12 : 0;
                formsPlot1.Plot.SetAxisLimits(xMin: _xMinX, xMax: null);
                formsPlot1.Plot.SetAxisLimitsY(yMin: -1.5, yMax: 1.5);
            }


            formsPlot1.Refresh();



            if (_counter == 99)
            {
                _bufferX.Add(_ms);
                _bufferY.Add(_number);
            }
            if (_counter == 100)
            {
                _dataX = _bufferX.ToArray();
                _dataY = _bufferY.ToArray();
                _bufferX.Clear();
                _bufferY.Clear();
                _counter = 0;
            }
        }



        private void DropChartParam()
        {
            _dataX = [];
            _dataY = [];
            _counter = 0;
            _xMinX = 0;
            _ms = 0;
        }


       

    }
}
