using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiggy.Utilities
{
    internal class SearchUtils
    {
        public static List<SearchData> ReadSearchData(string excelFilePath,string sheetName)
        {
            List<SearchData> excelSearchList = new List<SearchData>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new FileStream(excelFilePath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                using (var reader= ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if(dataTable != null)
                    {
                        
                        foreach (DataRow row in dataTable.Rows)
                        {
                            SearchData data = new SearchData() { RestaurantName= GetValueOrDefault(row, "Restaurant Name"),FoodItemName=GetValueOrDefault(row,"Food Item Name") };

                            excelSearchList.Add(data);
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                    
                }
            }
            return excelSearchList;

        }
        static string GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }




    }
}
