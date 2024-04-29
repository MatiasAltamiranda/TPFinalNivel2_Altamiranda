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
    public partial class Articulos : Form
    {
        private List<Articulo> listaArticulos;
        public Articulos()
        {
            InitializeComponent();
        }

     

        private void Articulos_Load(object sender, EventArgs e)
        {
            cargar();

        }


        private void ocultarColumnas()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pbArticulo.Load("https://static.vecteezy.com/system/resources/previews/005/337/799/original/icon-image-not-found-free-vector.jpg");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
                cargarImagen(listaArticulos[0].ImagenUrl);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            frmAltaArticulo frm = new frmAltaArticulo();
            frm.ShowDialog();
            cargar();
        }


    }
}
