using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.FolderPanel.Model
{
    public class TreeItemModel
    {
        public string Text { get; set; }
        public List<TreeItemModel> Children { get; set; }


        public TreeItemModel()
        {
            Children = new List<TreeItemModel>();
        }
    }
}
