using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using ShoNS.Visualization;
using WorldSimulation.Entities;
using WorldSimulation.People;
using WorldSimulation.People.Generators;
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
        private readonly IList<Person> _people = new List<Person>();
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

            PersonalityGroupBox.Content = personalityHost;
            FateGroupBox.Content = fateHost;
        }

        public void AddPerson(Person person)
        {
            _people.Add(person);
            ListBoxPeople.Items.Add(person);

            if (ListBoxPeople.Items.Count == 1)
            {
                ListBoxPeople.SelectedItem = person;
            }
        }

        public void ClearPeople()
        {
            _people.Clear();
            ListBoxPeople.Items.Clear();
        }

        private void ListBoxPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the charts.
            _fateChart.ClearData();
            _personalitySeries.Points.Clear();

            //var person = _people.FirstOrDefault(p => p.Id.Equals(ListBoxPeople.SelectedItem));
            var person = ListBoxPeople.SelectedItem as Person;
            if (person == null)
            {
                return;
            }

            // Build personality chart.
            var personality = person.Personality;
            var facets = personality.GetFacets();
            _personalitySeries.Points.DataBindXY(facets, facets.Select(personality.GetFacetValue).ToList());

            // Build Fate graph
            _fateChart.AddSeries(person.Fate.FateNumbers);

            // Load info panel
            InfoPanel.LoadPerson(person);

            // History
            HistoryBlock.Inlines.Clear();
            foreach (var historyLine in person.History.Log)
            {
                HistoryBlock.Inlines.Add(historyLine + Environment.NewLine);
            }
        }

        private void InfoPanel_OnNavigatePersonRequest(object sender, ulong e)
        {
            var person = _people.FirstOrDefault(p => p.Id.Equals(e));
            if (person == null)
            {
                // Fetch the person in a read.
                //AddPerson(e);
            }

            ListBoxPeople.SelectedItem = person;
        }
    }
}
