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
            throw new NotImplementedException();
        }

        public List<FileData> GetDocumentNamesAndDates()
        {
            throw new NotImplementedException();
        }

        public bool SaveDocument(FileData fileData)
        {
            return service1.SaveDocument(fileData);
        }
    }
}
