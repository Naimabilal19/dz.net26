using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp26
{
    class Order
    {
        public string order { get; set; }
        public int count { get; set; }
        public string money { get; set; }

        static void Main(string[] args)
        {
            Order o = new Order();
            o.order = "canon";
            o.count = 2;
            o.money = "12 000";
            string m = Convert.ToString(o.count);
            XmlTextWriter xmlwriter = new XmlTextWriter("../../Orders.xml", Encoding.UTF8);
            xmlwriter.WriteStartDocument();
            xmlwriter.Formatting = Formatting.Indented; 
            xmlwriter.IndentChar = '\t'; 
            xmlwriter.Indentation = 1; 

            xmlwriter.WriteStartElement("Заказ 1");

            xmlwriter.WriteStartAttribute("Ваш заказ:" , null); 
            xmlwriter.WriteString(o.order); 
            xmlwriter.WriteEndAttribute();

            xmlwriter.WriteStartAttribute("Количество:", null);
            xmlwriter.WriteString(m);
            xmlwriter.WriteEndAttribute();

            xmlwriter.WriteStartAttribute("Cумма:", null);
            xmlwriter.WriteString(o.money);
            xmlwriter.WriteEndAttribute();

            xmlwriter.WriteEndElement();

            Console.WriteLine("Данные сохранены в XML-файл");
            xmlwriter.Close();



            XmlTextReader reader = new XmlTextReader("../../Orders.xml");
            string str = null;
            while (reader.Read()) 
            {
                if (reader.NodeType == XmlNodeType.Text)
                    str += reader.Value + "\n";

                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.HasAttributes)
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            str += reader.Value + "\n";
                        }
                    }
                }
            }
            Console.WriteLine(str);
            reader.Close();



            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("../../Orders.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("Заказ №1");
                    if (attr != null)
                        Console.WriteLine(attr.Value);
                }
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "Ваш заказ:")
                    {
                        Console.WriteLine("Заказ: {0}", childnode.FirstChild.Value);
                    }
                    if (childnode.Name == "Количество:")
                    {
                        Console.WriteLine("Количество: {0}", childnode.InnerText);
                    }
                    if (childnode.Name == "Cумма:")
                    {
                        Console.WriteLine("Сумма: {0}", childnode.InnerText);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
