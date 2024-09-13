using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Mvc;
using Porto.Models;
using Porto.Models.Enums;
using Porto.Repositorio;
using System.Text;


namespace Porto.Controllers
{
    public class Reports : Controller
    {
        public IMovementsRepository _repository;

        public Reports(IMovementsRepository repository)
        {
            _repository = repository;
        }

        public ActionResult MovementList()
        {
            var movementsList = _repository.ListAll();

            return View("MovementsList", movementsList);
        }

        public FileResult PrintReport()
        {
            var movementsList = _repository.ListAll().GroupBy(x => x.Container?.Client?.Name);

            StringBuilder sb = new StringBuilder();

            sb.Append("<span>");
            sb.Append("Total de Movimentações Agrupadas por Clientes: ");
            sb.Append("</span>");

            sb.Append("<table border='1' cellpadding='8' cellspacing='0' style='width: 100%; border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            sb.Append("<tr>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Cliente</th>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Quantidade</th>");
            sb.Append("</tr>");

            foreach (var item in movementsList)
            {
                sb.Append("<tr>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(item.Key);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(item.Count());
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            sb.Append("<br/>");
            sb.Append("<br/>");

            var movementsTypeList = _repository.ListAll().GroupBy(x => x.MovementType);

            sb.Append("<span>");
            sb.Append("Total de Movimentações Agrupadas por Tipo de Movimentação: ");
            sb.Append("</span>");

            sb.Append("<table border='1' cellpadding='8' cellspacing='0' style='width: 100%; border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            sb.Append("<tr>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Tipo Movimentação</th>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Quantidade</th>");
            sb.Append("</tr>");

            foreach (var item in movementsTypeList)
            {
                sb.Append("<tr>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(item.Key);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(item.Count());
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            using (MemoryStream stream = new MemoryStream())
            {
                StringReader sr = new StringReader(sb.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 10f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.HtmlStyleClass = "display: flex; align-items: center;";
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Relatorio.pdf");
            }
        }
    }
}
