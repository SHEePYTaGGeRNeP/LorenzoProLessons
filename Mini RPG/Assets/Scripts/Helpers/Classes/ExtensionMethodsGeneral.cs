using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if (!UNITY_WINRT)
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#endif
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
public static class ExtensionMethodsGeneral
{
    private static readonly System.Random _random = new System.Random();

    /// <summary>
    /// If the given <paramref name="type"/> is an array or some other collection
    /// comprised of 0 or more instances of a "subtype", get that type
    /// </summary>
    /// <param name="type">the source type</param>
    /// <returns></returns>
    public static Type GetEnumeratedType(this Type type)
    {
        // provided by Array
        var elType = type.GetElementType();
        if (null != elType) return elType;

        // otherwise provided by collection
        var elTypes = type.GetGenericArguments();
        if (elTypes.Length > 0) return elTypes[0];

        // otherwise is not an 'enumerated' type
        return null;
    }


    public static string ToRacePositionText(this int pos)
    {
        switch (pos)
        {
            case 1: return pos + "st";
            case 2: return pos + "nd";
            case 3: return pos + "rd";
            default: return pos + "th";
        }
    }

    public static string GetFullName(this Enum myEnum)
    {
        return String.Format("{0}.{1}", myEnum.GetType().Name, myEnum.ToString());
    }

    /// <summary>
    /// Returns the number of nodes between the startNode and the targetNode
    /// Uses .Next and .Previous.
    /// </summary>
    public static int CountNodesTo<T>(this LinkedList<T> list, LinkedListNode<T> startNode,
        LinkedListNode<T> targetNode)
    {
        int count = 0;
        LinkedListNode<T> curNode = startNode;
        for (int loop = 0; loop < list.Count; loop++)
        {
            if (curNode == null)
                curNode = list.First;
            if (curNode == targetNode)
                break;
            curNode = curNode.Next;
            count++;
        }
        bool found = curNode == targetNode;
        int resultNext = count;
        count = 0;
        curNode = startNode;
        for (int loop = 0; loop < list.Count; loop++)
        {
            if (curNode == null)
                curNode = list.Last;
            if (curNode == targetNode)
                break;
            curNode = curNode.Previous;
            count++;
        }
        if (!found)
            found = curNode == targetNode;
        if (!found)
            throw new Exception("targetNode not found in LinkedList");
        return Math.Min(count, resultNext);
    }

