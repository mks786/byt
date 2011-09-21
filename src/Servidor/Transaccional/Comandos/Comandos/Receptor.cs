
namespace Trascend.Bolet.Comandos.Comandos
{
    public class Receptor<T>
    {
        /// <summary>
        /// Objeto generico a ser almacenado
        /// </summary>
        T _objetoAlmacenado;

        /// <summary>
        /// Propiedad que retorna el objeto genérico almacenado
        /// </summary>
        public T ObjetoAlmacenado
        {
            get { return _objetoAlmacenado; }
        }

        /// <summary>
        /// Constructo predeterminado
        /// </summary>
        /// <param name="objetoRecibido">Objeto a almacenar</param>
        public Receptor(T objetoAlmacenado)
        {
            this._objetoAlmacenado = objetoAlmacenado;
        }
    }
}
