using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class FmCatalogo: Form
    {
        List<Articulo> listaArticulos;
        ArticuloNegocio negocio = new ArticuloNegocio();
        public FmCatalogo()
        {
            InitializeComponent();
        }

        private void FmCatalogo_Load(object sender, EventArgs e)
        {
            CargarInformacion();
            Articulo seleccionado=(Articulo) dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void CargarInformacion()
        {
            try
            {
                listaArticulos = null;
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            

        }
        private void cargarImagen(string urlImagen)
        {
            try
            {
                try
                {
                    pbxArticulo.Load(urlImagen);

                }
                catch (Exception)
                {

                    pbxArticulo.Load("https://img.freepik.com/vector-premium/vector-icono-imagen-predeterminado-pagina-imagen-faltante-diseno-sitio-web-o-aplicacion-movil-no-hay-foto-disponible_87543-11093.jpg?w=360");
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private void dgvArticulos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Articulo aux = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(aux.UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()) ;
            }
            
        }
    }
}
