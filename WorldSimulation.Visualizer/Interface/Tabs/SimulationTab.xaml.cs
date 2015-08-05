using System;
using System.Windows.Controls;

namespace WorldSimulation.Visualizer.Interface.Tabs
{
    /// <summary>
    /// Interaction logic for SimulationTab.xaml
    /// </summary>
    public partial class SimulationTab : UserControl
    {
        public ISimulation Simulation { get; set; }
        public event EventHandler SimulationComplete;
        public SimulationTab()
        {
            InitializeComponent();
        }

        private void ButtonStartSimulation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Simulation.Simulate(100);
            if (SimulationComplete != null)
            {
                SimulationComplete.Invoke(Simulation, EventArgs.Empty);
            }
        }
    }
}
