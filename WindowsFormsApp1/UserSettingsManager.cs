using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIPWindowsAgent
{ 

    internal class UserSettingsManager
    {

        public static void SaveUserSettings(string username, string password, string ipAddress)
        {
            //var text = JsonConvert.SerializeObject(x)
            //JsonConvert.DeserializeObject<>(text)
            // Create a new XDocument


            XDocument doc = new XDocument(
                new XElement("Settings",
                    new XElement("Username", username),
                    new XElement("Password", password),
                    new XElement("IPAddress", ipAddress)
                )
            );

            // Save the document to a file named after the username
            string filePath = $"{username}_Settings.xml";
            doc.Save(filePath);

            Console.WriteLine($"Settings for user '{username}' saved to {filePath}");
        }

        public static void LoadUserSettings(string username)
        {
            // Load the document from the file named after the username
            string filePath = $"{username}_Settings.xml";
            XDocument doc = XDocument.Load(filePath);

            // Retrieve settings from the document
            string loadedUsername = doc.Root.Element("Username").Value;
            string loadedPassword = doc.Root.Element("Password").Value;
            string loadedIPAddress = doc.Root.Element("IPAddress").Value;

            Console.WriteLine($"Loaded settings for user '{loadedUsername}':");
            Console.WriteLine($"Password: {loadedPassword}");
            Console.WriteLine($"IPAddress: {loadedIPAddress}");
        }

      
    }

}

