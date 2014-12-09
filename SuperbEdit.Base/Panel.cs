using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Abstract class providing a basic implementation for IPanel
    /// </summary>
    public abstract class Panel : Screen, IPanel
    {

        /// <summary>
        /// Default panel position
        /// </summary>
        public PanelPosition DefaultPosition { get; private set; }


        /// <summary>
        /// Actual position of the panel
        /// </summary>
        public PanelPosition Position
        {
            get; set;
        }

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                NotifyOfPropertyChange(() => Visible);
            }
        }

        public void ShowHide()
        {
            Visible = !Visible;
        }
    }
}
