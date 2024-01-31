using MatriculaApiViewModel;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;
using System.Reflection.PortableExecutable;

namespace RegistrationService
{
	public static class RegistrationService
	{
		public static DataViewModel pdfText(string filePath)
		{
			StringBuilder text = new StringBuilder();

			PdfReader pdfReader = new PdfReader(filePath);
			PdfDocument pdfDocument = new PdfDocument(pdfReader);

			for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
			{
				ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
				string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page), strategy);
				text.Append(pageText);
			}
			var result = removeMistakes(text.ToString());

			return result;
		}

		public static DataViewModel removeMistakes(string text)
		{
			DataViewModel DataviewModel = new DataViewModel();
			string regex = @"\b(?:11|21)\b";

			DataviewModel.StudentIdentification = Regex.Matches(text, regex);

			return DataviewModel;
		}
	}
}
