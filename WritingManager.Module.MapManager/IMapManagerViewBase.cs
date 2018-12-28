using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.MapManager
{
    public interface IMapManagerViewBase<PanelType>: IViewBase<PanelType>
    {
        string ImageName { set; }
        byte[] ImageArray { set; }

        event Action Add;
        event Action Load;
        FileData LoadImage(List<FileData> imagesInDB);
        string AddImage();
        string NameImage(string baseName);

        
    }
}
