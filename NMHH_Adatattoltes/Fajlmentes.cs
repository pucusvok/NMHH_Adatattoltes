using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;



namespace NMHH_Adatattoltes
{
    internal class Fajlmentes
    {
        public static void XMLMentes(List<Matrica> matricak, Ceg ceg, string mentesHelye)
        {
          


            string filePath = mentesHelye + DateTime.Now.ToString("yyyy-MM-dd") + ".xml";
            string kiadottMatricak = null;
            int i = 1;
            int j = 1;
            string datum;

           
              foreach(Matrica matrica in matricak)
            {
                datum = matrica.datum.Replace(".", "").Replace("-", "").Replace(" ","").Substring(0, 8);
                if (i < 10)
                {
                    kiadottMatricak = kiadottMatricak +
                        "      <mezo eazon=\"01000" + j+"C000" + i + "AA\">"+ matrica.szamlaszam +"</mezo>\n" +
                        "      <mezo eazon=\"01000" + j+"C000" + i + "BA\">" +datum+"</mezo>\n" +
                        "      <mezo eazon=\"01000" + j+"C000" + i + "CA\">" + matrica.kod.Replace("-", "") + "</mezo>\n";
                }
                else
                {
                    kiadottMatricak = kiadottMatricak +
                       "      <mezo eazon=\"01000" + j+"C00" + i + "AA\">" + matrica.szamlaszam + "</mezo>\n" +
                       "      <mezo eazon=\"01000" + j+"C00" + i + "BA\">" + datum + "</mezo>\n" +
                       "      <mezo eazon=\"01000" + j+"C00" + i + "CA\">" + matrica.kod.Replace("-", "") + "</mezo>\n";
                }

                if(i == 35)
                {
                    j++;

                    kiadottMatricak = kiadottMatricak +
                        "      <mezo eazon=\"01000" + j+"B001A\">"+j+"</mezo>\n" +
                        "      <mezo eazon=\"01000" + j+"B002A\">"+ceg.cegnev+"</mezo>\n" +
                        "      <mezo eazon=\"01000" + j+"B003A\">"+ceg.adoszam+"</mezo>\n";
                    i = 0;

                }

                i++;
            }

            string input = "\n  <nyomtatvanyok xmlns=\"http://www.apeh.hu/abev/nyomtatvanyok/2005/01\">\r\n  <abev>\r\n    <hibakszama>0</hibakszama>\n      <hash>" + "</hash>\n    <programverzio>v.3.22.0</programverzio>\r\n  </abev>\r\n  <nyomtatvany>\r\n    <nyomtatvanyinformacio>\r\n      <nyomtatvanyazonosito>AATKOD</nyomtatvanyazonosito>\r\n     " +
             " <nyomtatvanyverzio>2.0</nyomtatvanyverzio>\r\n      <adozo>\r\n        <nev>" + ceg.cegnev + "</nev>\r\n        <adoszam>" + ceg.adoszam + "</adoszam>\r\n      </adozo>\r\n    </nyomtatvanyinformacio>\r\n    <mezok>\n      <mezo eazon=\"010001B001A\">1</mezo>\n      <mezo eazon=\"010001B002A\">" + ceg.cegnev + "</mezo>\r\n      <mezo eazon=\"010001B003A\">" +
             ceg.adoszam + "</mezo>\n" + kiadottMatricak + "      <mezo eazon=\"0A0001C010A\">" + ceg.adoszam + "</mezo>\r\n      <mezo eazon=\"0A0001C011A\">" + ceg.cegnev + "</mezo>\r\n    </mezok>\r\n  </nyomtatvany>\r\n</nyomtatvanyok>\r\n";

            if (!Directory.Exists(mentesHelye)) // Check if the directory exists
            {
                Directory.CreateDirectory(mentesHelye); // Create the directory if it doesn't exist
            }


            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteRaw(input);
            }


        }
    }
}
