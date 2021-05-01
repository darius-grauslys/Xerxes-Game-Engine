using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine
{
    public class PersistentArray<T>
    {
        public readonly int MAX_ELEMENT_COUNT;
        
        internal T[] elementArray;
        protected int elementEndIndex;

        internal int[] orderedIndexies;
        internal int orderedCount;

        protected int[] addIndex;
        protected int addCount;

        protected int[] elementResortOrder;
        protected int elementResortCount;

        public PersistentArray(int maxCount)
        {
            MAX_ELEMENT_COUNT = maxCount;
            elementArray = new T[maxCount];
            orderedIndexies = new int[maxCount];
            addIndex = new int[maxCount];
            elementResortOrder = new int[maxCount];
        }

        public int Add(T element)
        {
            int id = addIndex[addCount];
            if (id < 0)
            {
                id = elementEndIndex;
                elementEndIndex++;
            }
            else
            {
                ShiftArray(elementArray, elementEndIndex - 1, elementEndIndex - id, -1, 1);
                AppendWithCount(elementResortOrder, ref elementResortCount, id);
                if (addCount == 0)
                    addIndex[addCount] = -1;
                else
                    addCount--;
            }
            elementArray[id] = element;
            return id;
        }

        public void Remove(int id)
        {
            addIndex[addCount] = id;
            addCount++;
            elementArray[id] = default(T);
            elementEndIndex--;

            ShiftArray(elementArray, id, elementEndIndex - id, 1);
            ShiftArray(orderedIndexies, id, elementEndIndex - id, 1);
        }

        public static void ShiftArray<Y>(Y[] elements, int index, int count, int direction = -1, int shiftOffset = -1)
        {
            int offset = 0;
            for (int i = 0; i < count; i++)
            {
                offset += direction;
                elements[index + offset + shiftOffset] = elements[index + offset];
            }
        }

        public static void AppendWithCount<Y>(Y[] elements, ref int count, Y element)
        {
            elements[count] = element;
            count++;
        }

        public static void DetachWithCount<Y>(Y[] elements, ref int count)
        {
            elements[count] = default(Y);
            count--;
        }

        public virtual void Sort()
        {
            if (orderedCount == 0)
                orderedCount++;
            while (elementResortCount > 0)
            {
                int id = elementResortOrder[elementResortCount - 1];
                for (int i = 0; i < orderedCount; i++)
                {
                    if (Compare(id, orderedIndexies[i]) && //right hand greater, and: left hand index -1 or indexed value is lesser.
                        (
                        i == 0 ||
                        Compare(orderedIndexies[i-1], id)
                        ))
                    {
                        ShiftArray(orderedIndexies, orderedCount, orderedCount - i, -1, 1); //from index:[i] shift to the right.
                        orderedIndexies[i] = id;
                    }
                }
            }
        }

        /// <summary>
        /// Compare if t1 is less than t2.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        protected virtual bool Compare(int index1, int index2) => index1 < index2;
    }
}
