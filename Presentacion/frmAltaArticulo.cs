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
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
            btnAgregar.Text = "Modificar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
               if(articulo == null)
                    articulo = new Articulo();

               articulo.Codigo = tbCodigo.Text;
               articulo.Nombre = tbNombre.Text;
               articulo.Descripcion = tbDescripcion.Text;
               articulo.ImagenUrl = tbImagen.Text;
               articulo.Marca = (Marca) cbMarca.SelectedItem;
               articulo.Categoria = (Categoria)cbCategorias.SelectedItem;
               articulo.Precio = decimal.Parse(tbPrecio.Text);


               if(articulo.Id != 0)
                {
                    articuloNegocio.modificar(articulo);
                    MessageBox.Show("Articulo modificado exitosamente");
                }
                else
                {
                    articuloNegocio.agregar(articulo);
                    MessageBox.Show("Articulo agregado exitosamente");
                }



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
                cbMarca.ValueMember = "Id";
                cbMarca.DisplayMember = "Descripcion";
                cbCategorias.DataSource = categoriaNegocio.listar();
                cbCategorias.ValueMember = "Id";
                cbCategorias.DisplayMember = "Descripcion";


                if (articulo !=null)
                {
                    tbCodigo.Text = articulo.Codigo;
                    tbNombre.Text = articulo.Nombre;
                    tbDescripcion.Text = articulo.Descripcion;
                    tbImagen.Text = articulo.ImagenUrl;
                    tbPrecio.Text = articulo.Precio.ToString();
                    cargarImagen(articulo.ImagenUrl);
                    cbMarca.SelectedValue = articulo.Marca.Id;
                    cbCategorias.SelectedValue = articulo.Categoria.Id;
                }
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
