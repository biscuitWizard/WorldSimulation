using System.Collections.Generic;

namespace WorldSimulation.Disposition.DispositionConfigurations
{
    public class PersonalityDispositionConfiguration : IDispositionConfiguration
    {
        public string Name { get { return "Personality"; } }
        public IList<Proclivity> GenerateDefaultProclivites(IList<Proclivity> proclivities)
        {
            const string leftPersonality = "Less";
            const string rightPersonality = "More";

            proclivities.Add(new Proclivity("Ease in Public", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Opening up to Others", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Diplomacy", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Persuasion", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Leading", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Taking responsibility", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Organization", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Vision", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Self Confidence", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Independency", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Creativity", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Autonomy", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Stress Management", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Responsiveness", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Patience", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Respect for Authority", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Determination", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Ambition", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Work Ethic", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Competitive Spirit", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Empathy", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Self Control", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Conscientiousness", leftPersonality, rightPersonality));
            proclivities.Add(new Proclivity("Resourcefulness", leftPersonality, rightPersonality));

            return proclivities;
        }
    }
}
