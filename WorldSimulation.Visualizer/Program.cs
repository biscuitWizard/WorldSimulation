using System;
using System.Windows;
using WorldSimulation.Visualizer.Interface;
using WorldSimulation.Visualizer.Simulations;

namespace WorldSimulation.Visualizer
{
    public class Program
    {
        private readonly ISimulation _simulation;
        private readonly Application _application;

        protected Program(ISimulation simulation)
        {
            _simulation = simulation;
            _application = new Application();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            _simulation.SetupSimulation();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run(Window startupWindow)
        {
            _application.Run(startupWindow);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            var random = new Random();
            var simulation = new BasicSimulation(random);
            var program = new Program(simulation);
            program.Initialize();
            program.Run(new MainWindow());
        }
    }
}
