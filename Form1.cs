namespace LaboratorioDeColas
{
    public partial class Form1 : Form
    {
        // Lista de pacientes
        private List<Paciente> pacientes = new List<Paciente>();
        // Lista de Pacientes en colas de servicios
        private List<Paciente> pacientesLaboratorio = new List<Paciente>();
        private List<Paciente> pacientesRadiologia = new List<Paciente>();
        private List<Paciente> pacientesRehabilitacion = new List<Paciente>();

        Paciente? pacienteActivo = null;
        Servicio? laboratorio = null;
        Servicio? radiologia = null;
        Servicio? rehabilitacion = null;

        // Inicializar la cola única prioritaria
        OrdenadorProbabilisticoColaUnicaPrioritaria colaUnicaPrioritaria = new OrdenadorProbabilisticoColaUnicaPrioritaria();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializar algunos pacientes de ejemplo
            pacientes.Add(new Paciente("Juan", "Pérez", 61, "M", "O+"));
            pacientes.Add(new Paciente("Ana", "García", 25, "F", "A-"));
            pacientes.Add(new Paciente("Luis", "Martínez", 15, "M", "B+"));
            pacientes.Add(new Paciente("María", "López", 35, "F", "AB-"));
            pacientes.Add(new Paciente("Pedro", "Sánchez", 65, "M", "O-"));
            pacientes.Add(new Paciente("Laura", "Ramírez", 75, "F", "A+"));
            pacientes.Add(new Paciente("Carlos", "Torres", 60, "M", "B-"));
            pacientes.Add(new Paciente("Sofía", "Hernández", 45, "F", "AB+"));
            pacientes.Add(new Paciente("Javier", "Gómez", 66, "M", "O+"));
            pacientes.Add(new Paciente("Isabel", "Díaz", 8, "F", "A-"));
            pacientes.Add(new Paciente("Andrés", "Cruz", 80, "M", "B+"));
            pacientes.Add(new Paciente("Patricia", "Morales", 2, "F", "AB-"));
            pacientes.Add(new Paciente("Fernando", "Reyes", 30, "M", "O-"));
            pacientes.Add(new Paciente("Gabriela", "Jiménez", 50, "F", "A+"));
            pacientes.Add(new Paciente("Diego", "Mendoza", 40, "M", "B-"));
            pacientes.Add(new Paciente("Valeria", "Salazar", 55, "F", "AB+"));
            pacientes.Add(new Paciente("Ricardo", "Paredes", 70, "M", "O+"));
            pacientes.Add(new Paciente("Claudia", "Cordero", 20, "F", "A-"));


            // Agregar los pacientes a la cola única prioritaria
            pacientes = colaUnicaPrioritaria.Reordenar(pacientes);
            //Cargar Servicios
            laboratorio = new Servicio("Laboratorio", 0.2);
            radiologia = new Servicio("Radiologia", 0.3);
            rehabilitacion = new Servicio("Rehabilitacion", 0.5);

            // Mostrar los pacientes en el TextBox
            txtColaRegistro.Clear();
            foreach (var paciente in pacientes)
            {
                txtColaRegistro.AppendText(
                    $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                );
            }
        }

        private Paciente pacienteTemporal = null;

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            if (pacientes.Count > 0)
            {
                pacienteTemporal = pacientes[0];
                pacienteActivo = pacienteTemporal;
                // Mostrar los datos del paciente en los TextBox
                pacientes.RemoveAt(0);
                txtNombres.Text = pacienteTemporal?.Nombres;
                txtApellidos.Text = pacienteTemporal?.Apellidos;
                txtEdad.Text = pacienteTemporal?.Edad.ToString();
                txtSexo.Text = pacienteTemporal?.Sexo;
                txtTipoSangre.Text = pacienteTemporal?.TipoDeSangre;
                txtPrioridad.Text = pacienteTemporal?.PesoPrioridad.ToString();
                //Reordenar la cola
                pacientes = colaUnicaPrioritaria.Reordenar(pacientes);
                // Actualizar el TextBox para mostrar la cola actualizada
                txtColaRegistro.Clear();
                foreach (var paciente in pacientes)
                {
                    txtColaRegistro.AppendText(
                        $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                    );
                }
                // Bloquear el botón de registro hasta que se atienda al paciente
                btnRegistro.Enabled = false;
                
            }
            else
            {
                pacienteTemporal = null;
                MessageBox.Show("No hay más pacientes en la cola.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnServicio_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null && chlbServicios.CheckedItems.Count > 0)
            {
                // Asignar servicios seleccionados
                pacienteActivo.Servicios = new List<Servicio>();

                foreach (var item in chlbServicios.CheckedItems)
                {
                    string servicioNombre = item.ToString();
                    if (servicioNombre == "Laboratorio" && laboratorio != null)
                        pacienteActivo.Servicios.Add(laboratorio);
                    else if (servicioNombre == "Radiologia" && radiologia != null)
                        pacienteActivo.Servicios.Add(radiologia);
                    else if (servicioNombre == "Rehabilitacion" && rehabilitacion != null)
                        pacienteActivo.Servicios.Add(rehabilitacion);
                }
                //Asignacion de paciente activo a colas correspondientes segun el peso de los servicios
                if (pacienteActivo.Servicios.Count > 0)
                {
                    // Asignar el paciente a la cola correspondiente según el servicio seleccionado segun el que tenga mayor peso
                    double mayorPeso = 0;
                    pacienteActivo.Servicios.Sort((x, y) => y.Peso.CompareTo(x.Peso));
                    // Asignar el paciente a la cola correspondiente al primer servicio
                    if (pacienteActivo.Servicios[0].Nombre == "Laboratorio")
                    {
                        pacientesLaboratorio.Add(pacienteActivo);
                        txtColaServicio1.Clear();
                        // Mostrar el paciente en la cola de laboratorio
                        foreach (var paciente in pacientesLaboratorio)
                        {
                            txtColaServicio1.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                    else if (pacienteActivo.Servicios[0].Nombre == "Radiologia")
                    {
                        pacientesRadiologia.Add(pacienteActivo);
                        txtColaServicio2.Clear();
                        // Mostrar el paciente en la cola de radiología
                        foreach (var paciente in pacientesRadiologia)
                        {
                            txtColaServicio2.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                    else if (pacienteActivo.Servicios[0].Nombre == "Rehabilitacion")
                    {
                        pacientesRehabilitacion.Add(pacienteActivo);
                        txtColaServicio3.Clear();
                        // Mostrar el paciente en la cola de rehabilitación
                        foreach (var paciente in pacientesRehabilitacion)
                        {
                            txtColaServicio3.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }

                    // Mostrar los servicios seleccionados en una alerta
                    string serviciosSeleccionados = "Servicios seleccionados:\r\n";
                    foreach (var servicio in pacienteActivo.Servicios)
                    {
                        serviciosSeleccionados += $"{servicio.Nombre}\r\n";
                    }
                    MessageBox.Show(serviciosSeleccionados, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Limpiar campos de txtBox
                    txtNombres.Clear();
                    txtApellidos.Clear();
                    txtEdad.Clear();
                    txtSexo.Clear();
                    txtTipoSangre.Clear();
                    txtPrioridad.Clear();
                    chlbServicios.ClearSelected();
                    // Limpiar el paciente activo
                    pacienteActivo = null;
                    // Habilitar el botón de registro para el siguiente paciente
                    btnRegistro.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún servicio.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Limpiar checkbox de Servicios
                    chlbServicios.ClearSelected();


                }

            }
            else
            {
                MessageBox.Show("No hay pacientes en servicio.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAtencion3_Click(object sender, EventArgs e)
        {
            //Boton de atencion a la cola de rehabilitacion
            if (pacientesRehabilitacion.Count > 0)
            {
                Paciente pacienteAtendido = pacientesRehabilitacion[0];
                pacientesRehabilitacion.RemoveAt(0);
                // Reordenar cola para progreso controlado
                pacientesRehabilitacion = colaUnicaPrioritaria.Reordenar(pacientesRehabilitacion);
                // Mostrar el paciente atendido en un mensaje
                MessageBox.Show($"Paciente atendido en Rehabilitacion:\r\n{pacienteAtendido.Nombres} {pacienteAtendido.Apellidos}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Actualizar la cola de rehabilitación
                txtColaServicio3.Clear();
                foreach (var paciente in pacientesRehabilitacion)
                {
                    txtColaServicio3.AppendText(
                        $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                    );
                }

                // Eliminar servicio ya atendido
                pacienteAtendido.Servicios.RemoveAt(0);
                //si el paciente tiene servicios sin atender con mayor peso
                if (pacienteAtendido.Servicios.Count > 0)
                {
                    // Asignar el paciente a la cola correspondiente según el servicio seleccionado
                    if (pacienteAtendido.Servicios[0].Nombre == "Laboratorio")
                    {
                        pacientesLaboratorio.Add(pacienteAtendido);
                        txtColaServicio1.Clear();
                        // Mostrar el paciente en la cola de laboratorio
                        foreach (var paciente in pacientesLaboratorio)
                        {
                            txtColaServicio1.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                    else if (pacienteAtendido.Servicios[0].Nombre == "Radiologia")
                    {
                        pacientesRadiologia.Add(pacienteAtendido);
                        txtColaServicio2.Clear();
                        // Mostrar el paciente en la cola de radiología
                        foreach (var paciente in pacientesRadiologia)
                        {
                            txtColaServicio2.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("No hay pacientes en la cola de Rehabilitacion.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAtencion2_Click(object sender, EventArgs e)
        {
            //Boton de atencion a la cola de Radiologia
            if (pacientesRadiologia.Count > 0)
            {
                Paciente pacienteAtendido = pacientesRadiologia[0];
                pacientesRadiologia.RemoveAt(0);
                // Reordenar cola para progreso controlado
                pacientesRadiologia = colaUnicaPrioritaria.Reordenar(pacientesRadiologia);
                // Mostrar el paciente atendido en un mensaje
                MessageBox.Show($"Paciente atendido en Radiologia:\r\n{pacienteAtendido.Nombres} {pacienteAtendido.Apellidos}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Actualizar la cola de radiología
                txtColaServicio2.Clear();
                foreach (var paciente in pacientesRadiologia)
                {
                    txtColaServicio2.AppendText(
                        $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                    );
                }
                // Eliminar servicio ya atendido
                pacienteAtendido.Servicios.RemoveAt(0);
                //si el paciente tiene servicios sin atender con mayor peso
                if (pacienteAtendido.Servicios.Count > 0)
                {
                    // Asignar el paciente a la cola correspondiente según el servicio seleccionado
                    if (pacienteAtendido.Servicios[0].Nombre == "Laboratorio")
                    {
                        pacientesLaboratorio.Add(pacienteAtendido);
                        txtColaServicio1.Clear();
                        // Mostrar el paciente en la cola de laboratorio
                        foreach (var paciente in pacientesLaboratorio)
                        {
                            txtColaServicio1.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                    else if (pacienteAtendido.Servicios[0].Nombre == "Rehabilitacion")
                    {
                        pacientesRehabilitacion.Add(pacienteAtendido);
                        txtColaServicio3.Clear();
                        // Mostrar el paciente en la cola de rehabilitación
                        foreach (var paciente in pacientesRehabilitacion)
                        {
                            txtColaServicio3.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay pacientes en la cola de Radiologia.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnAtencion1_Click(object sender, EventArgs e)
        {
            //Boton de atencion a la cola de laboratorio
            if (pacientesLaboratorio.Count > 0)
            {
                Paciente pacienteAtendido = pacientesLaboratorio[0];
                pacientesLaboratorio.RemoveAt(0);
                // Reordenar cola para progreso controlado
                pacientesLaboratorio = colaUnicaPrioritaria.Reordenar(pacientesLaboratorio);
                // Mostrar el paciente atendido en un mensaje
                MessageBox.Show($"Paciente atendido en Laboratorio:\r\n{pacienteAtendido.Nombres} {pacienteAtendido.Apellidos}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Actualizar la cola de laboratorio
                txtColaServicio1.Clear();
                foreach (var paciente in pacientesLaboratorio)
                {
                    txtColaServicio1.AppendText(
                        $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                    );
                }
                // Eliminar servicio ya atendido
                pacienteAtendido.Servicios.RemoveAt(0);
                //si el paciente tiene servicios sin atender con mayor peso
                if (pacienteAtendido.Servicios.Count > 0)
                {
                    // Asignar el paciente a la cola correspondiente según el servicio seleccionado
                    if (pacienteAtendido.Servicios[0].Nombre == "Radiologia")
                    {
                        pacientesRadiologia.Add(pacienteAtendido);
                        txtColaServicio2.Clear();
                        // Mostrar el paciente en la cola de radiología
                        foreach (var paciente in pacientesRadiologia)
                        {
                            txtColaServicio2.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                    else if (pacienteAtendido.Servicios[0].Nombre == "Rehabilitacion")
                    {
                        pacientesRehabilitacion.Add(pacienteAtendido);
                        txtColaServicio3.Clear();
                        // Mostrar el paciente en la cola de rehabilitación
                        foreach (var paciente in pacientesRehabilitacion)
                        {
                            txtColaServicio3.AppendText(
                                $"{paciente.Nombres} {paciente.Apellidos}\r\n"
                            );
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay pacientes en la cola de Laboratorio.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
