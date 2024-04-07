using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace AC2_M3UF5
{
    public class Region
    {
        private const string DefaultStringValue = "-";
        private const int DefaultIntValue = 0;


        [Name("Any")]
        public int Year { get; set; }

        [Name("Codi comarca")]
        public int Code { get; set; }

        [Name("Comarca")]
        public string Name { get; set; }

        [Name("Població")]
        public int Population { get; set; }

        [Name("Domèstic xarxa")]
        public int DomesticConsum { get; set; }

        [Name("Activitats econòmiques i fonts pròpies")]
        public int EconomyConsum { get; set; }

        [Name("Total")]
        public int TotalConsum { get; set; }

        [Name("Consum domèstic per càpita")]
        public float ConsumCapita { get; set; }

        public Region(int year, int code, string name, int population, int domesticConsum, int economyConsum, int totalConsum, float consumPerCapita)
        {
            Year = year;
            Code = code;
            Name = name;
            Population = population;
            DomesticConsum = domesticConsum;
            EconomyConsum = economyConsum;
            TotalConsum = totalConsum;
            ConsumCapita = consumPerCapita;
        }

        public Region() : this(DefaultIntValue, DefaultIntValue, DefaultStringValue, DefaultIntValue, DefaultIntValue, DefaultIntValue, DefaultIntValue, DefaultIntValue)
        { }
        public override string ToString()
        {
            //Mostrem només els camps que són més importants per a la visualització de les dades.
            return $"Year: {Year}, Code: {Code}, Name: {Name}, Population: {Population}, TotalConsum: {TotalConsum}";
        }
    }
}
