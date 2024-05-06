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
            cbCampo.Items.Add("Precio");
            cbCampo.Items.Add("Marca");
            cbCampo.Items.Add("Categoria");
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
                imgCaption.Text = seleccionado.Nombre;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articuloSeleccionado;

            try
            {
                DialogResult respuesta = MessageBox.Show("Estas por eliminar el articulo permanentemente, ¿Estas seguro?", "Eliminar Articulo", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                negocio.eliminar(articuloSeleccionado.Id);
                cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAltaArticulo frmModificar = new frmAltaArticulo(seleccionado);
            frmModificar.ShowDialog();
            cargar();
        }

   

        private void tbFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = tbFiltro.Text;

            if (filtro.Length >= 2)
            {
                listaFiltrada = listaArticulos.FindAll(a => a.Nombre.ToLower().Contains(filtro.ToLower()) || a.Codigo.ToLower().Contains(filtro.ToLower()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void tbFiltro_Enter(object sender, EventArgs e)
        {
            tbFiltro.Text = "";
            tbFiltro.BackColor = Color.White;
            tbFiltro.ForeColor = Color.Black;
        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
           string opcion = cbCampo.SelectedItem.ToString();

           if(opcion == "Precio")
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Mayor a");
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Igual a");
            }
            else
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Comienza con");
                cbCriterio.Items.Add("Termina con");
                cbCriterio.Items.Add("Contiene");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {

            string campo = cbCampo.SelectedItem.ToString();
            string critero = cbCriterio.SelectedItem.ToString();    
            string filtro = tbValor.Text;
             dgvArticulos.DataSource = negocio.filtrar(campo, critero, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

      
    }
}
