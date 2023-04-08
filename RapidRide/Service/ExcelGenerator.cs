using OfficeOpenXml.Style;
using OfficeOpenXml;
using RapidRide.Entities;

namespace RapidRide.Service
{

    public class ExcelGenerator
    {

        public static byte[] GenerateRechargeCardsInvoiceExcel(List<RechargeCard> rechargeCards)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Recharge Cards Invoice");

            // Add title
            worksheet.Cells[1, 1].Value = "Recharge Cards Invoice";
            worksheet.Cells[1, 1, 1, 4].Merge = true;
            worksheet.Cells[1, 1].Style.Font.Size = 18;
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Add table headers
            worksheet.Cells[2, 1].Value = "Id";
            worksheet.Cells[2, 2].Value = "Category";
            worksheet.Cells[2, 3].Value = "Number";
            worksheet.Cells[2, 4].Value = "Date";
            worksheet.Cells[2, 1, 2, 4].Style.Font.Bold = true;

            // Add data rows
            int rowIndex = 3;
            foreach (var rechargeCard in rechargeCards)
            {
                worksheet.Cells[rowIndex, 1].Value = rechargeCard.Id;
                worksheet.Cells[rowIndex, 2].Value = rechargeCard.Category;
                worksheet.Cells[rowIndex, 3].Value = rechargeCard.Number;
                worksheet.Cells[rowIndex, 4].Value = rechargeCard.Date;
                worksheet.Cells[rowIndex, 4].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                rowIndex++;
            }

            // Autofit columns
            worksheet.Cells[1, 1, rowIndex - 1, 4].AutoFitColumns();

            return package.GetAsByteArray();
        }


    }
}
