using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnectorServiceWCF;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterDataWCF : ITextWriterDataConnection
    {
        private IService1 service1;

        public TextWriterDataWCF()
        {
            service1 = new Service1();
        }

        public string GetDocument(FileData fileData)
        {
            return service1.GetDocument(fileData);
        }

        public List<FileData> GetDocumentNamesAndDates()
        {
            return service1.GetDocumentNamesAndDates();
        }

        public bool SaveDocument(FileData fileData)
        {
            return service1.SaveDocument(fileData);
        }
    }
}
