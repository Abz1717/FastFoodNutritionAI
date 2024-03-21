using CsvHelper.Configuration;
using CsvHelper;
using FastFoodNutritionAI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.CodeDom;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;

namespace FastFoodNutritionAI
{
    /**
     * Class to define a formal problem
     */
    public class Problem
    {
        public FastFoodNutritionAI.State initialState; // initial state is the state of the initial node - which is the type of item that it is? initial is none?
        public List<FastFoodNutritionAI.State> goalStates; // list of all goal states of the problem
        //private List<State> loadedMenuItems;

        /**
		 * Problem constructor specifying the initial state of the problem
		 */
        public Problem(FastFoodNutritionAI.State initial, List<FastFoodNutritionAI.State> goals)
        {
            this.initialState = initial;
            this.goalStates = goals;
        }

        /**
		 * Return the actions that can be executed in the given state
		 * result should be a list
		 */
        public List<Action> actions(State state)
        {
            Search search = new Search();
            List<State> loadedMenuItems = search.LoadMenuItems();
            List<Action> possible_actions = new List<Action>();
            // for the root node, add all paths to main meal nodes as potential action
            if (state == null)
            {
                foreach (State item in loadedMenuItems)
                {
                    if (item.Category == "Salads" || item.Category == "Beef & Pork" || item.Category == "Breakfast" || item.Category == "Chicken & Fish" && item.Calories != 0)
                    {
                        Action newAction = new Action(null, item);
                        possible_actions.Add(newAction);
                    }
                }
                // display possible items to move to
                Console.WriteLine("Actions:");
                foreach (Action action in possible_actions)
                {
                    // prints correct here
                    //Console.WriteLine(action.getResult().Item);
                }

            } 
            else if (state.Category == "Breakfast")
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Snacks & Sides" && item.Calories != 0)
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }
            else if (state.Category == "Salads" || state.Category == "Beef & Pork" || state.Category == "Chicken & Fish")
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Snacks & Sides" && item.Calories != 0)
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }
            else if (state.Category == "Snacks & Sides")
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Beverages") //  || item.Category == "Coffee & Tea" || item.Category == "Smoothies")
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }
            else if (state.Category == "Beverages") // || state.Category == "Coffee & Tea" || state.Category == "Smoothies" )
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Desserts")
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }
            else
            {
                // dessert state so no possible actions
            }
            
            return possible_actions;
        }

        /**
		 * Return the state that results from executing the given
		 * action in the given state
		 * The action must be one of this.actions(state).
		 */
        public State result(State state, Action action)
        {
            // given an action and a state, return the resulting state
            State resultState = action.getResult();
            return resultState;
        }

        /*
		 * Check if a given state is a goal state by comparing to all goal states
		*/
        public bool goalTest(State state)
        {
            foreach (State s in goalStates)
            {
                if (state != null)
                {
                    Console.WriteLine("Goal test:" + s.Item + " " + state.Item);
                    if (state.Item == s.Item)
                    {
                        return true;
                    }
                }
            }
            // return false if not a goal state
            return false;
        }

        /*
         * Return the cost of a solution path that arrives at state2 from
         * state1 via action, assuming cost c to get up to state1
         */
        public int path_cost(int currentCost, State state1, Action action, State state2)
        {
            int totalCost;
            totalCost = currentCost + state2.Calories; // add calories of state 2 to get the total cost of the solution
            return totalCost;
        }

        /*
         * heuristic function 
         */
        public double HeuristicFunction(State state)
        {
            
            if (state == null)
            {
                return 0;
            }
            
            /*
            // a check for the start state (you might want to handle this differently)
            if (state.Category.Equals("Breakfast"))
            {
                Console.WriteLine("Heuristic Function called with initial state.");
                // a neutral heuristic value to  start state and allow expanding
                return 0;
            }
            */

            //avoiding division by zero if protein is zero
            if (state.Protein <= 0)
            {
                //Console.WriteLine("Heuristic Function called with a state having zero protein.");
                return double.PositiveInfinity; //large number to indicate nonideal state
            }

                double heuristicValue = 1 / ((double)state.Calories / state.Protein);
                //Console.WriteLine($"Heuristic Function called for Item: {state.Item}, Category: {state.Category}, Calories: {state.Calories}, Protein: {state.Protein}, Heuristic Value: {heuristicValue}");
                //favouring items with more protien per calorie
                return heuristicValue;
            
            
        }
    }
}
