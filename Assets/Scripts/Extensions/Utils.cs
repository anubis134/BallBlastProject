using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class Extensions
    {
        [CanBeNull]
        public static T FindThe<T>(this MonoBehaviour monoBehaviour) where T : class =>
                   Object.FindObjectsOfType<MonoBehaviour>().FirstOrDefault(b => b is T) as T;

        public static T FindOrException<T>(this MonoBehaviour monoBehaviour) where T : class =>
            Object.FindObjectsOfType<MonoBehaviour>().FirstOrDefault(b => b is T) as T
            ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        public static T GetOrException<T>(this GameObject gameObject) where T : class =>
            gameObject.GetComponents<MonoBehaviour>().FirstOrDefault(b => b is T) as T
            ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        public static Behaviour GetOrException(this GameObject gameObject, Type type) =>
            gameObject.GetComponents<MonoBehaviour>().FirstOrDefault(b => b.GetType() == type)
            ?? throw new NullReferenceException($"Object of type {type.Name} not found");

        public static T GetOrException<T>(this MonoBehaviour monoBehaviour) where T : class =>
            GetOrException<T>(monoBehaviour.gameObject);

        public static T[] FindMultiple<T>(this MonoBehaviour monoBehaviour, bool includeInactive = false)
            where T : class => Object.FindObjectsOfType<MonoBehaviour>(includeInactive).OfType<T>().ToArray();

        public static T[] FindMultiple<T>(bool includeInactive = false)
            where T : class => Object.FindObjectsOfType<MonoBehaviour>(includeInactive).OfType<T>().ToArray();

        public static T[] GetMultipleInChildren<T>(this MonoBehaviour monoBehaviour) where T : class =>
            monoBehaviour.GetComponentsInChildren<MonoBehaviour>().OfType<T>().ToArray();

        public static Task Await(this float seconds) => Task.Delay(TimeSpan.FromSeconds(seconds));

        public static Vector2 FlatVector(this Vector3 vector3) => new Vector2(vector3.x, vector3.z);

        public static Vector3 VolumeVector(this Vector2 vector3) => new Vector3(vector3.x, 0, vector3.y);

    }
}
