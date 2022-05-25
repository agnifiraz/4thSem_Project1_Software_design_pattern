using DocumentFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Director
{
    class Program
    {

        static void Main(string[] args)
        {
            int count = 0;
            string doctype = "";
            string[] commands;
            var list = File.ReadAllText("Test.txt");
            commands = list.Split('#');

            foreach (var command in commands)
            {
                count++;
                var strippedCommand = Regex.Replace(command, @"\t|\n|\r", "");
                var commandList = strippedCommand.Split(':', ';');
                if (count == 1)
                {
                    doctype = commandList[1];
                }
                switch (doctype)
                {
                    case "Markdown":
                        switch (commandList[0])
                        {
                            case "Document":
                                MarkDownFactory.GetInstance();
                                MarkDownFactory.markDownFactory.CreateDocument(commandList[2]);
                                break;
                            case "Element":
                                //MarkDownFactory.GetInstance();
                                //MarkDownFactory.markDownFactory.CreateElement(commandList[1], commandList[3]);
                                break;
                            case "Run":
                                count = 0;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Html":
                        switch (commandList[0])
                        {
                            case "Document":
                                HTMLFactory.GetInstance();
                                HTMLFactory.htmlFactory.CreateDocument(commandList[2]);
                                break;
                            case "Element":
                                //HTMLFactory.GetInstance();
                                HTMLFactory.htmlFactory.CreateElement(commandList[1], commandList[3]);
                                break;
                            case "Run":
                                HTMLDocument.GetInstance();
                                HTMLDocument.htmlDocument.RunDocument();
                                count = 0;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Director
//{
//    class Program
//    {

//        static void Main(string[] args)
//        {
//            string[] commands;
//            var list = File.ReadAllText("CreateDocumentScript.txt");
//            commands = list.Split('#');

//            foreach (var command in commands)
//            {
//                var strippedCommand = Regex.Replace(command, @"\t|\n|\r", "");
//                var commandList = strippedCommand.Split(':');
//                switch (commandList[0])
//                {
//                    case "Document":
//                        // Your document creation code goes here
//                        break;
//                    case "Element":
//                        // Your element creation code goes here
//                        break;
//                    case "Run":
//                        // Your document running code goes here
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//    }
//}
