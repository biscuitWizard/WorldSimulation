using System.Collections.Generic;

namespace WorldSimulation.Disposition.DispositionConfigurations
{
    public class ScienceDispositionConfiguration : IDispositionConfiguration
    {
        public string Name
        {
            get { return "Science"; }
        }

        public IList<Proclivity> GenerateDefaultProclivites(IList<Proclivity> proclivities)
        {
            const string scienceLeft = "Ignore";
            const string scienceRight = "Focus";
            proclivities.Add(new Proclivity("Stem Cell Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Power Storage Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Power Generation Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Personal Weapons Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Vehicle Weapons Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("High Power Computing Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Medical Technology Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Life-Extension Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Construction Efficiency", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Offworld Mining Efficiency", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Terraforming Technology", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Cloaking Technology", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Nanotechnology Damage Control", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Shield Systems", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Laser Weaponry", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Plasma Beam Weaponry", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Meson Beam Weaponry", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Microwave Weaponry", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Industrial Cargo Handling", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Engineering Tooling", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Salvage Technology", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Contagion Cure Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Railgun Weaponry", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Gauss Cannon Weaponry", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Utility Infrastructure", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Civilian CommodityBlueprint Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Engine Fuel Efficiency Research", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Power Plant Emissions", scienceLeft, scienceRight));
            proclivities.Add(new Proclivity("Genetic Recombination", scienceLeft, scienceRight));

            return proclivities;
        }
    }
}