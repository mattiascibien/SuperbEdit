using System;
using System.Windows.Controls;

namespace SuperbEdit.Views
{
	/// <summary>
	/// Description of PanelsView.
	/// </summary>
	public partial class PanelsView : UserControl
	{
		public PanelsView()
		{
			InitializeComponent();
            this.IsVisibleChanged += PanelsView_IsVisibleChanged;
		}

        void PanelsView_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            (this.Parent as ContentControl).Visibility = this.Visibility;
        }
	}
}
