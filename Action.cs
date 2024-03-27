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
using System.Windows.Forms;

namespace FastFoodNutritionAI
{
    /**
     * Class to define an action - the action is defined by the path between two nodes
     */
    public class Action
    {

        private string start;
        private State result;

        /*
         * Constructor for action class
         */
        public Action(string start, State result)
        {
            this.start = start;
            this.result = result;
        }

        /*
         * Get the starting state of the action
         */
        public string getStart()
        {
            return start;
        }

        /*
         * Get the resulting state of the action
         */
        public State getResult()
        {
            return result;
        }

        /*
         * Set the starting state of an action
         */
        public void setStart(string start)
        {
            this.start = start;
        }

        /*
         * Set the resulting state of the action
         */
        public void setResult(State result)
        {
            this.result = result;
        }

    }
}
