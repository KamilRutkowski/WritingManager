using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DatabaseConnectorServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<FileData> GetDocumentNamesAndDates();
        
        [OperationContract]
        string GetDocument(FileData fileData);

        [OperationContract]
        bool SaveDocument(FileData fileData);

        [OperationContract]
        List<CharacterData> GetCharactersAndDates();

        [OperationContract]
        CharacterData GetCharacter(CharacterData nameAndDate);

        [OperationContract]
        bool SaveCharacter(CharacterData character);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class FileData
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class CharacterData
    {
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string CharacterName { get; set; }

        [DataMember]
        public string BaseInformations { get; set; }

        [DataMember]
        public string Appearance { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
