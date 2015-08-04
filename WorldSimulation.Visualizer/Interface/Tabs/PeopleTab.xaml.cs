using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using ShoNS.Visualization;
using Grid = System.Windows.Controls.Grid;

namespace WorldSimulation.Visualizer.Interface.Tabs
{
    /// <summary>
    /// Interaction logic for PeopleTab.xaml
    /// </summary>
    public partial class PeopleTab : UserControl
    {
        private readonly ShoChart _fateChart;
        private readonly Chart _personalityChart;
        private readonly ChartArea _personalityChartArea;
        private readonly Series _personalitySeries;
        private readonly string[] _xs = new[] { "Openness To Experience", "Conscientiousness", "Extraversion", "Agreeableness", "Neuroticism" };
        public PeopleTab()
        {
            InitializeComponent();

            _fateChart = new ShoChart();
            _personalityChart = new Chart();
            _personalityChartArea = new ChartArea();
            _personalitySeries = new Series();
            InitializeChart();
            InitializePersonalityChart();
        }

        private void InitializeChart()
        {
            var range = Enumerable.Range(0, 100);
            _fateChart.AddSeries(range, range.Select(r => 1));
        }

        private void InitializePersonalityChart()
        {
            _personalityChart.ChartAreas.Add(_personalityChartArea);
            _personalityChartArea.BackColor = System.Drawing.Color.OldLace;
            _personalityChartArea.BackSecondaryColor = System.Drawing.Color.White;
            _personalityChartArea.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
            _personalityChartArea.ShadowColor = System.Drawing.Color.Transparent;
            _personalityChartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            _personalityChartArea.AxisX.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
            _personalityChartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
            _personalityChartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            _personalityChartArea.AxisY.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
            _personalityChartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
            _personalityChartArea.AxisY.MajorTickMark.Size = 0.6F;
            _personalityChart.Width = 300;
            _personalityChart.Height = 300;
            _personalityChart.Series.Add(_personalitySeries);
            _personalitySeries.ChartType = SeriesChartType.Radar;
            _personalitySeries.Points.DataBindXY(_xs, _xs.Select(x => 0).ToArray());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var fateHost = new WindowsFormsHost {Child = _fateChart};
            var personalityHost = new WindowsFormsHost {Child = _personalityChart};

            PersonalityGrid.Children.Add(personalityHost);

            DetailsGrid.Children.Add(fateHost);
            Grid.SetRow(fateHost, 2);
            Grid.SetColumn(fateHost, 0);
            Grid.SetColumnSpan(fateHost, 2);

            Grid.SetColumn(personalityHost, 1);
        }
    }
}
