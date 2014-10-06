using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SuperbEdit.Base
{
    [Export]
    public class TabService
    {
        [Import] private IConfig config;
        [ImportMany] private IEnumerable<ExportFactory<ITab, ITabMetadata>> tabFactories;

        public ITab RequestFallbackTab()
        {
            return tabFactories.First(fact => fact.Metadata.IsFallback).CreateExport().Value;
        }

        public ITab RequestDefaultTab()
        {
            string defaultTabName = "";

            defaultTabName = config.RetrieveConfigValue<string>("default_tab");

            ITab tab = RequestSpeficTab(defaultTabName);
            return tab ?? RequestFallbackTab();
        }


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