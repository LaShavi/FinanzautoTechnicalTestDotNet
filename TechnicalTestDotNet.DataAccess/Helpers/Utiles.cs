using System.Text.RegularExpressions;
using System.Text;

namespace TechnicalTestDotNet.DataAccess.Helpers
{
    public class Utiles
    {
        // Variables para crear Password.
        private static readonly string Numeros = "0123456789";
        private static readonly string LetrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string LetrasMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string CaracteresEspeciales = "!@#$&*.?";

        #region Dependency Injection        

        public Utiles()
        {

        }

        #endregion

        // Metodos

        public string GeneratePassword(int longitud)
        {

            // Ejecutamos
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Asegurarse de que la longitud esté dentro del rango especificado
            if (longitud < 6 || longitud > 30)
            {
                throw new ArgumentOutOfRangeException("La longitud debe estar entre 6 y 30 caracteres.");
            }

            // Agregar al menos un carácter de cada categoría
            password.Append(Numeros[random.Next(Numeros.Length)]);
            password.Append(LetrasMinusculas[random.Next(LetrasMinusculas.Length)]);
            password.Append(LetrasMayusculas[random.Next(LetrasMayusculas.Length)]);
            password.Append(CaracteresEspeciales[random.Next(CaracteresEspeciales.Length)]);

            // Completar la longitud restante de forma aleatoria
            while (password.Length < longitud)
            {
                string categoriaAleatoria = ObtenerCategoriaAleatoria(random);
                password.Append(categoriaAleatoria[random.Next(categoriaAleatoria.Length)]);
            }

            // Mezclar los caracteres para obtener una cadena aleatoria
            string passwordAleatorio = new string(password.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());

            // Verificar si cumple con la expresión regular
            if (Regex.IsMatch(passwordAleatorio, @"^((?=\D*\d)(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=.*[!@#$&*.?]).{6,30})"))
            {
                return passwordAleatorio;
            }
            else
            {
                // Si no cumple, intentar nuevamente
                return GeneratePassword(longitud);
            }
        }

        private static string ObtenerCategoriaAleatoria(Random random)
        {
            string[] categorias = { Numeros, LetrasMinusculas, LetrasMayusculas, CaracteresEspeciales };
            return categorias[random.Next(categorias.Length)];
        }
    }
}
