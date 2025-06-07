using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; // <--- AÑADE ESTA LÍNEA

namespace ForensicApp
{
    internal class DatabaseManager
    {
        public static async Task GuardarInvestigacion(string tipoSistema, string naturalezaIncidente, string objetivosInvestigacion, string promptEnviado, string respuestaAPI)
        {
            // Asumiendo que MainForm.SqlConnectionString está definido correctamente en tu clase MainForm
            // y es accesible desde aquí (por ejemplo, si es public static string).
            using (SqlConnection connection = new SqlConnection(MainForm.SqlConnectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Casos (TipoSistema, NaturalezaIncidente, ObjetivosInvestigacion, PromptEnviado, RespuestaAPI, FechaRegistro)
                    VALUES (@TipoSistema, @NaturalezaIncidente, @ObjetivosInvestigacion, @PromptEnviado, @RespuestaAPI, GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TipoSistema", tipoSistema);
                    command.Parameters.AddWithValue("@NaturalezaIncidente", naturalezaIncidente);
                    command.Parameters.AddWithValue("@ObjetivosInvestigacion", objetivosInvestigacion);
                    command.Parameters.AddWithValue("@PromptEnviado", promptEnviado);
                    command.Parameters.AddWithValue("@RespuestaAPI", respuestaAPI);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}