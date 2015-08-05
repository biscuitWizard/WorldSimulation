using System;

using StructureMap;

using WorldSimulation.Caches;
using WorldSimulation.Caches.People;
using WorldSimulation.Caches.World;
using WorldSimulation.People;

namespace WorldSimulation.Visualizer
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(
                x =>
                    {
                        x.For<IPersonCache>().Use<DictionaryPersonCache>().Singleton();
                        x.For<IProfessionCache>().Use<DictionaryProfessionCache>().Singleton();
                        x.For<ISimulationTime>().Use(new MonthlySimulationTime(DateTime.UtcNow));
                        x.For<IModule>().Use<PopulationModule>().Singleton();
                        x.For<Random>().Use(new Random()).Singleton();
                        x.For<PopulationModule>().Use<PopulationModule>().Singleton();
                    });

            return ObjectFactory.Container;
        }
    }
}
