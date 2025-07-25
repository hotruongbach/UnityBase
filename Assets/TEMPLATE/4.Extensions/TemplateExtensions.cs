using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Template.Utilities
{
    public static class TemplateExtensions
    {
        #region LIST
        public static T First<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new System.InvalidOperationException("The list is empty or null.");
            }

            return list[0];
        }

        public static T Last<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new System.InvalidOperationException("The list is empty or null.");
            }

            return list[list.Count - 1];
        }

        public static T SecondLast<T>(this List<T> list)
        {
            if (list == null || list.Count < 2)
            {
                throw new System.InvalidOperationException("The list is empty or null.");
            }

            return list[list.Count - 2];
        }

        public static void Display<T>(this List<T> list, string element)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Bug.Log($"{element} {i} : {list[i]}");
            }
        }
        public static Dictionary<T, int> CountDuplicates<T>(List<T> list)
        {
            // Dictionary to store counts of each unique element
            Dictionary<T, int> elementCount = new Dictionary<T, int>();

            // Loop through the list and count occurrences of each element
            foreach (T element in list)
            {
                // Check if the element is already in the dictionary
                if (elementCount.ContainsKey(element))
                {
                    // If so, increment the count
                    elementCount[element]++;
                }
                else
                {
                    // If not, add the element to the dictionary with count 1
                    elementCount[element] = 1;
                }
            }
            return elementCount;
        }
        public static T WeightedRandom<T>(this List<T> items, List<float> weights)
        {
            if (items.Count != weights.Count)
            {
                throw new ArgumentException("Number of items must match number of weights.");
            }

            // Calculate total weight
            float totalWeight = 0;
            foreach (float weight in weights)
            {
                totalWeight += weight;
            }

            // Generate a random number between 0 and the total weight
            float randomValue = UnityEngine.Random.Range(0, totalWeight);

            // Find the corresponding item based on the random value
            float weightSum = 0;
            for (int i = 0; i < items.Count; i++)
            {
                weightSum += weights[i];
                if (randomValue <= weightSum)
                {
                    return items[i];
                }
            }

            // This should not happen, but in case of rounding errors, return the last item
            return items[items.Count - 1];
        }

        /// <summary>
        /// Get a specific number of items from a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> GetRandomElements<T>(this List<T> list, int x = 0)
        {
            if (x == 0) x = 1;
            // Check if x is greater than the number of elements in the list
            if (x > list.Count)
            {
                throw new ArgumentException("Number of elements to choose cannot exceed the number of elements in the list.");
            }

            List<T> result = new List<T>(list);

            // Fisher-Yates shuffle algorithm to shuffle the list
            for (int i = result.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                T temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            // Select the first x elements from the shuffled list
            result.RemoveRange(x, result.Count - x);

            return result;
        }
        #endregion

        #region ARRAY
        public static T GetRandomElement<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array is null or empty");
            }
            if (array.Length == 1)
            {
                return array[0];
            }

            // Get a random index
            //int randomIndex = random.Next(0, array.Length);
            int randomIndex = Random.Range(0, array.Length);

            // Return the random element
            return array[randomIndex];
        }
        #endregion

        #region COROUTINE

        public static void StartDelayAction(this MonoBehaviour mono, float time, Action callback)
        {
            if (mono.gameObject.activeSelf)
            {
                mono.StartCoroutine(Delay(callback, time));
            }
        }
        public static void StartDelayActionUnscaleTime(this MonoBehaviour mono, float time, Action callback)
        {
            if (mono.gameObject.activeSelf)
            {
                mono.StartCoroutine(DelayUnscaleTime(callback, time));
            }
        }
        private static IEnumerator DelayUnscaleTime(Action callBack, float time)
        {
            yield return new WaitForSecondsRealtime(time);
            callBack.Invoke();
        }
        public static void StartActionEndOfFrame(this MonoBehaviour mono, Action callback)
        {
            mono.StartCoroutine(DelayEndOfFrame(callback));
        }

        private static IEnumerator Delay(Action callBack, float time)
        {
            yield return Yielder.Get(time);
            callBack.Invoke();
        }

        private static IEnumerator DelayEndOfFrame(Action callBack)
        {
            yield return new WaitForEndOfFrame();
            callBack.Invoke();
        }


        #endregion

        #region TRANSFORM
        public static Vector3 GetLookHorizontalPosition(this Transform tf, Transform tfTarget)
        {
            Vector3 posLook = new Vector3(tfTarget.transform.position.x, tf.transform.position.y, tfTarget.transform.position.z);
            return posLook;
        }
        public static Vector3 GetLookHorizontalPosition(this Transform tf, Vector3 posTarget)
        {
            Vector3 posLook = new Vector3(posTarget.x, tf.transform.position.y, posTarget.z);
            return posLook;
        }
        public static Vector3 GetWorldPosition(this RectTransform rect)
        {
            return rect.TransformPoint(rect.transform.position);
        }
        #endregion

        #region RECT TRANSFORM
        public static Vector2 GetRandomPositionInRect(this RectTransform rectTransform)
        {
            if (rectTransform != null)
            {
                // Get the size of the RectTransform
                Vector2 rectSize = rectTransform.rect.size;

                // Calculate random x and y coordinates within the RectTransform bounds
                float randomX = UnityEngine.Random.Range(-rectSize.x / 2f, rectSize.x / 2f);
                float randomY = UnityEngine.Random.Range(-rectSize.y / 2f, rectSize.y / 2f);

                // Return the random position
                return new Vector2(randomX, randomY);
            }
            else
            {
                return Vector2.zero;
            }
        }
        #endregion

        #region STRING
        static string STR_KEY = "k";
        public static string ToDictKey(this int value)
        {
            return string.Concat(STR_KEY, value);
        }
        public static int DictKeyToInt(this string dictKey)
        {
            return int.Parse(dictKey.Substring(1));
        }
        public static bool NotEquals(this string value, string pair)
        {
            return !value.Equals(pair);
        }
        public static T ToEnum<T>(this string value)
        {
            value.ToUpper();
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static string TimeToString(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // Format the result as a string
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        public static DateTime ToDate(this string timeString, string format = "dd/MM/yyyy")
        {
            return DateTime.ParseExact(timeString, format, null);
        }
        public static string ToDateString(this DateTime dateTime, string format = "dd/MM/yyyy")
        {
            return dateTime.ToString(format);
        }
        public static string ToDetailedString(this DateTime dateTime)
        {
            return dateTime.ToString();
        }
        public static DateTime ToDetailedDatetime(this string s)
        {
            return DateTime.Parse(s);
        }
        public static bool TryGetDateTime(this string timeString, out DateTime result)
        {
            result = DateTime.Now;
            try
            {
                result = timeString.ToDetailedDatetime();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool TryGetDate(this string timeString, out DateTime result)
        {
            result = DateTime.Now;
            try
            {
                result = timeString.ToDate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ToShortString(this long num)
        {
            if (num < 1000000)
            {
                return num.ToString("n0");
            }
            else if (num < 1000000000)
            {
                int integer = (int)num / 1000000;
                int decim = (int)num % 1000000;

                if (decim >= 10000)
                {
                    return (num / (float)1000000).ToString("f2") + "M";
                }
                else
                {
                    return integer.ToString() + "M";
                }
            }
            else
            {
                return (num / (float)1000000000).ToString("f2") + "B";
            }
        }

        public static string ToShortString(this int num)
        {
            return ((long)num).ToShortString();
        }

        public static string ToShortString(this float num)
        {
            return ((long)num).ToShortString();
        }
        public static string SetColor(this string message, string color)
        {
            return $"<color={color}>{message}</color>";
        }
        public static string SetColor(this string message, Color color)
        {
            return $"<color={color.ToString().ToLowerInvariant()}>{message}</color>";
        }
        #endregion

        #region TIMESPAN
        public static string GetFormattedTimerShort(this TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", ts.Days * 24 + ts.Hours, ts.Minutes, ts.Seconds);
        }
        public static string ToTimeFormat(this int totalSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);

            if (timeSpan.TotalDays >= 1)
            {
                return $"{(int)timeSpan.TotalDays}D: {timeSpan.Hours:D2}H";
            }
            else
            {
                if (timeSpan.TotalSeconds > 0)
                {
                    return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                }
                else
                {
                    return $"Finished";
                }
            }
        }

        #endregion

        #region ENUM
        public static T RandomEnumValue<T>(this T enumType) where T : Enum
        {
            // Get all values of the enum
            Array enumValues = Enum.GetValues(typeof(T));

            // Generate a random index to select a random enum value
            int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);

            // Retrieve the random enum value
            T randomEnumValue = (T)enumValues.GetValue(randomIndex);

            return randomEnumValue;
        }

        #endregion

        #region VECTOR
        public static Vector3 X(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        // Set Y component of Vector3
        public static Vector3 Y(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        // Set Z component of Vector3
        public static Vector3 Z(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
        #endregion

        #region MATH
        public static float MapLerp(float InStart, float InEnd, float OutStart, float OutEnd, float input)
        {
            return Mathf.Lerp(OutStart, OutEnd, Mathf.InverseLerp(InStart, InEnd, input));
        }

        public static string WithSign(this int f)
        {
            if (f >= 0) return "+" + f;
            else return f.ToString();
        }
        #endregion

        #region DATETIME
        public static bool IsWithinRange(this DateTimeOffset now, DateTimeOffset start, DateTimeOffset end)
        {
            bool result = now >= start && now <= end;
            Bug.Log($"Is {now} in range from {start} to {end} = {result}");
            return result;
        }
        #endregion


    }
    public class Yielder
    {
        private static Dictionary<float, WaitForSeconds> yields = new Dictionary<float, WaitForSeconds>();
        private static Dictionary<float, WaitForSecondsRealtime> realtimeYields = new Dictionary<float, WaitForSecondsRealtime>();

        public static WaitForSeconds Get(float delay)
        {
            if (yields.ContainsKey(delay))
            {
                return yields[delay];
            }
            else
            {
                WaitForSeconds tmp = new WaitForSeconds(delay);
                yields.Add(delay, tmp);
                return tmp;
            }
        }

        public static WaitForSecondsRealtime GetRealTime(float delay)
        {
            if (realtimeYields.ContainsKey(delay))
            {
                return realtimeYields[delay];
            }

            WaitForSecondsRealtime tmp = new WaitForSecondsRealtime(delay);
            realtimeYields.Add(delay, tmp);
            return tmp;
        }
    }
}
