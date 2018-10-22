using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.CharactersManager
{
    public class CharactersManagerController<PanelType> : IControllerBase<PanelType>
    {
        private ICharactersManagerViewBase<PanelType> _view { get; set; }

        public string ModuleName { get; private set; }

        private ICharactersManagerDatabaseConnection _database;

        public CharactersManagerController(ICharactersManagerViewBase<PanelType> view, ICharactersManagerDatabaseConnection database)
        {
            ModuleName = "Characters manager";
            _view = view;
            _database = database;
        }

        public void RegisterShortcuts(IList<Shortcut<PanelType>> shortcuts)
        {
            throw new NotImplementedException();
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
    }
}
