﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterController<PanelType> : IControllerBase<PanelType>
    {        
        private ITextWriterViewBase<PanelType> _view { get; set; }

        public string ModuleName { get; private set; }

        private ITextWriterDatabaseConnection _database;

        public TextWriterController(ITextWriterViewBase<PanelType> view, ITextWriterDatabaseConnection database)
        {
            ModuleName = "Text writer";
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
