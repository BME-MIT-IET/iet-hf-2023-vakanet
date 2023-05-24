using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.ImageSequence
{
    internal class LoopingEnumerator<T> : IEnumerator<T>
    {
        private List<T> items;
        private int index = 0;
        public LoopingEnumerator(List<T> images)
        {
            this.items = images;
        }

        public T Current => items[index];

        object IEnumerator.Current => items[index];

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (index < items.Count - 1)
                index++;
            else index = 0;
            return true;

        }

        public void Reset()
        {
            index = 0;
        }
    }
}
