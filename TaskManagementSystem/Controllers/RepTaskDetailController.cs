using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace TaskManagementSystem.Controllers
{
    public class RepTaskDetailController : Controller
    {
        // Easyfis data context
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        public ActionResult TaskDetailController(Int32 taskId)
        {
            // PDF settings
            MemoryStream workStream = new MemoryStream();
            Rectangle rectangle = new Rectangle(PageSize.A3);
            Document document = new Document(rectangle, 72, 72, 72, 72);
            document.SetMargins(30f, 30f, 30f, 30f);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            Image logo = Image.GetInstance("http://localhost:57369/images/callTicketHeader.png");
            logo.ScalePercent(70f);


            // Document Starts
            document.Open();

            // Fonts Customization
            Font fontArial17Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
            Font fontArial11Bold = FontFactory.GetFont("Arial", 11, Font.BOLD);
            Font fontArial11 = FontFactory.GetFont("Arial", 11);
            Font fontArial9 = FontFactory.GetFont("Arial", 9);
            Font fontArial10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);

            // line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

            //Header
            Paragraph header = new Paragraph("Innosoft Solution Inc.");
            header.Alignment = Element.ALIGN_JUSTIFIED;
            
            var tasks = from d in db.trnTasks
                        where d.Id == taskId
                        select d;

            var staff = from s in db.mstStaffs
                        where s.Id == tasks.FirstOrDefault().StaffId
                        select s;

            var verifiedBy = from v in db.mstStaffs
                        where v.Id == tasks.FirstOrDefault().VerifiedBy
                        select v;

            var action = from a in db.trnTaskSubs
                         where a.TaskId == taskId
                         select a;

            var product = from p in db.mstProducts
                          where p.Id == tasks.FirstOrDefault().ProductId
                          select p;

            var client = from c in db.mstClients
                         where c.Id == tasks.FirstOrDefault().ClientId
                         select c;


            //tableHeaderPage.AddCell(new PdfPCell(new Phrase("Printed " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt"), fontArial11)) { Border = 0, PaddingTop = 5f, HorizontalAlignment = 2 });
            PdfPTable table = new PdfPTable(4);
            float[] widthsCellsheaderPage2 = new float[] { 35f, 100f, 35f, 100f };
            table.SetWidths(widthsCellsheaderPage2);
            PdfPCell cell = new PdfPCell(new Phrase("Call Ticket", fontArial17Bold)) { HorizontalAlignment = 2 };
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            table.AddCell("Date");
            table.AddCell(tasks.FirstOrDefault().TaskDate.ToShortDateString());
            table.AddCell("Call Date");
            table.AddCell(tasks.FirstOrDefault().TaskDate.ToShortDateString());
            table.AddCell("Staff");
            table.AddCell(staff.FirstOrDefault().StaffName.ToString());
            table.AddCell("Client");
            table.AddCell(client.FirstOrDefault().CompanyName.ToString());

            
            table.AddCell("Action");
            if (action.Any())
            {
                table.AddCell(action.FirstOrDefault().Action.ToString()); 
            }
            else { table.AddCell("NA"); }

                table.AddCell("Issue");
            table.AddCell(tasks.FirstOrDefault().Concern.ToString());
            PdfPCell remarksCell = new PdfPCell(new Phrase("Remarks"));
            remarksCell.Rowspan = 2;
            table.AddCell(remarksCell);

            PdfPCell remarksSpaceCell = new PdfPCell(new Phrase(tasks.FirstOrDefault().Remarks.ToString()));
            remarksSpaceCell.Rowspan = 2;
            table.AddCell(remarksSpaceCell);

            table.AddCell("Product");
            table.AddCell(product.FirstOrDefault().ProductCode.ToString());
            table.AddCell("Cost");
            table.AddCell(" ");

            table.AddCell("Date");
            table.AddCell("Time In:");
            table.AddCell("Time Out:");
            table.AddCell("Verified By:");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(verifiedBy.FirstOrDefault().StaffName.ToString());
            remarksCell.Padding = 10f;

            document.Add(logo);
            //document.Add(header);
            document.Add(line);
            document.Add(Chunk.NEWLINE);
            document.Add(table);


            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }


    }

    
}