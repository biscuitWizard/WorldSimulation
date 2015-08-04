namespace WorldSimulation.Visualizer
{
    public interface ISimulation
    {
        void SetupSimulation();
        void Simulate(int years);
    }
}
