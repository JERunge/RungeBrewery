using BrewUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrewUI.Models
{
    public class ArduinoParse
    {
        private static string _message { get; set; }

        public static string ToParse(ArduinoMessage _input)
        {
            try
            {
                _message = "<" + _input.AIndex.ToString() + _input.AMessage + ">";
            }
            catch (NullReferenceException e)
            {
                _message = "<>";
            }

            return _message;
        }

        public static ArduinoMessage FromParse(string recMessage)
        {
            ArduinoMessage arduinoMessage = new ArduinoMessage();

            try
            {
                recMessage = recMessage.Split('<').Last();
                recMessage = recMessage.Split('>').First();
                arduinoMessage.AIndex = recMessage[0];
                arduinoMessage.AMessage = recMessage.Substring(1, recMessage.Length - 1);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }

            return arduinoMessage;
        }
    }
}
