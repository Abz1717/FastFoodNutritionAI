using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{

    /*
     * A node in the search tree contains a pointer to the parent and also the actual state for this node
     * includes the action that led to this node and also the total path cost to reach the node as well as the parent and depth of the node
     * function outlines taken from - https://github.com/aimacode/aima-python
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

        /*
         * Create and return a child node
         * problem - the problem definition we are using
         * action - the action that led to the child node
         */
        public Node childNode(Problem problem, Action action)
        {
            // get the state of the next node
            State next_state = problem.result(this.state, action);
            
            if (next_state == null)
            {
                throw new InvalidOperationException("Next state cant be null.");
            }
            
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
    }
}
