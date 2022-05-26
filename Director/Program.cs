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
            //int count = 0;
            //string doctype = "";
            string[] commands;
            var list = File.ReadAllText("CreateDocumentScript.txt");
            commands = list.Split('#');
            bool htmlelement = false;
            bool markelement= false;
            foreach (var command in commands)
            {
                var strippedCommand = Regex.Replace(command, @"\t|\n|\r", "");
                var commandList = strippedCommand.Split(':');

                switch (commandList[0])
                {
                    case "Document":
                        var oneWord = commandList[1].Split(';');
                        if (oneWord[0] == "Markdown")
                        {
                            htmlelement = false;
                            markelement = true;
                            MarkDownFactory.GetInstance();
                            MarkDownFactory.markDownFactory.CreateDocument(oneWord[1]);
                        }
                        else if (oneWord[0] == "Html")
                        {
                            htmlelement = true;
                            markelement = false;
                            HTMLFactory.GetInstance();
                            HTMLFactory.htmlFactory.CreateDocument(oneWord[1]);
                        }

                        break;
                    case "Element":
                       
                            if (markelement == true)
                            {
                                MarkDownFactory.markDownFactory.CreateElement(commandList[1], commandList[2]);

                            }
                            else if (htmlelement == true)
                            {
                                HTMLFactory.htmlFactory.CreateElement(commandList[1], commandList[2]);
                            }
                       
                        break;
                    case "Run":
                        if (markelement == true)
                        {
                            //wwwww
                        }
                        else if (htmlelement == true)
                        {
                            HTMLDocument.GetInstance();
                            HTMLDocument.htmlDocument.RunDocument();
                        }
                        break;
                    default:
                        break;
                }          
            }
        }
    }
}