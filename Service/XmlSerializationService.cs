using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LearnApplication.Service
{
    public static class XmlSerializationService
    {
        public static void SerializeToXml<T>(string filePath, T data)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath,FileMode.OpenOrCreate))
                {

                }
                XmlSerializer xmlSerializer = new(typeof(T));
                using StreamWriter writer = new(filePath);
                xmlSerializer.Serialize(writer, data);

            }
            catch (Exception ex)
            {
                App.Current?.MainPage?.DisplayAlert("Ошибка чтения файла сохранения!",ex.Message, "OK");
            }
        }
        public static T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                XmlSerializer xmlSerializer = new(typeof(T));
                using StreamReader reader = new(filePath);
                if(xmlSerializer.Deserialize(reader) is T xmlDeserialize)
                    return xmlDeserialize;
            }
            catch (Exception ex)
            {
                _ = App.Current?.MainPage?.DisplayAlert("Ошибка  сохранения файла!", ex.Message, "OK");
            }
            return default;
        }
    }
}
