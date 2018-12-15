using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DatabaseConnectorServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Studia\\WritingManager\\DatabaseConnectorServiceWCF\\App_Data\\Database.mdf;Integrated Security=True";

        public string GetDocument(FileData fileData)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetDocumentDates(string documentName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetDocumentNames()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var request = "SELECT FileName FROM [dbo].[FileNames];";
                var command = new SqlCommand(request, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var result = new List<string>();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                return result;
            }
        }

        public List<FileData> GetDocumentNamesAndDates()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var request = "SELECT f.FileName, d.SaveDate FROM [dbo].[FileNames] as f, [dbo].[SavedDocuments] as d where f.Id = d.FileNameId;";
                var command = new SqlCommand(request, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var result = new List<FileData>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new FileData() { FileName = reader.GetString(0), Date = reader.GetDateTime(1) });
                    }
                }
                reader.Close();
                return result;
            }
        }

        public bool SaveDocument(FileData fileData)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var request = "SELECT Id FROM [dbo].[FileNames] WHERE FileName like @filename;";
                var command = new SqlCommand(request, connection);
                int indexFound = -1;
                command.Parameters.AddWithValue("@filename", fileData.FileName);
                connection.Open();
                try
                {
                    indexFound = (int)((decimal)command.ExecuteScalar());
                    
                }
                catch (Exception e) { }
                if(indexFound == -1)
                {
                    var insertDocumentNameQuerry = "Insert Into [dbo].[FileNames] (FileName) Values (@value); Select SCOPE_IDENTITY();";
                    var insertDocumentNameCommand = new SqlCommand(insertDocumentNameQuerry, connection);
                    insertDocumentNameCommand.Parameters.AddWithValue("@value", fileData.FileName);
                    decimal tmp = (decimal)insertDocumentNameCommand.ExecuteScalar();
                    indexFound = (int)tmp;
                }
                var insertDocumentQuerry = "Insert Into [dbo].[SavedDocuments] (FileNameId, SaveDate, Text) values (@FNId, @date, @text)";
                var insertDocumentCommand = new SqlCommand(insertDocumentQuerry, connection);
                insertDocumentCommand.Parameters.AddWithValue("@FNId", indexFound);
                insertDocumentCommand.Parameters.AddWithValue("@date", fileData.Date);
                insertDocumentCommand.Parameters.AddWithValue("@text", fileData.Text);
                return insertDocumentCommand.ExecuteNonQuery() > 0;
            }
        }
    }
}
