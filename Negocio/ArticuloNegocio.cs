using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        { 

        List<Articulo> listaArticulos = new List<Articulo>();
        ConexionDB conexionDB = new ConexionDB();


            try
            {
                conexionDB.setearConsulta("select A.Id,Codigo,Nombre,A.Descripcion,M.Descripcion Marca,C.Descripcion Categoria,ImagenUrl, Precio,C.id IdCategoria,M.Id IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdCategoria = C.Id and A.IdMarca = M.Id");
                conexionDB.ejecutarLectura();
                while (conexionDB.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)conexionDB.Lector["Id"];
                    articulo.Codigo = (string)conexionDB.Lector["Codigo"];
                    articulo.Nombre = (string)conexionDB.Lector["Nombre"];
                    articulo.Descripcion = (string)conexionDB.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)conexionDB.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)conexionDB.Lector["Marca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)conexionDB.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)conexionDB.Lector["Categoria"];
                    articulo.ImagenUrl = (string)conexionDB.Lector["ImagenUrl"];
                    articulo.Precio = Math.Round((decimal)conexionDB.Lector["Precio"],2);
                    listaArticulos.Add(articulo);
              
                }
                 return listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexionDB.cerrarConexion();
            }
    }

        public void agregar(Articulo articulo)
        {
            ConexionDB conexionDB = new ConexionDB();
            try
            {
                conexionDB.setearConsulta("Insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values (@Codigo,@Nombre,@Descripcion,@IdMarca, @IdCategoria,@ImagenUrl,@Precio)");
                conexionDB.setearParametro("@Codigo", articulo.Codigo);
                conexionDB.setearParametro("@Nombre", articulo.Nombre);
                conexionDB.setearParametro("@Descripcion", articulo.Descripcion);
                conexionDB.setearParametro("@IdMarca", articulo.Marca.Id);
                conexionDB.setearParametro("@IdCategoria", articulo.Categoria.Id);
                conexionDB.setearParametro("@ImagenUrl", articulo.ImagenUrl);
                conexionDB.setearParametro("@Precio", articulo.Precio);
                conexionDB.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexionDB.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            try
            {
                ConexionDB conexionDB = new ConexionDB();
                conexionDB.setearConsulta("Delete from ARTICULOS where id=@id");
                conexionDB.setearParametro("@id", id);
                conexionDB.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

}
}
