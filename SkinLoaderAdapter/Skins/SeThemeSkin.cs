using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace Tomers.WPF.Themes.Skins
{
	public sealed class SeThemeSkin : Skin
	{
		string _zipFile = "";

		public SeThemeSkin(string name, string zipFile) : base(name)
		{
			_zipFile = zipFile;
		}

		protected override void LoadResources()
		{
			using (FileStream reader = new FileStream(_zipFile, FileMode.Open))
			{
				using (ZipArchive zipArch = new ZipArchive(reader, ZipArchiveMode.Read))
				{
					foreach (var entry in zipArch.Entries)
					{
						var streamEntry = entry.Open();
						ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(streamEntry);
						this.Resources.Add(dic);
                    }
				}
			}
		}
	}
}
