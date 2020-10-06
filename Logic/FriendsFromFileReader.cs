using FriendsTracker.Models;
using System.Xml;
using System;
using System.Collections.Generic;

namespace FriendsTracker.Logic
{
    public class FriendsFromFileReader
    {
        const string xmlFilePath = "C:/Users/Denys/Desktop/FriendsTracker/FriendsXMLFile.xml";
        const int defaultAge = 0;
        const string defaultName = "";
        const string userTag = "user";
        const string nameTag = "name";
        const string ageTag = "age";
        public List<Friend> ReadXML()
        {
            var friends = new List<Friend>();
            var xDoc = new XmlDocument();
            xDoc.Load(xmlFilePath);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                var name = defaultName;
                int age = defaultAge;
                foreach (XmlNode childnode in xnode.ChildNodes) if (xnode.Name == userTag)
                {
                    if (childnode.Name == nameTag)
                    {
                        name = childnode.InnerText;
                    }
                    if (childnode.Name == ageTag)
                    {
                        age = Convert.ToInt32(childnode.InnerText);
                    }
                }
                friends.Add(new Friend() { Name = name, Age = age });
            }
            return friends;
        }
    }
}
