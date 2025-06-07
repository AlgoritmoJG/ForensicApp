using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForensicApp
{
    public partial class MainForm : Form
    {


        // --- Configuración ---
        private const string GroqApiKey = "";
        private const string GroqApiUrl = "";

        // Reemplaza esto con tu cadena de conexión a SQL Server
        public static string SqlConnectionString = "Server=MSI\\SQLEXPRESS;Database=InvestigacionDigitalApp;Trusted_Connection=True;TrustServerCertificate=True;";        // Ejemplo para LocalDB:
        // private const st
        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }
        private void InitializeCustomComponents()
        {


            // --- Poblar ComboBox de Tipos de Sistema ---
            if (this.Controls.ContainsKey("cmbTipoSistemaa")) // Verifica si el control existe
            {
                // var cmbTipoSistemaControl = this.Controls["cmbTipoSistema"] as ComboBox; // Si no está declarado como campo de clase
                // Si cmbTipoSistema ya es un campo de la clase (declarado en el designer.cs o manualmente):
                if (cmbNaturalezaIncidente != null)
                {
                    cmbTipoSistemaa.Items.Clear(); // Limpiar items por si acaso
                    cmbTipoSistemaa.Items.AddRange(new object[] {
                "Windows",
                "Linux (Servidor)",
                "Linux (Escritorio)",
                "macOS",
                "Android",
                "iOS",
                "Servidor (Otro)",
                "Dispositivo IoT",
                "Otro"
            });

                    // Opcional: Seleccionar un item por defecto
                    if (cmbTipoSistemaa.Items.Count > 0)
                    {
                        cmbTipoSistemaa.SelectedIndex = 0; // Selecciona el primer ítem ("Windows")
                    }

                    // Opcional: Hacer que el ComboBox no sea editable (solo selección de la lista)
                    cmbTipoSistemaa.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            // --- Poblar ComboBox de Naturaleza del Incidente ---
            if (this.Controls.ContainsKey("cmbNaturalezaIncidente")) // Verifica si el control existe
            {
                // Si cmbNaturalezaIncidente ya es un campo de la clase (declarado en el designer.cs o manualmente):
                if (cmbNaturalezaIncidente != null)
                {
                    cmbNaturalezaIncidente.Items.Clear(); // Limpiar items
                    cmbNaturalezaIncidente.Items.AddRange(new object[] {
                "Acceso No Autorizado",
                "Malware / Ransomware",
                "Phishing / Ingeniería Social",
                "Fuga de Datos / Exfiltración de Información",
                "Fraude Electrónico",
                "Ataque de Denegación de Servicio (DoS/DDoS)",
                "Uso Indebido de Recursos TI",
                "Compromiso de Correo Electrónico Empresarial (BEC)",
                "Robo de Identidad",
                "Espionaje Industrial",
                "Alteración o Destrucción de Datos",
                "Incidente de Dispositivo Móvil",
                "Otro"
            });

                    // Opcional: Seleccionar un item por defecto
                    if (cmbNaturalezaIncidente.Items.Count > 0)
                    {
                        cmbNaturalezaIncidente.SelectedIndex = 0; // Selecciona el primer ítem
                    }

                    // Opcional: Hacer que el ComboBox no sea editable
                    cmbNaturalezaIncidente.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }

            if (this.Controls.ContainsKey("lblStatus")) // Verifica si el control existe
            {
                var lblStatusControl = this.Controls["lblStatus"] as Label;
                if (lblStatusControl != null)
                {
                    lblStatusControl.Text = "Estado: Listo";
                    lblStatusControl.ForeColor = Color.Black;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Identificar el botón que disparó el evento (si necesitas referenciarlo)
            Button clickedButton = sender as Button; // O usa el nombre directo si es button1

            // --- LECTURA DE DATOS DESDE LA UI ---
            // Asegúrate de que los nombres de control coincidan con los de tu diseñador.
            string tipoSistema = cmbTipoSistemaa.SelectedItem?.ToString() ?? "No especificado";
            string naturalezaIncidente = cmbNaturalezaIncidente.Text.Trim();
            string objetivosInvestigacion = txtObjetivosInvestigacion.Text.Trim();

            if (string.IsNullOrEmpty(naturalezaIncidente) || string.IsNullOrEmpty(objetivosInvestigacion))
            {
                MessageBox.Show("Por favor, complete los campos 'Naturaleza del Incidente' y 'Objetivos de la Investigación'.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- ACTUALIZACIÓN DE LA UI (ANTES DE LLAMADAS LARGAS) ---
            if (clickedButton != null) clickedButton.Enabled = false;
            lblStatus.Text = "Estado: Generando prompt y contactando API...";
            lblStatus.ForeColor = Color.Blue; // Un color para indicar proceso
            rtbPromptEnviado.Clear();
            rtbRespuestaAPI.Clear();
            // Application.DoEvents(); // Generalmente no es necesario con async/await para actualizaciones simples de UI

            // MessageBox.Show("Iniciando proceso...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); // Movido o eliminado para evitar interrupción

            string prompt = ConstruirPrompt(tipoSistema, naturalezaIncidente, objetivosInvestigacion);

            // --- MOSTRAR PROMPT EN LA UI ---
            rtbPromptEnviado.Text = prompt;

            try
            {
                // --- ACTUALIZACIÓN DE LA UI ---
                lblStatus.Text = "Estado: Obteniendo respuesta de la API...";
                // Application.DoEvents(); // No es necesario

                string respuestaApi = await GroqApiClient.ObtenerRespuestaGroq(prompt);

                // --- MOSTRAR RESPUESTA EN LA UI ---
                rtbRespuestaAPI.Text = respuestaApi;

                // --- ACTUALIZACIÓN DE LA UI ---
                lblStatus.Text = "Estado: Guardando en base de datos...";
                // Application.DoEvents(); // No es necesario

                await DatabaseManager.GuardarInvestigacion(tipoSistema, naturalezaIncidente, objetivosInvestigacion, prompt, respuestaApi);

                // --- ACTUALIZACIÓN DE LA UI (PROCESO COMPLETADO) ---
                lblStatus.Text = "Estado: ¡Completado! Análisis generado y guardado.";
                lblStatus.ForeColor = Color.Green;
                MessageBox.Show("¡Completado! Análisis generado y guardado.", "Proceso Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nDetalles: {ex.StackTrace}", "Error en la Operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // --- ACTUALIZACIÓN DE LA UI (ERROR) ---
                lblStatus.Text = $"Estado: Error - Consulte el mensaje anterior.";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                // --- ACTUALIZACIÓN DE LA UI (RESTAURAR ESTADO) ---
                if (clickedButton != null) clickedButton.Enabled = true;
            }
        }
        private string ConstruirPrompt(string tipoSistema, string naturalezaIncidente, string objetivosInvestigacion)
        {
            // Prompt detallado para la API de Groq (tu código original)
            return $@"Eres un asistente experto en forense digital. Basado en el siguiente escenario, genera la documentación técnica, materiales y aspectos legales relevantes.
Proporciona respuestas concisas pero completas para cada subsección.

Escenario de Investigación Forense:
- Tipo de sistema: {tipoSistema}
- Naturaleza del incidente: {naturalezaIncidente}
- Objetivos de la investigación: {objetivosInvestigacion}

Por favor, estructura tu respuesta detalladamente en las siguientes secciones principales y subsecciones:

I. DOCUMENTACIÓN TÉCNICA GENERADA:
    A. Procedimientos de adquisición de evidencia (enumera los pasos clave):
      1. [Paso 1]
      2. [Paso 2]
      ...
    B. Herramientas recomendadas (menciona al menos 2 comerciales y 2 de código abierto, con una breve descripción de su uso en este escenario):
      1. Comercial: [Nombre Herramienta] - [Descripción/Uso]
      2. Comercial: [Nombre Herramienta] - [Descripción/Uso]
      3. Código Abierto: [Nombre Herramienta] - [Descripción/Uso]
      4. Código Abierto: [Nombre Herramienta] - [Descripción/Uso]
      ...
    C. Técnicas de análisis específicas para el escenario (describe brevemente 2-3 técnicas relevantes):
      1. [Técnica 1] - [Descripción]
      2. [Técnica 2] - [Descripción]
      ...

II. MATERIALES PRODUCIDOS (describe el contenido que deberían tener):
    A. Contenido para Informe técnico en Word con metodología paso a paso (estructura sugerida):
        1. Resumen Ejecutivo
        2. Alcance y Objetivos de la Investigación
        3. Metodología Detallada (Fases: Preparación, Identificación, Adquisición, Análisis, Presentación de Hallazgos)
        4. Herramientas Utilizadas
        5. Hallazgos Clave (con referencia a evidencia)
        6. Conclusiones y Recomendaciones
        7. Apéndices (si aplica, ej: cadena de custodia, logs relevantes)
    B. Contenido y estructura para Presentación PowerPoint (sugiere diapositivas clave y su contenido, incluyendo qué tipo de diagramas o capturas serían útiles):
        1. Diapositiva Título: Caso [Naturaleza del Incidente]
        2. Diapositiva Resumen del Incidente y Objetivos
        3. Diapositiva Metodología Aplicada (puede ser un diagrama de flujo)
        4. Diapositiva Hallazgo Principal 1 (con captura de evidencia conceptual o descripción)
        5. Diapositiva Hallazgo Principal 2 (con captura de evidencia conceptual o descripción)
        6. Diapositiva Conclusiones y Pasos Siguientes
    C. Contenido para Listas de verificación para garantizar la cadena de custodia (ítems clave a verificar):
        1. Identificación única de la evidencia.
        2. Fecha y hora de recolección.
        3. Nombre y firma del recolector.
        4. Descripción detallada de la evidencia.
        5. Ubicación donde se encontró.
        6. Hash (MD5/SHA256) de la evidencia digital (si aplica).
        7. Registro de cada transferencia de custodia (quién, a quién, cuándo, por qué).
        ...

III. ASPECTOS LEGALES:
      A. Consideraciones sobre admisibilidad de evidencia (principios generales aplicables):
        1. Relevancia: La evidencia debe probar o refutar un hecho en cuestión.
        2. Autenticidad: Se debe demostrar que la evidencia es lo que dice ser.
        3. Integridad: La evidencia no ha sido alterada indebidamente.
        4. Legalidad en la obtención: La evidencia fue obtenida sin violar derechos.
        ...
      B. Requisitos jurisdiccionales relevantes (menciona aspectos generales si no se especifica jurisdicción, ej. notificación a autoridades, privacidad de datos):
        1. [Requisito general 1]
        2. [Requisito general 2]
        ...
      C. Documentación requerida para diferentes contextos legales (ej. informe pericial, declaración jurada):
        1. [Tipo de Documento Legal 1] - [Propósito/Contexto]
        2. [Tipo de Documento Legal 2] - [Propósito/Contexto]
        ...

Proporciona la información de manera clara y organizada bajo cada uno de estos puntos.
";
        }

        private void txtObjetivosInvestigacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbRespuestaAPI.Text))
            {
                MessageBox.Show("Primero debe obtener una respuesta de la API para generar el documento.",
                                "Respuesta API Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string contenidoPrincipalDelWord;
           
            if (rtbRespuestaAPI.Text.Contains("I. DOCUMENTACIÓN TÉCNICA GENERADA:"))
            {
            
                contenidoPrincipalDelWord = "Sección de Documentación Técnica:\n" +
                                            rtbRespuestaAPI.Text.Substring(rtbRespuestaAPI.Text.IndexOf("I. DOCUMENTACIÓN TÉCNICA GENERADA:"));
            }
            else
            {
                contenidoPrincipalDelWord = "Contenido del informe (parseo de ejemplo):\n" + rtbRespuestaAPI.Text;
            }
            // --- Fin del Parseo (MUY SIMPLIFICADO) ---


            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
                saveFileDialog.Title = "Guardar Informe de Word";
                saveFileDialog.FileName = "InformeForense_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx"; // Nombre de archivo sugerido con fecha

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    try
                    {
                        // Llamada al método estático de tu clase GeneradorDocumentos
                        GeneradorDocumentosa.GenerarDocumentoWord(filePath, contenidoPrincipalDelWord);
                        MessageBox.Show($"Documento de Word generado exitosamente en:\n{filePath}",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al generar el documento de Word:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                                        "Error de Generación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbRespuestaAPI.Text))
            {
                MessageBox.Show("Primero debe obtener una respuesta de la API para generar la presentación.",
                                "Respuesta API Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            string tituloDiapositiva1 = "Resumen del Incidente"; // Este texto debería ser extraído de la respuesta de la API de Groq

            string[] puntosSlide1 = {
            "Naturaleza del Incidente: " + (cmbNaturalezaIncidente.SelectedItem?.ToString() ?? "No especificado"),
            "Tipo de Sistema: " + (cmbTipoSistemaa.SelectedItem?.ToString() ?? "No especificado"), // Asegúrate que cmbTipoSistemaa sea el nombre correcto
            "Objetivos de la Investigación: " + txtObjetivosInvestigacion.Text.Trim()
            
        };

         

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PowerPoint Presentation (*.pptx)|*.pptx";
                saveFileDialog.Title = "Guardar Presentación de PowerPoint";
                saveFileDialog.FileName = "PresentacionForense_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pptx"; // Nombre de archivo sugerido

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    try
                    {
                    
                        GeneradorDocumentos.GenerarPresentacionPowerPoint(filePath, tituloDiapositiva1, puntosSlide1);

                        MessageBox.Show($"Presentación de PowerPoint generada exitosamente en:\n{filePath}",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al generar la presentación de PowerPoint:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                                        "Error de Generación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


    

