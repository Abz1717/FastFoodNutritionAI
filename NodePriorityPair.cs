/**
*  AI Assignmnet Group 2
* Team members:
* - Marylou das Chagas e Silva 2501402
* - Lirit Dampier 2560877 
* - Laura Clark 2419746
* - Adam Munro 2455360
* - Abz Mohamed 245757
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{
    /*
     * Class to compare the priority of nodes in an ordered bag
     * Got help with implementation from - https://github.com/timdetering/Wintellect.PowerCollections
     */
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
}
