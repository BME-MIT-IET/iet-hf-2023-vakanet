using System;
using System.Collections.Generic;

namespace BaseRPG.View.Animation.ImageSequence
{
    public class LastItemHoldingEnumerator<T> : IEnumerator<T>
    {
        private List<T> items;
        private int index = 0;
        public LastItemHoldingEnumerator(List<T> images)
        {
            this.items = images;
        }

        public T Current => items[index];


        object System.Collections.IEnumerator.Current => items[index];

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (index < items.Count - 1)
                index++;
            return true;

        }

        public void Reset()
        {
            index = 0;
        }
    }
}
