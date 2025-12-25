using System;

namespace GenericAVLTree
{
    public class AVLTree<T> where T : IComparable<T>
    {


        private class AVLNode
        {
            public T Value { get; set; }
            public AVLNode Left { get; set; }
            public AVLNode Right { get; set; }
            public int Height { get; set; }

            public AVLNode(T value)
            {
                Value = value;
                Height = 1;
            }
        }




        private AVLNode _root = null;


        public AVLTree() { }
        public AVLTree(T value) { this._root = new AVLNode(value); }
        

       

        private int Height(AVLNode node) => node?.Height ?? 0;

        private int BalanceFactor(AVLNode node) => node == null ? 0 : Height(node.Left) - Height(node.Right);




        private AVLNode RightRotate(AVLNode parent)
        {
            AVLNode leftChild = parent.Left;      
            AVLNode leftChildRightSubtree = leftChild.Right;

            
            leftChild.Right = parent;
            parent.Left = leftChildRightSubtree;

            
            UpdateHeight(parent);
            UpdateHeight(leftChild);

            return leftChild;
        }



        private AVLNode LeftRotate(AVLNode parent)
        {
            AVLNode rightChild = parent.Right;
            AVLNode rightChildLeftSubtree = rightChild.Left;

            
            rightChild.Left = parent;
            parent.Right = rightChildLeftSubtree;

            
            UpdateHeight(parent);
            UpdateHeight(rightChild);

            return rightChild;
        }



        private void UpdateHeight(AVLNode node) { node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right)); }
        



        private AVLNode Balance(AVLNode node)
        {
            int balance = BalanceFactor(node);

            
            if (balance > 1 && BalanceFactor(node.Left) >= 0)
                return RightRotate(node);


            if (balance < -1 && BalanceFactor(node.Right) <= 0)
                return LeftRotate(node);


            if (balance > 1 && BalanceFactor(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            

            if (balance < -1 && BalanceFactor(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }



        private AVLNode Insert(AVLNode node, T value)
        {
            if (node == null)
                return new AVLNode(value);

            int cmp = value.CompareTo(node.Value);
            if (cmp < 0)
                node.Left = Insert(node.Left, value);
            else if (cmp > 0)
                node.Right = Insert(node.Right, value);
            else
                return node;

            UpdateHeight(node);
            return Balance(node);
        }


        private AVLNode Delete(AVLNode node, T value)
        {
            if (node == null)
                return null;

            int cmp = value.CompareTo(node.Value);


            if (cmp < 0)
                node.Left = Delete(node.Left, value);


            else if (cmp > 0)
                node.Right = Delete(node.Right, value);


            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;


                AVLNode temp = MinValueNode(node.Right);
                node.Value = temp.Value;
                node.Right = Delete(node.Right, temp.Value);
            }


            UpdateHeight(node);

            return Balance(node);
        }





        private AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;

            while (current.Left != null)
                current = current.Left;


            return current;
        }



        private bool Exists(AVLNode node, T value)
        {
            if (node == null) return false;

            int cmp = value.CompareTo(node.Value);

            if (cmp < 0) return Exists(node.Left, value);

            if (cmp > 0) return Exists(node.Right, value);


            return true;
        }





        private void InOrder(AVLNode node, Action<T> action)
        {
            if (node == null) return;

            InOrder(node.Left, action);

            action(node.Value);

            InOrder(node.Right, action);
        }


        private void PreOrder(AVLNode node, Action<T> action)
        {
            if (node == null) return;

            action(node.Value);

            PreOrder(node.Left, action);

            PreOrder(node.Right, action);
        }


        private void PostOrder(AVLNode node, Action<T> action)
        {
            if (node == null) return;

            PostOrder(node.Left, action);

            PostOrder(node.Right, action);

            action(node.Value);
        }

        






        public void Add(T value) { _root = Insert(_root, value); }
        public void Remove(T value) { _root = Delete(_root, value); }
        public bool Exists(T value) { return Exists(_root, value); }


        public void InOrderTraversal(Action<T> action) { InOrder(_root, action); }
        public void PreOrderTraversal(Action<T> action) { PreOrder(_root, action); }
        public void PostOrderTraversal(Action<T> action) { PostOrder(_root, action); }
    }
}
