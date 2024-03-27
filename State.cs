/**
*  AI Assignmnet Group 2
* Team members:
* - Marylou das Chagas e Silva 2501402
* - Lirit Dampier 2560877 
* - Laura Clark 2419746
* - Adam Munro 2455360
* - Abz Mohamed 245757
*/

using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{
    /**
     * The state of a menu item - includes all of the information about it
     */
    public class State
    {
        public string Category { get; set; }
        public string Item { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }

        //Using a class map because our class doesnt match the header names and we cant write the classes in way we need
        public sealed class MenuItemMap : ClassMap<State>
        {
            public MenuItemMap()
            {
                Map(m => m.Category).Name("Category");
                Map(m => m.Item).Name("Item");
                Map(m => m.Calories).Name("Calories");
                Map(m => m.Protein).Name("Protein");
            }
        }
    }
}

