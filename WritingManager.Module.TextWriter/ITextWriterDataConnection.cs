using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.TextWriter
{
    public interface ITextWriterDataConnection
    {
        List<FileData> GetDocumentNamesAndDates();
        string GetDocument(FileData fileData);
        bool SaveDocument(FileData fileData);
    }
}
