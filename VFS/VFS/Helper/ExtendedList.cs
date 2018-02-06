// ------------------------------------------------------------------------
// ExtendedList.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      29.12.2016
// Last update on:  23.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFS.Helpers
{
    /// <summary>
    /// Represents a normal list which fires an event when the items will be changed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtendedList<T> : List<T>
    {
        /// <summary>
        /// This method describes the event OnItemChanged with an item-array which contains the items that was deleted or added
        /// </summary>
        /// <param name="item"></param>
        public delegate void onItemChanged(T[] item);

        /// <summary>
        /// This event will be thrown when the items will be changed (added/deleted)
        /// </summary>
        public event onItemChanged OnItemChanged;

        /// <summary>
        /// Instantiates a new list
        /// </summary>
        public ExtendedList()
        { }

        /// <summary>
        /// See List Of T for information
        /// </summary>
        /// <param name="item">See List Of T for information</param>
        public new void Add(T item)
        {
            base.Add(item);
            this.CallEvent(item);
        }

        /// <summary>
        /// See List Of T for information
        /// </summary>
        /// <param name="items">See List Of T for information</param>
        public new void AddRange(IEnumerable<T> items)
        {
            base.AddRange(items);
            this.CallEvent((T[])items);
        }

        /// <summary>
        /// See List Of T for information
        /// </summary>
        /// <param name="item">See List Of T for information</param>
        public new void Remove(T item)
        {
            base.Remove(item);
        }

        /// <summary>
        /// See List Of T for information
        /// </summary>
        /// <param name="index">See List Of T for information</param>
        public new void RemoveAt(int index)
        {
            if (index > -1 && index <= this.Count() - 1)
            {
                T currentItem = base[index];
                base.RemoveAt(index);
                this.CallEvent(currentItem);
            }
        }

        /// <summary>
        /// See List Of T for information
        /// </summary>
        /// <param name="index">See List Of T for information</param>
        /// <param name="count">See List Of T for information</param>
        public new void RemoveRange(int index, int count)
        {
            T[] items = new T[count];

            int counter = 0;
            for (int i = index; i <= index + count - 1; i++)
                items[counter++] = base[i];

            base.RemoveRange(index, count);
            this.CallEvent(items);
        }

        /// <summary>
        /// This method throws the event and makes it possible to throw the event also from other objects
        /// </summary>
        /// <param name="items">The items which were added/removed</param>
        public void CallEvent(T[] items)
        {
            this.OnItemChanged?.Invoke(items);
        }

        /// <summary>
        /// This method throws the event and makes it possible to throw the event also from other objects
        /// </summary>
        /// <param name="item">The item which was added/removed</param>
        public void CallEvent(T item)
        {
            this.CallEvent(new T[] { item });
        }
    }
}
