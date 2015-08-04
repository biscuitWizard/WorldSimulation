using System;

namespace WorldSimulation.Disposition
{
    public class Proclivity
    {
        private const float MaxLeftInclination = -10;
        private const float MaxRightInclination = 10;
        private const float MaxIntensity = 10;
        private const float MinimumIntensity = 0;

        public Proclivity(string category, string leftProclivity, string rightProclivity)
        {
            Category = category;
            LeftProclivity = leftProclivity;
            RightProclivity = rightProclivity;
        }

        // How strongly the issue is felt. More intense issues are harder to change.
        public float Intensity { get; private set; }
        // Value -10 to 10, where -10 is left, and 10 is right, 0 is neutral
        public float Inclination { get; private set; }

        // The names for the issues.
        public string LeftProclivity { get; private set; }
        public string RightProclivity { get; private set; }
        public string Category { get; private set; }


        public void IncrementLeft(float amount = 1)
        {
            float newInclination = (Inclination - amount);
            newInclination = newInclination - (newInclination*(Intensity/MaxIntensity - .1f));
            Inclination = Math.Min(Math.Max(newInclination, MaxLeftInclination), MaxRightInclination);
        }

        public void IncrementRight(float amount = 1)
        {
            float newInclination = (Inclination + amount);
            newInclination = newInclination - (newInclination*(Intensity/MaxIntensity - .1f));
            Inclination = Math.Min(Math.Max(newInclination, MaxLeftInclination), MaxRightInclination);
        }

        public void Reset()
        {
            Intensity = 0;
            Inclination = 0;
        }

        public void IncreaseIntensity(float amount = 1)
        {
            float newIntensity = Intensity + amount;
            Intensity = Math.Min(newIntensity, MaxIntensity);
        }

        public void DecreaseIntensity(float amount = 1)
        {
            float newIntensity = Intensity - amount;
            Intensity = Math.Max(newIntensity, MinimumIntensity);
        }

        public string GetDominantInclination()
        {
            return Inclination > 0 ? RightProclivity : LeftProclivity;
        }

        public float GetIntensity()
        {
            return Intensity;
        }
    }
}