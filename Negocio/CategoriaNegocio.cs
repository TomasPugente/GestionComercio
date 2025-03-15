using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public  class CategoriaNegocio
    {
        private List<Categoria> listaCategoria;
        private AccesoDatos datos = new AccesoDatos();

        public List<Categoria> listar()
        {
            try
            {

                datos.setearConsulta("select id, descripcion from categorias");
                datos.EjecutarLectura();
                cargarLista();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return listaCategoria;
        }

        public void cargarLista()
        {
            try
            {
                listaCategoria = null;
                listaCategoria = new List<Categoria>();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Descripcion = (String)datos.Lector["descripcion"];

                    listaCategoria.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
