using System;
using System.Windows;

using StructureMap;

using WorldSimulation.Visualizer.Interface;
using WorldSimulation.Visualizer.Simulations;

namespace WorldSimulation.Visualizer
{
    public class Program
    {
        private readonly ISimulation _simulation;
        private readonly Application _application;
        private readonly IContainer _container;

        protected Program(IContainer container, ISimulation simulation)
        {
            _container = container;
            _simulation = simulation;
            _application = new Application();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            _simulation.SetupSimulation(new SimulationParameters());
            
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
            var container = IoC.Initialize();
            var simulation = new BasicSimulation(container);
            var program = new Program(container, simulation);
            program.Initialize();
            program.Run(new MainWindow
            {
                Simulation = simulation
            });
        }
    }
}
