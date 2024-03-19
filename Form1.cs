using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.CodeDom;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;




namespace FastFoodNutritionAI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            loadMenuItems();
           // var menuItems = loadMenuItems();
        }

        //each menu item, add or remove items whenver just change add the correct csv heading in the mapping function
        public class MenuItem
        {
            public string Category { get; set; }
            public string Item { get; set; }
            public string ServingSize { get; set; }
            public int Calories { get; set; }
            public int TotalFat { get; set; }
            public int SaturatedFat { get; set; }
            public int TransFat { get; set; }
            public int Cholesterol { get; set; }
            public int Sodium { get; set; }
            public int Carbohydrates { get; set; }
            public int DietaryFiber { get; set; }
            public int Sugar { get; set; }
            public int Protein { get; set; }
            public int Iron { get; set; }


        }


        //Using a class map because our class doesnt match the header names and we cant write the classes in way we need

        public sealed class MenuItemMap : ClassMap<MenuItem>
        {
            public MenuItemMap()
            {
                Map(m => m.Category).Name("Category");
                Map(m => m.Item).Name("Item");
                Map(m => m.ServingSize).Name("Serving Size");
                Map(m => m.Calories).Name("Calories");
                Map(m => m.TotalFat).Name("Total Fat");
                Map(m => m.SaturatedFat).Name("Saturated Fat");
                Map(m => m.TransFat).Name("Trans Fat");
                Map(m => m.Cholesterol).Name("Cholesterol");
                Map(m => m.Sodium).Name("Sodium");
                Map(m => m.Carbohydrates).Name("Carbohydrates");
                Map(m => m.DietaryFiber).Name("Dietary Fiber");
                Map(m => m.Sugar).Name("Sugars");
                Map(m => m.Iron).Name("Iron (% Daily Value)");


            }
        }

            private object loadMenuItems()
            {

            // file path from fastfoodnutrionAI->Data->menu.csv
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "FastFoodNutritionAI", "Data", "menu.csv");
            filepath = Path.GetFullPath(filepath); 

            // initilizinf empt list which will store menu items
            List<MenuItem> data = new List<MenuItem>();

            // try catch to handle errors
            try
            {
                //using csvhelper package to read csv files
                //opening CSV file and reading it
                using (var reader = new StreamReader(filepath))
                // initializing a Cvsreader with StreamReader.CsvConfiguration used to specific options for reading
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // to say taht true CSV has a header row 
                    Delimiter = ",", // character 
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    IgnoreBlankLines = true
                }))
                {
                    //reading records from CSV file and and converting them to a list of objects
                    data = csv.GetRecords<MenuItem>().ToList();

                    if (data.Any())
                    {
                        Console.WriteLine("CSV file has been read successfully. Here are some loaded items:");
                        // checking first couple of records 
                        foreach (var item in data.Take(5)) 
                        {
                            Console.WriteLine($"Item: {item.Item}, Category: {item.Category}, Calories: {item.Calories}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Items were loaded from the CSV file.");
                    }
                }

            }
            catch  (FileNotFoundException ex)
            {
                Console.WriteLine($"The file was not found: '{ex.Message}'");
            }
            return data; //returning list of menu items, will be empty if theres an error 
        }



    }
}
