﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Data;
using ReportCreator.Entities;
using System.Data.SqlClient;

namespace ReportCreator.Model
{
    public class Repository : IRepository
    {
        SqlCeConnection con = new SqlCeConnection(ConfigurationManager.ConnectionStrings["ReportCreatorConnectionString"].ConnectionString);
        public static IDictionary<int, string> opciones = new Dictionary<int, string>() {
            { 0, "seleccionar"}, 
            {1, "generico"},
            {2, "campaniaElectoral"},
            {3, "campaniaFinanciera"},
            {4, "fiscalizacion"},
            {5, "actividad"},
            {6, "pegatina"},
            {7, "mesa"},
            {8, "prensa"},
            {9, "cotizacion"},
            {10, "circulo"},
            {11, "balance"}};

        public long AgregarInforme(string titulo)
        {
            long resultado = 0;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO informe (estado_id, fecha_creacion, asunto) VALUES (@estado_id, @fecha_creacion, @asunto)", con);
            cmd.Parameters.AddWithValue("@estado_id", 1);
            cmd.Parameters.AddWithValue("@fecha_creacion", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@asunto", titulo);

            try
            {
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY";
                resultado = Convert.ToInt64(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

            return resultado;
        }

        public long AgregarEntrada(long idInforme, string titulo, int tipo)
        {
            long resultado = 0;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO entrada_informe (informe_id, titulo, tipo) VALUES (@informe_id, @titulo, @tipo)", con);
            cmd.Parameters.AddWithValue("@informe_id", idInforme);
            cmd.Parameters.AddWithValue("@titulo", titulo);
            cmd.Parameters.AddWithValue("@tipo", tipo);

            try
            {
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY";
                resultado = Convert.ToInt64(cmd.ExecuteScalar());
                switch (tipo)
                {
                    case 1:
                        AgregarEntradaGenerica(resultado, "");
                        break;
                    case 2:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 3:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 4:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 5:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 6:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 7:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 8:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 9:
                        AgregarEntradaCotizacion(resultado);
                        break;
                    case 10:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                    case 11:
                        //MainWindow.self.Content = new NuevoBorrador();
                        break;
                };
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

            return resultado;
        }

        private void AgregarEntradaCotizacion(long idEntrada)
        {
            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO entrada_cotizacion (id, mes, anio) VALUES (@id, 0, 0)", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);
            int ejecuto = 0;

            try
            {
                ejecuto = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
        }

        private void AgregarEntradaGenerica(long idEntrada, string texto)
        {
            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO entrada_generica (id, data) VALUES (@id, @data)", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);
            cmd.Parameters.AddWithValue("@data", texto);
            int ejecuto = 0;

            try
            {
                ejecuto = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
        }

        public Notificacion GuardarEntradaGenerica(EntradaGenerica entradaGenerica)
        {
            Notificacion respuesta = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("UPDATE entrada_generica SET data=@data WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", entradaGenerica.id);
            cmd.Parameters.AddWithValue("@data", entradaGenerica.data);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

            return respuesta;
        }

        public Informe ObtenerDatosInforme(long id)
        {
            Informe informe = new Informe();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM informe WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    informe.id = rdr.GetInt64(0);
                    informe.estado = rdr.GetInt16(1);
                    informe.fechaCreacion = rdr.GetDateTime(2);
                    informe.asunto = rdr.GetString(3);
                }
            }

            con.Close();

            return informe;
        }

        public IList<Entrada> ObtenerEntradasInforme(long id)
        {
            IList<Entrada> entradas = new List<Entrada>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT ei.id, ei.informe_id, ei.tipo, ei.titulo, et.descripcion 
                FROM entrada_informe ei 
                INNER JOIN entrada_tipo et ON et.id = ei.tipo 
                WHERE ei.informe_id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Entrada entrada = new Entrada();
                    entrada.id = rdr.GetInt64(0);
                    entrada.idInforme = rdr.GetInt64(1);
                    entrada.tipo = new TipoEntrada(rdr.GetInt16(2), rdr.GetString(4));
                    entrada.titulo = rdr.GetString(3);
                    entrada.tipoDescripcion = entrada.tipo.descripcion;
                    entradas.Add(entrada);
                }
            }

            con.Close();

            return entradas;
        }

        public Informe ObtenerInforme(long id)
        {
            Informe informe = ObtenerDatosInforme(id);
            informe.entradas = ObtenerEntradasInforme(id);

            return informe;
        }

        public Notificacion GuardarInforme(long idInforme, string asunto)
        {
            Notificacion respuesta = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("UPDATE informe SET asunto=@asunto WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", idInforme);
            cmd.Parameters.AddWithValue("@asunto", asunto);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

            return respuesta;
        }

        public IList<Informe> ObtenerInformesBorrador()
        {
            IList<Informe> informes = new List<Informe>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"SELECT * FROM informe", con);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Informe informe = new Informe();
                    informe.id = rdr.GetInt64(0);
                    informe.estado = rdr.GetInt16(1);
                    informe.fechaCreacion = rdr.GetDateTime(2);
                    informe.asunto = rdr.GetString(3);
                    informes.Add(informe);
                }
            }

