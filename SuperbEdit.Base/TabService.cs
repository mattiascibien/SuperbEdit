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

            if (((IDictionary<string, Object>)config.UserConfig).Keys.Contains("default_tab"))
            {
                defaultTabName = config.UserConfig.default_tab;
            }
            else if (((IDictionary<string, Object>)config.DefaultConfig).Keys.Contains("default_tab"))
            {
                defaultTabName = config.DefaultConfig.default_tab;
            }

            if (!string.IsNullOrEmpty(defaultTabName))
            {
                var requestedTabFactory = tabFactories.FirstOrDefault(fact => fact.Metadata.Name == defaultTabName);
                if(requestedTabFactory != null)
                    return requestedTabFactory.CreateExport().Value;
                return RequestFallbackTab();
            }

            return RequestFallbackTab();
        }
    }
}
