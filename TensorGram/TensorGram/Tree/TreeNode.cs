using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Tree
{
    class TreeNode<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeNode<T>> Children { get; set; }
    }
}
