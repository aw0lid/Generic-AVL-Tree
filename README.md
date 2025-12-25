# Generic AVL Tree (C#)

A **self-balancing generic binary search tree** implemented in C#.  
Supports any type implementing `IComparable<T>` and provides efficient operations while maintaining balance automatically.

## Features

- **Add / Insert:** Inserts values while keeping the tree balanced.  
- **Remove / Delete:** Deletes nodes with zero, one, or two children with automatic rebalancing.  
- **Search / Exists:** Quickly check if a value exists in the tree.  
- **Traversals:** InOrder, PreOrder, PostOrder using `Action<T>` for flexible operations on nodes.  
- Maintains **height and balance factor** for optimal performance.

## Example Usage

```csharp
var tree = new AVLTree<int>();

// Add values
tree.Add(10);
tree.Add(5);
tree.Add(20);
tree.Add(15);

// Check if a value exists
Console.WriteLine(tree.Exists(15)); // True
Console.WriteLine(tree.Exists(100)); // False

// InOrder traversal (sorted output)
tree.InOrderTraversal(value => Console.WriteLine(value));

// Remove a value
tree.Remove(5);
