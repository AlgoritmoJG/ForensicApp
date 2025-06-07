using System;
using System.Windows.Forms; // Aseg�rate de tener este using

namespace ForensicApp // Aseg�rate de que este sea tu namespace correcto
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicaci�n.
        /// </summary>
        [STAThread] // Este atributo es importante para las aplicaciones de Windows Forms
        static void Main()
        {
            // Habilita los estilos visuales de Windows.
            Application.EnableVisualStyles();

            // Establece el modo de representaci�n de texto compatible predeterminado.
            // Generalmente se recomienda false para usar los m�todos de representaci�n m�s nuevos.
            Application.SetCompatibleTextRenderingDefault(false);

            // ESTA ES LA L�NEA M�S IMPORTANTE:
            // Crea una instancia de tu formulario principal (MainForm) y la ejecuta.
            // Esto inicia el bucle de mensajes de la aplicaci�n y mantiene el formulario visible.
            // Si esta l�nea falta o es incorrecta, la aplicaci�n se cerrar� inmediatamente.
            Application.Run(new MainForm());
        }
    }
}