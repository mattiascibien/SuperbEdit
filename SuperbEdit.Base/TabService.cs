using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Class used for requesting an instance of a Tab
    /// </summary>
    [Export]
    public class TabService
    {
        const string FALLBACKNAME = "FallbackTab";

        [Import] private IConfig config;
        [ImportMany] private IEnumerable<ExportFactory<ITab, ITabMetadata>> tabFactories;

        /// <summary>
        /// Requests the default tab (the one that is always available)
        /// </summary>
        /// <returns></returns>
        public ITab RequestFallbackTab()
        {
            return tabFactories.First(fact => fact.Metadata.Name == FALLBACKNAME).CreateExport().Value;
        }


        /// <summary>
        /// Request the default_tab specified in config
        /// </summary>
        /// <returns></returns>
        public ITab RequestDefaultTab()
        {
            string defaultTabName = "";

            defaultTabName = config.RetrieveConfigValue<string>("default_tab");

            ITab tab = RequestSpeficTab(defaultTabName);
            return tab ?? RequestFallbackTab();
        }

        /// <summary>
        /// Reuqest a tab with a particoular name
        /// </summary>
        /// <param name="friendlyName">the name of the tab</param>
        /// <returns></returns>
        public ITab RequestSpeficTab(string friendlyName)
        {
            if (!string.IsNullOrEmpty(friendlyName))
            {
                ExportFactory<ITab, ITabMetadata> requestedTabFactory =
                    tabFactories.FirstOrDefault(fact => fact.Metadata.Name == friendlyName);
                if (requestedTabFactory != null)
                    return requestedTabFactory.CreateExport().Value;
            }
            return null;
        }
    }
}