/**
*  AI Assignmnet Group 2
* Team members:
* - Marylou das Chagas e Silva 2501402
* - Lirit Dampier 2560877 
* - Laura Clark 2419746
* - Adam Munro 2455360
* - Abz Mohamed 245757
*/

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
     * function outlines taken from - https://github.com/aimacode/aima-python
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

        /*
		 * Return the actions that can be executed in the given state
		 * result should be a list of actions that can be performed at the given state
		 * loaded menu items is the list of menu items that have been loaded ready to use
		 */
        public List<Action> actions(State state, List<State> loadedMenuItems)
        {
            List<Action> possible_actions = new List<Action>();
            // for the root node, add all paths to main meal nodes as potential action
            if (state == null)
            {
                foreach (State item in loadedMenuItems)
                {
                    if (item.Category == "Salads" || item.Category == "Beef & Pork" || item.Category == "Breakfast" || item.Category == "Chicken & Fish" || item.Category == "Snacks")
                    {
                        Action newAction = new Action(null, item);
                        possible_actions.Add(newAction);
                    }
                }
            }
            else if (state.Category == "Breakfast")
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Beverages" || item.Category == "Coffee & Tea" || item.Category == "Smoothies")
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }
            // main meals can either have a side or a drink as the next node
            else if (state.Category == "Beef & Pork" || state.Category == "Chicken & Fish" || state.Category == "Snacks")
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Sides" || state.Category == "Beverages" || state.Category == "Coffee & Tea" || state.Category == "Smoothies")
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }
            else if (state.Category == "Salads")
            {
                foreach (State item in loadedMenuItems)
                {

                    if (item.Category == "Sides" || item.Category == "Beverages" || item.Category == "Coffee & Tea" || item.Category == "Smoothies")
                    {
                        Action newAction = new Action(state.Item, item);

                        possible_actions.Add(newAction);
                    }
                }
            }

            else if (state.Category == "Sides")
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
            else if (state.Category == "Beverages" || state.Category == "Coffee & Tea" || state.Category == "Smoothies")
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

        /*
		 * Return the state that results from executing the given action in the given state
		 * state - the starting state the action is performed in
		 * action - the action performed on the state
		 * return - the resulting state
		 */
        public State result(State state, Action action)
        {
            // given an action and a state, return the resulting state
            State resultState = action.getResult();
            return resultState;
        }

        /*
		 * Check if a given state is a goal state by comparing to all goal states
		 * state - the state we are checking if it is a goal state or not
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
         * heuristic function - to estimate the cost of reaching a goal node from the current state
         * node - the node that we want to estimate the cost to the goal from
         */
        public int HeuristicFunction(Node node)
        {
            int heuristicValue = 0;
            // an admissible heuristic cannot overestimate the cost, so we use the lowest value in each category to estimate
            int lowestSide = 15;
            int lowestDessert = 45;
            int lowestDrink = 80;
            if (node.state == null)
            {
                return 1000; // - this should hopefully never happen but to stop it crashing in case?
            }
            // if the state cateogry is desserts it is a goal node, so estimated cost to a goal is 0
            else if (node.state.Category == "Desserts")
            {
                return 0;
            }
            else if (node.state.Category == "Breakfast")
            {
                heuristicValue = lowestDrink + lowestDessert;
            }
            else if (node.state.Category == "Chicken & Fish" || node.state.Category == "Beef & Pork" || node.state.Category == "Snacks")
            {
                // calories of main meal is path cost from root to meal, then heuristic is estimate from meal to dessert
                // lowest cost from any of the main meal nodes will be the lowest value to side, then drink, then dessert
                heuristicValue = lowestSide + lowestDrink + lowestDessert;
            }
            else if (node.state.Category == "Salads")
            {
                heuristicValue = lowestDrink + lowestDessert;
            }
            else if (node.state.Category == "Sides")
            {
                // heuristic value is average cost of all lowest beverages + lowest dessert
                heuristicValue = lowestDrink + lowestDessert;
            }
            else if (node.state.Category == "Beverages" || node.state.Category == "Coffee & Tea" || node.state.Category == "Smoothies")
            {
                heuristicValue = lowestDessert;
            }
            return heuristicValue;

        }
    }
}
