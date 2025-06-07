using System;
using System.Windows.Forms; // Asegúrate de tener este using

namespace ForensicApp // Asegúrate de que este sea tu namespace correcto
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread] // Este atributo es importante para las aplicaciones de Windows Forms
        static void Main()
        {
            // Habilita los estilos visuales de Windows.
            Application.EnableVisualStyles();

            // Establece el modo de representación de texto compatible predeterminado.
            // Generalmente se recomienda false para usar los métodos de representación más nuevos.
            Application.SetCompatibleTextRenderingDefault(false);

            // ESTA ES LA LÍNEA MÁS IMPORTANTE:
            // Crea una instancia de tu formulario principal (MainForm) y la ejecuta.
            // Esto inicia el bucle de mensajes de la aplicación y mantiene el formulario visible.
            // Si esta línea falta o es incorrecta, la aplicación se cerrará inmediatamente.
            Application.Run(new MainForm());
        }
    }
}