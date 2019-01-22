using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.MapManager
{
    public interface IMapManagerDataConnection
    {
        ImageData GetImage(ImageData imageData);

        bool SaveImage(ImageData imageData);

        List<ImageData> GetImagesAndDates();
    }
}
