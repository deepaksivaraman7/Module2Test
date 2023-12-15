using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestAutomation.Utilities
{
    internal class ExcelUtilities
    {
        public static List<UserDetails> ReadExcelData(string excelFilePath, string sheetName)
        {
            List<UserDetails> excelDataList = new();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    var dataTable = result.Tables[sheetName];
                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            UserDetails userData = new()
                            {
                                Username = GetValueOrDefault(row, "username"),
                                Password = GetValueOrDefault(row, "password"),
                                FirstName= GetValueOrDefault(row,"firstname"),
                                LastName= GetValueOrDefault(row,"lastname"),
                                Email= GetValueOrDefault(row,"email"),
                                Comment= GetValueOrDefault(row,"comment"),
                                CourseIndex=GetValueOrDefault(row,"courseindex")
                            };
                            excelDataList.Add(userData);
                        }
                    }
                    else
                    {
                        Console.WriteLine(sheetName + " not found in the excel file.");
                    }
                }
            }
            return excelDataList;
        }
        static string GetValueOrDefault(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}
