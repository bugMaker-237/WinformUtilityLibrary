﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormUtilityLibrary.LINQ
{
    public class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {

        readonly List<TElement> elements;

        public Grouping(IGrouping<TKey, TElement> grouping)
        {
            if (grouping == null)
                throw new ArgumentNullException("grouping");
            Key = grouping.Key;
            elements = grouping.ToList();
        }

        public TKey Key { get; private set; }

        public IEnumerator<TElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        
    }
}
