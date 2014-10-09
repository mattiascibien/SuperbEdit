using System;
using System.ComponentModel.Composition;

namespace SuperbEdit.Base
{
    public enum PanelPosition
    {
        Left = 0,
        Right = 1,
        Bottom = 2
    }

    public interface IPanelMetadata
    {
        PanelPosition DefaultPosition { get; }
    }


    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportPanelMetadata : ExportAttribute, IPanelMetadata
    {
        public ExportPanelMetadata()
            : base(typeof(IPanel))
        {
        }

        public PanelPosition DefaultPosition { get; set; }

        public bool IsFallback
        {
            get { return false; }
        }
    }

    public interface IPanel
    {
        PanelPosition DefaultPosition { get; }
        PanelPosition Position { get; }
    }

}