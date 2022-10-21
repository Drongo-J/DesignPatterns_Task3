using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DesignPatterns_Task2.Models
{
    public interface IAdapter
    {
        void Write(User user, string filename);
        void Read();
    }

    class JsonAdapter : IAdapter
    {
        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Write(User user, string filename)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(filename))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, user);
                }
            }
        }
    }

    class XmlAdapter : IAdapter
    {
        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Write(User user, string filename)
        {
            XmlSerializer inst = new XmlSerializer(typeof(User));
            TextWriter writer = new StreamWriter(filename);
            inst.Serialize(writer, user);
            writer.Close();
        }
    }

    class Converter
    {
        private readonly IAdapter _adapter;

        public Converter(IAdapter adapter)
        {
            _adapter = adapter;
        }

        public void Read()
        {
            _adapter.Read();
        }

        public void Write(User user, string filename)
        {
            _adapter.Write(user, filename);
        }
    }
}

