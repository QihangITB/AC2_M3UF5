using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace AC2_M3UF5
{
    public class Helper
    {
        public static List<Region> ReadCSVFile(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            var records = csv.GetRecords<Region>();
            
            return records.ToList();
        }

        public static void CreateXMLFileWithLINQ(string xmlFilePath, Region region)
        {
            const string MsgCreated = "XML file created successfully.";

            XDocument xmlDoc = new XDocument(
                new XElement("regions",
                    new XElement("region",
                        new XElement("year", region.Year),
                        new XElement("code", region.Code),
                        new XElement("name", region.Name),
                        new XElement("population", region.Population),
                        new XElement("domesticConsum", region.DomesticConsum),
                        new XElement("economyConsum", region.EconomyConsum),
                        new XElement("totalConsum", region.TotalConsum),
                        new XElement("consumCapita", region.ConsumCapita)
                    )
                )
            );

            xmlDoc.Save(xmlFilePath);
            Console.WriteLine(MsgCreated);
        }

        public static void AddRegionToXMLFileWithLINQ(string xmlFilePath, Region region)
        {
            const string MsgAdded = "Region added to XML file successfully.";

            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            XElement newRegion = new XElement("region",
                new XElement("year", region.Year),
                new XElement("code", region.Code),
                new XElement("name", region.Name),
                new XElement("population", region.Population),
                new XElement("domesticConsum", region.DomesticConsum),
                new XElement("economyConsum", region.EconomyConsum),
                new XElement("totalConsum", region.TotalConsum),
                new XElement("consumCapita", region.ConsumCapita)
            );

            xmlDoc.Root.Add(newRegion);
            xmlDoc.Save(xmlFilePath);
            Console.WriteLine(MsgAdded);
        }
    }
}
