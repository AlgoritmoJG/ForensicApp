using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;  // Para PowerPoint
using P = DocumentFormat.OpenXml.Presentation; // Alias para Presentation
using D = DocumentFormat.OpenXml.Drawing;    // Alias para Drawing (necesario si añades formas con texto)
using System; // Necesario para String, etc.
// using System.Collections.Generic; // Si usas colecciones genéricas
// using System.Linq; // Si usas LINQ

namespace ForensicApp
{
    // Puedes añadir este método a la clase GeneradorDocumentos que ya tienes,
    // o crear una nueva clase si lo prefieres.
    // Aquí lo añado a la clase GeneradorDocumentos.
    internal static partial class GeneradorDocumentos // Usando 'partial class' si está en otro archivo
    {
        // El método ahora es estático y pertenece a la clase GeneradorDocumentos
        public static void GenerarPresentacionPowerPoint(string filepath, string tituloSlide1, string[] puntosSlide1)
        {
            using (PresentationDocument presentationDocument = PresentationDocument.Create(filepath, PresentationDocumentType.Presentation))
            {
                PresentationPart presentationPart = presentationDocument.AddPresentationPart();
                presentationPart.Presentation = new P.Presentation();

                // Crear Slide Master Part (mínimo)
                SlideMasterPart slideMasterPart = presentationPart.AddNewPart<SlideMasterPart>("rId1");
                slideMasterPart.SlideMaster = new P.SlideMaster(
                    new P.CommonSlideData(new P.ShapeTree()), // ShapeTree para placeholders globales si los hubiera
                    new P.ColorMap( // Definición de ColorMap
                        new D.ExtensionList(
                            new D.Extension(
                                new DocumentFormat.OpenXml.Office2013.Theme.OfficeArtExtensionList()
                            )
                            { Uri = "{05A4C25C-085E-4340-85A3-A5591D54F800}" }
                        )
                    )
                    {
                        Background1 = D.ColorSchemeIndexValues.Light1,
                        Text1 = D.ColorSchemeIndexValues.Dark1,
                        Background2 = D.ColorSchemeIndexValues.Light2,
                        Text2 = D.ColorSchemeIndexValues.Dark2,
                        Accent1 = D.ColorSchemeIndexValues.Accent1,
                        Accent2 = D.ColorSchemeIndexValues.Accent2,
                        Accent3 = D.ColorSchemeIndexValues.Accent3,
                        Accent4 = D.ColorSchemeIndexValues.Accent4,
                        Accent5 = D.ColorSchemeIndexValues.Accent5,
                        Accent6 = D.ColorSchemeIndexValues.Accent6,
                        Hyperlink = D.ColorSchemeIndexValues.Hyperlink,
                        FollowedHyperlink = D.ColorSchemeIndexValues.FollowedHyperlink
                    },
                    new P.SlideLayoutIdList(new P.SlideLayoutId() { Id = 2147483649U, RelationshipId = "rIdSM1" }) // Referencia a un layout
                );
                slideMasterPart.SlideMaster.Append(new P.TextStyles(new P.TitleStyle(), new P.BodyStyle(), new P.OtherStyle()));


                // Crear Slide Layout Part (mínimo, asociado al Slide Master)
                SlideLayoutPart slideLayoutPart = slideMasterPart.AddNewPart<SlideLayoutPart>("rIdSM1"); // rId debe coincidir con SlideLayoutId en SlideMaster
                slideLayoutPart.SlideLayout = new P.SlideLayout(
                    new P.CommonSlideData(new P.ShapeTree()), // ShapeTree para placeholders del layout
                     new P.ColorMapOverride(new D.MasterColorMapping()), // Usar el ColorMap del Master
                    new P.ExtensionListWithModification(
                        new P.Extension(
                            new DocumentFormat.OpenXml.Office2013.PowerPoint.SlideGuideList()
                        )
                        { Uri = "{BEA40315-A254-4F27-8805-07224C207340}" }
                    )
                );
                slideLayoutPart.SlideLayout.Type = P.SlideLayoutValues.Blank; // Ejemplo: layout en blanco

                // Crear una diapositiva
                SlidePart slidePart = presentationPart.AddNewPart<SlidePart>("rId2"); // rId único para la relación de esta diapositiva
                slidePart.Slide = new P.Slide(
                    new P.CommonSlideData(new P.ShapeTree()), // ShapeTree para el contenido de la diapositiva
                    new P.ColorMapOverride(new D.MasterColorMapping()), // Usar el ColorMap del Master
                     new P.Transition(new P.CutTransition()) { Duration = "100" } // Transición simple
                );
                slidePart.CreateRelationshipToPart(slideLayoutPart); // Relacionar la diapositiva con su layout

                // Añadir la diapositiva a la lista de diapositivas de la presentación
                if (presentationPart.Presentation.SlideIdList == null)
                    presentationPart.Presentation.SlideIdList = new P.SlideIdList();

                uint slideIdCounter = 256U; // Los IDs de diapositiva suelen empezar en 256
                presentationPart.Presentation.SlideIdList.AppendChild(new P.SlideId() { Id = slideIdCounter, RelationshipId = presentationPart.GetIdOfPart(slidePart) });

                // --- Añadir contenido a la diapositiva (slidePart.Slide.CommonSlideData.ShapeTree) ---
                // Esto sigue siendo la parte más compleja y específica.
                // El siguiente es un ejemplo MUY BÁSICO para añadir un cuadro de texto con el título.
                // Necesitarás definir posiciones, tamaños, fuentes, etc.

                P.ShapeTree currentShapeTree = slidePart.Slide.CommonSlideData.ShapeTree;

                // Crear una forma para el título
                P.Shape titleShape = currentShapeTree.AppendChild(new P.Shape());

                // Propiedades no visuales de la forma (ID, nombre, etc.)
                titleShape.NonVisualShapeProperties = new P.NonVisualShapeProperties(
                    new P.NonVisualDrawingProperties() { Id = 2U, Name = "Title Placeholder" },
                    new P.NonVisualShapeDrawingProperties(new D.ShapeLocks() { NoGrouping = true }),
                    new P.ApplicationNonVisualDrawingProperties(new P.PlaceholderShape() { Type = P.PlaceholderValues.Title })
                );

                // Propiedades visuales de la forma (geometría, relleno, etc.)
                titleShape.ShapeProperties = new P.ShapeProperties(
                    new D.Transform2D( // Posición y tamaño (en EMUs - English Metric Units)
                        new D.Offset() { X = 914400L, Y = 457200L }, // Ejemplo: 1 pulgada desde arriba e izquierda
                        new D.Extents() { Cx = 7315200L, Cy = 1143000L }  // Ejemplo: 8 pulgadas de ancho, 1.25 pulgadas de alto
                    ),
                    new D.PresetGeometry(new D.AdjustValueList()) { Preset = D.ShapeTypeValues.Rectangle } // Forma rectangular
                );

                // Cuerpo del texto
                titleShape.TextBody = new P.TextBody(
                    new D.BodyProperties(), // Propiedades del cuerpo del texto
                    new D.ListStyle(),      // Estilo de lista (ninguno en este caso)
                    new D.Paragraph(        // Párrafo
                        new D.Run(          // Segmento de texto con formato uniforme
                            new D.RunProperties() { Language = "es-ES", FontSize = 3200, Bold = true }, // Español, tamaño 32pt, negrita
                            new D.Text(tituloSlide1) // El texto del título
                        )
                    )
                );

                // Aquí añadirías más formas para los 'puntosSlide1', cada uno con su TextBody, Paragraph, Run, Text.

                // Guardar la presentación
                presentationPart.Presentation.Save();
            }
        }
    }
}