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
        private ListBox lbResults;



        public Form1()
        {
            InitializeComponent();
            InitalizeControls();
            //setUpSearch();
            //SetupGreedySearch();
            Search search = new Search();
            search.setUpSearch();



        }

        private void InitalizeControls()
        {
            lbResults = new ListBox { Location = new Point(50, 50), Size = new Size(300, 400) };
            Controls.Add(lbResults);
        }




        private void SetupGreedySearch()
        {
            /*GreedySearch greedySearch = new GreedySearch(loadedMenuItems, 2000, GoalTestFunction);
            List<MenuItem> optimalMeal = greedySearch.findOptimalMeal();

            // test remove after UI is built
            foreach (var item in optimalMeal)
            {
                lbResults.Items.Add($"{item.Item} - Calories: {item.Calories}, Protein: {item.Protein}");
            }*/
        }



        // goaltest function 
        //private bool GoalTestFunction(Node node)
        //{
        //check if dessert has been added
        //return node.Meal.Any(item => item.Category == "Desserts");
        //}


    }




}

