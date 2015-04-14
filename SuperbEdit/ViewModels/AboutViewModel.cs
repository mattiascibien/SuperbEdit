using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Octokit;
using SuperbEdit.Base;
using SuperbEdit.Base.Scripting;

namespace SuperbEdit.ViewModels
{
    public class PackageItem
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
    }

    public class ContributorItem
    {
        public string Name { get; set; }
        public string GitHubUrl { get; set; }
        public ImageSource Photo { get; set; }
    }

    [Export]
    public sealed class AboutViewModel : Screen
    {
        public AboutViewModel()
        {
            DisplayName = "About SuperbEdit";
            Contributors = new ObservableCollection<ContributorItem>();
            LoadedPackages =
                AssemblyListComposer.loadedAssemblies
                    .Select(ass => new PackageItem
                    {
                        Name = ass.GetName().Name,
                        Version = ass.GetName().Version.ToString(),
                        Author = ass.GetCustomAttribute<AssemblyCompanyAttribute>().Company
                    });

            LoadedPackages = LoadedPackages.Concat(
                ScriptListComposer.loadedAssemblies
                    .Select(ass => new PackageItem
                    {
                        Name = ass.GetName().Name,
                        Version = ass.GetName().Version.ToString(),
                        //TODO: implement a way to diplay authorship information for scripted plugins
                        Author = "Compiled From Script"
                    }));
        }

        protected override async void OnActivate()
        {
            try
            {
                var github = new GitHubClient(new ProductHeaderValue("SuperbEdit"));
                var contributors = await github.Repository.GetAllContributors("SuperbEdit", "SuperbEdit", true);
                Contributors.Clear();

                foreach (var contributor in contributors)
                {
                    var githubUser = await github.User.Get(contributor.Login);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(githubUser.AvatarUrl, UriKind.Absolute);
                    bitmap.EndInit();

                    Contributors.Add(new ContributorItem()
                    {
                        GitHubUrl = contributor.HtmlUrl,
                        Name = githubUser.Name,
                        Photo = bitmap
                    });
                }
            }
            catch (Exception)
            {

            }

            base.OnActivate();
        }

        public string Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }

        public bool ErrorsPresent
        {
            get
            {
                return ScriptListComposer.HasErrors;
            }
        }

        public List<string> ScriptErrors
        {
            get
            {
                var ret = new List<string>();
                foreach (var ex in ScriptListComposer.Exceptions)
                {
                    ret.AddRange(ex.Errors);
                }
                return ret;
            }
        }

        public ObservableCollection<ContributorItem> Contributors
        {
            get; private set;
        }

        public string Copyright
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                return assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            }
        }

        public IEnumerable<PackageItem> LoadedPackages { get; set; }
    }
}