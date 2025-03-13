using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {
        private List<Articulo> listaArticulos=null;
        private AccesoDatos datos = new AccesoDatos();

        public List<Articulo> listar()
        {
            try
            {
                datos.setearConsulta("select a.id id, a.Codigo codigo, a.nombre nombre, a.descripcion descripcion, c.id idCategoria, c.Descripcion descripcionCategoria, m.id idMarca, m.descripcion DescripcionMarca, a.imagenUrl urlImagen, a.precio precio from Articulos a, Categorias c, marcas m where a.IdMarca=m.id and a.IdCategoria= c.id; ;\r\n");
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
            return listaArticulos;
        }

        public void cargarLista()
        {
            try
            {
                listaArticulos = null;
                listaArticulos = new List<Articulo>();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["id"];
                    aux.Codigo = (String)datos.Lector["Codigo"];
                    aux.Nombre = (String)datos.Lector["nombre"];
                    aux.Descripcion = (String)datos.Lector["Descripcion"];
                    aux.Categoria.Id = (int)datos.Lector["idCategoria"];
                    aux.Categoria.Descripcion = (String)datos.Lector["DescripcionCategoria"];
                    aux.Marca.Id = (int)datos.Lector["idMarca"];
                    aux.Marca.Descripcion = (String)datos.Lector["descripcionMarca"];
                    aux.UrlImagen = (String)datos.Lector["UrlImagen"];
                    aux.Precio = (decimal)datos.Lector["precio"];

                    listaArticulos.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
