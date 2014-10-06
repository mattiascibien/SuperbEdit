using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Baml2006;
using System.Windows.Markup;

namespace Tomers.WPF.Themes
{
	public static class BamlHelper
	{
		public static TRoot LoadBaml<TRoot>(Stream stream)
		{
            Baml2006Reader baml = new Baml2006Reader(stream);
            object bamlRoot = XamlReader.Load(baml);
			return (TRoot)bamlRoot;
		}
	}
}
