using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
    }

    public void InsertOrdered(T data)
    {
        var comparable = (IComparable<T>)data!;
        var newNode = new Node<T>(data);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        if (comparable.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        var current = _head;
        while (current.Next != null && comparable.CompareTo(current.Next.Data) > 0)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        newNode.Previous = current;

        if (current.Next != null)
        {
            current.Next.Previous = newNode;
        }
        else
        {
            _tail = newNode;
        }
        current.Next = newNode;
    }

    public void Remove(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                if (current == _head)
                {
                    _head = _head.Next;
                    if (_head != null)
                        _head.Previous = null;
                }
                else if (current == _tail)
                {
                    _tail = _tail.Previous;
                    if (_tail != null)
                        _tail.Next = null;
                }
                else
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
                return;
            }
            current = current.Next;
        }
    }

    public void Reverse()
    {
        if (_head == null) return;

        var current = _head;
        while (current != null)
        {
            (current.Previous, current.Next) = (current.Next, current.Previous);
            current = current.Previous;
        }

        (_head, _tail) = (_tail, _head);
    }

    public void Sort()
    {
        if (_head == null) return;

        bool swapped;
        do
        {
            swapped = false;
            var current = _head;
            while (current.Next != null)
            {
                var a = (IComparable<T>)current.Data!;
                if (a.CompareTo(current.Next.Data) > 0)
                {
                    (current.Next.Data, current.Data) = (current.Data, current.Next.Data);
                    swapped = true;
                }
                current = current.Next;
            }
        } while (swapped);
    }

    override public string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }

    public string ToStringReverse()
    {
        var current = _tail;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Previous;
        }
        result += "null";
        return result;
    }
}