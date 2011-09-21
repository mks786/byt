
namespace Trascend.Bolet.Comandos.Comandos
{
    /// <summary>
    /// Comando que devuelve un objeto genérico
    /// </summary>
    /// <typeparam name="T">Objeto genérico</typeparam>
    public abstract class ComandoBase<T>
    {
        Receptor<T> _receptor = null;

        /// <summary>
        /// Propiedad que asigna u obtiene el objeto genérico
        /// </summary>
        public Receptor<T> Receptor
        {
            get { return _receptor; }
            protected set { _receptor = value; }
        }

        /// <summary>
        /// Método que Ejecuta la acción del comando
        /// </summary>
        public abstract void Ejecutar();
    }
}
