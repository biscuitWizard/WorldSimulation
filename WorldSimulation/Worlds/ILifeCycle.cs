namespace WorldSimulation.Worlds
{
    /// <summary>
    /// Main logic loop for life in a galaxy.
    /// </summary>
    public interface ILifeCycle
    {
        /// <summary>
        /// Adds a lifecycle to this lifecycle.
        /// 
        /// The lifecycle implementing this interface will be responsible for handling these
        /// additional lifecycles.
        /// </summary>
        /// <param name="lifeCycle">The life cycle.</param>
        void AddLifeCycle(ILifeCycle lifeCycle);

        /// <summary>
        /// Processes exactly one tick of the lifecycle.
        /// </summary>
        void UpdateTick();
    }
}
