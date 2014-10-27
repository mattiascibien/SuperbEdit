using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    public class PanelsViewModel : Conductor<IPanel>.Collection.OneActive
    {
        private PanelPosition _panelPosition;
        public PanelPosition PanelPosition
        {
            get
            {
                return _panelPosition;
            }
            set
            {
                if(_panelPosition != value)
                {
                    _panelPosition = value;
                    NotifyOfPropertyChange(() => PanelPosition);
                }
            }
        }

        public PanelsViewModel([ImportMany] IEnumerable<Lazy<IPanel, IPanelMetadata>> lazyPanels, PanelPosition defaultPanelPosition)
        {
            PanelPosition = defaultPanelPosition;
            Items.AddRange(lazyPanels.Where(x => x.Metadata.DefaultPosition == defaultPanelPosition).Select(x => x.Value));
        }
    }
}