    /// <summary>
    /// Determines whether the collection is null or contains no elements.
    /// </summary>
    /// <typeparam name="T">The IEnumerable type.</typeparam>
    /// <param name="enumerable">The enumerable, which may be null or empty.</param>
    /// <returns>
    ///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        switch (enumerable)
        {
            case null:
                return true;
            case ICollection<T> collection:
                return collection.Count < 1;
            default:
                return !enumerable.Any();
        }
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        if (list == null) return;
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int index = _random.Next(0, n + 1);
            T value = list[index];
            list[index] = list[n];
            list[n] = value;
        }
    }

    public static T RandomItem<T>(this IList<T> list)
    {
        return list[_random.Next(0, list.Count)];
    }

    public static string ConvertBitValuesToString(this bool[] bits)
    {
        StringBuilder sb = new StringBuilder();
        foreach (bool b in bits)
            sb.Append(b ? "1" : "0");
        return sb.ToString();
    }

    /// <summary>
    /// returns the byte decimal numbers -> 00 13 16 53 31
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToStringDecimals(this byte[] bytes) => String.Join(" ", bytes.Select(b => b.ToString("D2")));
    public static string ToText(this byte[] bytearr) => System.Text.Encoding.Default.GetString(bytearr).Trim('\0');

    public static async Task<bool> WaitOneAsync(this WaitHandle handle, int millisecondsTimeout, CancellationToken cancellationToken)
    {
        RegisteredWaitHandle registeredHandle = null;
        CancellationTokenRegistration tokenRegistration = default;
        try
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            registeredHandle = ThreadPool.RegisterWaitForSingleObject(
                handle, (state, timedOut) => ((TaskCompletionSource<bool>)state).TrySetResult(!timedOut),
                tcs, millisecondsTimeout, true);
            tokenRegistration = cancellationToken.Register(
                state => ((TaskCompletionSource<bool>)state).TrySetCanceled(), tcs);
            return await tcs.Task;
        }
        finally
        {
            registeredHandle?.Unregister(null);
            tokenRegistration.Dispose();
        }
    }

    public static List<string> DebugDetailedCompare<T>(this T val1, T val2)
    {
        List<string> variances = new List<string>();
        FieldInfo[] fi = val1.GetType().GetFields();
        PropertyInfo[] pi = val1.GetType().GetProperties();
        foreach (FieldInfo f in fi)
        {
            object o1, o2;
            try
            { o1 = f.GetValue(val1); }
            catch (Exception e)
            { o1 = e.Message; }
            try
            { o2 = f.GetValue(val2); }
            catch (Exception e)
            { o2 = e.Message; }
            if (!Equals(o1, o2))
                variances.Add($"{f.Name} val1: {o1} val2: {o2}");
        }
        foreach (PropertyInfo p in pi)
        {
            object o1, o2;
            try
            { o1 = p.GetValue(val1); }
            catch (Exception e)
            { o1 = e.Message; }
            try
            { o2 = p.GetValue(val2); }
            catch (Exception e)
            { o2 = e.Message; }
            if (!Equals(o1, o2))
                variances.Add($"{p.Name} val1: {o1} val2: {o2}");
        }
        return variances;
    }

    public static StringBuilder Prepend(this StringBuilder sb, string content) => sb.Insert(0, content);
    public static StringBuilder PrependLine(this StringBuilder sb, string content) => sb.Prepend(content).Prepend(Environment.NewLine);

    public static async Task<bool> WaitOneAsync(this WaitHandle handle, TimeSpan timeout, CancellationToken cancellationToken) => await handle.WaitOneAsync((int)timeout.TotalMilliseconds, cancellationToken);

    public static async Task<bool> WaitOneAsync(this WaitHandle handle, CancellationToken cancellationToken) => await handle.WaitOneAsync(Timeout.Infinite, cancellationToken);

    // https://stackoverflow.com/questions/9300169/linq-indexof-a-particular-entry
    public static int FirstIndexOfExt<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        int index = 0;
        foreach (TSource item in source)
        {
            if (predicate.Invoke(item))
                return index;
            index++;
        }
        return -1;
    }

    public static void RemoveAll<K, V>(this IDictionary<K, V> dict, Func<K, V, bool> match)
    {
        // https://www.codeproject.com/Tips/494499/Implementing-Dictionary-RemoveAll ToArray() makes it faster
        foreach (K key in dict.Keys.ToArray().Where(key => match(key, dict[key])))
            dict.Remove(key);
    }
    
    public static int NumberOfDigits(this int n)
    {
        int abs = Math.Abs(n);
        if (abs == 0)
            return 1;
        double log10 = Math.Log10(abs);
        int final = 1 + (int)log10;
        return final;
    }
    public static int GetDecimalCount(this decimal d, bool trimTrailingZeros)
    {
        string text = d.ToString(System.Globalization.CultureInfo.InvariantCulture);
        if (trimTrailingZeros)
            text = text.TrimEnd('0');
        int decpoint = text.IndexOf('.');
        if (decpoint < 0)
            return 0;
        return text.Length - decpoint - 1;
    }
    public static string MethodSignature(this MethodBase mb)
    {
        string[] param = mb.GetParameters()
            .Select(p => String.Format("{0} {1}", p.ParameterType.Name, p.Name)).ToArray();
        MethodInfo normalMethod = mb as MethodInfo;
        if (normalMethod != null)
            return String.Format("{0} {1}({2})", normalMethod.ReturnType.Name, mb.Name, String.Join(",", param));
        else
            return String.Format("{0}({1})", mb.Name, String.Join(",", param));
    }

    #region GetProperties/Fields

    public static PropertyInfo[] GetFilteredProperties(this Type type, int? softVersion) => GetFilteredProperties(type, softVersion, BindingFlags.Instance | BindingFlags.Public);
    public static PropertyInfo[] GetFilteredProperties(this Type type, int? softVersion, BindingFlags flags)
    {
        Type[] notAllowedAttributes = GetNotAllowedAttributes(softVersion);
        return type.GetProperties(flags).Where(prop => !notAllowedAttributes.Any(t => Attribute.IsDefined(prop, t))).ToArray();
    }

    public static FieldInfo[] GetFilteredFields(this Type type, int? softVersion) => GetFilteredFields(type, softVersion, BindingFlags.Instance | BindingFlags.Public);
    public static FieldInfo[] GetFilteredFields(this Type type, int? softVersion, BindingFlags flags)
    {
        Type[] notAllowedAttributes = GetNotAllowedAttributes(softVersion);
        return type.GetFields(flags).Where(prop => !notAllowedAttributes.Any(t => Attribute.IsDefined(prop, t))).ToArray();
    }

    public class DoNotIncludeAttribute : Attribute
    {
    }
    public class Soft2Attribute : Attribute
    {
    }

    private static Type[] GetNotAllowedAttributes(int? softwareVersion)
    {
        switch (softwareVersion)
        {
            case 0:
                return new[]
                    {typeof(DoNotIncludeAttribute), typeof(Soft2Attribute)};
            case 1:
                return new[] { typeof(DoNotIncludeAttribute), typeof(Soft2Attribute) };
            case 2:
                return new[] { typeof(DoNotIncludeAttribute) };
            default:
                return new[] { typeof(DoNotIncludeAttribute) };
        }
    }

    #endregion

    #region Floating Point Comparisons

    private const double DEFAULT_DOUBLE_COMPARISON_TOLERANCE = 1e-8d;
    private const float DEFAULT_FLOAT_COMPARISON_TOLERANCE = 1e-8f;
    private const decimal DEFAULT_DECIMAL_COMPARISON_TOLERANCE = 1e-8m;

    // > and < is not always enough for floating point numbers, because 1.000000000001 and 1.0 you might want to return false / are not equal;
    public static bool IsAbout(this double d, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => Math.Abs(d - other) <= tolerance;
    public static bool IsMoreThan(this double d, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => d - other > tolerance;
    public static bool IsLessThan(this double d, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => other - d > tolerance;
    public static bool IsMoreThanOrAbout(this double d, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsMoreThan(d, other, tolerance);
    public static bool IsLessThanOrAbout(this double d, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsLessThan(d, other, tolerance);

    public static bool IsAbout(this float f, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => Math.Abs(f - other) <= tolerance;
    public static bool IsMoreThan(this float f, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => f - other > tolerance;
    public static bool IsLessThan(this float f, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => other - f > tolerance;
    public static bool IsMoreThanOrAbout(this float d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsMoreThan(d, other, tolerance);
    public static bool IsLessThanOrAbout(this float d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsLessThan(d, other, tolerance);

    public static bool IsAbout(this decimal d, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => Math.Abs(d - other) <= tolerance;
    public static bool IsMoreThan(this decimal d, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => d - other > tolerance;
    public static bool IsLessThan(this decimal d, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => other - d > tolerance;
    public static bool IsMoreThanOrAbout(this decimal d, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsMoreThan(d, other, tolerance);
    public static bool IsLessThanOrAbout(this decimal d, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsLessThan(d, other, tolerance);

    public static bool IsAbout(this double d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => Math.Abs(d - other) <= tolerance;
    public static bool IsMoreThan(this double d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => d - other > tolerance;
    public static bool IsLessThan(this double d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => other - d > tolerance;
    public static bool IsMoreThanOrAbout(this double d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsMoreThan(d, other, tolerance);
    public static bool IsLessThanOrAbout(this double d, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsLessThan(d, other, tolerance);

    public static bool IsAbout(this float f, double other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => Math.Abs(f - other) <= tolerance;
    public static bool IsMoreThan(this float f, double other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => f - other > tolerance;
    public static bool IsLessThan(this float f, double other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => other - f > tolerance;
    public static bool IsMoreThanOrAbout(this float f, double other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(f, other, tolerance) || IsMoreThan(f, other, tolerance);
    public static bool IsLessThanOrAbout(this float f, double other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(f, other, tolerance) || IsLessThan(f, other, tolerance);

    public static bool IsAbout(this int i, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => Math.Abs(i - other) <= tolerance;
    public static bool IsMoreThan(this int i, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => i - other > tolerance;
    public static bool IsLessThan(this int i, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => other - i > tolerance;
    public static bool IsMoreThanOrAbout(this int i, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(i, other, tolerance) || IsMoreThan(i, other, tolerance);
    public static bool IsLessThanOrAbout(this int i, float other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(i, other, tolerance) || IsLessThan(i, other, tolerance);

    public static bool IsAbout(this int i, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => Math.Abs(i - other) <= tolerance;
    public static bool IsMoreThan(this int i, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => i - other > tolerance;
    public static bool IsLessThan(this int i, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => other - i > tolerance;
    public static bool IsMoreThanOrAbout(this int i, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => IsAbout(i, other, tolerance) || IsMoreThan(i, other, tolerance);
    public static bool IsLessThanOrAbout(this int i, double other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => IsAbout(i, other, tolerance) || IsLessThan(i, other, tolerance);

    public static bool IsAbout(this int i, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => Math.Abs(i - other) <= tolerance;
    public static bool IsMoreThan(this int i, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => i - other > tolerance;
    public static bool IsLessThan(this int i, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => other - i > tolerance;
    public static bool IsMoreThanOrAbout(this int i, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => IsAbout(i, other, tolerance) || IsMoreThan(i, other, tolerance);
    public static bool IsLessThanOrAbout(this int i, decimal other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => IsAbout(i, other, tolerance) || IsLessThan(i, other, tolerance);

    public static bool IsAbout(this float f, int other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => Math.Abs(f - other) <= tolerance;
    public static bool IsMoreThan(this float f, int other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => f - other > tolerance;
    public static bool IsLessThan(this float f, int other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => other - f > tolerance;
    public static bool IsMoreThanOrAbout(this float f, int other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(f, other, tolerance) || IsMoreThan(f, other, tolerance);
    public static bool IsLessThanOrAbout(this float f, int other, float tolerance = DEFAULT_FLOAT_COMPARISON_TOLERANCE) => IsAbout(f, other, tolerance) || IsLessThan(f, other, tolerance);

    public static bool IsAbout(this double d, int other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => Math.Abs(d - other) <= tolerance;
    public static bool IsMoreThan(this double d, int other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => d - other > tolerance;
    public static bool IsLessThan(this double d, int other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => other - d > tolerance;
    public static bool IsMoreThanOrAbout(this double d, int other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsMoreThan(d, other, tolerance);
    public static bool IsLessThanOrAbout(this double d, int other, double tolerance = DEFAULT_DOUBLE_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsLessThan(d, other, tolerance);

    public static bool IsAbout(this decimal d, int other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => Math.Abs(d - other) <= tolerance;
    public static bool IsMoreThan(this decimal d, int other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => d - other > tolerance;
    public static bool IsLessThan(this decimal d, int other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => other - d > tolerance;
    public static bool IsMoreThanOrAbout(this decimal d, int other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsMoreThan(d, other, tolerance);
    public static bool IsLessThanOrAbout(this decimal d, int other, decimal tolerance = DEFAULT_DECIMAL_COMPARISON_TOLERANCE) => IsAbout(d, other, tolerance) || IsLessThan(d, other, tolerance);

    #endregion


#if (!UNITY_WINRT)
    /// <summary>
    /// Class must be marked [Serializable].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepClone<T>(this T obj)
    {
        if (obj == null)
        {
            T returnValue = default;
            return returnValue;
        }
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            T returnValue = (T)binaryFormatter.Deserialize(memoryStream);
            return returnValue;
        }
    }
#endif

    /// <summary>
    /// Clears the contents of the string builder. This method exists in .Net 4.0 but not in 2.0
    /// </summary>
    /// <param name="value">
    /// The <see cref="StringBuilder"/> to clear.
    /// </param>
    public static void ClearContents(this StringBuilder value)
    {
        value.Length = 0;
        value.Capacity = 0;
    }

    #region MoreLINQ

    #region License and Terms

    // MoreLINQ - Extensions to LINQ to Objects
    // Copyright (c) 2008-2011 Jonathan Skeet. All rights reserved.
    // 
    // Licensed under the Apache License, Version 2.0 (the "License");
    // you may not use this file except in compliance with the License.
    // You may obtain a copy of the License at
    // 
    //     http://www.apache.org/licenses/LICENSE-2.0
    // 
    // Unless required by applicable law or agreed to in writing, software
    // distributed under the License is distributed on an "AS IS" BASIS,
    // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    // See the License for the specific language governing permissions and
    // limitations under the License.

    #endregion

    /// <summary>
    /// Returns the minimal element of the given sequence, based on
    /// the given projection.
    /// </summary>
    /// <remarks>
    /// If more than one element has the minimal projected value, the first
    /// one encountered will be returned. This overload uses the default comparer
    /// for the projected type. This operator uses immediate execution, but
    /// only buffers a single result (the current minimal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <returns>The minimal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
    {
        return source.MinBy(selector, Comparer<TKey>.Default);
    }

    /// <summary>
    /// Returns the minimal element of the given sequence, based on
    /// the given projection and the specified comparer for projected values.
    /// </summary>
    /// <remarks>
    /// If more than one element has the minimal projected value, the first
    /// one encountered will be returned. This overload uses the default comparer
    /// for the projected type. This operator uses immediate execution, but
    /// only buffers a single result (the current minimal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <param name="comparer">Comparer to use to compare projected values</param>
    /// <returns>The minimal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
    /// or <paramref name="comparer"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector, IComparer<TKey> comparer)
    {
        if (source == null)
            throw new ArgumentNullException("source");
        if (selector == null)
            throw new ArgumentNullException("selector");
        if (comparer == null)
            throw new ArgumentNullException("comparer");
        using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            TSource min = sourceIterator.Current;
            TKey minKey = selector(min);
            while (sourceIterator.MoveNext())
            {
                TSource candidate = sourceIterator.Current;
                TKey candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, minKey) >= 0) continue;
                min = candidate;
                minKey = candidateProjected;
            }
            return min;
        }
    }

    /// <summary>
    /// Returns the maximimal element of the given sequence, based on the given projection.
    /// </summary>
    /// <remarks>
    /// If more than one element has the maximal projected value, the first
    /// one encountered will be returned. This overload uses the default comparer
    /// for the projected type. This operator uses immediate execution, but
    /// only buffers a single result (the current maximal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <returns>The maximal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
    {
        return source.MaxBy(selector, Comparer<TKey>.Default);
    }

    /// <summary>
    /// Returns the maximal element of the given sequence, based on
    /// the given projection and the specified comparer for projected values.
    /// </summary>
    /// <remarks>
    /// If more than one element has the maximal projected value, the first
    /// one encountered will be returned. This overload uses the default comparer
    /// for the projected type. This operator uses immediate execution, but
    /// only buffers a single result (the current maximal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <param name="comparer">Comparer to use to compare projected values</param>
    /// <returns>The maximal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
    /// or <paramref name="comparer"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector, IComparer<TKey> comparer)
    {
        if (source == null)
            throw new ArgumentNullException("source");
        if (selector == null)
            throw new ArgumentNullException("selector");
        if (comparer == null)
            throw new ArgumentNullException("comparer");
        using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            TSource max = sourceIterator.Current;
            TKey maxKey = selector(max);
            while (sourceIterator.MoveNext())
            {
                TSource candidate = sourceIterator.Current;
                TKey candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) <= 0) continue;
                max = candidate;
                maxKey = candidateProjected;
            }
            return max;
        }
    }

    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        if (val.CompareTo(max) > 0) return max;
        return val;
    }


    #endregion

    public static bool IsNullOrEmpty(this string str) => String.IsNullOrEmpty(str);

    public static bool IsNullOrWhiteSpace(this string str) => String.IsNullOrWhiteSpace(str);

    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    /// <summary>
    /// Returns the elements of a collection to a string using a seperator for each element.
    /// </summary>
    public static string ToStringCollection<T>(this IEnumerable<T> something, string seperator)
    {
        StringBuilder builder = new StringBuilder();
        foreach (T t in something)
            builder.Append(t + seperator);
        builder.Remove(builder.Length - seperator.Length, seperator.Length);
        return builder.ToString();
    }

}