using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExcelDataReader;
using Microsoft.Win32;

namespace NMHH_Adatattoltes
{
    internal class Fajlmegnyitas
    {
        public List<Matrica> matricak = new List<Matrica>();


        public List<Matrica> XlsMegnyitas(string eleres)
        {

            using (var stream = File.Open(eleres, FileMode.Open, FileAccess.Read))
            {
                // Create an ExcelDataReader object
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the first worksheet from the Excel file
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = false
                        }
                    }).Tables[0];

                    // Iterate through each row of the worksheet
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        // Create a new Matrica object and set its properties from the current row of the worksheet
                        var matrica = new Matrica();
                        matrica.szamlaszam = result.Rows[i][0].ToString();
                        matrica.datum = result.Rows[i][1].ToString();
                         matrica.kod = result.Rows[i][2].ToString();

                        // Add the Matrica object to the list
                        matricak.Add(matrica);
                    }
                }
            }
            return matricak;
        }
    }

   
}

