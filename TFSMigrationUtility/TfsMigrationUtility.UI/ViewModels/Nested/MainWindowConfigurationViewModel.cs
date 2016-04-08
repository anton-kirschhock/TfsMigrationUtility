using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TfsMigrationUtility.Core;


namespace TfsMigrationUtility.UI.ViewModels.Nested
{
    public class MainWindowConfigurationViewModel : AbstractNestedViewModel
    {
        public ICell<bool> MigrateSingleProject { get; set; }
        public ICell<bool> DebugMode { get; set; }
        public ICell<bool> CleanupLocal { get; set; }
        public ICell<bool> AutocreateProject { get; set; }
        public ICell<string[]> Projects { get; set; }
        public ICell<TfsTeamProjectCollection> SourceTFS { get; set; }
        public ICell<TfsTeamProjectCollection> TargetTFS { get; set; }

        public string SourceTFSName
        {
            get
            {
                return (SourceTFS.Value == null ? "No Collection selected" : SourceTFS.Value.Name);
            }
        }

        public string TargetTFSName
        {
            get
            {
                return (TargetTFS.Value == null ? "No Collection selected" : TargetTFS.Value.Name);
            }
        }


        public MainWindowConfigurationViewModel(IViewModel Parent, string propertyName) : base(Parent, propertyName)
        {
            MigrateSingleProject = new Cell<bool>(true, this, nameof(MigrateSingleProject));
            DebugMode = new Cell<bool>(true, this, nameof(DebugMode));
            CleanupLocal = new Cell<bool>(false, this, nameof(CleanupLocal));
            AutocreateProject = new Cell<bool>(false, this, nameof(AutocreateProject));
            Projects = new Cell<string[]>(new string[1], this, nameof(Projects));
            SourceTFS = new Cell<TfsTeamProjectCollection>(null, this, nameof(SourceTFSName));
            TargetTFS = new Cell<TfsTeamProjectCollection>(null, this, nameof(TargetTFSName));
        }

        public ICommand OnStart
        {
            get
            {
                return new RelayCommand(_ => {
                    //Relay data to other view
                },_=>Projects.HasValue() && SourceTFS.HasValue() && TargetTFS.HasValue());
            }

        }

        public ICommand OnSelectCollection
        {
            get
            {
                return new RelayCommand(
                    sender =>
                    {
                        //mode = multipleproject if source, collection if target
                        TeamProjectPickerMode mode = (sender as string == "source" ?
                        (this.MigrateSingleProject.Value ?
                            TeamProjectPickerMode.SingleProject
                            : TeamProjectPickerMode.MultiProject)
                        : TeamProjectPickerMode.NoProject);
                        TeamProjectPicker picker = new TeamProjectPicker(mode, false);
                        if (picker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (sender as string == "source")
                            {
                                this.SourceTFS.Value = picker.SelectedTeamProjectCollection;
                                this.Projects.Value = picker.SelectedProjects.Select(a=>"$/" + a.Name).ToArray();
                                //set projects
                            }
                            else if (sender as string == "target")
                            {
                                this.TargetTFS.Value = picker.SelectedTeamProjectCollection;
                            }
                        }
                    }
                    );
            }
        }
    }
}
