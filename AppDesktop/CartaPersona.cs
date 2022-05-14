
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
    public partial class CartaPersona : UserControl
    {
        Personas Personas;
        rPersonas r;
        public CartaPersona(Personas _personas, rPersonas _r)
        {
            InitializeComponent();
            lblNombre.Text = _personas.Nombre;
            lblApellido.Text = _personas.Apellido;
            lblEmail.Text = _personas.Email;
            Personas = _personas;
            r = _r;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            r.acc = "u";
            r.errorProvider1.Clear();
            r.Id = Personas.Id;
            r.txtNombre.Text = Personas.Nombre;
            r.txtApellido.Text = Personas.Apellido;
            r.txtEmail.Text = Personas.Email;
            r.btnGuardar.Text = "EDITAR";
            panelEstatus.BackColor = Color.FromArgb(31, 112, 251);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            r.acc = "d";
            r.Id = Personas.Id;
            r.errorProvider1.Clear();
            r.txtNombre.Text = Personas.Nombre;
            r.txtApellido.Text = Personas.Apellido;
            r.txtEmail.Text = Personas.Email;
            r.btnGuardar.Text = "ELIMINAR";
            panelEstatus.BackColor = Color.Red;
        }
    }
}
