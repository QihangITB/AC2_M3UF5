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
    public class FileHelper
    {
        private const string DefaultStringValue = "-";
        private const int DefaultIntValue = 0;

        public static List<Region> ReadCSVFile(string csvFilePath)
        {
            const string MsgRead = "CSV file read successfully.";

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            var records = csv.GetRecords<Region>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MsgRead);
            Console.ResetColor();

            return records.ToList();
        }

        public static void CreateRegionXMLWithLINQ(string xmlFilePath, Region region)
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MsgCreated);
            Console.ResetColor();
        }

        public static void AddRegionToXMLWithLINQ(string xmlFilePath, Region region)
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

            xmlDoc.Root?.Add(newRegion);
            xmlDoc.Save(xmlFilePath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MsgAdded);
            Console.ResetColor();
        }

        public static void DeleteXMLFile(string xmlFilePath)
        {
            const string MsgDeleted = "XML file deleted successfully.";
            File.Delete(xmlFilePath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MsgDeleted);
            Console.ResetColor();
        }

        public static List<Region> ReadXMLFileWithLINQ(string xmlFilePath)
        {
            const string MsgRead = "XML file read successfully.";

            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            var regions = from region in xmlDoc.Descendants("region")
                          where region.Element("year") != null && 
                                region.Element("code") != null && 
                                region.Element("name") != null && 
                                region.Element("population") != null && 
                                region.Element("domesticConsum") != null && 
                                region.Element("economyConsum") != null && 
                                region.Element("totalConsum") != null && 
                                region.Element("consumCapita") != null
                          select new Region
                          {
                                Year = (int?)region.Element("year") ?? DefaultIntValue,
                                Code = (int?)region.Element("code") ?? DefaultIntValue,
                                Name = (string?)region.Element("name") ?? DefaultStringValue,
                                Population = (int?)region.Element("population") ?? DefaultIntValue,
                                DomesticConsum = (int?)region.Element("domesticConsum") ?? DefaultIntValue,
                                EconomyConsum = (int?)region.Element("economyConsum") ?? DefaultIntValue,
                                TotalConsum = (int?)region.Element("totalConsum") ?? DefaultIntValue,
                                ConsumCapita = (float?)region.Element("consumCapita") ?? DefaultIntValue
                          };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MsgRead);
            Console.ResetColor();

            return regions.ToList();
        }

        public static void PrintReadedRegions(List<Region> regions)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Region region in regions)
            {
                Console.WriteLine(region);
            }
            Console.ResetColor();
        }

    }
}
