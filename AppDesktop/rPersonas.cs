
using AppDesktop;
using AppDesktop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDesktop
{
    public partial class rPersonas : Form
    {
        private PersonasServices PersonasServices = new PersonasServices();
        private TokenServices TokenServices = new TokenServices();
        private List<Personas> Personas = new List<Personas>();
        public string acc = "c";
        public int Id = 0;
        public rPersonas()
        {
            InitializeComponent();
        }


        private void closeFormCategory_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private async void rPersonas_Load(object sender, EventArgs e)
        {
            if (!await TokenServices.Token())
            {
                MessageBox.Show("Se cerrara la aplicacion. Token no generado, no podra consumir el WebApi", "Error interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
            LimpiarCargar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (acc == "c")
                this.Dispose();
            else if (acc == "u" || acc == "d")
            {
                LimpiarCargar();
                acc = "c";
                btnGuardar.Text = "GUARDAR";
            }
        }

        private async void LimpiarCargar()
        {
            acc = "c";
            this.Id = 0;
            btnGuardar.Text = "GUARDAR";
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            Personas = await PersonasServices.GetPersonas();
            if (Personas.Count > 0)
            {
                grd.Columns.Clear();
                grd.DataSource = Personas;
                grd.Columns["Id"].Visible = false;
                grd.Columns["Nulo"].Visible = false;
                grd.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                AgregarBtnGrid();
                FLP.Controls.Clear();
                foreach (var item in Personas)
                {
                    FLP.Controls.Add(new CartaPersona(item, this));
                }
            }

        }

        private bool Validar()
        {
            bool validar = false;
            errorProvider1.Clear();
            if (String.IsNullOrEmpty(txtNombre.Text) || txtNombre.TextLength <= 3)
            {
                errorProvider1.SetError(txtNombre, "Nombre esta vacio o no ingreso un valor valido.");
                validar = true;
            }
            if (String.IsNullOrEmpty(txtApellido.Text) || txtApellido.TextLength <= 3)
            {
                errorProvider1.SetError(txtApellido, "Apellido esta vacio o no ingreso un valor valido.");
                validar = true;
            }
            if (String.IsNullOrEmpty(txtEmail.Text) || txtEmail.TextLength <= 3)
            {
                errorProvider1.SetError(txtEmail, "Email esta vacio o no ingreso un valor valido.");
                validar = true;
            }
            if(acc == "u" || acc == "c")
            {
                var p = Personas.Where(x => x.Email.Replace(" ", "").ToLower() == txtEmail.Text.Replace(" ", "").ToLower() && x.Id != Id).ToList();
                if(p.Count > 0)
                {
                    errorProvider1.SetError(txtEmail, "Este email ya se encuentra en uso, favor utilizar otro.");
                    validar = true;
                }
                    
            }
            return validar;
        }

        private Personas LlenaClase(Personas p)
        {
            if (acc == "c")
                p.Id = 0;
            else
                p.Id = this.Id;
            p.Nombre = txtNombre.Text;
            p.Apellido = txtApellido.Text;
            p.Email = txtEmail.Text;
            p.Nulo = false;
            return p;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            Personas personas = new Personas();
            bool paso = false;

            if (Validar())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            personas = LlenaClase(personas);

            if (acc == "c")
            {
                if (paso = await PersonasServices.PostPersona(personas))
                    MessageBox.Show("Guardado", "Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No se pudo Guardar", "Error!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (acc == "u")
            {
                if (MessageBox.Show("Estas seguro de modificar este registro ?", "Modificar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (paso = await PersonasServices.PutPersonas(personas))
                        MessageBox.Show("Modificado", "Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("No se pudo Modificar", "Error!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (acc == "d")
            {
                if (MessageBox.Show("Estas seguro de eliminar este registro ?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (paso = await PersonasServices.DeletePersonas(personas.Id))
                    {
                        MessageBox.Show("Eliminado", "Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se pudo Eliminar", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (paso)
                LimpiarCargar();
        }
        private void AgregarBtnGrid()
        {
            if (grd.Rows.Count > 0)
            {
                DataGridViewButtonColumn btnModificar = new DataGridViewButtonColumn();
                btnModificar.Name = "M";
                grd.Columns.Add(btnModificar);
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.Name = "E";
                grd.Columns.Add(btnEliminar);
            }
        }

        private void grd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (grd.Rows.Count > 0)
            {
                if (e.ColumnIndex >= 0 && this.grd.Columns[e.ColumnIndex].Name == "M" && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    DataGridViewButtonCell celBoton = this.grd.Rows[e.RowIndex].Cells["M"] as DataGridViewButtonCell;
                    Icon icoAtomico = AppDesktop.Properties.Resources.edit;
                    celBoton.FlatStyle = FlatStyle.Flat;
                    celBoton.Style.BackColor = Color.FromArgb(2, 117, 216);
                    celBoton.Style.SelectionBackColor = Color.FromArgb(2, 117, 216);
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 7, e.CellBounds.Top + 7);

                    this.grd.Rows[e.RowIndex].Height = icoAtomico.Height + 18;
                    this.grd.Columns[e.ColumnIndex].Width = icoAtomico.Width + 18;

                    e.Handled = true;
                }
                else if (e.ColumnIndex >= 0 && this.grd.Columns[e.ColumnIndex].Name == "E" && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    DataGridViewButtonCell celBoton = this.grd.Rows[e.RowIndex].Cells["E"] as DataGridViewButtonCell;
                    Icon icoAtomico = AppDesktop.Properties.Resources.trash;
                    celBoton.FlatStyle = FlatStyle.Flat;
                    celBoton.Style.BackColor = Color.FromArgb(217, 83, 79);
                    celBoton.Style.SelectionBackColor = Color.FromArgb(217, 83, 79);
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 7, e.CellBounds.Top + 7);
                    this.grd.Rows[e.RowIndex].Height = icoAtomico.Height + 18;
                    this.grd.Columns[e.ColumnIndex].Width = icoAtomico.Width + 18;

                    e.Handled = true;
                }
            }
        }

        private void grd_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (this.grd.Columns[e.ColumnIndex].Name == "M")
            {
                acc = "u";
                errorProvider1.Clear();
                var p = Personas.Where(x => x.Id == (int)grd.CurrentRow.Cells["Id"].Value).FirstOrDefault();
                this.Id = p.Id;
                txtNombre.Text = p.Nombre;
                txtApellido.Text = p.Apellido;
                txtEmail.Text = p.Email;
                btnGuardar.Text = "EDITAR";
            }
            else if (this.grd.Columns[e.ColumnIndex].Name == "E")
            {
                acc = "d";
                errorProvider1.Clear();
                var p = Personas.Where(x => x.Id == (int)grd.CurrentRow.Cells["Id"].Value).FirstOrDefault();
                this.Id = p.Id;
                txtNombre.Text = p.Nombre;
                txtApellido.Text = p.Apellido;
                txtEmail.Text = p.Email;
                btnGuardar.Text = "ELIMINAR";
            }
        }
    }
}
