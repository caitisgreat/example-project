using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge
{
    /// <summary>
    /// The Token Tree is comprised of TreeNode objects
    /// Each TreeNode knows of it's parent TreeNode, and any children it has. 
    /// Beyond that, the insertion method performs a basic insertion into a List<TreeNode>, and follows that up by doing an insertion sort to get everything in alphabetical order. 
    /// </summary>
    
    public class TreeNode
    {
        public string key;
        public TreeNode parent;
        public List<TreeNode> children;

        public void Insert(TreeNode child)
        {
            if (this.children == null)
            {
                this.children = new List<TreeNode>();
                this.children.Add(child);
            }
            else {
                this.children.Add(child);
                
                int i = 1;
                while (i < this.children.Count)
                {
                    TreeNode tmp = children[i];
                    int j = i - 1;
                    while ((j >= 0) && (children[j].key.CompareTo(tmp.key) > 0))
                    {
                        this.children[j + 1] = this.children[j];
                        j--;
                    }

                    this.children[j + 1] = tmp;
                    i++;
                }
            }
        }
    }

    /// <summary>
    /// The TokenTree object represents a bunch of methods used to build or manipulate the tree data structure.
    /// </summary>
    public class TokenTree
    {
        private string data;
        private int printDepth = -1;
        private TreeNode head = null;
        private TreeNode last = null;        
        public TreeNode root = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenTree"/> class.
        /// </summary>
        /// <param name="data">The provided string data.</param>
        public TokenTree(string data)
        {
            this.data = data;
            GenerateTreeFromData();
        }

        /// <summary>
        /// Iterates recursively over the tree printing the structure in-order.  Leaf depth is captured to handle the hyphen marks.
        /// </summary>
        /// <param name="node">The node.</param>
        public void PrintTree(TreeNode node) {
            
            string printString = (printDepth > 0 ? new string('-', printDepth) + " " : "") + node.key;
            System.Console.WriteLine(printString);

            if (node.children != null)
            {
                printDepth++;
                for (int i = 0; i < node.children.Count; i++)
                {                    
                    PrintTree(node.children[i]);
                }
                printDepth--;
            }
        }

        /// <summary>
        /// Parses the data string and generates a tree structure from it.  
        /// </summary>
        private void GenerateTreeFromData()
        {
            // read each character from the data and iterate over it looking for seperators
            for (int i = 0; i < data.Length; i++)
            {
                char token = data[i];

                // start a new branch under this head node
                if (token == '(')
                {
                    if (root == null)
                    {
                        // create the root node and make it the head of the tree
                        root = new TreeNode();
                        head = root;
                    }
                    else
                    {
                        //make the last inserted node the head of the tree
                        head = last;
                    }

                    TreeNode node = CreateTreeNode(i);
                    node.parent = head;
                    head.Insert(node);
                    last = node;
                }

                // close off the branch and return to the parent node
                if (token == ')')
                {
                    head = head.parent;
                }

                // list a new child in the current head
                if (token == ',')
                {
                    TreeNode node = CreateTreeNode(i);
                    node.parent = head;
                    head.Insert(node);
                    last = node;
                }
            }
        }

        /// <summary>
        /// Creates the tree node.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private TreeNode CreateTreeNode(int index)
        {
            TreeNode node = new TreeNode();

            char[] separators = ",()".ToArray();
            int j = data.Substring(index + 1).IndexOfAny(separators);
            if (j != -1)
            {
                node.key = data.Substring(index + 1, j).Trim(); // handle white space for the sake of sorting
            }

            return node;
        }
    }
}
