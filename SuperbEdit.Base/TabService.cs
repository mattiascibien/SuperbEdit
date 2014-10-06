using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    [Export]
    public class TabService
    {
        [ImportMany] private IEnumerable<ExportFactory<ITab, ITabMetadata>> tabFactories;

        [Import] private IConfig config;

        public ITab RequestFallbackTab()
        {
            return tabFactories.First(fact => fact.Metadata.IsFallback).CreateExport().Value;
        }

        public ITab RequestDefaultTab()
        {
            string defaultTabName = "";

            defaultTabName = config.RetrieveConfigValue<string>("default_tab");

            var tab = RequestSpeficTab(defaultTabName);
            return tab ?? RequestFallbackTab();
        }


        public ITab RequestSpeficTab(string friendlyName)
        {
            if (!string.IsNullOrEmpty(friendlyName))
            {
                var requestedTabFactory = tabFactories.FirstOrDefault(fact => fact.Metadata.Name == friendlyName);
                if (requestedTabFactory != null)
                    return requestedTabFactory.CreateExport().Value;
            }
            return null;
        }
    }
}
