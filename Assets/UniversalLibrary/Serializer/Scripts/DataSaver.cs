using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Datasaver
{
    public class DataSaver
    {
        public DataSaver()
        {

        }

        JsonSerializer serializer = new JsonSerializer();

        Semaphore sem = new Semaphore(1, 1);

        /// <summary>
        /// Сериализует переданный объект в строку json и вернет ее
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetSerializeJson<T>(T obj)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Десериализует строку в указанный тип и вернет его
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Сериализует любой объект по указанному пути
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public void Serialize<T>(T obj, string path)
        {
            AsyncSerialize<T>(obj, path);
        }

        /// <summary>
        /// Десереализует любой json-файл по указанному пути и возвращает типизированный список
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<T> Deserialize<T>(string path)
        {
            List<T> result = AsyncDeserialize<T>(path).GetAwaiter().GetResult();

            return result;
        }

        //private async void SerializeAsync<T>(T obj, string path)
        //{
        //    sem.WaitOne();

        //    Serialize<T>(obj, path);

        //    sem.Release();

        //}

        //private async Task<List<T>> DeserializeAsync<T>( string path)
        //{
        //    sem.WaitOne();

        //    var result = Deserialize<T>(path);

        //    sem.Release();
        //    return result;
        //}

        private async void AsyncSerialize<T>(T obj, string path)
        {
            sem.WaitOne();

            serializer.Converters.Add(new JavaScriptDateTimeConverter());

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }

            sem.Release();
        }

        private async Task<List<T>> AsyncDeserialize<T>(string path)
        {
            sem.WaitOne();

            List<T> result = null;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<T>>(json);
            }

            sem.Release();
            return result;
        }
    }
}