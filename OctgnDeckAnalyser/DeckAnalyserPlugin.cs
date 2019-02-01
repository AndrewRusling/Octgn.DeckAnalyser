using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using Octgn.Core.DataManagers;
using Octgn.Core.Plugin;
using Octgn.DataNew.Entities;
using Octgn.DeckAnalyser;

namespace OctGn.DeckAnalyser
{ 
    public class DeckAnalyserPlugin : IDeckBuilderPlugin
    {
        private static ILog Log;

        public IEnumerable<IPluginMenuItem> MenuItems
        {
            get
            {
                // Add your menu items here.
                return new List<IPluginMenuItem> { new AnalyseWH40KCUpdateNotes() };
            }
        }

        public void OnLoad(GameManager games)
        {
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            Log.Info("Starting OnLoad");
        }

        public Guid Id
        {
            get
            {
                // unique GUID for Octgn.DeckAnalyserPlugin
                // http://www.guidgenerator.com/online-guid-generator.aspx
                return Guid.Parse("a78cefc3-cee0-4d4e-a6f4-d5f9493018ce");
            }
        }

        public string Name
        {
            get
            {
                // Display name of the plugin.
                return "Deck Analyser plugin";
            }
        }

        public Version Version
        {
            get
            {
                // Version of the plugin.
                // This code will pull the version from the assembly.
                return Assembly.GetCallingAssembly().GetName().Version;
            }
        }

        public Version RequiredByOctgnVersion
        {
            get
            {
                // Don't allow this plugin to be used in any version less than 3.0.12.58
                return Version.Parse("3.2.0.0");
            }
        }

    }

    public class AnalyseWH40KCUpdateNotes : IPluginMenuItem
    {
        private static ILog Log;
        
        public string Name
        {
            get
            {
                return "Replace notes with WH40K Conquest deck analysis";
            }
        }

        /// <summary>
        /// This happens when the menu item is clicked.
        /// </summary>
        /// <param name="con"></param>
        public void OnClick(IDeckBuilderPluginController con)
        {
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            Log.Info("Starting OnClick");

            ObservableDeck curDeck = (ObservableDeck)con.GetLoadedDeck();
            if (curDeck == null)
            {
                MessageBox.Show("Failed to load Deck");
                return;
            }
            
            DeckAnalysis results = WH40KCDeckAnalyser.Analyse(curDeck);
            
            curDeck.Notes = TextAnalysisRenderer.RenderAnalysis(results);
        }
        
    }

}
