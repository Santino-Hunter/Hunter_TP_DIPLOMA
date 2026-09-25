namespace GUI
{
    partial class FRMPerfiles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabListado = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.lstPermisos = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tvFamilias = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.tvPerfiles = new System.Windows.Forms.TreeView();
            this.tabGestion = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPerfiles = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbFamilias = new System.Windows.Forms.ComboBox();
            this.cmbPermisos = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEliminarPermiso = new System.Windows.Forms.Button();
            this.btnModificarPermiso = new System.Windows.Forms.Button();
            this.btnCrearPermiso = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnQuitarPermisoAFamilia = new System.Windows.Forms.Button();
            this.btnAsignarPermisoAFamilia = new System.Windows.Forms.Button();
            this.btnEliminarFamilia = new System.Windows.Forms.Button();
            this.btnModificarFamilia = new System.Windows.Forms.Button();
            this.btnCrearFamilia = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuitarFamiliaAPerfil = new System.Windows.Forms.Button();
            this.btnAsignarFamiliaAPerfil = new System.Windows.Forms.Button();
            this.btnQuitarPermisoAPerfil = new System.Windows.Forms.Button();
            this.btnAsignarPermisoAPerfil = new System.Windows.Forms.Button();
            this.btnEliminarPerfil = new System.Windows.Forms.Button();
            this.btnModificarPerfil = new System.Windows.Forms.Button();
            this.btnCrearPerfil = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabListado.SuspendLayout();
            this.tabGestion.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabListado);
            this.tabControl1.Controls.Add(this.tabGestion);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(878, 444);
            this.tabControl1.TabIndex = 9;
            // 
            // tabListado
            // 
            this.tabListado.Controls.Add(this.label6);
            this.tabListado.Controls.Add(this.lstPermisos);
            this.tabListado.Controls.Add(this.label5);
            this.tabListado.Controls.Add(this.tvFamilias);
            this.tabListado.Controls.Add(this.label1);
            this.tabListado.Controls.Add(this.tvPerfiles);
            this.tabListado.Location = new System.Drawing.Point(4, 22);
            this.tabListado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabListado.Name = "tabListado";
            this.tabListado.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabListado.Size = new System.Drawing.Size(870, 418);
            this.tabListado.TabIndex = 0;
            this.tabListado.Text = "Listado";
            this.tabListado.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(36, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 31);
            this.label6.TabIndex = 16;
            this.label6.Text = "PERMISOS";
            // 
            // lstPermisos
            // 
            this.lstPermisos.FormattingEnabled = true;
            this.lstPermisos.Location = new System.Drawing.Point(4, 48);
            this.lstPermisos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstPermisos.Name = "lstPermisos";
            this.lstPermisos.Size = new System.Drawing.Size(225, 368);
            this.lstPermisos.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(501, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 31);
            this.label5.TabIndex = 14;
            this.label5.Text = "PERFILES";
            // 
            // tvFamilias
            // 
            this.tvFamilias.Location = new System.Drawing.Point(233, 47);
            this.tvFamilias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tvFamilias.Name = "tvFamilias";
            this.tvFamilias.Size = new System.Drawing.Size(222, 369);
            this.tvFamilias.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(280, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "FAMILIAS";
            // 
            // tvPerfiles
            // 
            this.tvPerfiles.Location = new System.Drawing.Point(459, 47);
            this.tvPerfiles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tvPerfiles.Name = "tvPerfiles";
            this.tvPerfiles.Size = new System.Drawing.Size(222, 369);
            this.tvPerfiles.TabIndex = 8;
            // 
            // tabGestion
            // 
            this.tabGestion.Controls.Add(this.groupBox4);
            this.tabGestion.Controls.Add(this.label4);
            this.tabGestion.Controls.Add(this.label3);
            this.tabGestion.Controls.Add(this.label2);
            this.tabGestion.Controls.Add(this.txtDescripcion);
            this.tabGestion.Controls.Add(this.txtNombre);
            this.tabGestion.Controls.Add(this.groupBox3);
            this.tabGestion.Controls.Add(this.groupBox2);
            this.tabGestion.Controls.Add(this.groupBox1);
            this.tabGestion.Location = new System.Drawing.Point(4, 22);
            this.tabGestion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabGestion.Name = "tabGestion";
            this.tabGestion.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabGestion.Size = new System.Drawing.Size(870, 418);
            this.tabGestion.TabIndex = 1;
            this.tabGestion.Text = "Gestion";
            this.tabGestion.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.cmbPerfiles);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cmbFamilias);
            this.groupBox4.Controls.Add(this.cmbPermisos);
            this.groupBox4.Location = new System.Drawing.Point(598, 6);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(269, 116);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Combos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label8.Location = new System.Drawing.Point(198, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "perfiles:";
            // 
            // cmbPerfiles
            // 
            this.cmbPerfiles.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbPerfiles.FormattingEnabled = true;
            this.cmbPerfiles.Location = new System.Drawing.Point(180, 54);
            this.cmbPerfiles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbPerfiles.Name = "cmbPerfiles";
            this.cmbPerfiles.Size = new System.Drawing.Size(84, 21);
            this.cmbPerfiles.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label9.Location = new System.Drawing.Point(110, 39);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "familias:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label7.Location = new System.Drawing.Point(20, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "permisos:";
            // 
            // cmbFamilias
            // 
            this.cmbFamilias.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbFamilias.FormattingEnabled = true;
            this.cmbFamilias.Location = new System.Drawing.Point(92, 54);
            this.cmbFamilias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbFamilias.Name = "cmbFamilias";
            this.cmbFamilias.Size = new System.Drawing.Size(84, 21);
            this.cmbFamilias.TabIndex = 17;
            // 
            // cmbPermisos
            // 
            this.cmbPermisos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbPermisos.FormattingEnabled = true;
            this.cmbPermisos.Location = new System.Drawing.Point(4, 54);
            this.cmbPermisos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbPermisos.Name = "cmbPermisos";
            this.cmbPermisos.Size = new System.Drawing.Size(84, 21);
            this.cmbPermisos.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(96, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 31);
            this.label4.TabIndex = 24;
            this.label4.Text = "GESTION";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(38, 180);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Descripcion:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(38, 145);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Nombre:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(38, 196);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDescripcion.MaxLength = 49;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(273, 62);
            this.txtDescripcion.TabIndex = 21;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(38, 160);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombre.MaxLength = 49;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(273, 20);
            this.txtNombre.TabIndex = 20;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEliminarPermiso);
            this.groupBox3.Controls.Add(this.btnModificarPermiso);
            this.groupBox3.Controls.Add(this.btnCrearPermiso);
            this.groupBox3.Location = new System.Drawing.Point(382, 2);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(212, 119);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PERMISO";
            // 
            // btnEliminarPermiso
            // 
            this.btnEliminarPermiso.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnEliminarPermiso.Location = new System.Drawing.Point(106, 17);
            this.btnEliminarPermiso.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEliminarPermiso.Name = "btnEliminarPermiso";
            this.btnEliminarPermiso.Size = new System.Drawing.Size(97, 44);
            this.btnEliminarPermiso.TabIndex = 2;
            this.btnEliminarPermiso.Text = "Eliminar Permiso";
            this.btnEliminarPermiso.UseVisualStyleBackColor = true;
            this.btnEliminarPermiso.Click += new System.EventHandler(this.btnEliminarPermiso_Click);
            // 
            // btnModificarPermiso
            // 
            this.btnModificarPermiso.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnModificarPermiso.Location = new System.Drawing.Point(4, 71);
            this.btnModificarPermiso.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnModificarPermiso.Name = "btnModificarPermiso";
            this.btnModificarPermiso.Size = new System.Drawing.Size(97, 44);
            this.btnModificarPermiso.TabIndex = 1;
            this.btnModificarPermiso.Text = "Modificar Permiso";
            this.btnModificarPermiso.UseVisualStyleBackColor = true;
            this.btnModificarPermiso.Click += new System.EventHandler(this.btnModificarPermiso_Click);
            // 
            // btnCrearPermiso
            // 
            this.btnCrearPermiso.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCrearPermiso.Location = new System.Drawing.Point(4, 17);
            this.btnCrearPermiso.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCrearPermiso.Name = "btnCrearPermiso";
            this.btnCrearPermiso.Size = new System.Drawing.Size(97, 44);
            this.btnCrearPermiso.TabIndex = 0;
            this.btnCrearPermiso.Text = "Crear Permiso";
            this.btnCrearPermiso.UseVisualStyleBackColor = true;
            this.btnCrearPermiso.Click += new System.EventHandler(this.btnCrearPermiso_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnQuitarPermisoAFamilia);
            this.groupBox2.Controls.Add(this.btnAsignarPermisoAFamilia);
            this.groupBox2.Controls.Add(this.btnEliminarFamilia);
            this.groupBox2.Controls.Add(this.btnModificarFamilia);
            this.groupBox2.Controls.Add(this.btnCrearFamilia);
            this.groupBox2.Location = new System.Drawing.Point(382, 124);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(486, 123);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FAMILIA";
            // 
            // btnQuitarPermisoAFamilia
            // 
            this.btnQuitarPermisoAFamilia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnQuitarPermisoAFamilia.Location = new System.Drawing.Point(384, 66);
            this.btnQuitarPermisoAFamilia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuitarPermisoAFamilia.Name = "btnQuitarPermisoAFamilia";
            this.btnQuitarPermisoAFamilia.Size = new System.Drawing.Size(97, 44);
            this.btnQuitarPermisoAFamilia.TabIndex = 7;
            this.btnQuitarPermisoAFamilia.Text = "Quitar Permiso";
            this.btnQuitarPermisoAFamilia.UseVisualStyleBackColor = true;
            this.btnQuitarPermisoAFamilia.Click += new System.EventHandler(this.btnQuitarPermisoAFamilia_Click);
            // 
            // btnAsignarPermisoAFamilia
            // 
            this.btnAsignarPermisoAFamilia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnAsignarPermisoAFamilia.Location = new System.Drawing.Point(384, 17);
            this.btnAsignarPermisoAFamilia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAsignarPermisoAFamilia.Name = "btnAsignarPermisoAFamilia";
            this.btnAsignarPermisoAFamilia.Size = new System.Drawing.Size(97, 44);
            this.btnAsignarPermisoAFamilia.TabIndex = 6;
            this.btnAsignarPermisoAFamilia.Text = "Asignar Permiso";
            this.btnAsignarPermisoAFamilia.UseVisualStyleBackColor = true;
            this.btnAsignarPermisoAFamilia.Click += new System.EventHandler(this.btnAsignarPermisoAFamilia_Click);
            // 
            // btnEliminarFamilia
            // 
            this.btnEliminarFamilia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnEliminarFamilia.Location = new System.Drawing.Point(106, 17);
            this.btnEliminarFamilia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEliminarFamilia.Name = "btnEliminarFamilia";
            this.btnEliminarFamilia.Size = new System.Drawing.Size(97, 44);
            this.btnEliminarFamilia.TabIndex = 5;
            this.btnEliminarFamilia.Text = "Eliminar Familia";
            this.btnEliminarFamilia.UseVisualStyleBackColor = true;
            this.btnEliminarFamilia.Click += new System.EventHandler(this.btnEliminarFamilia_Click);
            // 
            // btnModificarFamilia
            // 
            this.btnModificarFamilia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnModificarFamilia.Location = new System.Drawing.Point(4, 66);
            this.btnModificarFamilia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnModificarFamilia.Name = "btnModificarFamilia";
            this.btnModificarFamilia.Size = new System.Drawing.Size(97, 44);
            this.btnModificarFamilia.TabIndex = 4;
            this.btnModificarFamilia.Text = "Modificar Familia";
            this.btnModificarFamilia.UseVisualStyleBackColor = true;
            this.btnModificarFamilia.Click += new System.EventHandler(this.btnModificarFamilia_Click);
            // 
            // btnCrearFamilia
            // 
            this.btnCrearFamilia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCrearFamilia.Location = new System.Drawing.Point(4, 17);
            this.btnCrearFamilia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCrearFamilia.Name = "btnCrearFamilia";
            this.btnCrearFamilia.Size = new System.Drawing.Size(97, 44);
            this.btnCrearFamilia.TabIndex = 3;
            this.btnCrearFamilia.Text = "Crear Familia";
            this.btnCrearFamilia.UseVisualStyleBackColor = true;
            this.btnCrearFamilia.Click += new System.EventHandler(this.btnCrearFamilia_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQuitarFamiliaAPerfil);
            this.groupBox1.Controls.Add(this.btnAsignarFamiliaAPerfil);
            this.groupBox1.Controls.Add(this.btnQuitarPermisoAPerfil);
            this.groupBox1.Controls.Add(this.btnAsignarPermisoAPerfil);
            this.groupBox1.Controls.Add(this.btnEliminarPerfil);
            this.groupBox1.Controls.Add(this.btnModificarPerfil);
            this.groupBox1.Controls.Add(this.btnCrearPerfil);
            this.groupBox1.Location = new System.Drawing.Point(382, 252);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(486, 163);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PERFIL";
            // 
            // btnQuitarFamiliaAPerfil
            // 
            this.btnQuitarFamiliaAPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnQuitarFamiliaAPerfil.Location = new System.Drawing.Point(283, 66);
            this.btnQuitarFamiliaAPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuitarFamiliaAPerfil.Name = "btnQuitarFamiliaAPerfil";
            this.btnQuitarFamiliaAPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnQuitarFamiliaAPerfil.TabIndex = 14;
            this.btnQuitarFamiliaAPerfil.Text = "Quitar Familia";
            this.btnQuitarFamiliaAPerfil.UseVisualStyleBackColor = true;
            this.btnQuitarFamiliaAPerfil.Click += new System.EventHandler(this.btnQuitarFamiliaAPerfil_Click);
            // 
            // btnAsignarFamiliaAPerfil
            // 
            this.btnAsignarFamiliaAPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnAsignarFamiliaAPerfil.Location = new System.Drawing.Point(283, 17);
            this.btnAsignarFamiliaAPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAsignarFamiliaAPerfil.Name = "btnAsignarFamiliaAPerfil";
            this.btnAsignarFamiliaAPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnAsignarFamiliaAPerfil.TabIndex = 13;
            this.btnAsignarFamiliaAPerfil.Text = "Asignar Familia";
            this.btnAsignarFamiliaAPerfil.UseVisualStyleBackColor = true;
            this.btnAsignarFamiliaAPerfil.Click += new System.EventHandler(this.btnAsignarFamiliaAPerfil_Click);
            // 
            // btnQuitarPermisoAPerfil
            // 
            this.btnQuitarPermisoAPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnQuitarPermisoAPerfil.Location = new System.Drawing.Point(384, 66);
            this.btnQuitarPermisoAPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuitarPermisoAPerfil.Name = "btnQuitarPermisoAPerfil";
            this.btnQuitarPermisoAPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnQuitarPermisoAPerfil.TabIndex = 12;
            this.btnQuitarPermisoAPerfil.Text = "Quitar Permiso";
            this.btnQuitarPermisoAPerfil.UseVisualStyleBackColor = true;
            this.btnQuitarPermisoAPerfil.Click += new System.EventHandler(this.btnQuitarPermisoAPerfil_Click);
            // 
            // btnAsignarPermisoAPerfil
            // 
            this.btnAsignarPermisoAPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnAsignarPermisoAPerfil.Location = new System.Drawing.Point(384, 17);
            this.btnAsignarPermisoAPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAsignarPermisoAPerfil.Name = "btnAsignarPermisoAPerfil";
            this.btnAsignarPermisoAPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnAsignarPermisoAPerfil.TabIndex = 11;
            this.btnAsignarPermisoAPerfil.Text = "Asignar Permiso";
            this.btnAsignarPermisoAPerfil.UseVisualStyleBackColor = true;
            this.btnAsignarPermisoAPerfil.Click += new System.EventHandler(this.btnAsignarPermisoAPerfil_Click);
            // 
            // btnEliminarPerfil
            // 
            this.btnEliminarPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnEliminarPerfil.Location = new System.Drawing.Point(4, 115);
            this.btnEliminarPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEliminarPerfil.Name = "btnEliminarPerfil";
            this.btnEliminarPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnEliminarPerfil.TabIndex = 10;
            this.btnEliminarPerfil.Text = "Eliminar Perfil";
            this.btnEliminarPerfil.UseVisualStyleBackColor = true;
            this.btnEliminarPerfil.Click += new System.EventHandler(this.btnEliminarPerfil_Click);
            // 
            // btnModificarPerfil
            // 
            this.btnModificarPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnModificarPerfil.Location = new System.Drawing.Point(4, 66);
            this.btnModificarPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnModificarPerfil.Name = "btnModificarPerfil";
            this.btnModificarPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnModificarPerfil.TabIndex = 9;
            this.btnModificarPerfil.Text = "Modificar Perfil";
            this.btnModificarPerfil.UseVisualStyleBackColor = true;
            this.btnModificarPerfil.Click += new System.EventHandler(this.btnModificarPerfil_Click);
            // 
            // btnCrearPerfil
            // 
            this.btnCrearPerfil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCrearPerfil.Location = new System.Drawing.Point(4, 17);
            this.btnCrearPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCrearPerfil.Name = "btnCrearPerfil";
            this.btnCrearPerfil.Size = new System.Drawing.Size(97, 44);
            this.btnCrearPerfil.TabIndex = 8;
            this.btnCrearPerfil.Text = "Crear Perfil";
            this.btnCrearPerfil.UseVisualStyleBackColor = true;
            this.btnCrearPerfil.Click += new System.EventHandler(this.btnCrearPerfil_Click);
            // 
            // FRMPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(896, 463);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMPerfiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRMPerfiles";
            this.Load += new System.EventHandler(this.FRMPerfiles_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabListado.ResumeLayout(false);
            this.tabListado.PerformLayout();
            this.tabGestion.ResumeLayout(false);
            this.tabGestion.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabListado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstPermisos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView tvFamilias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvPerfiles;
        private System.Windows.Forms.TabPage tabGestion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPerfiles;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFamilias;
        private System.Windows.Forms.ComboBox cmbPermisos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEliminarPermiso;
        private System.Windows.Forms.Button btnModificarPermiso;
        private System.Windows.Forms.Button btnCrearPermiso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnQuitarPermisoAFamilia;
        private System.Windows.Forms.Button btnAsignarPermisoAFamilia;
        private System.Windows.Forms.Button btnEliminarFamilia;
        private System.Windows.Forms.Button btnModificarFamilia;
        private System.Windows.Forms.Button btnCrearFamilia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuitarFamiliaAPerfil;
        private System.Windows.Forms.Button btnAsignarFamiliaAPerfil;
        private System.Windows.Forms.Button btnQuitarPermisoAPerfil;
        private System.Windows.Forms.Button btnAsignarPermisoAPerfil;
        private System.Windows.Forms.Button btnEliminarPerfil;
        private System.Windows.Forms.Button btnModificarPerfil;
        private System.Windows.Forms.Button btnCrearPerfil;
    }
}