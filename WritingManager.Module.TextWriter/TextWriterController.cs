using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterController<PanelType> : ControllerBase<PanelType>
    {
        
        private TextWriterViewBase<PanelType> _view { get; set; }
        private ITextWriterDatabaseConnection _database;

        public TextWriterController(TextWriterViewBase<PanelType> view, ITextWriterDatabaseConnection database):base(view, database)
        {
            ModuleName = "Text writer";
            _view = view;
            _database = database;
        }

        public override void RegisterShortcuts(IList<Shortcut<PanelType>> shortcuts)
        {
            throw new NotImplementedException();
        }

        public override void ShowOnPanel(PanelType panel)
        {
            _view.Panel = panel;
            _view.Show();
        }

        public override void UnloadFromPanel()
        {
            throw new NotImplementedException();
        }

        public override bool PendingChanges()
        {
            throw new NotImplementedException();
        }
    }
}
