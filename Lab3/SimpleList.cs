using System;
using System.Collections.Generic;
using System.Collections;

namespace Containers
{
    public class SLItem<DataType>
    {
        public DataType Data { get; set; }
        public SLItem<DataType> Next { get; set; }

        public SLItem(DataType data)
        {
            Data = data;
        }
    }

    public class SimpleList<T> : IEnumerable<T> where T : IComparable
    {
        protected SLItem<T> first = null;
        protected SLItem<T> last  = null;

        public int Count { get; protected set; }

        public void Add(T element)
        {
            SLItem<T> newItem = new SLItem<T>(element);
            this.Count++;
            
            if (last == null) first = newItem;
            else last.Next = newItem;

            last = newItem;
        }

        public SLItem<T> GetItem(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new IndexOutOfRangeException();
            }

            SLItem<T> current = first;
            for (int i = 0; i < index; i++) current = current.Next;

            return current;
        }

        public T Get(int index)
        {
            return GetItem(index).Data;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            SLItem<T> current = first;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        // Реализация обощенного IEnumerator<T> требует реализации необобщенного интерфейса
        // Данный метод добавляется автоматически при реализации интерфейса
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Sort()
        {
            Sort(0, this.Count - 1);
        }

        /// <param name="low"></param>
        /// <param name="high"></param>
        private void Sort(int low, int high)
        {
            int i = low;
            int j = high;
            T x = Get((low + high) / 2);

            do
            {
                while (Get(i).CompareTo(x) < 0) ++i;
                while (Get(j).CompareTo(x) > 0) --j;
                if (i <= j)
                {
                    Swap(i, j);
                    i++; j--;
                }
            } while (i <= j);

            if (low < j)  Sort(low, j);
            if (i < high) Sort(i, high);
        }

        private void Swap(int i, int j)
        {
            SLItem<T> ci = GetItem(i);
            SLItem<T> cj = GetItem(j);

            T temp = ci.Data;
            ci.Data = cj.Data;
            cj.Data = temp;
        }
    }
}
