namespace AC2_M3UF5
{
    class Program
    {
        public static void Main()
        { 
            //CONSTANTS
            const string MsgTitle = "Welcome to the Water Consumption Program of the different regions of Catalonia";
            const string MsgMenu = "Select what you want to do:\n" +
                                    "1. Read CSV File\n" +
                                    "2. Create XML File (the process can be a bit long)\n" +
                                    "3. Read XML File\n" +
                                    "4. Print XML File Data\n" +
                                    "5. Consult Information\n" +
                                    "6. Delete XML File\n" +
                                    "0. Exit";
            const string MsgInvalidInput = "Invalid input. Please try again.";
            const string MsgExit = "Goodbye!";
            const string MsgCreateXMLFirst = "Please create first the XML File.";
            const string MsgReadCSVFirst = "Please read first the CSV File.";
            const string MsgReadXMLFirst = "Please read first the XML File.";
            const string MsgFileNotExist = "The file does not exist.";

            const int VoidList = 0;

            //VARIABLES
            string csvPath = "../../../files/Consum_d_aigua_a_Catalunya_per_comarques_20240402.csv";
            string xmlPath = "../../../files/WaterConsumByRegions.xml";

            int input;
            bool exit = false;

            List<Region> regionsCSV = new List<Region>();
            List<Region> regionsXML = new List<Region>();

            //PROGRAMA
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(MsgTitle);
            Console.ResetColor();

            while (!exit)
            {
                Console.WriteLine(MsgMenu);
                input = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (input)
                {
                    case 0: //EXIT PROGRAM
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(MsgExit);
                        Console.ResetColor();
                        exit = true;
                        break;

                    case 1: //READ CSV FILE
                        regionsCSV = FileHelper.ReadCSVFile(csvPath);
                        break;

                    case 2: //CREATE XML FILE
                        if (regionsCSV.Count == VoidList)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgReadCSVFirst);
                            Console.ResetColor();
                        }
                        else
                        {
                            foreach (Region region in regionsCSV)
                            {
                                if (File.Exists(xmlPath))
                                {
                                    FileHelper.AddRegionToXMLWithLINQ(xmlPath, region);
                                }
                                else
                                {
                                    FileHelper.CreateRegionXMLWithLINQ(xmlPath, region);
                                }
                            }
                        }

                        break;

                    case 3: //READ XML FILE
                        if (!File.Exists(xmlPath))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgCreateXMLFirst);
                            Console.ResetColor();
                        }
                        else
                        {
                            regionsXML = FileHelper.ReadXMLFileWithLINQ(xmlPath);
                        }
                        break;

                    case 4: //PRINT XML FILE DATA
                        if (regionsXML.Count == VoidList)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgReadXMLFirst);
                            Console.ResetColor();
                        }
                        else
                        {
                            FileHelper.PrintReadedRegions(regionsXML);
                        }
                        break;

                    case 5: //CONSULT INFORMATION
                        if (regionsXML.Count == VoidList)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgReadXMLFirst);
                            Console.ResetColor();
                        }
                        else
                        {
                            ConsultMenu(regionsXML);
                        }
                        break;

                    case 6: //DELETE XML FILE
                        if (!File.Exists(xmlPath))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgFileNotExist);
                            Console.ResetColor();
                        }
                        else
                        {
                            FileHelper.DeleteXMLFile(xmlPath);
                        }
                        break;

                    default: //INVALID INPUT
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(MsgInvalidInput);
                        Console.ResetColor();
                        break;
                }

            }            
        }

        private static void ConsultMenu(List<Region> regions)
        {
            //CONSTANTS
            const string MsgMenu = "Select what you want to consult:\n" +
                                          "1. Regions with more than 200000 of Population\n" +
                                          "2. Average domestic consumption by region\n" +
                                          "3. Regions with higher consumption per capita\n" +
                                          "4. Regions with lower consumption per capita\n" +
                                          "5. Filter regions by name or code\n" +
                                          "0. Exit";
            const string MsgInvalidInput = "Invalid input. Please try again.";
            const string MsgEnterFilter = "Enter the filter (Name or Code):";
            const string MsgExit = "Exiting consult menu.";
            const string MsgNotFound = "No results found.";

            const int ExercisePopulation = 200000;

            //VARIABLES
            string? filter;
            int input;
            bool exit = false;

            List<Region> result;
            List<(string regionName, float consumAverage)> resultAverage;

            //PROGRAMA
            while (!exit)
            {
                Console.WriteLine(MsgMenu);
                input = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (input)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(MsgExit);
                        Console.ResetColor();
                        exit = true;
                        break;

                    case 1:
                        result = QueryMethods.SelectRegionByPopulation(regions, ExercisePopulation);
                        
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (Region region in result)
                        {
                            Console.WriteLine($"Year: {region.Year} | Region: {region.Name} | Population: {region.Population}");
                        }
                        Console.ResetColor();

                        break;

                    case 2:
                        resultAverage = QueryMethods.AverageDomesticConsumption(regions);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (var average in resultAverage)
                        {
                            Console.WriteLine($"Region name: {average.regionName} | Average of the domestic consumption: {average.consumAverage}");
                        }
                        Console.ResetColor();

                        break;

                    case 3:
                        result = QueryMethods.SelectRegionByHigherConsumCapita(regions);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (Region region in result)
                        {
                            Console.WriteLine($"Year: {region.Year} | Region: {region.Name} | Domestic consumption by habitant: {region.ConsumCapita}");
                        }
                        Console.ResetColor();

                        break;

                    case 4:
                        result = QueryMethods.SelectRegionByLowerConsumCapita(regions);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (Region region in result)
                        {
                            Console.WriteLine($"Year: {region.Year} | Region: {region.Name} | Domestic consumption by habitant: {region.ConsumCapita}");
                        }
                        Console.ResetColor();

                        break;

                    case 5:
                        Console.WriteLine(MsgEnterFilter);

                        while (string.IsNullOrEmpty(filter = Console.ReadLine()))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgInvalidInput);
                            Console.ResetColor();
                        }

                        result = QueryMethods.SelectRegionByNameOrCode(regions, filter);

                        if (result.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(MsgNotFound);
                            Console.ResetColor();
                        }
                        else
                        {
                            FileHelper.PrintReadedRegions(result);
                        }

                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(MsgInvalidInput);
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
