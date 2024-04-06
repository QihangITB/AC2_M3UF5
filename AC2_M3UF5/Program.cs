namespace AC2_M3UF5
{
    class Program
    {
        static void Main()
        {
            string csvPath = "../../../files/Consum_d_aigua_a_Catalunya_per_comarques_20240402.csv";
            string xmlPath = "../../../files/WaterConsumByRegions.xml";

            List<Region> regions = FileController.ReadCSVFile(csvPath);

            foreach (Region region in regions)
            {
                if (File.Exists(xmlPath))
                {
                    FileController.AddRegionToXMLFileWithLINQ(xmlPath, region);
                }
                else
                {
                    FileController.CreateXMLFileWithLINQ(xmlPath, region);
                }
            }
        }
    }
}
