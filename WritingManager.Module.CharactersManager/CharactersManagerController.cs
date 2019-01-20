using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.CharactersManager
{
    public class CharactersManagerController<PanelType> : IControllerBase<PanelType>
    {
        private ICharactersManagerViewBase<PanelType> _view;
        private ICharactersManagerDataConnection _dataConnection;

        public string ModuleName { get; private set; }


        public CharactersManagerController(ICharactersManagerViewBase<PanelType> view, ICharactersManagerDataConnection dataConnection)
        {
            ModuleName = "Characters manager";
            _view = view;
            _dataConnection = dataConnection;
            _view.Load += Load;
            _view.Save += Save;
            _view.NewCharacter += NewCharacter;
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

        private void Save()
        {
            var data = new CharacterData()
            {
                CharacterName = _view.Name,
                Appearance = _view.Appearance,
                BaseInformations = _view.BaseInformation,
                Description = _view.Description,
                Date = DateTime.Now
            };
            _dataConnection.SaveCharacter(data);
        }

        private void NewCharacter()
        {
            
            _view.Name = "";
            _view.Appearance = "";
            _view.BaseInformation = "";
            _view.Description = "";
        }

        private void Load()
        {
            (var result, var characterData) = _view.LoadFile(
                _dataConnection
                .GetCharactersAndDates()
                .Select(cd => new FileData() { FileName = cd.CharacterName, Date = cd.Date }));
            if (!result)
                return;
            var data = _dataConnection
                .GetCharacter(new CharacterData() { CharacterName = characterData.FileName, Date = characterData.Date });
            _view.Name = data.CharacterName;
            _view.Appearance = data.Appearance;
            _view.BaseInformation = data.BaseInformations;
            _view.Description = data.Description;
        }
    }
}
