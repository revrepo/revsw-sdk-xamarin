using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace RacerApp.Service
{
    public class StubDataGenerator
    {
        public async Task<string> GetAsync(string format, int sizeBytes)
        {
            var so = new Data();

            string singleObjectString = string.Empty;

            if (format != "application/json")
            {
                singleObjectString = Serialize(so);
            }
            else
            {
                singleObjectString = JsonConvert.SerializeObject(so, Formatting.Indented);
            }

            var mult = Math.Round((sizeBytes / (double)singleObjectString.Length));

            var count = (int)mult;

            if (count < 1) count = 1;

            var list = new List<Data>(count);

            for (int i = 0; i < count; i++)
            {
                list.Add(new Data());
            }

            if (format != "application/json")
            {
                return Serialize(list);
            }
            else
            {
                return JsonConvert.SerializeObject(list, Formatting.Indented);
            }
        }

        public static string Serialize<T>(T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public class Data
        {
            private static Random _random;
            static Data()
            {
                var dt = DateTime.Now;
                _random = new Random(dt.Millisecond + dt.Second * dt.Minute);
            }

            public int index { get; set; } = _random.Next(1000, 9999);
            public Guid guid { get; set; } = Guid.NewGuid();
            public bool isActive { get; set; } = _random.Next(100, 200) > 50 ? true : false;
            public double balance { get; set; } = _random.Next(100, 999);
            public string picture { get; set; } = Guid.NewGuid().ToString("B");
            public int age { get; set; } = _random.Next(10, 19);
            public string eyeColor { get; set; } = Guid.NewGuid().ToString("B");
            public string name { get; set; } = Guid.NewGuid().ToString("B");
            public string gender { get; set; } = Guid.NewGuid().ToString("B");
            public string company { get; set; } = Guid.NewGuid().ToString("B");
            public string email { get; set; } = Guid.NewGuid().ToString("B");
            public string phone { get; set; } = Guid.NewGuid().ToString("B");
            public string address { get; set; } = Guid.NewGuid().ToString("B");
            public string about { get; set; } = Guid.NewGuid().ToString("B");

            public Data()
            {

            }
        }
    }
}
