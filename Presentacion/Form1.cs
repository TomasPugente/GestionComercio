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
            ocultarColumnas();
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
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["id"].Visible = false;
            dgvArticulos.Columns["urlImagen"].Visible = false;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmVentanaDatos ventanaDatos = new frmVentanaDatos();
                ventanaDatos.ShowDialog();
                CargarInformacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmVentanaDatos ventanaDatos = new frmVentanaDatos(articulo);
                ventanaDatos.ShowDialog();
                CargarInformacion();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado=MessageBox.Show("Esta seguro de eliminar?\n No se puede deshacer esta accion", "Eliminando...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(resultado== DialogResult.Yes)
                negocio.eliminar((Articulo)dgvArticulos.CurrentRow.DataBoundItem);
            MessageBox.Show("Eliminado Correctamente", "Eliminado");
            CargarInformacion();
        }
    }
}
