using System;
using System.Text;

namespace Octgn.DeckAnalyser
{
    public class TextAnalysisRenderer
    {

        public static string RenderAnalysis(DeckAnalysis results)
        {
            StringBuilder analysisText = new StringBuilder();
            analysisText.AppendFormat("=== DECK ANALYSIS ==={0}{0}", Environment.NewLine);

            analysisText.AppendFormat("1 Command for 1 cost: {0}{1}", results.OneCommandForOneCost, Environment.NewLine);
            analysisText.AppendFormat("2 Command for 2 cost: {0}{1}", results.TwoCommandForTwoCost, Environment.NewLine);
            analysisText.AppendFormat("2 Command for 1 cost: {0}{1}{1}", results.TwoCommandForOneCost, Environment.NewLine);

            analysisText.AppendFormat("Totals cards: {0}{1}{1}", results.TotalCards, Environment.NewLine);

            analysisText.AppendFormat("Shields Average: {0}, Total: {1}, Cards: {2}{3}{3}", results.Shields.AverageValue.ToString("0.##"), results.Shields.TotalValue, results.Shields.CardCount, Environment.NewLine);

            analysisText.AppendFormat("Command Average: {0}, Total: {1}, Cards: {2}{3}{3}", results.Command.AverageValue.ToString("0.##"), results.Command.TotalValue, results.Command.CardCount, Environment.NewLine);

            for (int i = 0; i <= 10; i++)
            {
                analysisText.AppendFormat("{0,3} Cost / Attack: {1,3}    /    {2,3}{3}", i.ToString(), results.CountOfCost[i], results.CountOfAttack[i], Environment.NewLine);
            }
            analysisText.AppendFormat("{0}", Environment.NewLine);

            return analysisText.ToString();
        }
    }
}
