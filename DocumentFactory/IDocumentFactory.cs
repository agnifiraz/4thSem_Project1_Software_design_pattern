using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace DocumentFactory
{
    public interface IDocument
    {
        void RunDocument();
        void AddElement(IElement newElement);
    }
    public interface IElement { }
    public interface IDocumentFactory
    {
        IDocument CreateDocument(string fileName);
        IElement CreateElement(string elementType, string props);
    }

    public class HTMLElement : IElement { }
    public class HTMLHeaderElement : HTMLElement
    {
        private string theData;
        public HTMLHeaderElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');
            return $"<h{temp[0]}>" + temp[1] + $"</h{temp[0]}>" + "\n";
        }
    }

    public class HTMLImageElement : HTMLElement
    {
        private string theData;
        public HTMLImageElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');
            return "<img src=\""+  temp[0] + "\" alt=\"" + temp[1] + "\" title=\"" + temp[2] + "\">";
            
        }
    }

    public class HTMLListElement : HTMLElement
    {
        private string theData;
        public HTMLListElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');

            //return "<img src=\"" + temp[0] + "\" alt=\"" + temp[1] + "\" title=\"" + temp[2] + "\">";
            if (temp[0]== "Ordered")
            {
                return "<ol>"+ "<li>" + temp[1]+"</li>" + "<li>" + temp[2] + "</li>"+ "<li>" + temp[3] + "</li></ol>";
            }
            else
            {
                return "<ul>"+ "<li>" + temp[1] + "</li>" + "<li>" + temp[2] + "</li>" + "<li>" + temp[3] + "</li></ul>";
            }
        }
    }
    public class HTMLTableElement : HTMLElement
    {
        private string theData;
        public HTMLTableElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');
            String allInfo = "";
            foreach (var command in temp)
            {
                var tableinfo = command.Split('$');
                if (tableinfo[0] == "Head")
                {
                    allInfo = allInfo + $"<thead><tr><th> {tableinfo[1]} </th><th> {tableinfo[2]} </th><th> {tableinfo[3]} </th></tr></thead><tbody>";
                }
                else
                {
                    allInfo = allInfo + $"<tr><td> {tableinfo[1]} </td><td> {tableinfo[2]} </td><td> {tableinfo[3]} </td></tr>";
                }
            }
            return "<table>" +allInfo + "</tbody></ table >";
        }
    }
    public class HTMLFactory : IDocumentFactory
    {
        public static HTMLFactory htmlFactory;
        HTMLDocument htmlDocument;
        public IDocument CreateDocument(string fileName)
        {
            htmlDocument = new HTMLDocument(fileName);
            var fileCreation = File.Create(fileName);
            fileCreation.Close();
            return htmlDocument;
        }
        public IElement CreateElement(string elementType, string data)
        {
            IElement element = null;
            switch (elementType)
            {
                case "Header":
                    element = new HTMLHeaderElement(data);
                    break;
                case "Image":
                    element = new HTMLImageElement(data);
                    break;
                case "List":
                    element = new HTMLListElement(data);
                    break;
                case "Table":
                    element = new HTMLTableElement(data);
                    break;
                default:
                    break;
            //break;
            }
            htmlDocument.AddElement(element);
            return element;
        }

        private HTMLFactory()
        {

        }

        public static HTMLFactory GetInstance()
        {
            if (htmlFactory == null)
            {
                htmlFactory = new HTMLFactory();
            }
            return htmlFactory;
        }
    }
    public class HTMLDocument : IDocument
    {
        public static HTMLDocument htmlDocument;
        private string fileName;
        List<HTMLElement> content = new List<HTMLElement>();
        string allContent = "";
        public HTMLDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public HTMLDocument()
        {
        }

        public void AddElement(IElement newElement)
        {
            var tempEl = (HTMLElement)newElement;
            content.Add(tempEl);
            for (int i = content.Count-1; i < content.Count; i++)
            {
                allContent = allContent + content[i].ToString() + "\n";
            }

            File.WriteAllText(fileName, allContent);
        }
        public void RunDocument()
        {
            System.Diagnostics.Process.Start($"{Directory.GetCurrentDirectory()}\\{fileName}", "chrome.exe");
        }
        public static HTMLDocument GetInstance()
        {
            if (htmlDocument == null)
            {
                htmlDocument = new HTMLDocument();
            }
            return htmlDocument;
        }
    }
    public class MarkDownElement : IElement { }
    public class MarkDownHeaderElement : MarkDownElement
    {
        private string theData;
        public MarkDownHeaderElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');
            string val = "";
            int intValue = int.Parse(temp[0]);
            for (int i=0; i < intValue;i++)
            {
                val=val + "#";
            }
            return  val+ " "+ temp[1] + "\n" ;
        }
    }

    public class MarkDownImageElement : MarkDownElement
    {
        private string theData;
        public MarkDownImageElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');
            return $"![{temp[1]}]({temp[0]} \"{temp[2]}\")" + "\n";

        }
    }

    public class MarkDownListElement : MarkDownElement
    {
        private string theData;
        public MarkDownListElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');

            if (temp[0] == "Ordered")
            {
                return "1. " + temp[1] + "\n" + "2. " + temp[2] + "\n" + "3. " + temp[3] + "\n";
            }
            else
            {
                return "* " + temp[1] + "\n"+ "* " + temp[2] + "\n" + "* " + temp[3] + "\n";
            }
        }
    }
    public class MarkDownTableElement : MarkDownElement
    {
        private string theData;
        public MarkDownTableElement(string tempData)
        {
            theData = tempData;
        }
        public override string ToString()
        {
            var temp = theData.Split(';');
            String allInfo = "";
            foreach (var command in temp)
            {
                var tableinfo = command.Split('$');
                if (tableinfo[0] == "Head")
                {
                    allInfo = allInfo + "| " + tableinfo[1]+ " | " + tableinfo[2] + " | " +tableinfo[3] + " | " + "\n" + "| --- | --- | --- |" +"\n";
                }
                else
                {
                    allInfo = allInfo + $"| {tableinfo[1]} | {tableinfo[2]} | {tableinfo[3]} | \n";
                }
            }
            return  allInfo;
        }
    }
    public class MarkDownFactory : IDocumentFactory
    {
        public static MarkDownFactory markDownFactory;
        MarkDownDocument markDownDocument;
        public IDocument CreateDocument(string fileName)
        {
            markDownDocument = new MarkDownDocument(fileName);
            var fileCreation = File.Create(fileName);
            fileCreation.Close();

            return markDownDocument;
        }
        public IElement CreateElement(string elementType, string data)
        {
            IElement element = null;

            switch (elementType)
            {
                case "Header":
                    element = new MarkDownHeaderElement(data);
                    break;
                case "Image":
                    element = new MarkDownImageElement(data);
                    break;
                case "List":
                    element = new MarkDownListElement(data);
                    break;
                case "Table":
                    element = new MarkDownTableElement(data);
                    break;
                default:
                    break;
            }
            markDownDocument.AddElement(element);
            return element;
        }

        private MarkDownFactory()
        {

        }

        public static MarkDownFactory GetInstance()
        {
            if (markDownFactory == null)
            {
                markDownFactory = new MarkDownFactory();
            }
            return markDownFactory;
        }
    }
    public class MarkDownDocument : IDocument
    {
        public static MarkDownDocument markDownDocument;
        private string fileName;
        List<MarkDownElement> content = new List<MarkDownElement>();
        string allContent = "";
        public MarkDownDocument(string fileName)
        {
            this.fileName = fileName;
        }
        public MarkDownDocument() { }

        public void RunDocument()
        {
            
            System.Diagnostics.Process.Start($"{Directory.GetCurrentDirectory()}\\{fileName}", "chrome.exe");
        }
        public void AddElement(IElement newElement)
        {
            //var tempData = (MarkDownElement)newElement;
            content.Add((MarkDownElement)newElement);
            for (int i = content.Count-1; i < content.Count; i++)
            {
                allContent = allContent + content[i].ToString() + "\n";
            }

            File.WriteAllText(fileName, allContent);

        }

        public static MarkDownDocument GetInstance()
        {
            if(markDownDocument == null)
            {
                markDownDocument = new MarkDownDocument();
            }
            return markDownDocument;
        }
    }
}













