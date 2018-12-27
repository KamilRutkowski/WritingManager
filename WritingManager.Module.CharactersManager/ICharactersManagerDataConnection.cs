using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.CharactersManager
{
    public interface ICharactersManagerDataConnection
    {
        List<CharacterData> GetCharactersAndDates();
        CharacterData GetCharacter(CharacterData nameAndDate);
        bool SaveCharacter(CharacterData character);
    }
}
