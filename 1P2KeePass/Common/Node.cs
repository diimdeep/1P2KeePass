using System.Collections.Generic;

namespace _1Password2KeePass
{
	public class Node<T>
	{
		public Node()
		{
			Nodes = new List<Node<T>>();			
		}

		public Node<T> Parent { get; set; }
		public List<Node<T>> Nodes { get; set; } 
		public T AssociatedObject { get; set; }        
	}
}