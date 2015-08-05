using System;
using System.Windows;
using WorldSimulation.Caches.People;
using WorldSimulation.Visualizer.Simulations;

namespace WorldSimulation.Visualizer.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ISimulation Simulation { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SimulationTab.Simulation = Simulation;
        }

        private void SimulationTab_OnSimulationComplete(object sender, EventArgs e)
        {
            var simulation = (BasicSimulation) sender;
            var cache = simulation.PersonCache as DictionaryPersonCache;

            foreach (var person in cache.TakeRandom(1000))
            {
                PeopleTab.AddPerson(person);
            }
        }
    }
}
