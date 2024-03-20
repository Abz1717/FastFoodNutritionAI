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
using System.Xml.Linq;




namespace FastFoodNutritionAI
{
    public partial class Form1 : Form
    {
        private List<MenuItem> loadedMenuItems;
        private ListBox lbResults;


        public Form1()
        {
            InitializeComponent();
            InitalizeControls();
            loadedMenuItems = LoadMenuItems();
            SetupGreedySearch();
        }

        private void InitalizeControls()
        {
            lbResults = new ListBox { Location = new Point(50, 50), Size = new Size(300, 400) };
            Controls.Add(lbResults);
        }


        private void SetupGreedySearch()
        {
            GreedySearch greedySearch = new GreedySearch(loadedMenuItems, 2000, HeuristicFunction, GoalTestFunction);
            List<MenuItem> optimalMeal = greedySearch.findOptimalMeal();

            // test remove after UI is built
            foreach (var item in optimalMeal)
            {
                lbResults.Items.Add($"{item.Item} - Calories: {item.Calories}, Protein: {item.Protein}");
            }
        }

        // heuristic function 
        private double HeuristicFunction(MenuItem item)
        {
            //favouring items with more protien per calorie
            return 1 / ((double)item.Calories / item.Protein);
        }

        // goaltest function 
        private bool GoalTestFunction(Node node)
        {
            //check if dessert has been added
            return node.Meal.Any(item => item.Category == "Dessert");
        }


        private List<MenuItem> LoadMenuItems()
        {
            // file path from fastfoodnutrionAI->Data->menu.csv
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "FastFoodNutritionAI", "Data", "menu.csv");
            filepath = Path.GetFullPath(filepath);

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
                    return csv.GetRecords<MenuItem>().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load menu items: {ex.Message}");
                return new List<MenuItem>();
            }
        }
    }




}

