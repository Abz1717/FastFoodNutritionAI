using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodNutritionAI
{
    public class Search
    {
        private List<State> loadedMenuItems;
        Problem problem;
        Node root;


        /**
         * Set up to be able to search
         */
        public void setUpSearch()
        {
            loadedMenuItems = LoadMenuItems();
            root = new Node(null, 0, 0, null, null); // initialise root node
            List<State> goals = new List<State>();
            problem = new Problem(root.state, goals);

            // add each dessert item to the list of goal states
            foreach (State item in loadedMenuItems)
            {
                if (item.Category == "Desserts")
                {
                    problem.goalStates.Add(item);
                }
            }


            Node resultNode = breadth_first_tree_search(problem);

            //int limit = 5;
            //Node resultNode = DepthLimitedSearch(root, problem, limit);


            // get the path from the result to the root node
            if (resultNode != null) 
            {
                List<Node> solution = resultNode.path();
                Console.WriteLine("Solution");
                foreach (Node node in solution)
                {
                    if (node.state != null)
                    {
                        Console.WriteLine("\n" + node.state.Item);
                    }
                    
                }
            }
        }

        /*
         * Breadth first tree search
         */
        public Node breadth_first_tree_search(Problem problem)
        {
            Console.WriteLine("Breadth first tree search");
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(root);

            while (frontier.Count > 0)
            {
                Node node = frontier.Dequeue();
                if (problem.goalTest(node.state) == true)
                {
                    Console.WriteLine("Goal Reached: " + node.state.Item);
                    return node;
                }
                // add all child nodes to the frontier
                foreach (Node child in node.expand(problem))
                {
                    frontier.Enqueue(child);
                }
            }
            return null;
        }

        /**
         * Do greedy search
         */
        private void GreedySearch(Problem problem)
        {

        }

        /*
         * Depth-Limited Search
         */
        public Node DepthLimitedSearch(Node node, Problem problem, int limit)
        {
            return RecursiveDLS(node, problem, limit);
        }

        /*
         * Recursive Depth-Limited Search
         */
        private Node RecursiveDLS(Node node, Problem problem, int limit)
        {
            if (problem.goalTest(node.state))
            {
                Console.WriteLine("Goal Reached: " + node.state.Item);
                return node;
            }
            else if (limit == 0)
            {
                return null; // cutoff
            }
            else
            {
                bool cutoffOccurred = false;
                foreach (Node child in node.expand(problem))
                {
                    Node result = RecursiveDLS(child, problem, limit - 1);
                    if (result == null || cutoffOccurred)
                    {
                        cutoffOccurred = true;
                    }
                    else if (result != null)
                    {
                        return result;
                    }
                }
                if (cutoffOccurred)
                {
                    return null; // cutoff
                }
                else
                {
                    return null; // failure
                }
            }
        }
 
        

        /*
         * Load all of the menu items into a list
         */
        public List<State> LoadMenuItems()
        {
            // file path from fastfoodnutrionAI->Data->menu.csv
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data", "menu.csv");
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
                    return csv.GetRecords<State>().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load menu items: {ex.Message}");
                return new List<State>();
            }
        }
    }


}
