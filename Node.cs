using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{

    /**
     * A node in the search tree contains a pointer to the parent and also the actual state for this node
     * includes the action that led to this node and also the total path cost to reach the node
     * each menu item is a node
     */
    public class Node
    {
        // each node has an associated state, parent, action, pathcost and depth
        public int pathCost;
        public int depth;
        public State state;
        public Action action;
        public Node parent;

        public Node(State state, int pathCost, int depth, Node parent, Action action)
        {
            this.state = state;
            this.pathCost = pathCost;
            this.depth = depth;
            this.parent = parent;
            this.action = action;
        }

        /*
         * List all of the nodes reachable in one step from this node
         */
        public List<Node> expand(Problem problem, List<State> loadedItems)
        {
            // frontier is the list of all nodes one step away from this node
            List<Node> frontier = new List<Node>();
            // create a child node for every possible action from this state
            foreach (Action action in problem.actions(this.state, loadedItems))
            {
                //Console.WriteLine("Expanded actions:");
                // Console.WriteLine(action.getResult().Item);

                Node child = childNode(problem, action);
                frontier.Add(child);
            }
            return frontier;
        }

        /**
         * Create and return a child node
         */
        public Node childNode(Problem problem, Action action)
        {
            // get the state of the next node
            State next_state = problem.result(this.state, action);
            /*
            if (next_state == null)
            {
                throw new InvalidOperationException("Next state cant be null.");
            }
            */
            // get the next node
            Node next_node = new Node(next_state, problem.path_cost(this.pathCost, this.state, action, next_state), this.depth + 1, this, action);
            return next_node;
        }

        /*
         * Return a list of nodes forming the path from the root to this node
         */
        public List<Node> path()
        {
            List<Node> path = new List<Node>();
            Node node = this;
            while (node != null)
            {
                path.Add(node);
                node = node.parent;
            }
            return path;
        }

        /*
         * Return the sequence of actions to get from the root node to the current node
         */
        public List<Action> solution()
        {
            List<Action> actionList = new List<Action>();
            foreach (Node node in this.path())
            {
                actionList.Add(node.action);
            }
            return actionList;
        }


        
        public class NodePriorityPair : IComparable<NodePriorityPair>
        {
            public Node Node { get; private set; }
            public double Priority { get; private set; } 

            public NodePriorityPair(Node node, double priority)
            {
                Node = node;
                Priority = priority;
            }

            public int CompareTo(NodePriorityPair other)
            {
                //a lower numeric value indicates higher priority
                return Priority.CompareTo(other.Priority);
            }
        }




        // list of menu items that make a meal
        /* public List<MenuItem> Meal { get; private set; } = new List<MenuItem>();
         // sum of calories accross each all items in the meal
         public double TotalCalories { get; private set; }

         // IEnumerable allows us to iterate over a collection
         public Node(IEnumerable<MenuItem> meal)
         {
             Meal = new List<MenuItem>(meal);
             TotalCalories = Meal.Sum(item => item.Calories);
         }

         // makes new node by adding menuitem to current meal, giving a new potential 
         public Node AddItem(MenuItem item)
         {
             var newMeal = new List<MenuItem>(Meal) { item };   // list of menu items starting with items in current meal 
             return new Node(newMeal);

         }*/
    }
}
