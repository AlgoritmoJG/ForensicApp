// Asegúrate de tener las directivas using necesarias para Open XML SDK
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing; // Para Word
// using DocumentFormat.OpenXml.Presentation; // Para PowerPoint si está en la misma clase
// using P = DocumentFormat.OpenXml.Presentation;
// using D = DocumentFormat.OpenXml.Drawing;
using System; // Para StringSplitOptions, etc.

namespace ForensicApp // Debe estar en el mismo namespace que MainForm
{
    internal static class GeneradorDocumentosa // Puede ser 'internal class' si prefieres instanciarla
    {
        public static void GenerarDocumentoWord(string filepath, string contenidoPrincipal)
        {
            // Crear el documento
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Añadir un título (ejemplo)
                Paragraph titlePara = body.AppendChild(new Paragraph());
                Run titleRun = titlePara.AppendChild(new Run());
                titleRun.AppendChild(new Text("Informe Técnico Forense"));
                // Aplicar algunos estilos básicos al título
                RunProperties titleRunProperties = titleRun.PrependChild(new RunProperties());
                titleRunProperties.Bold = new Bold();
                titleRunProperties.FontSize = new FontSize() { Val = "28" }; // Tamaño 14pt (28 / 2)


                // Añadir el contenido principal, manejando saltos de línea
                if (!string.IsNullOrEmpty(contenidoPrincipal))
                {
                    string[] lineasContenido = contenidoPrincipal.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    foreach (string linea in lineasContenido)
                    {
                        Paragraph contentPara = body.AppendChild(new Paragraph());
                        Run contentRun = contentPara.AppendChild(new Run());
                        contentRun.AppendChild(new Text(linea));
                    }
                }

                mainPart.Document.Save();
            }
        }

        // Aquí iría el método GenerarPresentacionPowerPoint si lo pones en la misma clase
        // public static void GenerarPresentacionPowerPoint(string filepath, string tituloSlide1, string[] puntosSlide1)
        // {
        //     // ... tu código de PowerPoint ...
        // }
    }
}