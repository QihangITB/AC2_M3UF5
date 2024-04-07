using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC2_M3UF5
{
    public class QueryMethods
    {
        public static List<Region> SelectRegionByPopulation(List<Region> regions, int population)
        {
            var result = regions.Where(region => region.Population > population).ToList();

            return result;
        }

        public static List<(string Name, float Average)> AverageDomesticConsumption(List<Region> regions)
        {
            /*  
             * Per consum domestic mitjà per comarca, he entes que demana la mitjana de tots els consums 
             * domestics d'una mateixa comarca, durant els diferents anys. Per tant, he agrupat per comarca
             * i he utilitzat la funció Average per calcular la mitjana.
             */
            var linqQuery = from region in regions
                            group region by region.Name into regionGroup
                            select 
                            (
                                Name: regionGroup.Key, 
                                Average: (float) regionGroup.Average(region => region.DomesticConsum)
                            );

            return linqQuery.ToList();
        }

        public static List<Region> SelectRegionByHigherConsumCapita(List<Region> regions)
        {
            /* 
             * Com que no s'ha indicat un limit de regions a mostrar, he decidit ordenarles de manera
             * descendent, d'aquesta manera els que tenen major consum estaran al principi. 
             */
            var descOrder = regions.OrderByDescending(region => region.ConsumCapita).ToList();

            return descOrder;
        }

        public static List<Region> SelectRegionByLowerConsumCapita(List<Region> regions)
        {
            /*
             * Lo mateix que el cas anterior pero aquesta de forma ascendent, mostrant els que menys 
             * consum tenen al principi.
             */
            var ascOrder = regions.OrderBy(region => region.ConsumCapita).ToList();

            return ascOrder;
        }

        public static List<Region> SelectRegionByNameOrCode(List<Region> regions, string filter)
        {
            //Jo he entes que amb només contenir el filtre, ja es mostraria, i que no ha de ser exactament igual.

            var linqQuery = from region in regions
                        where region.Name.Contains(filter.ToUpper()) || region.Code.ToString().Contains(filter)
                        select region;

            return linqQuery.ToList();
        }
    }
}
