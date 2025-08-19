using static BCrypt.Net.BCrypt;

namespace SenaPlanning.Helpers
{
    /// <summary>
    /// Clase helper para el manejo seguro de contraseñas usando BCrypt
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Cifra una contraseña usando BCrypt con salt automático
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <returns>Hash de la contraseña cifrada</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifica si una contraseña coincide con su hash
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <param name="hash">Hash almacenado en la base de datos</param>
        /// <returns>True si la contraseña es correcta, False en caso contrario</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            try
            {
                return Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si un hash necesita ser actualizado (rehashed)
        /// Útil para migrar contraseñas existentes o actualizar el work factor
        /// </summary>
        /// <param name="hash">Hash actual</param>
        /// <param name="workFactor">Factor de trabajo deseado (por defecto 12)</param>
        /// <returns>True si necesita actualización</returns>
        public static bool NeedsRehash(string hash, int workFactor = 12)
        {
            try
            {
                return PasswordNeedsRehash(hash, workFactor);
            }
            catch
            {
                return true;
            }
        }
    }
}
