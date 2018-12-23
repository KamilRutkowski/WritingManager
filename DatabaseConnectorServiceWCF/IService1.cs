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

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class FileData
    {
        string _fileName = "";
        DateTime _date = DateTime.MinValue;
        string _text = "";

        [DataMember]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
