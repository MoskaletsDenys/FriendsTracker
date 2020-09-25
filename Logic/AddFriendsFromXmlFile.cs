using FriendsTracker.Models;
using System.Xml;
using System;
using System.Collections.Generic;

namespace FriendsTracker.Logic
{
    public static class AddFriendsFromXmlFile
    {
        public static IEnumerable<Friend> ReadFromXML()
        {
            List<Friend> friends = new List<Friend>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:/Users/Denys/Documents/GitHub/FriendsTracker/FriendsXMLFile.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                int age = 0;
                var name = "";
                foreach (XmlNode childnode in xnode.ChildNodes) if (xnode.Name == "user")
                {
                    if (childnode.Name == "name")
                    {
                        name = childnode.InnerText;
                    }
                    if (childnode.Name == "age")
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
