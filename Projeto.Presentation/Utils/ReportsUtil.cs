using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


namespace Projeto.Presentation.Utils
{
    public class ReportsUtil
    {
        public byte[] GetPDF(string conteudo)
        {
            byte[] pdf = null;
            MemoryStream ms = new MemoryStream();
            TextReader reader = new StringReader(conteudo);
            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            HTMLWorker html = new HTMLWorker(doc);
            doc.Open();
            html.StartDocument();
            html.Parse(reader);
            html.EndDocument();
            html.Close();
            doc.Close();
            pdf = ms.ToArray();
            return pdf;
        }
    }
}