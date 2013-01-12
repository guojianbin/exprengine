﻿#region License
//
// Expression Engine Library: ExpressionVisitor.cs
//
// Author:
//   Giacomo Stelluti Scala (gsscoder@gmail.com)
//
// Copyright (C) 2012 - 2013 Giacomo Stelluti Scala
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
#endregion
#region Using Directives
using System;
using System.Collections.Generic;
#endregion

namespace ExpressionEngine.Internal
{
    sealed class ObjectCache<TValue> : ICache<TValue>
        where TValue : class 
    {
        public ObjectCache()
        {
            _cache = new Dictionary<string, TValue>(32, StringComparer.Ordinal);
        }

        public TValue this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                if (!_cache.ContainsKey(key))
                {
                    throw new ArgumentException("key");
                }
                return _cache[key];
            }
            set
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                if (!_cache.ContainsKey(key))
                {
                    throw new ArgumentException("key");
                }
                _cache[key] = value;
            }
        }

        public void Add(string key, TValue value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (_cache.ContainsKey(key))
            {
                throw new ArgumentException("key");
            }
            _cache.Add(key, value);
        }

        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            return _cache.ContainsKey(key);
        }

        private readonly Dictionary<string, TValue> _cache;
    }
}