            con.Close();

            return informes;
        }

        public IList<Interno> ObtenerInternos()
        {
            IList<Interno> internos = new List<Interno>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"SELECT * FROM interno WHERE activo = 1", con);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Interno informe = new Interno();
                    informe.id = rdr.GetInt64(0);
                    informe.nombre = rdr.GetString(1);
                    informe.circulo = rdr.GetString(2);
                    internos.Add(informe);
                }
            }

            con.Close();

            return internos;
        }

        public Notificacion GuardarInternos(IList<Interno> internos)
        {
            Notificacion respuesta = new Notificacion();
            var aUpdatear = from interno in internos where interno.id != 0 select interno;
            var aInsertar = from interno in internos where interno.id == 0 select interno;
            IList<long> activos = new List<long>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            foreach (Interno interno in aUpdatear)
            {
                if (interno.nombre.Trim() != String.Empty)
                {
                    activos.Add(interno.id);

                    SqlCeCommand cmd = new SqlCeCommand("UPDATE interno SET nombre=@nombre, circulo=@circulo WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", interno.id);
                    cmd.Parameters.AddWithValue("@nombre", interno.nombre);
                    cmd.Parameters.AddWithValue("@circulo", interno.circulo);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE interno SET activo = 0 WHERE id NOT IN " + "(" + String.Join(",", activos.Select(x => x.ToString()).ToArray()) + ")", con);
            try
            {
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            foreach (Interno interno in aInsertar)
            {
                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO interno (nombre, circulo) VALUES (@nombre, @circulo)", con);
                cmd.Parameters.AddWithValue("@nombre", interno.nombre);
                cmd.Parameters.AddWithValue("@circulo", interno.circulo);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
            }

            return respuesta;
        }

        public Notificacion GuardarEntradaCotizacion(EntradaCotizacion cotizacion)
        {
            Notificacion respuesta = new Notificacion();
            IList<long> activos = new List<long>();

            var aUpdatear = from cotizacionInterno in cotizacion.cotizacionesInternos where cotizacionInterno.id != 0 select cotizacionInterno;
            var aInsertar = from cotizacionInterno in cotizacion.cotizacionesInternos where cotizacionInterno.id == 0 select cotizacionInterno;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE entrada_cotizacion SET mes = @mes, anio = @anio WHERE id = @id", con);
            cmd2.Parameters.AddWithValue("@id", cotizacion.id);
            cmd2.Parameters.AddWithValue("@mes", cotizacion.mes);
            cmd2.Parameters.AddWithValue("@anio", cotizacion.anio);

            try
            {
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            foreach (CotizacionInterno cotizacionInterno in aUpdatear)
            {
                if (cotizacionInterno != null)
                {
                    SqlCeCommand cmd = new SqlCeCommand("UPDATE cotizacion_interno SET interno = @interno, fecha_ingreso = @fecha_ingreso, pago = @pago, observacion = @observacion WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@interno", cotizacionInterno.interno.id);
                    cmd.Parameters.AddWithValue("@fecha_ingreso", cotizacionInterno.fecha_ingreso);
                    cmd.Parameters.AddWithValue("@pago", cotizacionInterno.pago);
                    cmd.Parameters.AddWithValue("@observacion", cotizacionInterno.observacion);

                    activos.Add(cotizacionInterno.id);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            SqlCeCommand cmd3 = new SqlCeCommand(@"
                DELETE FROM cotizacion_interno 
                WHERE entrada_cotizacion_id = " + cotizacion.id + (activos.Count > 0 ? " AND id NOT IN " + "(" + String.Join(",", activos.Select(x => x.ToString()).ToArray()) + ")" : ""), con);

            try
            {
                cmd3.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            foreach (CotizacionInterno cotizacionInterno in aInsertar)
            {
                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO cotizacion_interno (entrada_cotizacion_id, interno, fecha_ingreso, pago, observacion) VALUES (@entrada_cotizacion_id, @interno, @fecha_ingreso, @pago, @observacion)", con);
                cmd.Parameters.AddWithValue("@entrada_cotizacion_id", cotizacion.id);
                cmd.Parameters.AddWithValue("@interno", cotizacionInterno.interno.id);
                cmd.Parameters.AddWithValue("@fecha_ingreso", cotizacionInterno.fecha_ingreso);
                cmd.Parameters.AddWithValue("@pago", cotizacionInterno.pago);
                cmd.Parameters.AddWithValue("@observacion", cotizacionInterno.observacion);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
            }

            return respuesta;
        }

        public EntradaGenerica ObtenerEntradaGenerica(long idEntrada)
        {
            EntradaGenerica entradaGenerica = new EntradaGenerica();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT eg.id, eg.data, ei.informe_id, ei.titulo, ei.informe_id
                FROM entrada_generica eg 
                INNER JOIN entrada_informe ei ON ei.id=eg.id 
                WHERE ei.id=@id", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    entradaGenerica.id = rdr.GetInt64(0);
                    entradaGenerica.data = rdr.GetString(1);
                    entradaGenerica.idInforme = rdr.GetInt64(2);
                    entradaGenerica.titulo = rdr.GetString(3);
                    entradaGenerica.idInforme = rdr.GetInt64(4);
                }
            }

            con.Close();

            return entradaGenerica;
        }

        public EntradaCotizacion ObtenerEntradaCotizacion(long idEntrada)
        {
            EntradaCotizacion entradaCotizacion = new EntradaCotizacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT ec.id, ec.mes, ec.anio, ei.informe_id, ei.titulo, ei.informe_id
                FROM entrada_cotizacion ec
                INNER JOIN entrada_informe ei ON ei.id = ec.id 
                WHERE ei.id=@id", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    entradaCotizacion.id = rdr.GetInt64(0);
                    entradaCotizacion.mes = rdr.GetInt32(1);
                    entradaCotizacion.anio = rdr.GetInt32(2);
                    entradaCotizacion.idInforme = rdr.GetInt64(3);
                    entradaCotizacion.cotizacionesInternos = new List<CotizacionInterno>();
                }
            }

            SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM cotizacion_interno WHERE entrada_cotizacion_id=@entrada_cotizacion_id", con);
            cmd2.Parameters.AddWithValue("@entrada_cotizacion_id", idEntrada);

            using (SqlCeDataReader rdr = cmd2.ExecuteReader())
            {
                while (rdr.Read())
                {
                    CotizacionInterno cotizacionInterno = new CotizacionInterno();
                    cotizacionInterno.id = rdr.GetInt64(0);
                    cotizacionInterno.interno = ObtenerInterno(rdr.GetInt64(2), false);
                    cotizacionInterno.fecha_ingreso = rdr.GetDateTime(3);
                    cotizacionInterno.pago = rdr.GetInt32(4);
                    cotizacionInterno.observacion = rdr.GetString(5);
                    entradaCotizacion.cotizacionesInternos.Add(cotizacionInterno);
                }
            }

            con.Close();

            return entradaCotizacion;
        }

        private Interno ObtenerInterno(long internoId, bool closeConnection)
        {
            Interno interno = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM interno 
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", internoId);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    interno = new Interno();
                    interno.id = rdr.GetInt64(0);
                    interno.nombre = rdr.GetString(1);
                    interno.circulo = rdr.GetString(2);
                }
            }

            if (closeConnection)
                con.Close();

            return interno;
        }
    }
}
