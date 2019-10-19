using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2_AP1.BLL;
using P2_AP1.DAL;
using P2_AP1.Entidades;

namespace P2_AP1.UI.Registros
{
    public partial class rCategorias : Form
    {
        RepositorioBase<Categorias> GenericCategorias;
        public rCategorias()
        {
            InitializeComponent();
            GenericCategorias = new RepositorioBase<Categorias>();
        }
        private Categorias LlenaClase()
        {
            Categorias categorias = new Categorias();
            categorias.CategoriaId = Convert.ToInt32(IdnumericUpDown1.Value);
            categorias.Descripcion = DescripcionrichTextBox.Text;

            return categorias;
        }
        private bool Validar()
        {
            MyerrorProvider.Clear();
            bool paso = true;


            if (string.IsNullOrWhiteSpace(DescripcionrichTextBox.Text))
            {
                MyerrorProvider.SetError(DescripcionrichTextBox, "El campo no puede estar vacio");
                DescripcionrichTextBox.Focus();
                paso = false;
            }

            return paso;
        }
        private void LlenaCampos(Categorias categorias)
        {
            IdnumericUpDown1.Value = categorias.CategoriaId;
            DescripcionrichTextBox.Text = categorias.Descripcion;
        }



        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void RAnonimo_Load(object sender, EventArgs e)
        {

        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Categorias categorias = new Categorias();
            bool paso = false;

            if (!Validar())
                return;

            categorias = LlenaClase();


            if (IdnumericUpDown1.Value == 0)
                paso = GenericCategorias.Guardar(categorias);
            else
            {
                if (!ExisteEnBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un registro que no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = GenericCategorias.Modificar(categorias);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No Guardado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Limpiar()
        {
            IdnumericUpDown1.Value = 0;
            DescripcionrichTextBox.Text = string.Empty;
        }
        private bool ExisteEnBaseDeDatos()
        {
            Categorias categorias = GenericCategorias.Buscar((int)IdnumericUpDown1.Value);
            return (categorias != null);
        }

        private void Buscrabutton_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(IdnumericUpDown1.Value);
            Categorias categorias= new Categorias();
            GenericCategorias = new RepositorioBase<Categorias>();

            Limpiar();

            categorias = GenericCategorias.Buscar(id);

            if (categorias != null)
            {
                LlenaCampos(categorias);
            }
            else
            {
                MessageBox.Show("Asignatura no encontrada");
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {

            MyerrorProvider.Clear();

            int id;
            id = Convert.ToInt32(IdnumericUpDown1.Value);
            Contexto db = new Contexto();

            Categorias categorias = new Categorias();

            Limpiar();

            if (GenericCategorias.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MyerrorProvider.SetError(IdnumericUpDown1, "No se puede eliminar la asignatura");
            }
        }
    }
}
