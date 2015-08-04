using System.Collections.Generic;

namespace WorldSimulation.Disposition.DispositionConfigurations
{
    public class MarketDispositionConfiguration : IDispositionConfiguration
    {
        public string Name
        {
            get { return "Market"; }
        }

        public IList<Proclivity> GenerateDefaultProclivites(IList<Proclivity> proclivities)
        {
            const string marketLeft = "Sell";
            const string marketRight = "Buy";
            proclivities.Add(new Proclivity("Edible Commodities", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Industrial Components", marketLeft, marketRight));
            proclivities.Add(new Proclivity("High-Value Accessories", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Technology Components", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Hazardous Chemicals", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Medical Supplies", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Inert Chemicals", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Space Construction Components", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Ground Construction Components", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Domestic Appliances", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Consumer Technology", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Black-Market Narcotics", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Black-Market Alcohol", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Black-Market Ampethamines", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Black-Market Weapons", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Terraforming Technology", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Argicultural Technology", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Marine Equipment", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Power Generators", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Ancient Artifacts", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Experimental Chemicals", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Experimental Technology", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Military Technology", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Research Data", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Trade Data", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Rare Artwork", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Slaves", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Robotics", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Biowaste", marketLeft, marketRight));
            proclivities.Add(new Proclivity("Industrial Waste", marketLeft, marketRight));

            return proclivities;
        }
    }
}