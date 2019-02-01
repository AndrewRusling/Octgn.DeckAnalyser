using System.Collections.Generic;

namespace Octgn.DeckAnalyser
{
    public class DeckAnalysis
    {
        public int TotalCards;
        public IList<int> CountOfCost = new List<int>(11); //0 to 10 cost
        public IList<int> CountOfAttack = new List<int>(11); //0 to 10 attack 
        public int OneCommandForOneCost;
        public int TwoCommandForTwoCost;
        public int TwoCommandForOneCost;
        public PropertyAnalysis Command;
        public PropertyAnalysis Shields;

        public DeckAnalysis() { }
    }

    public class PropertyAnalysis
    {
        public int CardCount = 0;
        public int TotalValue = 0;
        public float AverageValue = 0;

        public PropertyAnalysis() { }
    }
}
