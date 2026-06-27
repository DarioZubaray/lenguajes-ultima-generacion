using System.Collections.Generic;

namespace Abstraccion
{
    public interface IGestor<T> where T : IEntidad
    {
        bool Guardar(T objeto);
        bool Baja(T objeto);

        List<T> ListarTodo();
        T ListarObjeto(T objeto);
    }
}
