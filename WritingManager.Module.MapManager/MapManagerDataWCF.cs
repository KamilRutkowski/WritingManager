using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnectorServiceWCF;

namespace WritingManager.Module.MapManager
{
    public class MapManagerDataWCF : IMapManagerDataConnection
    {
        IService1 _service;

        public MapManagerDataWCF()
        {
            _service = new Service1();
        }

        public ImageData GetImage(ImageData imageData)
        {
            return _service.GetImage(imageData);
        }

        public List<ImageData> GetImagesAndDates()
        {
            return _service.GetImagesAndDates();
        }

        public bool SaveImage(ImageData imageData)
        {
            return _service.SaveImage(imageData);
        }
    }
}
