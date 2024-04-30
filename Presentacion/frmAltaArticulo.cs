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
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Articulo nuevoArticulo = new Articulo();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
               nuevoArticulo.Codigo = tbCodigo.Text;
               nuevoArticulo.Nombre = tbNombre.Text;
               nuevoArticulo.Descripcion = tbDescripcion.Text;
               nuevoArticulo.ImagenUrl = tbImagen.Text;
               nuevoArticulo.Marca = (Marca) cbMarca.SelectedItem;
               nuevoArticulo.Categoria = (Categoria)cbCategorias.SelectedItem;
               nuevoArticulo.Precio = decimal.Parse(tbPrecio.Text);

               articuloNegocio.agregar(nuevoArticulo);
               MessageBox.Show("Articulo agregado exitosamente");
               Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cbMarca.DataSource = marcaNegocio.listar();
                cbCategorias.DataSource = categoriaNegocio.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void tbImagen_Leave(object sender, EventArgs e)
        {
           cargarImagen(tbImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbImagenAgregar.Load(imagen);
            }
            catch (Exception)
            {

                pbImagenAgregar.Load("https://static.vecteezy.com/system/resources/previews/005/337/799/original/icon-image-not-found-free-vector.jpg");
            }
        }

       
    }
}
