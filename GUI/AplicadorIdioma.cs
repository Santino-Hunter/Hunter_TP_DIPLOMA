using SERVICES.SERVICESCambioIdioma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    internal static class AplicadorIdioma
    {
        private static readonly Dictionary<string, string> originales = new Dictionary<string, string>();

        public static void Aplicar(Form formulario)
        {
            if (formulario == null)
                return;

            string nombreFormulario = formulario.Name;

            formulario.Text = Traducir(nombreFormulario + ".Text", formulario.Text);

            AplicarAControles(nombreFormulario, formulario.Controls);
        }

        private static void AplicarAControles(string nombreFormulario, Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (DebeTraducir(control))
                {
                    string clave = nombreFormulario + "." + control.Name + ".Text";
                    control.Text = Traducir(clave, control.Text);
                }

                MenuStrip menu = control as MenuStrip;

                if (menu != null)
                {
                    foreach (ToolStripItem item in menu.Items)
                    {
                        AplicarAItemMenu(nombreFormulario, item);
                    }
                }

                TabControl tabControl = control as TabControl;

                if (tabControl != null)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        string claveTab = nombreFormulario + "." + tabPage.Name + ".Text";
                        tabPage.Text = Traducir(claveTab, tabPage.Text);
                    }
                }
                DataGridView grilla = control as DataGridView;

                if (grilla != null)
                {
                    foreach (DataGridViewColumn columna in grilla.Columns)
                    {
                        string claveColumna = nombreFormulario + "." + grilla.Name + "." + columna.Name + ".HeaderText";
                        columna.HeaderText = Traducir(claveColumna, columna.HeaderText);
                    }
                }

                if (control.HasChildren)
                    AplicarAControles(nombreFormulario, control.Controls);
            }
        }

        private static void AplicarAItemMenu(string nombreFormulario, ToolStripItem item)
        {
            if (item == null)
                return;

            string clave = nombreFormulario + "." + item.Name + ".Text";
            item.Text = Traducir(clave, item.Text);

            ToolStripMenuItem menuItem = item as ToolStripMenuItem;

            if (menuItem != null)
            {
                foreach (ToolStripItem hijo in menuItem.DropDownItems)
                {
                    AplicarAItemMenu(nombreFormulario, hijo);
                }
            }
        }

        private static bool DebeTraducir(Control control)
        {
            return control is Label ||
                   control is Button ||
                   control is RadioButton ||
                   control is CheckBox ||
                   control is GroupBox;
        }

        private static string Traducir(string clave, string textoActual)
        {
            if (!originales.ContainsKey(clave))
                originales.Add(clave, textoActual);

            string textoOriginal = originales[clave];

            return TraductorIdioma.Texto(clave, textoOriginal);
        }

    }
}
