using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseConnectorServiceWCF;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterController<PanelType> : IControllerBase<PanelType>
    {        
        private ITextWriterViewBase<PanelType> _view { get; set; }
        private ITextWriterDataConnection _dataConnection { get; set; }

        public string ModuleName { get; private set; }

        public TextWriterController(ITextWriterViewBase<PanelType> view, ITextWriterDataConnection dataConnection)
        {
            ModuleName = "Text writer";
            _view = view;
            _dataConnection = dataConnection;
        }

        public void RegisterShortcuts(IList<Shortcut<PanelType>> shortcuts)
        {
        }

        public void ShowOnPanel(PanelType panel)
        {
            _view.Panel = panel;
            _view.Show();
        }

        public void UnloadFromPanel()
        {
            _view.Hide();
        }

        public bool PendingChanges()
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            if(_view.FileName.Count() > 0)
                _dataConnection.SaveDocument(new FileData { FileName = _view.FileName, Date = DateTime.Now, Text = _view.Data });
            else
            {
                string documentName = _view.SaveFileName();
                if (documentName.Count() > 0)
                {
                    _view.FileName = documentName;
                    _dataConnection.SaveDocument(new FileData { FileName = _view.FileName, Date = DateTime.Now, Text = _view.Data });
                }
            }
                
        }

        private void SaveNew()
        {
            var documentName = _view.SaveFileName();
            if (documentName.Count() > 0)
            {
                _view.FileName = documentName;
                _dataConnection.SaveDocument(new FileData { FileName = _view.FileName, Date = DateTime.Now, Text = _view.Data });
            }
        }

        private void Load()
        {
            var choosenFile = _view.LoadFile(_dataConnection.GetDocumentNamesAndDates());
            if((choosenFile.FileName != "") && (choosenFile.Date != null))
            {
                _view.Data = _dataConnection.GetDocument(choosenFile);
                _view.FileName = choosenFile.FileName;
            }
        }
    }
}
