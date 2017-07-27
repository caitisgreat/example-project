using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge
{
    public class Program
    {
        /*
         * Assumptions: 
         * 
         * This problem seems to follow the same pattern of markup languages like JSON wherein there are objects denoted with enclosing seperators () and properties that reside within those objects as comma-seperated values.
         * As true with JSON, properties may also be objects, and therefore are denoted with the () as well.
         * 
         * While this problem is about string parsing, there's also another side to it.  The properties of the object being represented are nested within the object at varying depths of.  See the employeeType property.
         * As such, I think it might be good to design this as a tree data structure.  The root object of the tree will contain references to each leaf, or property, on the object.  In-order traversal would then provide our unorganized result.
         * 
         * To handle the alphabetically ordered result, we could do an insertion sort while adding new items to the tree.
         * 
         * In terms of time-complexity, The worst individual operation time would be the insertion sort being performed as it can be quadratic.  While inserting the values requires little effort, 
         * likely O(n) where n is a word in the string, reorganizing those words can possibly take O(w*n) where w is the number of words being organized versus the number of the words in the string.  
         * Fortunately, on small data sets such as these, quadratic algorithyms perform very quickly when amortized. 
         * 
         * On average, I would expect time-complexity to perform, at worst, O(c) where c is the number of characters in the string.  I'm interating over each character of the string to find seperators.  
         * If we had a larger data set I would lean towards saying that the quadratic nature of the insertion sort would be the worst performer, in that case, I would sort post insertion via something like merge sort.         
         * 
         */

        static void Main(string[] args)
        {
            // the provided source data
            string data = "(id,created,employee(id,firstname,employeeType(id), lastname),location)";
            
            // print some fancy headers
            Console.WriteLine("Source Data: " + data);
            Console.WriteLine();
            Console.WriteLine("Result: ");

            // build the tree structure from the data provided
            TokenTree tree = new TokenTree(data);

            // print the tree structure layout to the console
            tree.PrintTree(tree.root);

            // wait for input, then quit
            Console.ReadLine();
        }
    }
}
