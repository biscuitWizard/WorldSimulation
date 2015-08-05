namespace WorldSimulation.Visualizer
{
    public interface ISimulation
    {
        void SetupSimulation(SimulationParameters simulationParameters);
        void Simulate(int years);
    }
}
