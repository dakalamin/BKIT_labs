using System;
using System.Collections.Generic;
using System.Text;

namespace Containers
{
    class SimpleStack<T> : SimpleList<T> where T : IComparable
    {
        public void Push(T element) => Add(element);

        public T Pop()
        {
            T result = default(T);
            if (this.Count == 0) return result;

            if (this.Count == 1)
            {
                result = this.first.Data;

                this.first = null;
                this.last = null;
            }
            else
            {
                SLItem<T> newLast = this.GetItem(this.Count - 2);
                result = newLast.Next.Data;

                this.last = newLast;
                newLast.Next = null;
            }

            this.Count--;
            return result;
        }
    }
}
