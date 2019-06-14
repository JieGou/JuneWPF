using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Data;
using System.Collections.ObjectModel;


namespace JuneWPF.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private ExternalEvent exEvent;
        public string WindowTitle { get; private set; }
        public int Progress { get; set; }

        public RelayCommand SelectCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand DeleteCommand { get; }

        private UIDocument uidoc = null;
        public List<KeyValuePair<string, string>> nvc { get; set; }
        public string _result { get; set; }

        public ObservableCollection<Model.DWGcontainer> DwgList { get; set; }

        public Model.DWGcontainer SelectedDWG { get; set; }

        private Model.RequestHandler handler { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //if (IsInDesignMode)
            //{
            //    WindowTitle = "design-mode title";
            //    Progress = 25;
            //}
            //else
            //{
            //    WindowTitle = " run model title";
            //    Task.Delay(2000).ContinueWith(t =>
            //    {
            //        while (Progress < 100)
            //        {
            //            Progress += 5;
            //            Task.Delay(500).Wait();
            //        }
            //    });
            //}
            WindowTitle = " run model title";
            Task.Delay(500).ContinueWith(t =>
            {
                while (Progress < 100)
                {
                    Progress += 15;
                    Task.Delay(100).Wait();
                }
            });

            DwgList = new ObservableCollection<Model.DWGcontainer>();

            handler = new Model.RequestHandler();

            exEvent = ExternalEvent.Create(handler);

            uidoc = Command._activeRevitUIDoc;

            SelectCommand = new RelayCommand(() => HandleSelect());
            OpenCommand = new RelayCommand(() => OpenView());
            DeleteCommand = new RelayCommand(() => DeleteDWG());

            
            //ListCollectionView collectionView = new ListCollectionView(employees);
        }

        private void DeleteDWG()
        {
            if (this.SelectedDWG != null)
            {
                handler.dwg = SelectedDWG.DWGElement;
                exEvent.Raise();
                DwgList.Remove(SelectedDWG);
            }
            else
                TaskDialog.Show("Error", "DWG not selected");
        }

        private void OpenView()
        {
            uidoc.Application.ActiveUIDocument.ActiveView = SelectedDWG.ViewElement;
        }


        private void HandleSelect()
        {
            DwgList = Model.FindDWG.listEmployees(uidoc.Document);
        }

        public RelayCommand MyRelayCommand {

            get
            {
                return new RelayCommand(LaunchCommand, true);
            }


        }

        private void LaunchCommand()
        {
            TaskDialog.Show("sss", "aaa");
        }


    }
}