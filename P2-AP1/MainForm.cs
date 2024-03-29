﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2_AP1.UI.Registros;
using P2_AP1.UI.Consultas;

namespace P2_AP1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Factura = new rFactura();
            Factura.Show();
        }

        private void CategoriasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form categorias = new rServicios();
            categorias.Show();
        }

        private void FacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form consulta = new cFactura();
            consulta.Show();
        }
    }
}
