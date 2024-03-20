using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{
    public class GreedySearch
    {
        //fields to hold the menu items, the heuristic, goal test and calorie limit
        /*private List<MenuItem> menuItems;
        private Func<MenuItem, double> heuristic;
        private Func<Node, bool> goalTest;
        private int calorieLimit;

        //constructor 
        public GreedySearch(List<MenuItem> menuItems, int calorieLimit, Func<MenuItem, double> heuristic, Func<Node, bool> goalTest)
        {
            this.menuItems = menuItems;
            this.calorieLimit = calorieLimit; // why do we need a calorie limit? don't think we do in this version of the project - we just want to minimise calories, not a limit
            this.heuristic = heuristic;
            this.goalTest = goalTest;

        }

        //method to find optimalmeal using heuristic and goal
        public List<MenuItem> findOptimalMeal()
        {
            var root = new Node(new List<MenuItem>()); // root node with empty meal
            var frontier = new List<Node> { root };  // setting frontier as root node

            // looping until there are no more nodes
            while (frontier.Any())  
            {
                Console.WriteLine("entering while loop");
                // current node choosen depending on the highest heuristic value
                var currentNode= frontier.OrderByDescending(node => node.Meal.Sum( item => heuristic(item))).FirstOrDefault();
                // check
                if (currentNode == null) 
                    break;
                // if node matches goal test, return optimal meal 
                if (goalTest(currentNode))
                    return currentNode.Meal;

                //expanding node by adding each time that fits under the calorie limit 
                foreach(var item in menuItems.Where(item => currentNode.TotalCalories + item.Calories <= calorieLimit && !currentNode.Meal.Contains(item)))
                {

                   var newNode = currentNode.AddItem(item); // making new node with added item
                    //preventing duplicates
                    if (!frontier.Any(node => node.Meal.SequenceEqual(newNode.Meal)))
                    {
                        frontier.Add(newNode);
                    }
                }

                
                frontier.Remove(currentNode); // removing current node from frontier because its been expanded
            }

            return new List<MenuItem>(); // return an empy list if goal meal wasnt found
        }*/
    }

}
