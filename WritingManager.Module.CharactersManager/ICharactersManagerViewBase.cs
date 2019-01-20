using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.CharactersManager
{
    public interface ICharactersManagerViewBase<PanelType> : IViewBase<PanelType>
    {
        string Name { get; set; }
        string BaseInformation { get; set; }
        string Appearance { get; set; }
        string Description { get; set; }
        event Action Save;
        event Action Load;
        event Action NewCharacter;
        bool ClearFormPrompt();
        string SaveFileName();
        (bool, FileData) LoadFile(IEnumerable<FileData> textFileInfos);
    }
}
