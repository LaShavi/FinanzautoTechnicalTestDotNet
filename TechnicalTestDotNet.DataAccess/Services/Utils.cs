using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TechnicalTestDotNet.DataAccess.Services
{
    public class Utils
    {
        // Variables para crear Password.
        private static readonly string Numeros = "0123456789";
        private static readonly string LetrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string LetrasMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string CaracteresEspeciales = "!@#$&*.?";

        #region Metodos

        /// <summary>
        /// Genera la fecha hora y secuencia con las especificaciones del web service
        /// </summary>
        /// <returns>Tupla de tres string</returns>
        public Tuple<string, string, string> GetDataDate(DateTime inputDate) // static
        {
            string date;
            string hour;
            string sequence;

            #region BuildDate

            string year = inputDate.Year.ToString();// Substring(DateTime.Now.Year.ToString().Length - 2, 2);

            string month = inputDate.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            string day = inputDate.Date.Day.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            }

            date = year + month + day;

            #endregion

            #region BuildHour

            string hours = inputDate.Hour.ToString();
            if (hours.Length == 1)
            {
                hours = "0" + hours;
            }

            string minutes = inputDate.Minute.ToString();
            if (minutes.Length == 1)
            {
                minutes = "0" + minutes;
            }

            string seconds = inputDate.Second.ToString();
            if (seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            hour = hours + minutes + seconds;
            #endregion

            #region BuildSequence

            var guid = Guid.NewGuid();
            var ramdonNumber = new string(guid.ToString().Where(char.IsDigit).ToArray());
            ramdonNumber = ramdonNumber.Substring(0, 6);
            sequence = hour + ramdonNumber;

            #endregion

            Tuple<string, string, string> oReturn = new Tuple<string, string, string>(date, hour, sequence);
            return oReturn;
        }


        /// <summary>
        /// Escribir archivo plano
        /// </summary>
        /// <param name="path">ruta</param>
        /// <param name="line">texto</param>
        public void EscribirArchivoPlano(string path, string line)
        {
            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                outputFile.WriteLine(line);
            }
        }

        /// <summary>
        /// Elimina las Tildes de una cadena ingresada
        /// </summary>
        /// <param name="InpuText">Texto de entrada</param>
        public string EliminarAcentos(string InpuText)
        {
            //var cleanText = Regex.Replace(InpuText.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ;-]+", "");
            var cleanText = Regex.Replace(InpuText.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9\: ;-]+", "");
            return cleanText;
        }

        /// <summary>
        /// Elimina texto adicional no deseado en String de Base64
        /// </summary>
        /// <param name="InpuText">Texto de entrada</param>
        public string EliminarTextoBase64(string InpuText, string SeparadorBase64)
        {

            var ListadoSeparadores = SeparadorBase64.Split('[');
            foreach (var item in ListadoSeparadores)
            {
                InpuText = InpuText.Replace(item, "");
            }

            return InpuText;
        }

        /// <summary>
        /// Eliminar caracteres de un string tipo moneda para poder convertirlo en decimal
        /// </summary>
        /// <param name="InpuText">Texto sin caracteres especiales</param>
        public string EliminarCaracteresEspecialesTipoMoneda(string InpuText) // static
        {
            return Regex.Replace(InpuText, @"\${1}", "");
        }

        /// <summary>
        /// Escribir archivo plano
        /// </summary>
        /// <param name="path">ruta</param>
        /// <param name="line">texto</param>
        public void WritePlainTextFile(string path, string line)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(path, true))
                {
                    outputFile.WriteLine(line);
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        /// <summary>
        /// Convierte un string de tipo "YYYYmmdd" a DateTime
        /// </summary>
        /// <param name="str">texto</param>
        public DateTime StringToDate(string str)
        {
            if (string.IsNullOrEmpty(str)) return DateTime.MinValue;
            var output = $"{str.Substring(0, 4)}/{str.Substring(4, 2)}/{str.Substring(6, 2)} {str.Substring(8, 2)}:{str.Substring(10, 2)}:{str.Substring(12, 2)}";
            return DateTime.Parse(output);
        }

        /// <summary>
        /// Convierte un List (Generico) en un DataTable 
        /// </summary>
        /// <param name="str">texto</param>
        public DataTable ConvertToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = dataTable.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// Generamos una Password Automatica, que cumple con las condiciones deseadas. 
        /// </summary>
        /// <param name="longitud">No de caracteres de Password</param>
        /// <returns>Clave generada de forma automatica</returns>
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

        #endregion

        #region Dependency Injection        

        public Utils()
        {

        }

        #endregion

        #region Encrypted

        /// <summary>
        /// Realiza cifrado de string
        /// </summary>
        /// <param name="textToEnrypt">Texto a cifrar</param>
        /// <returns>TExto cifrado</returns>
        public string Encryptor(string textToEnrypt)
        {
            string ciphertext;
            string decryptedtext;

            UTF8Encoding utf8 = new UTF8Encoding();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            // Crear el objeto de configuración a partir del archivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            //aes.IV = Encoding.ASCII.GetBytes(configuration.GetValue<string>("GeneralConfigParams:CriptoKey"));
            //aes.Key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("GeneralConfigParams:CriptoIV"));
            aes.IV = Encoding.ASCII.GetBytes(configuration["GeneralConfigParams:CriptoKey"]);
            aes.Key = Encoding.ASCII.GetBytes(configuration["GeneralConfigParams:CriptoIV"]);

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                byte[] bytes = utf8.GetBytes(textToEnrypt);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                ms.Position = 0;
                bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);
                ciphertext = Convert.ToBase64String(bytes);
            }

            return ciphertext;
        }

        /// <summary>
        /// Realiza decifrado de stirng
        /// </summary>
        /// <param name="textToDecrypt">Texto a decifrar</param>
        /// <returns>Texto descifrados</returns>
        public string Decryptor(string textToDecrypt)
        {
            string decryptedtext;

            UTF8Encoding utf8 = new UTF8Encoding();

            AesCryptoServiceProvider aes = new();

            // Crear el objeto de configuración a partir del archivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            //var CriptoKey = configuration.GetValue<string>("GeneralConfigParams:CriptoKey");
            //var CriptoIV = configuration.GetValue<string>("GeneralConfigParams:CriptoIV");
            var CriptoKey = configuration["GeneralConfigParams:CriptoKey"];
            var CriptoIV = configuration["GeneralConfigParams:CriptoIV"];

            aes.IV = Encoding.ASCII.GetBytes(CriptoKey);
            aes.Key = Encoding.ASCII.GetBytes(CriptoIV);

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
                byte[] bytes = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                ms.Position = 0;
                bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);
                decryptedtext = utf8.GetString(bytes);
            }

            return decryptedtext;
        }
        #endregion

        #region SystemDate
        /// <summary>
        /// Devuelve fecha y hora string
        /// </summary>
        /// <param name="date">fecha </param>
        /// <returns>string fecha</returns>
        public string GetDateStringApp(DateTime date)
        {
            string min = date.Minute < 10 ? "0" + date.Minute.ToString() : date.Minute.ToString();
            string result = date.Day + " " +
            date.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper() + " " +
            date.Year.ToString() + " - " +
            date.ToString("hh") + ":" +
            min + " " +
            date.ToString("tt", CultureInfo.InvariantCulture);

            return result;
        }

        public static DateTime GetLastDayMonth(DateTime date)
        {
            DateTime nextMonth = date.AddMonths(1);
            int days = nextMonth.Day;
            DateTime oReturn = nextMonth.AddDays(-days);
            return oReturn;
        }
        #endregion
    }
}
