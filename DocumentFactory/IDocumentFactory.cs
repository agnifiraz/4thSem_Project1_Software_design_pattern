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
            int i = 1;
            var temp = theData.Split(';');
            return $"<h{i}>" + temp[1] + $"</h{i}>";
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
            return "Image";
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
            return "List";
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
            return "Table";
        }
    }
    public class HTMLFactory : IDocumentFactory
    {
        public static HTMLFactory htmlFactory;
        HTMLDocument htmlDocument;
        public IDocument CreateDocument(string fileName)
        {
            htmlDocument = new HTMLDocument(fileName);
            File.Create(fileName);
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
        }
        public void RunDocument()
        {
            for (int i = 0; i < content.Count; i++)
            {
                allContent = allContent + content[i].ToString() + "\n";
            }
            File.WriteAllText(fileName, allContent);
            System.Diagnostics.Process.Start("chrome.exe", fileName);
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
            return $"<h{temp[0]}>" + theData[1] + $"</h{temp[0]}>";
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
            return "Image";
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
            return "List";
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
            return "Table";
        }
    }
    public class MarkDownFactory : IDocumentFactory
    {
        public static MarkDownFactory markDownFactory;
        MarkDownDocument markDownDocument;
        public IDocument CreateDocument(string fileName)
        {
            markDownDocument = new MarkDownDocument(fileName);
            File.Create(fileName);
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
        private string fileName;
        List<MarkDownElement> content = new List<MarkDownElement>();
        string allContent = "";
        public MarkDownDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public void RunDocument()
        {
            for (int i = 0; i < content.Count; i++)
            {
                allContent = allContent + content[i].ToString() + "\n";
            }
            File.WriteAllText(fileName, allContent);
            System.Diagnostics.Process.Start("chrome.exe", fileName);
        }
        public void AddElement(IElement newElement)
        {
            //var tempData = (MarkDownElement)newElement;
            content.Add((MarkDownElement)newElement);
        }
    }
}













