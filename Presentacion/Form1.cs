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
        private List<Articulo> listaArticulos;
        private ArticuloNegocio negocio = new ArticuloNegocio();
        private Marca marca;
        private Categoria categoria;
        private MarcaNegocio marcaNegocio= new MarcaNegocio();
        private CategoriaNegocio categoriaNegocio= new CategoriaNegocio();

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

            cboClase.Items.Add("Codigo");
            cboClase.Items.Add("Descripcion");
            cboClase.Items.Add("Marca");
            cboClase.Items.Add("Categoria");
            cboClase.Items.Add("Precio");

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
            dgvArticulos.Columns["Precio"].Visible = false;
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

        private void txtNombre_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            
            List<Articulo> listaFiltro = new List<Articulo>();

            try
            {   
                if(txtNombre.Text.Length > 1)
                {
                listaFiltro = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains(txtNombre.Text.ToLower()));
                }
                else
                {   
                    listaFiltro = listaArticulos;
                }
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaFiltro;
                ocultarColumnas();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            

            
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {

            try
            {   
                
                if (dgvArticulos.CurrentRow != null)
                {
                    Articulo aux = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    cargarImagen(aux.UrlImagen);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        private void cboClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTipo.DataSource = null;
            cboTipo.Items.Clear();
            lblTipo.Visible = true;
            cboTipo.Visible = true;
            txtFiltro.Visible = true;
            lblFiltro.Visible = true;
            switch (cboClase.Text)
            {
                case "Codigo":
                    lblTipo.Visible = false;
                    cboTipo.Visible = false;
                    break;
                case "Descripcion":
                    cboTipo.Items.Add("Empieza con");
                    cboTipo.Items.Add("Termina con");
                    cboTipo.Items.Add("Contiene");
                    break;
                case "Marca":
                    txtFiltro.Visible = false;
                    lblFiltro.Visible = false;
                    cboTipo.DataSource = marcaNegocio.listar();
                    break;
                case "Categoria":
                    txtFiltro.Visible = false;
                    lblFiltro.Visible = false;
                    cboTipo.DataSource = categoriaNegocio.listar();
                    break;
                default:
                    cboTipo.Items.Clear();
                    cboTipo.Items.Add("Mayor a");
                    cboTipo.Items.Add("Menor a");
                    cboTipo.Items.Add("Igual");
                    break;
                
            
            }
        }

        private void btnBusquedaAvanzada_Click(object sender, EventArgs e)
        {
            try
            {
                if(cboClase.Text == "Marca")
                    listaArticulos = negocio.filtrar(cboClase.Text, cboTipo.Text, txtFiltro.Text, (Marca)cboTipo.SelectedItem);
                else if(cboClase.Text== "Categoria")
                    listaArticulos = negocio.filtrar(cboClase.Text, cboTipo.Text, txtFiltro.Text,null ,(Categoria)cboTipo.SelectedItem);
                else
                    listaArticulos = negocio.filtrar(cboClase.Text, cboTipo.Text, txtFiltro.Text);

                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
