using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class FileHandler {

        public static void SaveToJSON<T> (List<T> toSave, string path) {
            var content = JsonHelper.ToJson<T> (toSave, true);
            Debug.Log(content);
            WriteFile (path, content);
        }

        public static void SaveToJSON<T> (T toSave, string path) {
            var content = JsonUtility.ToJson (toSave);
            WriteFile (path, content);
        }

        public static List<T> ReadListFromJSON<T> (string path) {
            var content = ReadFile (path);

            if (string.IsNullOrEmpty (content) || content == "{}") {
                return new List<T> ();
            }

            var res = JsonHelper.FromJson<T> (content).ToList ();

            return res;

        }

        public static T ReadFromJSON<T> (string path) {
            var content = ReadFile (path);

            if (string.IsNullOrEmpty (content) || content == "{}") {
                return default (T);
            }

            var res = JsonUtility.FromJson<T> (content);

            return res;
        }
        
        private static void WriteFile (string path, string content) {
            var fileStream = new FileStream (path, FileMode.Create);

            using var writer = new StreamWriter (fileStream);
            writer.Write (content);
        }

        private static string ReadFile (string path) {
            if (!File.Exists(path)) return "";
            using var reader = new StreamReader (path);
            var content = reader.ReadToEnd ();
            return content;
        }
    }

    public static class JsonHelper {
        public static List<T> FromJson<T> (string json) {
            var wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
            return wrapper.list;
        }

        public static string ToJson<T> (List<T> list) {
            var wrapper = new Wrapper<T>
            {
                list = list
            };
            
            return JsonUtility.ToJson (wrapper);
        }

        public static string ToJson<T> (List<T> list, bool prettyPrint) {
            var wrapper = new Wrapper<T>
            {
                list = list
            };
            
            return JsonUtility.ToJson (wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T> {
            public List<T> list;
        }
    }
}