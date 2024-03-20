using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodNutritionAI
{
    /**
     * Class to define an action
     */
    public class Action
    {

        private string start;
        private State result;

        public Action(string start, State result)
        {
            this.start = start;
            this.result = result;
        }

        public string getStart()
        {
            return start; 
        }
        public State getResult()
        { 
            return result;
        }

        public void setStart(string start) 
        { 
            this.start = start; 
        }

        public void setResult(State result) 
        { 
            this.result = result; 
        }
    }
}
