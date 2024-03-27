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
using Wintellect.PowerCollections;
using static FastFoodNutritionAI.Node;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace FastFoodNutritionAI
{
    /*
     * Search class where search functions are written and called
     * search function implementations from - https://github.com/aimacode/aima-python
     */
    public class Search
    {
        private List<State> loadedMenuItems;
        Problem problem;
        Node root;
        List<Node> visitedNodes = new List<Node>(); // list to store visited nodes

        public void setLoadedMenuItems(List<State> items)
        {
            loadedMenuItems = items;
        }

        public List<State> getLoadedMenuItems()
        {
            return loadedMenuItems;
        }

        /**
         * Set up to be able to search
         */
        public (Node, List<Node>, List<Node>, Problem) setUpSearch(string algorithm)
        {
            //loadedMenuItems = LoadMenuItems();
            setLoadedMenuItems(LoadMenuItems());
            Console.WriteLine("Menu loaded");
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


            /*
             * Search calls
             */

            Node resultNode;
            if (algorithm == "BFGS")
            {
                resultNode = breadth_first_graph_search(problem);
            }
            else if (algorithm == "GS")
            {
                resultNode = GreedySearch(problem);
            }
            else if (algorithm == "DLS")
            {
                int limit = 4;
                resultNode = DepthLimitedSearch(root, problem, limit);
                Console.WriteLine("ResultNode is being assigned to DLS");
            } 
            else if(algorithm == "AS")
            { 
              
                resultNode = AStarSearch(problem);

            }
            else
            {
                // default to AStarSearch if no valid algorithm is selected
                resultNode = AStarSearch(problem);
            }

            List<Node> solution = resultNode.path();

            // get the path from the result to the root node
            if (resultNode != null)
            { 
                Console.WriteLine("Solution:");
                foreach (Node node in solution)
                {
                    if (node.state != null)
                    {
                        Console.WriteLine("\n" + node.state.Item);
                    }

                }
            }

            return (root, visitedNodes, solution, problem); 
        }

        /*
         * Breadth first tree search
         * non-optimal but complete solution
         */
        public Node breadth_first_graph_search(Problem problem)
        {
            Console.WriteLine("Breadth first tree search");
            Queue<Node> frontier = new Queue<Node>();
            List<State> explored = new List<State>();

            Node node = root;
            if (node.state != null && problem.goalTest(node.state) == true)
            {
                Console.WriteLine("Goal Reached: " + node.state.Item);
                visitedNodes.Add(node); // Add the goal node to the visited nodes list
                return node;
            }
            // if it is not a goal node, add it to the frontier
            frontier.Enqueue(node);
            while (frontier.Count > 0)
            {
                node = frontier.Dequeue();
                explored.Add(node.state);
                visitedNodes.Add(node); // add the current node to the visitedNodes list
                // add all child nodes to the frontier
                foreach (Node child in node.expand(problem, loadedMenuItems))
                {
                    // check if the child has already been explored or is already on the frontier before adding 
                    if (explored.Contains(child.state) == false && frontier.Contains(child) == false)
                    {
                        if (problem.goalTest(child.state) == true)
                        {
                            Console.WriteLine("Goal Reached: " + child.state.Item);
                            visitedNodes.Add(child); // Add the goal node to the visited nodes list
                            return child;
                        }
                        frontier.Enqueue(child);
                    }

                }
            }
            return null;
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

            visitedNodes.Add(node); // add current node to visitedNodes

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
                foreach (Node child in node.expand(problem, loadedMenuItems))
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

        // search by choosing for the node with the lowest value of path cost to that node + estimated value from that node
        private Node AStarSearch(Problem problem)
        {
            // ordered bag implementation from here - https://github.com/timdetering/Wintellect.PowerCollections
            OrderedBag<NodePriorityPair> frontier = new OrderedBag<NodePriorityPair>();
            HashSet<State> explored = new HashSet<State>();
            Console.WriteLine("Starting AStarSearch...");


            if (root.state == null)
            {
                Console.WriteLine("Root node's state is null.");
                // Initialize root.state or handle the error appropriately before proceeding
            }
            else
            {
                // Add the root node to the frontier with initial priority
                frontier.Add(new NodePriorityPair(root, problem.HeuristicFunction(root) + root.pathCost));
            }

            frontier.Add(new NodePriorityPair(root, problem.HeuristicFunction(root)+root.pathCost));

            while (frontier.Count > 0)
            {
                Console.WriteLine("Frontier count: " + frontier.Count);
                // dequeu  node with highest priority - lowest num
                NodePriorityPair currentPair = frontier.RemoveFirst();
                Node currentNode = currentPair.Node;
                //Console.WriteLine($"Exploring node: {currentNode.state.Item} with priority {currentPair.Priority}.");


                if (problem.goalTest(currentNode.state))
                {
                    // Goal found
                    Console.WriteLine("Goal found: " + currentNode.state.Item);
                    visitedNodes.Add(currentNode); // add the current node to the visitedNodes list
                    return currentNode;
                }

                explored.Add(currentNode.state);
                visitedNodes.Add(currentNode); // add the current node to the visitedNodes list

                foreach (Node child in currentNode.expand(problem, loadedMenuItems))
                {
                    if (child == null)
                    {
                        //Console.WriteLine("child is null in AStarSearch during expansion.");
                        continue;
                    }
                    if (child.state == null)
                    {
                        //Console.WriteLine("Child state is null, skipping heuristic calculation and not adding to frontier.");
                        continue;
                    }
                    if (!explored.Contains(child.state) && !frontier.Any(np => np.Node.Equals(child)))
                    {
                        // add path cost and heurisitc for priority for A star
                        double priority = problem.HeuristicFunction(child)+ child.pathCost;
                        frontier.Add(new NodePriorityPair(child, priority));
                    }
                    // shouldnt be a need to check for updating nodes in the frontier with better paths,
                    // as each node state is unique here.
                }
            }
            Console.WriteLine("A* Search finished without finding a goal.");

            return null;
        }

        /**
         * Do greedy search
         */
        public Node GreedySearch(Problem problem)
        {
            // ordered bag implementation from here - https://github.com/timdetering/Wintellect.PowerCollections
            OrderedBag<NodePriorityPair> frontier = new OrderedBag<NodePriorityPair>();
            HashSet<State> explored = new HashSet<State>();
            Console.WriteLine("Starting GreedySearch...");


            if (root.state == null)
            {
                Console.WriteLine("Root node's state is null.");
                // Initialize root.state or handle the error appropriately before proceeding
            }
            else
            {
                // Add the root node to the frontier with initial priority
                // You need to define `root` and `problem.HeuristicFunction`
                //Console.WriteLine($"Adding root node to frontier with priority {problem.HeuristicFunction(root.state)}.");
                frontier.Add(new NodePriorityPair(root, problem.HeuristicFunction(root)));
            }

            frontier.Add(new NodePriorityPair(root, problem.HeuristicFunction(root)));

            while (frontier.Count > 0)
            {
                Console.WriteLine("Frontier count: " + frontier.Count);
                // dequeu  node with highest priority - lowest num
                NodePriorityPair currentPair = frontier.RemoveFirst();
                Node currentNode = currentPair.Node;
                //Console.WriteLine($"Exploring node: {currentNode.state.Item} with priority {currentPair.Priority}.");


                if (problem.goalTest(currentNode.state))
                {
                    // Goal found
                    Console.WriteLine("Goal found: " + currentNode.state.Item);
                    visitedNodes.Add(currentNode); // add the current node to the visited nodes list
                    return currentNode;
                }

                explored.Add(currentNode.state);

                foreach (Node child in currentNode.expand(problem, loadedMenuItems))
                {
                    if (child == null)
                    {
                        //Console.WriteLine("child is null in GreedySearch during expansion.");
                        continue;
                    }
                    if (child.state == null)
                    {
                        //Console.WriteLine("Child state is null, skipping heuristic calculation and not adding to frontier.");
                        continue;
                    }
                    // Console.WriteLine($"About to call HeuristicFunction with state: Item={child.state.Item}, Protein={child.state.Protein}, Calories={child.state.Calories}");

                    // Console.WriteLine($"Expanding node: {currentNode.state.Item}, considering child: {child.state.Item}.");
                    if (!explored.Contains(child.state) && !frontier.Any(np => np.Node.Equals(child)))
                    {
                        double priority = problem.HeuristicFunction(child);
                        frontier.Add(new NodePriorityPair(child, priority));
                    }
                    // shouldnt be a need to check for updating nodes in the frontier with better paths,
                    // as each node state is unique here.
                }

                visitedNodes.Add(currentNode); // add the current node to the visited nodes list
            }
            Console.WriteLine("GreedySearch finished without finding a goal.");

            return null;
        }

        /*
         * Load all of the menu items into a list
         * used csv helper to load in - https://joshclose.github.io/CsvHelper/getting-started/
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
