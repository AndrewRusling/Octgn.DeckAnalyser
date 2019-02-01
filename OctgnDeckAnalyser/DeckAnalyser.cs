using System;
using System.Linq;
using Octgn.Core.DataExtensionMethods;
using Octgn.DataNew.Entities;

namespace Octgn.DeckAnalyser
{
    public static class WH40KCDeckAnalyser
    {
        
        public static DeckAnalysis Analyse(ObservableDeck deck)
        {
            DeckAnalysis results = new DeckAnalysis();

            results.TotalCards = deck.CardCount();

            for (int i = 0; i <= 10; i++)
            {
                results.CountOfCost.Add(QuantityOfCardsEqualingValue(deck, "Cost", i.ToString()));
            }

            for (int i = 0; i <= 10; i++)
            {
                results.CountOfAttack.Add(QuantityOfCardsEqualingValue(deck, "Attack", i.ToString()));
            }

            results.OneCommandForOneCost = QuantityOfCardsEqualingValue(deck, "Cost", "1", "Command", "1");
            results.TwoCommandForTwoCost = QuantityOfCardsEqualingValue(deck, "Cost", "2", "Command", "2");
            results.TwoCommandForOneCost = QuantityOfCardsEqualingValue(deck, "Cost", "1", "Command", "2");

            results.Shields = AnalyseProperty(deck, "Shield");
            results.Command = AnalyseProperty(deck, "Command");

            return results;
        }

        private static int QuantityOfCardsEqualingValue(ObservableDeck deck, string propertyName, string propertyValue)
        {
            return deck.Sections.SelectMany(x => x.Cards).Where(c => c.PropertySet().FirstOrDefault(p => p.Key.Name.Equals(propertyName)).Value.ToString().Equals(propertyValue)).Select(x => x.Quantity).Sum(x => x);
        }

        private static int QuantityOfCardsEqualingValue(ObservableDeck deck, string propertyOneName, string propertyOneValue, string propertyTwoName, string propertyTwoValue)
        {
            return deck.Sections.SelectMany(x => x.Cards).Where(c => c.PropertySet().FirstOrDefault(p => p.Key.Name.Equals(propertyOneName)).Value.ToString().Equals(propertyOneValue))
                .Where(c => c.PropertySet().FirstOrDefault(p => p.Key.Name.Equals(propertyTwoName)).Value.ToString().Equals(propertyTwoValue))
                .Select(x => x.Quantity).Sum(x => x);
        }

        private static PropertyAnalysis AnalyseProperty(ObservableDeck deck, string propertyName)
        {
            PropertyAnalysis results = new PropertyAnalysis();
            if (deck.CardCount() == 0) return results;

            var cardsWithNonZeroPropertyValue = deck.Sections.SelectMany(x => x.Cards).Where(c =>
                !String.IsNullOrWhiteSpace(c.PropertySet().FirstOrDefault(p => p.Key.Name.Equals(propertyName)).Value.ToString()));

            results.CardCount = cardsWithNonZeroPropertyValue.Sum(ps => ps.Quantity);

            results.TotalValue = cardsWithNonZeroPropertyValue.Sum(ps => ps.Quantity *
                int.Parse(ps.PropertySet().FirstOrDefault(p => p.Key.Name.Equals(propertyName)).Value.ToString()));

            results.AverageValue = ((float)results.TotalValue / (float)deck.CardCount());

            return results;
        }
    }

}
