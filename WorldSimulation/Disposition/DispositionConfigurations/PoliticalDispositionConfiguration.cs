using System.Collections.Generic;

namespace WorldSimulation.Disposition.DispositionConfigurations
{
    public class PoliticalDispositionConfiguration : IDispositionConfiguration
    {
        public string Name
        {
            get { return "Politics"; }
        }

        public IList<Proclivity> GenerateDefaultProclivites(IList<Proclivity> proclivities)
        {
            const string politicsLeft = "Liberal";
            const string politicsRight = "Conservative";
            proclivities.Add(new Proclivity("Tax Structure", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Abortion Rights", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Animal Rights", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Police Regulation", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Privacy Rights", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Death Penalty", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Nuclear Power", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Pollution", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Labor Laws", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Gay Rights", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Corporate Law", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Free Speech", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Flag Burning", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Gun Control", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Civil Rights", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Drug Law", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Election Reform", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Immigration", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Women's Rights", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Military Spending", politicsLeft, politicsRight));
            proclivities.Add(new Proclivity("Human Rights", politicsLeft, politicsRight));

            return proclivities;
        }
    }
}