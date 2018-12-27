using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnectorServiceWCF;

namespace WritingManager.Module.CharactersManager
{
    public class CharactersManagerDataWCF : ICharactersManagerDataConnection
    {
        private IService1 _service1;

        public CharactersManagerDataWCF()
        {
            _service1 = new Service1();
        }

        public CharacterData GetCharacter(CharacterData nameAndDate)
        {
            return _service1.GetCharacter(nameAndDate);
        }

        public List<CharacterData> GetCharactersAndDates()
        {
            return _service1.GetCharactersAndDates();
        }

        public bool SaveCharacter(CharacterData character)
        {
            return _service1.SaveCharacter(character);
        }
    }
}
