using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.MapManager
{
    public class MapManagerController<PanelType> : IControllerBase<PanelType>
    {
        public string ModuleName { get; private set; } = "Map manager";

        private IMapManagerDataConnection _dataConnection;
        private IMapManagerViewBase<PanelType> _view;

        public MapManagerController(IMapManagerViewBase<PanelType> view, IMapManagerDataConnection dataConnection)
        {
            _dataConnection = dataConnection;
            _view = view;
            _view.Add += AddImage;
            _view.Load += LoadImage;
        }

        public bool PendingChanges()
        {
            throw new NotImplementedException();
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

        private void AddImage()
        {
            var imagePath = _view.AddImage();
            if (imagePath == "")
                return;
            var imageName = _view.NameImage(Path.GetFileNameWithoutExtension(imagePath));
            var imageItem = new ImageData();
            imageItem.ImageName = imageName;
            imageItem.Date = DateTime.Now;
            imageItem.ImageArray = File.ReadAllBytes(imagePath);            
            _dataConnection.SaveImage(imageItem);
        }

        private void LoadImage()
        {
            var imageData = _view.LoadImage(_dataConnection.GetImagesAndDates()
                .Select(imdata => new FileData()
                {
                    Date = imdata.Date,
                    FileName = imdata.ImageName
                }).ToList());
            var img = _dataConnection.GetImage(new ImageData()
            {
                Date = imageData.Date,
                ImageName = imageData.FileName
            });
            _view.ImageName = img.ImageName;
            _view.ImageArray = img.ImageArray;
        }
    }
}
