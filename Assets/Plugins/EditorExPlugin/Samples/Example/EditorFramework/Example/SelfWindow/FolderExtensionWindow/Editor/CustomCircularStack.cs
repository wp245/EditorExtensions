using System;

namespace EditorFramework
{
    public class CustomCircularStack<T>
    {
        private T[] _stack;
        private int _top;
        private int _size;
        private int _count;

        public CustomCircularStack(int capacity)
        {
            _stack = new T[capacity];
            _top = -1;
            _size = capacity;
            _count = 0;
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public bool IsFull()
        {
            return _count == _size;
        }

        public void Push(T item)
        {
            if (IsFull())
            {
                _top = (_top + 1) % _size; // Move top pointer forward to overwrite the oldest element
            }
            else
            {
                _top = (_top + 1) % _size;
                _count++;
            }
            _stack[_top] = item;
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Stack is empty");
            }

            T item = _stack[_top];
            _top = (_top - 1 + _size) % _size; // Move top pointer backward
            _count--;

            return item;
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Stack is empty");
            }

            return _stack[_top];
        }

        public int Count()
        {
            return _count;
        }

        public void Clear()
        {
            _stack = new T[_size];
            _top = -1;
            _count = 0;
        }
    }
}