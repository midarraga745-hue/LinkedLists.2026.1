namespace SimpleList
{
    public class Node<T>
    {
        public Node(T? data)
        {
            Data = data;
        }

        public object Data { get; internal set; }
        public Node<T> Next { get; internal set; }
    }
}