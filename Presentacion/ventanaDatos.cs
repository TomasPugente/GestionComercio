using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmVentanaDatos: Form
    {
        Articulo articulo;
        ArticuloNegocio articuloNegocio= new ArticuloNegocio();
        public frmVentanaDatos()
        {
            InitializeComponent();
        }
        public frmVentanaDatos(Articulo articulo)
        {
            InitializeComponent();
            Text = "Editar";
            this.articulo = articulo;
        }

        private void frmVentanaDatos_Load(object sender, EventArgs e)
        {
            try
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                cbxCategoria.DataSource = categoriaNegocio.listar();
                cbxMarca.DataSource = marcaNegocio.listar();
                if (articulo != null)
                {
                    txtNombre.Text = articulo.Nombre;
                    txtCodigo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImagen.Text = articulo.UrlImagen;
                    txtPrecio.Text = articulo.Precio.ToString();
                }
                cargarImagen(txtUrlImagen.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
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

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if(articulo== null)
                articulo = new Articulo();
                articulo.Nombre = txtNombre.Text;
                articulo.Codigo = txtCodigo.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.UrlImagen = txtUrlImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.Marca = (Marca)cbxMarca.SelectedItem;

                if (articulo.Id > 0)
                    articuloNegocio.editar(articulo);
                else
                    articuloNegocio.agregar(articulo);

                MessageBox.Show("COMPLETADO CON EXITO");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
