using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Configo
{
    public class ExcelDao
    {
        public List<ExcelSheet> GetSheets(string fileName, string fileExt)
        {
            var sheets = new List<ExcelSheet>();
            var schema = GetExcelSchema(fileName, fileExt);

            foreach (DataRow item in schema.Rows)
            {
                var sheetName = item?["TABLE_NAME"]?.ToString();
                if (string.IsNullOrWhiteSpace(sheetName) || !Constants.ExcelNameEndWithCharacters.Any(x => sheetName.EndsWith(x))) { continue; }
                var excelSheet = new ExcelSheet()
                {
                    Name = sheetName
                };

                sheets.Add(excelSheet);
            }

            return sheets;
        }

        public DataTable GetExcelSheetContents(string fileName, string fileExt, string command)
        {
            return ReadExcel(fileName, fileExt, command);
        }

        private DataTable GetExcelSchema(string fileName, string fileExt)
        {
            return ReadExcel(fileName, fileExt);
        }

        public DataTable ReadExcel(string fileName, string fileExt, string optCommand = null)
        {
            DataTable dataTable = new DataTable();
            string excelConStr = ExcelConnection(fileName, fileExt);

            using (OleDbConnection connection = new OleDbConnection(excelConStr))
            {
                using (OleDbCommand command = new OleDbCommand())
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        command.Connection = connection;
                        connection.Open();

                        if (!string.IsNullOrWhiteSpace(optCommand))
                        { // Run Command
                            command.CommandText = optCommand;
                            adapter.SelectCommand = command;
                            adapter.Fill(dataTable);
                        } 
                        else
                        { // Return Schema if No Command
                            dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        }
                        connection.Close();
                    }
                }
            }
            return dataTable;
        }

        private string ExcelConnection(string fileName, string fileExt)
        {
            return (fileExt == ".xls") ?
                string.Format(ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString, fileName) : //Excel 97-03.
                string.Format(ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString, fileName); //Excel 07 and above.
        }
    }
}
