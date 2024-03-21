using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace FastFoodNutritionAI
{
    //each menu item, add or remove items whenver just change add the correct csv heading in the mapping function
    public class MenuItem
    {
        public string Type { get; set; }
        public string Item { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }

        public string ID {  get; set; }



    }

    //Using a class map because our class doesnt match the header names and we cant write the classes in way we need
    public sealed class MenuItemMap : ClassMap<MenuItem>
    {
        public MenuItemMap()
        {
            Map(m => m.Type).Name("Type");
            Map(m => m.Item).Name("Item");
            Map(m => m.Calories).Name("Calories");
            Map(m => m.Protein).Name("Protein");
            Map(m => m.Protein).Name("ID");



        }
    }

}
