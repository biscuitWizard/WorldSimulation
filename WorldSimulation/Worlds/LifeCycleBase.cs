using System.Collections.Generic;

namespace WorldSimulation.Worlds
{
    public abstract class LifeCycleBase : ILifeCycle
    {
        private readonly IList<ILifeCycle> _lifeCycles = new List<ILifeCycle>();

        /// <summary>
        /// Adds a lifecycle to this lifecycle.
        /// The lifecycle implementing this interface will be responsible for handling these
        /// additional lifecycles.
        /// </summary>
        /// <param name="lifeCycle">The life cycle.</param>
        public void AddLifeCycle(ILifeCycle lifeCycle)
        {
            _lifeCycles.Add(lifeCycle);
        }

        /// <summary>
        /// Processes exactly one tick of the lifecycle.
        /// </summary>
        public virtual void UpdateTick()
        {
            foreach (var lifeCycle in _lifeCycles)
            {
                lifeCycle.UpdateTick();
            }
        }
    }
}
