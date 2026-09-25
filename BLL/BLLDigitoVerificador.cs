using DAL;
using SERVICES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLDigitoVerificador
    {
        private const string TIPO_DVV = "DVV";
        private const string TIPO_DVH = "DVH";
        private const string TIPO_FILA = "FILA";
        private const string TOTAL_DVH = "TOTAL_DVH";
        private const string PREFIJO_FILA = "PK:";
        private const string MARCADOR_TABLA_VACIA = "__TABLA_VACIA__";

        private readonly MP_DIGITOVERIFICADOR mp = new MP_DIGITOVERIFICADOR();
        private readonly SERVICESDigitoVerificador servicio = new SERVICESDigitoVerificador();

        private readonly List<ConfiguracionDV> tablas = new List<ConfiguracionDV>
        {
            new ConfiguracionDV
            {
                NombreTabla = "Usuario",
                Columnas = new List<string>
                {
                    "DNI", "Nombre", "Apellido", "Login", "Contraseña", "Rol",
                    "Mail", "Bloqueado", "Activo", "Contador", "Idioma"
                },
                ColumnasPK = new List<string> { "DNI" },
                ColumnasOrden = new List<string> { "DNI" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "Perfil",
                Columnas = new List<string> { "ID", "Nombre", "Descripcion" },
                ColumnasPK = new List<string> { "ID" },
                ColumnasOrden = new List<string> { "ID" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "Permiso",
                Columnas = new List<string> { "ID", "Nombre", "Descripcion" },
                ColumnasPK = new List<string> { "ID" },
                ColumnasOrden = new List<string> { "ID" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "Familia",
                Columnas = new List<string> { "ID", "Nombre", "Descripcion" },
                ColumnasPK = new List<string> { "ID" },
                ColumnasOrden = new List<string> { "ID" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "PERFILxFAMILIA",
                Columnas = new List<string> { "ID_P", "ID_F" },
                ColumnasPK = new List<string> { "ID_P", "ID_F" },
                ColumnasOrden = new List<string> { "ID_P", "ID_F" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "PERFILxPERMISO",
                Columnas = new List<string> { "ID_PF", "ID_PM" },
                ColumnasPK = new List<string> { "ID_PF", "ID_PM" },
                ColumnasOrden = new List<string> { "ID_PF", "ID_PM" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "PERMISOxFAMILIA",
                Columnas = new List<string> { "ID_P", "ID_F" },
                ColumnasPK = new List<string> { "ID_P", "ID_F" },
                ColumnasOrden = new List<string> { "ID_P", "ID_F" }
            },

            new ConfiguracionDV
            {
                NombreTabla = "FAMILIAxFAMILIA",
                Columnas = new List<string> { "ID_F", "ID_F2" },
                ColumnasPK = new List<string> { "ID_F", "ID_F2" },
                ColumnasOrden = new List<string> { "ID_F", "ID_F2" }
            }
        };

        public ResultadoIntegridad Verificar()
        {
            ResultadoIntegridad resultado = new ResultadoIntegridad();

            if (!InfraestructuraCreada(resultado))
                return resultado;

            foreach (ConfiguracionDV config in tablas)
            {
                DataTable datos = mp.ObtenerTablaParaDV(config.NombreTabla, config.Columnas, config.ColumnasOrden);
                DataTable guardados = mp.ObtenerDigitosGuardados(config.NombreTabla);

                if (guardados.Rows.Count == 0)
                {
                    resultado.Correcto = false;
                    resultado.RequiereInicializacion = true;
                    resultado.Mensaje = "Los Dígitos Verificadores todavía no fueron generados para todas las tablas protegidas. Debe recalcularlos para inicializar la integridad.";
                    return resultado;
                }

                if (!TieneDigitosDeFila(guardados))
                {
                    VerificarDVHTabla(config, datos, resultado);
                    VerificarDVVTabla(config, datos, guardados, resultado);

                    if (!resultado.Correcto)
                    {
                        resultado.RequiereInicializacion = false;

                        if (string.IsNullOrWhiteSpace(resultado.Mensaje))
                            resultado.Mensaje = "Se detectaron inconsistencias en la integridad de la base de datos.";

                        return resultado;
                    }

                    resultado.Correcto = false;
                    resultado.RequiereInicializacion = true;
                    resultado.Mensaje = "Los Dígitos Verificadores fueron generados con una versión anterior y no contienen el detalle por fila. Recalcule los DV una vez para inicializar la detección de altas, bajas y modificaciones.";
                    return resultado;
                }

                bool hayCambiosDeFila = VerificarFilasTabla(config, datos, guardados, resultado);

                if (!hayCambiosDeFila)
                {
                    VerificarDVHTabla(config, datos, resultado);
                    VerificarDVVTabla(config, datos, guardados, resultado);
                }
            }

            if (!resultado.Correcto && string.IsNullOrWhiteSpace(resultado.Mensaje))
                resultado.Mensaje = "Se detectaron inconsistencias en la integridad de la base de datos.";

            return resultado;
        }

        public ResultadoIntegridad Recalcular()
        {
            ResultadoIntegridad resultado = new ResultadoIntegridad();

            if (!InfraestructuraCreada(resultado))
                return resultado;

            foreach (ConfiguracionDV config in tablas)
            {
                RecalcularTabla(config);
            }

            resultado.Correcto = true;
            resultado.Mensaje = "Los Dígitos Verificadores fueron recalculados correctamente.";
            return resultado;
        }

        public void RecalcularSeguridad()
        {
            ResultadoIntegridad resultado = Recalcular();

            if (!resultado.Correcto)
                throw new Exception(resultado.Mensaje);
        }

        public void RecalcularUsuario()
        {
            RecalcularSeguridad();
        }

        private bool InfraestructuraCreada(ResultadoIntegridad resultado)
        {
            if (!mp.ExisteTabla("DigitoVerificador"))
            {
                resultado.Correcto = false;
                resultado.InfraestructuraCreada = false;
                resultado.Mensaje = "No existe la tabla DigitoVerificador. Ejecute primero el script SQL de T08.";
                return false;
            }

            foreach (ConfiguracionDV config in tablas)
            {
                if (!mp.ExisteTabla(config.NombreTabla))
                {
                    resultado.Correcto = false;
                    resultado.InfraestructuraCreada = false;
                    resultado.Mensaje = "No existe la tabla requerida para Dígito Verificador: " + config.NombreTabla;
                    return false;
                }

                if (!mp.ExisteColumna(config.NombreTabla, "DVH"))
                {
                    resultado.Correcto = false;
                    resultado.InfraestructuraCreada = false;
                    resultado.Mensaje = "La tabla " + config.NombreTabla + " no tiene la columna DVH.";
                    return false;
                }

                foreach (string columna in config.Columnas)
                {
                    if (!mp.ExisteColumna(config.NombreTabla, columna))
                    {
                        resultado.Correcto = false;
                        resultado.InfraestructuraCreada = false;
                        resultado.Mensaje = "La tabla " + config.NombreTabla + " no tiene la columna requerida: " + columna;
                        return false;
                    }
                }
            }

            return true;
        }

        private void RecalcularTabla(ConfiguracionDV config)
        {
            DataTable datos = mp.ObtenerTablaParaDV(config.NombreTabla, config.Columnas, config.ColumnasOrden);

            foreach (DataRow fila in datos.Rows)
            {
                long dvh = servicio.CalcularDVH(fila, config.Columnas);
                mp.ActualizarDVHFila(config.NombreTabla, config.ColumnasPK, fila, dvh);
                fila["DVH"] = dvh;
            }

            Dictionary<string, long> valoresVerticales = CalcularValoresTabla(config, datos);
            Dictionary<string, long> valoresFilas = CalcularValoresFilas(config, datos);

            mp.EliminarDigitosTabla(config.NombreTabla);

            if (valoresFilas.Count == 0)
            {
                mp.GuardarDigito(
                    config.NombreTabla,
                    PREFIJO_FILA + MARCADOR_TABLA_VACIA,
                    TIPO_FILA,
                    0);
            }
            else
            {
                foreach (KeyValuePair<string, long> item in valoresFilas)
                {
                    mp.GuardarDigito(config.NombreTabla, PREFIJO_FILA + item.Key, TIPO_FILA, item.Value);
                }
            }

            foreach (KeyValuePair<string, long> item in valoresVerticales)
            {
                string tipo = item.Key == TOTAL_DVH ? TIPO_DVH : TIPO_DVV;
                mp.GuardarDigito(config.NombreTabla, item.Key, tipo, item.Value);
            }
        }

        private bool VerificarFilasTabla(ConfiguracionDV config, DataTable datos, DataTable guardados, ResultadoIntegridad resultado)
        {
            bool hayCambios = false;
            Dictionary<string, long> actuales = CalcularValoresFilas(config, datos);
            Dictionary<string, long> guardadosFilas = ObtenerValoresFilasGuardados(guardados);

            foreach (KeyValuePair<string, long> guardado in guardadosFilas)
            {
                if (!actuales.ContainsKey(guardado.Key))
                {
                    hayCambios = true;
                    resultado.Correcto = false;

                    resultado.Detalles.Add(new DetalleInconsistencia
                    {
                        Tabla = config.NombreTabla,
                        Campo = guardado.Key,
                        Tipo = TIPO_FILA,
                        Operacion = "REGISTRO ELIMINADO",
                        ValorGuardado = guardado.Value,
                        ValorCalculado = 0
                    });

                    continue;
                }

                long valorActual = actuales[guardado.Key];

                if (guardado.Value != valorActual)
                {
                    hayCambios = true;
                    resultado.Correcto = false;

                    resultado.Detalles.Add(new DetalleInconsistencia
                    {
                        Tabla = config.NombreTabla,
                        Campo = guardado.Key,
                        Tipo = TIPO_FILA,
                        Operacion = "REGISTRO MODIFICADO",
                        ValorGuardado = guardado.Value,
                        ValorCalculado = valorActual
                    });
                }
            }

            foreach (KeyValuePair<string, long> actual in actuales)
            {
                if (!guardadosFilas.ContainsKey(actual.Key))
                {
                    hayCambios = true;
                    resultado.Correcto = false;

                    resultado.Detalles.Add(new DetalleInconsistencia
                    {
                        Tabla = config.NombreTabla,
                        Campo = actual.Key,
                        Tipo = TIPO_FILA,
                        Operacion = "REGISTRO AGREGADO",
                        ValorGuardado = 0,
                        ValorCalculado = actual.Value
                    });
                }
            }

            return hayCambios;
        }

        private Dictionary<string, long> CalcularValoresFilas(ConfiguracionDV config, DataTable datos)
        {
            Dictionary<string, long> valores = new Dictionary<string, long>();

            foreach (DataRow fila in datos.Rows)
            {
                string clave = ObtenerIdentificadorFila(config, fila);
                long dvh = servicio.CalcularDVH(fila, config.Columnas);

                if (!valores.ContainsKey(clave))
                    valores.Add(clave, dvh);
            }

            return valores;
        }

        private Dictionary<string, long> ObtenerValoresFilasGuardados(DataTable guardados)
        {
            Dictionary<string, long> valores = new Dictionary<string, long>();

            foreach (DataRow fila in guardados.Rows)
            {
                string tipo = fila["Tipo"].ToString();

                if (tipo != TIPO_FILA)
                    continue;

                string clave = fila["NombreColumna"].ToString();

                if (clave.StartsWith(PREFIJO_FILA))
                    clave = clave.Substring(PREFIJO_FILA.Length);

                if (clave == MARCADOR_TABLA_VACIA)
                    continue;

                if (!valores.ContainsKey(clave))
                    valores.Add(clave, Convert.ToInt64(fila["Valor"]));
            }

            return valores;
        }

        private bool TieneDigitosDeFila(DataTable guardados)
        {
            return guardados.AsEnumerable()
                .Any(x => x["Tipo"].ToString() == TIPO_FILA);
        }

        private void VerificarDVHTabla(ConfiguracionDV config, DataTable datos, ResultadoIntegridad resultado)
        {
            foreach (DataRow fila in datos.Rows)
            {
                long dvhGuardado = Convert.ToInt64(fila["DVH"]);
                long dvhCalculado = servicio.CalcularDVH(fila, config.Columnas);

                if (dvhGuardado != dvhCalculado)
                {
                    resultado.Correcto = false;

                    resultado.Detalles.Add(new DetalleInconsistencia
                    {
                        Tabla = config.NombreTabla,
                        Campo = ObtenerIdentificadorFila(config, fila),
                        Tipo = TIPO_DVH,
                        ValorGuardado = dvhGuardado,
                        ValorCalculado = dvhCalculado
                    });
                }
            }
        }

        private void VerificarDVVTabla(ConfiguracionDV config, DataTable datos, DataTable guardados, ResultadoIntegridad resultado)
        {
            Dictionary<string, long> calculados = CalcularValoresTabla(config, datos);

            foreach (KeyValuePair<string, long> calculado in calculados)
            {
                DataRow guardado = BuscarGuardado(guardados, calculado.Key);

                if (guardado == null)
                {
                    resultado.Correcto = false;

                    resultado.Detalles.Add(new DetalleInconsistencia
                    {
                        Tabla = config.NombreTabla,
                        Campo = calculado.Key,
                        Tipo = calculado.Key == TOTAL_DVH ? TIPO_DVH : TIPO_DVV,
                        ValorGuardado = 0,
                        ValorCalculado = calculado.Value
                    });

                    continue;
                }

                long valorGuardado = Convert.ToInt64(guardado["Valor"]);

                if (valorGuardado != calculado.Value)
                {
                    resultado.Correcto = false;

                    resultado.Detalles.Add(new DetalleInconsistencia
                    {
                        Tabla = config.NombreTabla,
                        Campo = calculado.Key,
                        Tipo = guardado["Tipo"].ToString(),
                        ValorGuardado = valorGuardado,
                        ValorCalculado = calculado.Value
                    });
                }
            }
        }

        private Dictionary<string, long> CalcularValoresTabla(ConfiguracionDV config, DataTable datos)
        {
            Dictionary<string, long> valores = new Dictionary<string, long>();

            for (int i = 0; i < config.Columnas.Count; i++)
            {
                string columna = config.Columnas[i];
                long dvv = servicio.CalcularDVV(datos, columna, i + 1);
                valores.Add(columna, dvv);
            }

            long totalDVH = 0;

            foreach (DataRow fila in datos.Rows)
                totalDVH += servicio.CalcularDVH(fila, config.Columnas);

            valores.Add(TOTAL_DVH, totalDVH);

            return valores;
        }

        private DataRow BuscarGuardado(DataTable guardados, string nombreColumna)
        {
            return guardados.AsEnumerable()
                .FirstOrDefault(x => x["NombreColumna"].ToString() == nombreColumna);
        }

        private string ObtenerIdentificadorFila(ConfiguracionDV config, DataRow fila)
        {
            List<string> partes = new List<string>();

            foreach (string pk in config.ColumnasPK)
                partes.Add(pk + "=" + fila[pk].ToString());

            return string.Join(" / ", partes);
        }

        private class ConfiguracionDV
        {
            public string NombreTabla { get; set; }
            public List<string> Columnas { get; set; }
            public List<string> ColumnasPK { get; set; }
            public List<string> ColumnasOrden { get; set; }
        }
    }
}
