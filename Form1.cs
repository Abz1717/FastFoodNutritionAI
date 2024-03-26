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

        private Timer timer;
        private int currentIndex;
        private List<Node> visitedNodes;
        private List<Node> solution; 
        private List<int> rowsToHighlight = new List<int>();
        int totalPathCost = 0;
        private Problem problem;




        public Form1()
        {
            InitializeComponent();

            // add columns to the DataGridView
            dataGridView1.Columns.Add("Depth", "Depth");
            dataGridView1.Columns.Add("Item", "Item");
            dataGridView1.Columns.Add("Heuristic", "Heuristic");
            dataGridView1.Columns.Add("Calories", "Calories");
            dataGridView1.Columns.Add("Category", "Category"); // Add Category column
            dataGridView1.Columns.Add("Goal Test", "Goal Test"); // Add Category column


            // adjust column width
            dataGridView1.Columns["Depth"].Width = 50;
            dataGridView1.Columns["Item"].Width = 300;
            dataGridView1.Columns["Heuristic"].Width = 100;
            dataGridView1.Columns["Calories"].Width = 100;
            dataGridView1.Columns["Category"].Width = 100; 
            dataGridView1.Columns["Goal Test"].Width = 100; 

            
            dataGridView1.ReadOnly = true; //set DataGridView to read-only mode
            // disable resizing of rows and columns
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }


        /**
         * handles the search buttons click event
         */
        private void handleButtonClick(string algorithm, Button clickedButton)
        {
            totalPathCost = 0;
            lblTotalPathCost.Text = "Total Path Cost: ";
            clickedButton.BackColor = Color.LightBlue;  // change the colour of the selected button
            
            foreach (Control control in Controls) // reset the colour of the other buttons
            {
                if (control is Button button && button != clickedButton)
                {
                    button.BackColor = DefaultBackColor;
                }
            }
           
            dataGridView1.Rows.Clear();  // clear the existing data
            
            // stop the timer if running
            if (timer != null && timer.Enabled)
            {
                timer.Stop();
            }

            // reset currentIndex and rowsToHighlight
            currentIndex = 0;
            rowsToHighlight.Clear();

            displayVisitedNodesInfo(algorithm);

        }

        private void btnDLS_Click(object sender, EventArgs e)
        {
            Console.WriteLine("DLS IS CLICKED!");
            handleButtonClick("DLS", sender as Button);
        }

        private void btnGS_Click(object sender, EventArgs e)
        {
            Console.WriteLine("GS IS CLICKED!");
            handleButtonClick("GS", sender as Button);
        }

        private void btnBFGS_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BFGS IS CLICKED!");
            handleButtonClick("BFGS", sender as Button);
        }
        private void btnAS_Click(object sender, EventArgs e)
        {
            Console.WriteLine("AS IS CLICKED!");
            handleButtonClick("AS", sender as Button);
        }

        /*
        * displays information about visited nodes by adding them to the grid
        */
        private void displayVisitedNodesInfo(string algorithm)
        {
            // set up the search
            Search search = new Search(); 
            (Node root, List<Node> visitedNodes, List<Node> solution, Problem problem) = search.setUpSearch(algorithm);

            // initialize the problem, visitedNodes & soltuion 
            this.problem = problem;
            this.visitedNodes = visitedNodes; 
            this.solution = solution; 


            // display visited nodes info to console to check
            Console.WriteLine("displaying info on the visited nodes...:");
            foreach (Node visitedNode in visitedNodes)
            {
                if (visitedNode.state != null)
                {
                    Console.WriteLine("Depth: " + visitedNode.depth);
                    Console.WriteLine("Item: " + visitedNode.state.Item);
                    Console.WriteLine("Heuristic: " + problem.HeuristicFunction(visitedNode));
                    Console.WriteLine("Calories: " + visitedNode.state.Calories);
                    Console.WriteLine("Category: " + visitedNode.state.Category);
                }
            }

            // add each node  to the grid
            timer = new Timer();
            timer.Interval = 10; // add a delay
            timer.Tick += AddNodeToGrid_Tick; 
            timer.Start(); 
        }


        /*
        * event handler for adding nodes to the grid
        */
        private void AddNodeToGrid_Tick(object sender, EventArgs e)
        {
            if (currentIndex < visitedNodes.Count)
            {
                Node visitedNode = visitedNodes[currentIndex];

                if (visitedNode.state != null)
                {
                    // add the row to the grid
                    int rowIndex = dataGridView1.Rows.Add(visitedNode.depth,
                   visitedNode.state.Item, problem.HeuristicFunction(visitedNode),
                    visitedNode.state.Calories, visitedNode.state.Category,
                    "Fail" // default value for Goal Test column
                   );

                    // check if the visitedNode is in the solution
                    if (solution.Contains(visitedNode))
                    {
                        // mark the row index for highlighting later
                        rowsToHighlight.Add(rowIndex);
                        totalPathCost += visitedNode.state.Calories;
                    }

                    // check if the visitedNode passes the goal test
                    if (problem.goalTest(visitedNode.state))
                    {
                        dataGridView1.Rows[rowIndex].Cells["Goal Test"].Value = "Pass";
                    }

                }

                currentIndex++;
            }
            else
            {
                // all nodes added so stop the timer
                timer.Stop();

                // highlight solution rows
                foreach (int rowIndex in rowsToHighlight)
                {
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                }

                // display the total path cost 
                lblTotalPathCost.Text = "Total Path Cost: " + totalPathCost.ToString() + " kcal";
            }
        }


    }




}

