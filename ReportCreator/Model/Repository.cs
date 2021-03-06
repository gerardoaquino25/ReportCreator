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
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using ReportCreator.Entities.UtilityObject;
using System.Collections.ObjectModel;

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

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO informe (estado_id, fecha_creacion, asunto, usuario_id) VALUES (@estado_id, @fecha_creacion, @asunto, @usuario_id)", con);
            cmd.Parameters.AddWithValue("@estado_id", 1);
            cmd.Parameters.AddWithValue("@fecha_creacion", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@asunto", titulo);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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
                        AgregarEntradaCampaniaFinanciera(resultado, null);
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
                        AgregarEntradaPrensa(resultado);
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
        }

        private void AgregarEntradaCampaniaFinanciera(long idEntrada, long? idCampaniaFinanciera)
        {
            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO entrada_campania_financiera (id, campania_financiera_id) VALUES (@id, @campania_financiera_id)", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);
            object param1 = idCampaniaFinanciera;
            if (param1 == null)
                param1 = DBNull.Value;
            cmd.Parameters.AddWithValue("@campania_financiera_id", param1);

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
        }

        public Notificacion GuardarEntradaGenerica(EntradaGenericaUO entradaGenerica)
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
                    entrada.informeId = rdr.GetInt64(1);
                    entrada.tipo = new Tipo(rdr.GetInt16(2), rdr.GetString(4));
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

            SqlCeCommand cmd = new SqlCeCommand(@"SELECT * FROM informe WHERE usuario_id = @usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM interno 
                WHERE activo = 1 AND usuario_id = @usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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
                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO interno (nombre, circulo, usuario_id) VALUES (@nombre, @circulo, @usuario_id)", con);
                cmd.Parameters.AddWithValue("@nombre", interno.nombre);
                cmd.Parameters.AddWithValue("@circulo", interno.circulo);
                cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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

        public Notificacion GuardarEntradaCotizacion(EntradaCotizacionUO cotizacion)
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

        public Notificacion GuardarMailSender(MailSender mailSender)
        {
            Notificacion respuesta = new Notificacion();
            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            string prueba = Decrypt(Encrypt(mailSender.password));

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE mail_sender 
                SET email=@email, puerto=@puerto, smtp=@smtp, password=@password
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", mailSender.id);
            cmd.Parameters.AddWithValue("@email", mailSender.email);
            cmd.Parameters.AddWithValue("@password", Encrypt(mailSender.password));
            cmd.Parameters.AddWithValue("@puerto", mailSender.puerto);
            cmd.Parameters.AddWithValue("@smtp", mailSender.smtp);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            return respuesta;
        }

        public long AgregarMailSender(string email, string password, int puerto, string smtp)
        {
            long resultado = 0;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO mail_sender (email, password, puerto, smtp, usuario_id) VALUES (@email, @password, @puerto, @smtp, @usuario_id)", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", Encrypt(password));
            cmd.Parameters.AddWithValue("@puerto", puerto);
            cmd.Parameters.AddWithValue("@smtp", smtp);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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

        public IList<MailSender> ObtenerMailSenders()
        {
            IList<MailSender> mailSenders = new List<MailSender>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM mail_sender
                WHERE usuario_id = @usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    MailSender mailSender = new MailSender();
                    mailSender.id = rdr.GetInt32(0);
                    mailSender.email = rdr.GetString(1);
                    mailSender.smtp = rdr.GetString(2);
                    mailSender.puerto = rdr.GetInt32(4);
                    mailSenders.Add(mailSender);
                }
            }

            return mailSenders;
        }

        public EntradaGenericaUO ObtenerEntradaGenerica(long idEntrada)
        {
            EntradaGenericaUO entradaGenerica = new EntradaGenericaUO();

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
                    entradaGenerica.informeId = rdr.GetInt64(2);
                    entradaGenerica.titulo = rdr.GetString(3);
                    entradaGenerica.informeId = rdr.GetInt64(4);
                }
            }

            con.Close();

            return entradaGenerica;
        }

        public EntradaCotizacionUO ObtenerEntradaCotizacion(long idEntrada)
        {
            EntradaCotizacionUO entradaCotizacion = new EntradaCotizacionUO();
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
                    entradaCotizacion.informeId = rdr.GetInt64(3);
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

        private Interno ObtenerInterno(long internoId, bool closeConnection = false)
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

        private Externo ObtenerExterno(long externoId, bool closeConnection = false)
        {
            Externo externo = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM externo 
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", externoId);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    externo = new Externo();
                    externo.id = rdr.GetInt64(0);
                    externo.nombre = rdr.GetString(1);
                    externo.internoAsociado = ObtenerInterno(rdr.GetInt64(2), false);
                    externo.observacion = rdr.GetString(3);
                }
            }

            if (closeConnection)
                con.Close();

            return externo;
        }

        public MailSender ObtenerMailSender(int email_id)
        {
            MailSender mailSender = new MailSender();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"SELECT * FROM mail_sender WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", email_id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    mailSender.id = rdr.GetInt32(0);
                    mailSender.email = rdr.GetString(1);
                    mailSender.smtp = rdr.GetString(2);
                    mailSender.password = rdr.GetString(3);
                    mailSender.puerto = rdr.GetInt32(4);
                }
            }

            con.Close();

            return mailSender;
        }

        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");
        private const int keysize = 256;

        private string Encrypt(string pass)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(pass);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes("RO-SOFTWARE-GDA", null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes("RO-SOFTWARE-GDA", null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public Notificacion BorrarMailSender(int id)
        {
            Notificacion respuesta = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("DELETE FROM mail_sender WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

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

        public Notificacion BorrarEntrada(long id, int tipo)
        {
            Notificacion respuesta = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            try
            {
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM entrada_informe WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                //TODO: Ir agregando a medida que se van creando las demas funciones.
                switch (tipo)
                {
                    case 1:
                        cmd = new SqlCeCommand("DELETE FROM entrada_generica WHERE id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        break;
                    case 8:
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCeCommand("DELETE FROM entrada_prensa WHERE id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCeCommand("DELETE FROM prensa WHERE entrada_prensa_id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        break;
                    case 9:
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCeCommand("DELETE FROM entrada_cotizacion WHERE id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCeCommand("DELETE FROM cotizacion_interno WHERE entrada_cotizacion_id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        break;
                }
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

        public Notificacion BorrarInforme(long id)
        {
            Notificacion respuesta = new Notificacion();
            Informe informe = ObtenerInforme(id);

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            try
            {
                foreach (Entrada entrada in informe.entradas)
                    BorrarEntrada((long)entrada.id, entrada.tipo.id);

                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM entrada_informe WHERE informe_id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
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

        public IList<MailReceiver> ObtenerMailReceivers()
        {
            IList<MailReceiver> emailReceivers = new List<MailReceiver>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM mail_receiver 
                WHERE activo = 1 AND usuario_id = @usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    MailReceiver emailReceiver = new MailReceiver();
                    emailReceiver.id = rdr.GetInt32(0);
                    emailReceiver.email = rdr.GetString(1);
                    emailReceiver.descripcion = !rdr.IsDBNull(2) ? rdr.GetString(2) : "";
                    emailReceiver.nombre = !rdr.IsDBNull(3) ? rdr.GetString(3) : "";
                    emailReceivers.Add(emailReceiver);
                }
            }

            return emailReceivers;
        }

        public Notificacion GuardarMailReceivers(IList<MailReceiver> receivers)
        {
            Notificacion respuesta = new Notificacion();
            var aUpdatear = from interno in receivers where interno.id != 0 select interno;
            var aInsertar = from interno in receivers where interno.id == 0 select interno;
            IList<long> activos = new List<long>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            foreach (MailReceiver emailReceiver in aUpdatear)
            {
                if (emailReceiver.nombre.Trim() != String.Empty)
                {
                    activos.Add(emailReceiver.id);

                    SqlCeCommand cmd = new SqlCeCommand("UPDATE mail_receiver SET email=@email, descripcion=@descripcion, nombre=@nombre WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", emailReceiver.id);
                    cmd.Parameters.AddWithValue("@email", emailReceiver.email);
                    cmd.Parameters.AddWithValue("@descripcion", emailReceiver.descripcion);
                    cmd.Parameters.AddWithValue("@nombre", emailReceiver.nombre);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE mail_receiver SET activo = 0 WHERE id NOT IN " + "(" + String.Join(",", activos.Select(x => x.ToString()).ToArray()) + ")", con);
            try
            {
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            foreach (MailReceiver emailReceiver in aInsertar)
            {
                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO mail_receiver (email, descripcion, nombre) VALUES (@email, @descripcion, @nombre)", con);
                cmd.Parameters.AddWithValue("@email", emailReceiver.email);
                object param1 = emailReceiver.descripcion;
                if (param1 == null)
                    param1 = DBNull.Value;
                cmd.Parameters.AddWithValue("@descripcion", param1);
                object param2 = emailReceiver.nombre;
                if (param1 == null)
                    param1 = DBNull.Value;
                cmd.Parameters.AddWithValue("@nombre", param2);

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

        public IList<CampaniaFinanciera> ObtenerCFs(string orderBy)
        {
            IList<CampaniaFinanciera> campaniasFinancieras = new List<CampaniaFinanciera>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            string orderbyAux = "id";
            switch (orderBy)
            {
                case "NOMBRE":
                    orderbyAux = "nombre";
                    break;
                case "FECHA_CREACION":
                    orderbyAux = "fecha_creacion";
                    break;
            }

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM campania_financiera
                WHERE usuario_id = @usuario_id 
                ORDER BY " + orderbyAux + " DESC", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    CampaniaFinanciera campaniaFinanciera = new CampaniaFinanciera();
                    campaniaFinanciera.id = rdr.GetInt64(0);
                    campaniaFinanciera.nombre = rdr.GetString(1);
                    campaniaFinanciera.fechaCreacion = rdr.GetDateTime(2);
                    campaniasFinancieras.Add(campaniaFinanciera);
                }
            }

            return campaniasFinancieras;
        }

        public CampaniaFinanciera ObtenerCF(long id, bool closeConn)
        {
            CampaniaFinanciera campaniaFinanciera = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM campania_financiera 
                WHERE  id=" + id, con);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    campaniaFinanciera = new CampaniaFinanciera();
                    campaniaFinanciera.id = rdr.GetInt64(0);
                    campaniaFinanciera.nombre = rdr.GetString(1);
                    campaniaFinanciera.fechaCreacion = rdr.GetDateTime(2);
                }
            }

            if (closeConn)
                con.Close();

            return campaniaFinanciera;
        }

        public long AgregarCF(string nombre)
        {
            long resultado = 0;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                INSERT INTO campania_financiera (nombre, usuario_id) 
                VALUES (@nombre, @usuario_id)", con);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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

        public IList<Externo> ObtenerExternos()
        {
            IList<Externo> externos = new List<Externo>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM externo
                WHERE usuario_id = @usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Externo externo = new Externo();
                    externo.id = rdr.GetInt64(0);
                    externo.nombre = rdr.GetString(1);
                    externo.internoAsociado = ObtenerInterno(rdr.GetInt64(2), false);
                    externo.observacion = rdr.GetString(3);
                    externos.Add(externo);
                }
            }

            con.Close();

            return externos;
        }

        public Externo AgregarExterno(string nombre, string observacion, Interno interno)
        {
            Externo externo = new Externo();
            externo.nombre = nombre;
            externo.observacion = observacion;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO externo (nombre, observacion, interno_asociado_id, usuario_id) VALUES (@nombre, @observacion, @interno_asociado_id, @usuario_id)", con);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@interno_asociado_id", interno.id);
            object param1 = observacion;
            if (param1 == null)
                param1 = DBNull.Value;
            cmd.Parameters.AddWithValue("@observacion", param1);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            try
            {
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY";
                externo.id = Convert.ToInt64(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

            return externo;
        }

        public Notificacion AgregarPadronCF(PadronCF padron)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"INSERT INTO campania_financiera_padron (aportante_id, compromiso, tipo_aportante, campania_financiera_id, observacion, entrada_campania_financiera_id) 
                VALUES (@aportante_id, @compromiso, @tipo_aportante, @campania_financiera_id, @observacion, @entrada_campania_financiera_id)", con);
            cmd.Parameters.AddWithValue("@aportante_id", padron.aportanteId);
            cmd.Parameters.AddWithValue("@compromiso", padron.compromiso);
            cmd.Parameters.AddWithValue("@tipo_aportante", padron.tipoAportante);
            cmd.Parameters.AddWithValue("@campania_financiera_id", padron.campaniaFinancieraId);
            cmd.Parameters.AddWithValue("@observacion", padron.observacion);
            cmd.Parameters.AddWithValue("@entrada_campania_financiera_id", padron.entradaCampaniaFinancieraId);

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

            return resultado;
        }

        public Notificacion AgregarAporteCF(AporteCF aporte)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                INSERT INTO campania_financiera_aporte (aportante_id, pago, tipo_aportante, fecha_aporte, campania_financiera_id, observacion, rechazo, entrada_campania_financiera_id) 
                VALUES (@aportante_id, @pago, @tipo_aportante, @fecha_aporte, @campania_financiera_id, @observacion, @rechazo, @entrada_campania_financiera_id)", con);
            cmd.Parameters.AddWithValue("@aportante_id", aporte.aportanteId);
            cmd.Parameters.AddWithValue("@pago", aporte.pago);
            cmd.Parameters.AddWithValue("@tipo_aportante", aporte.tipoAportante);
            cmd.Parameters.AddWithValue("@fecha_aporte", aporte.fechaAporte);
            cmd.Parameters.AddWithValue("@campania_financiera_id", aporte.campaniaFinancieraId);
            cmd.Parameters.AddWithValue("@observacion", aporte.observacion);
            cmd.Parameters.AddWithValue("@rechazo", aporte.rechazo);
            cmd.Parameters.AddWithValue("@entrada_campania_financiera_id", aporte.entradaCampaniaFinancieraId);

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

            return resultado;
        }

        public IList<AporteCF> ObtenerAportesCF(long entradaId, long campaniaFinancieraId)
        {
            IList<AporteCF> aportes = new List<AporteCF>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM campania_financiera_aporte
                WHERE campania_financiera_id=@campania_financiera_id", con);
            cmd.Parameters.AddWithValue("@campania_financiera_id", campaniaFinancieraId);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    AporteCF aporte = new AporteCF();
                    aporte.id = rdr.GetInt64(0);
                    aporte.aportante = ObtenerAportanteCF(rdr.GetInt64(1), rdr.GetInt32(3));
                    aporte.pago = rdr.GetInt32(2);
                    aporte.fechaAporte = rdr.GetDateTime(4);
                    aporte.campaniaFinancieraId = rdr.GetInt64(5);
                    aporte.observacion = rdr.GetString(6);
                    aporte.rechazo = rdr.GetBoolean(7);
                    aporte.entradaCampaniaFinancieraId = rdr.GetInt64(8);
                    aporte.perteneceEntrada = aporte.campaniaFinancieraId.Equals(entradaId);
                    aportes.Add(aporte);
                }
            }

            con.Close();

            return aportes;
        }

        public object ObtenerAportanteCF(long id, int tipo, bool closeConn = false)
        {
            switch (tipo)
            {
                case 1:
                    return ObtenerInterno(id, closeConn);
                case 2:
                    return ObtenerExterno(id, closeConn);

            }

            return null;
        }

        public IList<PadronCF> ObtenerPadronesCF(long entradaId, long campaniaFinancieraId)
        {
            IList<PadronCF> padrones = new List<PadronCF>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM campania_financiera_padron 
                WHERE entrada_campania_financiera_id=@entrada_campania_financiera_id OR campania_financiera_id=@campania_financiera_id
                ORDER BY CASE entrada_campania_financiera_id WHEN @entrada_campania_financiera_id THEN 0 ELSE 1 END DESC", con);
            cmd.Parameters.AddWithValue("@entrada_campania_financiera_id", entradaId);
            cmd.Parameters.AddWithValue("@campania_financiera_id", campaniaFinancieraId);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    PadronCF padron = new PadronCF();
                    padron.id = rdr.GetInt64(0);
                    padron.aportante = ObtenerAportanteCF(rdr.GetInt64(1), rdr.GetInt32(3));
                    padron.compromiso = rdr.GetInt32(2);
                    padron.campaniaFinancieraId = rdr.GetInt64(4);
                    padron.observacion = rdr.GetString(5);
                    padron.entradaCampaniaFinancieraId = rdr.GetInt64(6);
                    padron.perteneceEntrada = padron.entradaCampaniaFinancieraId.Equals(entradaId);
                    padrones.Add(padron);
                }
            }

            con.Close();

            return padrones;
        }

        public EntradaCampaniaFinancieraUO ObtenerEntradaCampaniaFinanciera(long idEntrada)
        {
            EntradaCampaniaFinancieraUO entradaCampaniaFinanciera = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT ei.informe_id, ei.titulo, ecf.campania_financiera_id
                FROM entrada_campania_financiera ecf
                INNER JOIN entrada_informe ei ON ei.id = ecf.id
                WHERE ecf.id = " + idEntrada, con);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    entradaCampaniaFinanciera = new EntradaCampaniaFinancieraUO();
                    entradaCampaniaFinanciera.id = idEntrada;
                    entradaCampaniaFinanciera.informeId = rdr.GetInt64(0);
                    entradaCampaniaFinanciera.titulo = rdr.GetString(1);
                    entradaCampaniaFinanciera.campaniaFinanciera = rdr.IsDBNull(2) ? null : ObtenerCF(rdr.GetInt64(2), false);
                }
            }

            con.Close();

            return entradaCampaniaFinanciera;
        }

        public Notificacion GuardarAporteCF(AporteCF aporte)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE campania_financiera_aporte 
                SET aportante_id = @aportante_id, pago = @pago, tipo_aportante = @tipo_aportante, fecha_aporte = @fecha_aporte, campania_financiera_id = @campania_financiera_id, observacion = @observacion, rechazo = @rechazo, entrada_campania_financiera_id = @entrada_campania_financiera_id 
                 WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", aporte.id);
            cmd.Parameters.AddWithValue("@aportante_id", aporte.aportanteId);
            cmd.Parameters.AddWithValue("@pago", aporte.pago);
            cmd.Parameters.AddWithValue("@tipo_aportante", aporte.tipoAportante);
            cmd.Parameters.AddWithValue("@fecha_aporte", aporte.fechaAporte);
            cmd.Parameters.AddWithValue("@campania_financiera_id", aporte.campaniaFinancieraId);
            cmd.Parameters.AddWithValue("@observacion", aporte.observacion);
            cmd.Parameters.AddWithValue("@rechazo", aporte.rechazo);
            cmd.Parameters.AddWithValue("@entrada_campania_financiera_id", aporte.entradaCampaniaFinancieraId);

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

            return resultado;
        }

        public Notificacion GuardarPadronCF(PadronCF padron)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"UPDATE campania_financiera_padron 
                SET aportante_id = @aportante_id, compromiso = @compromiso, tipo_aportante = @tipo_aportante, campania_financiera_id = @campania_financiera_id, observacion = @observacion, entrada_campania_financiera_id = @entrada_campania_financiera_id
                WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", padron.id);
            cmd.Parameters.AddWithValue("@aportante_id", padron.aportanteId);
            cmd.Parameters.AddWithValue("@compromiso", padron.compromiso);
            cmd.Parameters.AddWithValue("@tipo_aportante", padron.tipoAportante);
            cmd.Parameters.AddWithValue("@campania_financiera_id", padron.campaniaFinancieraId);
            cmd.Parameters.AddWithValue("@observacion", padron.observacion);
            cmd.Parameters.AddWithValue("@entrada_campania_financiera_id", padron.entradaCampaniaFinancieraId);

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

            return resultado;
        }

        public Notificacion GuardarEntradaCampaniaFinanciera(EntradaCampaniaFinancieraUO entradaCampaniaFinanciera)
        {
            Notificacion respuesta = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE entrada_campania_financiera 
                SET campania_financiera_id=@campania_financiera_id 
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", entradaCampaniaFinanciera.id);
            cmd.Parameters.AddWithValue("@campania_financiera_id", entradaCampaniaFinanciera.campaniaFinanciera.id);

            try
            {
                cmd.ExecuteNonQuery();
                cmd = new SqlCeCommand(@"
                UPDATE entrada_informe 
                SET titulo=@titulo 
                WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", entradaCampaniaFinanciera.id);
                cmd.Parameters.AddWithValue("@titulo", entradaCampaniaFinanciera.titulo);
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

        public bool ExisteUsuario(string username)
        {
            return ObtenerUsuarioByUsername(username) != null;
        }

        public Usuario ObtenerUsuarioByUsername(string username, bool closeConn = false)
        {
            Usuario usuario = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT *
                FROM usuario
                WHERE username = @username", con);
            cmd.Parameters.Add("@username", username);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    usuario = new Usuario();
                    usuario.id = rdr.GetInt32(0);
                    usuario.username = rdr.GetString(1);
                    usuario.email = rdr.GetString(2);
                }
            }

            if (closeConn)
                con.Close();

            return usuario;
        }

        public Usuario ObtenerUsuarioByEmail(string email, bool closeConn = false)
        {
            Usuario usuario = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT *
                FROM usuario
                WHERE email = @email", con);
            cmd.Parameters.Add("@email", email);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    usuario = new Usuario();
                    usuario.id = rdr.GetInt32(0);
                    usuario.username = rdr.GetString(1);
                    usuario.email = rdr.GetString(2);
                }
            }

            if (closeConn)
                con.Close();

            return usuario;
        }

        public Notificacion AgregarUsuario(string username, string email, string password)
        {
            Notificacion resultado = new Notificacion();

            if (!ExisteUsuario(username))
            {

                if (!con.State.Equals(ConnectionState.Open))
                    con.Open();

                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO usuario (username, email, password) VALUES (@username, @email, @password)", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", Encrypt(password));

                try
                {
                    cmd.ExecuteNonQuery();
                    resultado.Detalle = Notificacion.USUARIO_CREADO;
                }
                catch (Exception e)
                {
                    resultado.Detalle = Notificacion.USUARIO_CREADO_KO;
                }
                finally
                {
                    con.Close();
                }
            }
            else
                resultado.Detalle = Notificacion.USUARIO_YA_EXISTENTE;

            return resultado;
        }

        public Notificacion CambiarContrasenia(int usuarioId, string nuevaContrasenia)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE usuario 
                SET password = @password
                WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", usuarioId);
            cmd.Parameters.AddWithValue("@password", Encrypt(nuevaContrasenia));

            try
            {
                cmd.ExecuteNonQuery();
                resultado.Detalle = Notificacion.CAMBIO_CONTRASENIA;
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.CAMBIO_CONTRASENIA_KO;
            }
            finally
            {
                con.Close();
            }

            return resultado;

        }

        public Notificacion EnviarEmail(string emailFrom, string password, string smtp, string puerto, string emailTo, string subject, string html)
        {
            Notificacion resultado = new Notificacion();

            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(puerto);
                client.Host = smtp;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(emailFrom, password);

                MailMessage mm = new MailMessage(emailFrom, emailTo, subject, html);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
                resultado.Detalle = Notificacion.EMAIL_ENVIADO;
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.EMAIL_ENVIADO_KO;
            }

            return resultado;
        }

        public Notificacion RecuperarContraseña(string username)
        {
            Usuario usuario = ObtenerUsuarioByUsername(username);

            if (usuario != null)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var nuevaContrasenia = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                if (CambiarContrasenia(usuario.id, nuevaContrasenia).Detalle == Notificacion.CAMBIO_CONTRASENIA_KO)
                    return new Notificacion() { Detalle = Notificacion.CAMBIO_CONTRASENIA_KO };

                return EnviarEmail("gerardo.aquino25@gmail.com", "17244793", "smtp.googlemail.com", "587", usuario.email, "Recuperar Contraseña", "Nueva contraseña:" + nuevaContrasenia);
            }
            else
            {
                return new Notificacion() { Detalle = Notificacion.USUARIO_NO_ENCONTRADO };
            }
        }

        public Notificacion GuardarOpcionesGenerales(IList<OpcionGeneral> listaOpcionesGenerales)
        {
            Notificacion resultado = new Notificacion();

            foreach (OpcionGeneral opcion in listaOpcionesGenerales)
            {
                switch (opcion.nombre)
                {
                    case OpcionGeneral.ASUNTO_DEFAULT:
                        break;
                    case OpcionGeneral.CAMBIO_CONTRASENIA:
                        break;

                }
            }

            return resultado;
        }

        private void AgregarEntradaPrensa(long idEntrada)
        {
            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO entrada_prensa (id) VALUES (@id)", con);
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

        public long AgregarActividad(Actividad actividad)
        {
            long resultado = 0;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO actividad (nombre, detalle, fecha) VALUES (@nombre, @detalle, @fecha)", con);
            cmd.Parameters.AddWithValue("@nombre", actividad.nombre);
            cmd.Parameters.AddWithValue("@detalle", actividad.detalle);
            cmd.Parameters.AddWithValue("@fecha", actividad.fecha);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

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

        public Notificacion AgregarEntradaPrensa(EntradaPrensaUO prensaUO)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            long entradaId = AgregarEntrada((long)prensaUO.informeId, prensaUO.titulo, 8);
            if (entradaId != 0)
            {
                foreach (PrensaOB prensa in prensaUO.prensas)
                {
                    prensa.entradaPrensaId = entradaId;
                    if (prensa.id == null)
                        AgregarPrensa(prensa);
                    else if (prensa.modificado)
                        GuardarPrensa(prensa);
                }

                foreach (SuscripcionOB suscripcion in prensaUO.suscripciones)
                {
                    suscripcion.entradaPrensaId = entradaId;
                    if (suscripcion.id == null)
                        AgregarSuscripcion(suscripcion);
                    else if (suscripcion.modificado)
                        GuardarSuscripcion(suscripcion);
                }

                con.Close();
            }
            else
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;

            return resultado;
        }

        public Notificacion GuardarEntradaPrensa(EntradaPrensaUO prensaUO)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("UPDATE entrada_informe SET titulo=@titulo WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", prensaUO.id);
            cmd.Parameters.AddWithValue("@titulo", prensaUO.titulo);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            foreach (PrensaOB prensa in prensaUO.prensas)
            {
                if (prensa.id == null)
                    AgregarPrensa(prensa);
                else if (prensa.modificado)
                    GuardarPrensa(prensa);
            }

            foreach (SuscripcionOB suscripcion in prensaUO.suscripciones)
            {
                if (suscripcion.id == null)
                    AgregarSuscripcion(suscripcion);
                else if (suscripcion.modificado)
                    GuardarSuscripcion(suscripcion);
            }

            con.Close();

            return resultado;
        }

        public Notificacion AgregarSuscripcion(SuscripcionOB suscripcion, bool closeConnection = false)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                INSERT INTO suscripcion (tipo_suscripcion, tipo_suscriptor, suscriptor_id, fecha_suscripcion, fecha_vencimiento, observacion, usuario_id, entrada_prensa_id)
                VALUES (@tipo_suscripcion, @tipo_suscriptor, @suscriptor_id, @fecha_suscripcion, @fecha_vencimiento, @observacion, @usuario_id, @entrada_prensa_id)", con);
            cmd.Parameters.AddWithValue("@tipo_suscripcion", suscripcion.tipoSuscripcion);
            int param1 = suscripcion.suscriptor.GetType() == typeof(Interno) ? 1 : 2;
            cmd.Parameters.AddWithValue("@tipo_suscriptor", param1);
            long param2 = suscripcion.suscriptor.GetType() == typeof(Interno) ? ((Interno)suscripcion.suscriptor).id : ((Externo)suscripcion.suscriptor).id;
            cmd.Parameters.AddWithValue("@suscriptor_id", param2);
            cmd.Parameters.AddWithValue("@fecha_suscripcion", suscripcion.fechaSuscripcion);
            cmd.Parameters.AddWithValue("@fecha_vencimiento", suscripcion.fechaVencimiento);
            cmd.Parameters.AddWithValue("@observacion", suscripcion.observacion);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);
            cmd.Parameters.AddWithValue("@entrada_prensa_id", suscripcion.entradaPrensaId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                if (closeConnection)
                    con.Close();
            }

            return resultado;
        }

        public Notificacion AgregarPrensa(PrensaOB prensa, bool closeConnection = false)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                INSERT INTO prensa (entrada_prensa_id, tipo_pasaje, tipo_comprador, interno_id, prensa_numero, aporte, comprador_id, actividad_id, observacion) 
                VALUES (@entrada_prensa_id, @tipo_pasaje, @tipo_comprador, @interno_id, @prensa_numero, @aporte, @comprador_id, @actividad_id, @observacion)", con);
            cmd.Parameters.AddWithValue("@entrada_prensa_id", prensa.entradaPrensaId);
            cmd.Parameters.AddWithValue("@tipo_pasaje", prensa.tipoPasaje.id);
            int param1 = prensa.comprador == null || prensa.comprador.GetType() == typeof(Externo) ? 2 : 1;
            cmd.Parameters.AddWithValue("@tipo_comprador", param1);
            cmd.Parameters.AddWithValue("@interno_id", prensa.interno.id);
            cmd.Parameters.AddWithValue("@prensa_numero", prensa.prensaNumero);
            cmd.Parameters.AddWithValue("@aporte", prensa.aporte);
            object param2 = prensa.comprador;
            if (param2 != null)
            {
                if (param2.GetType() == typeof(Interno))
                    param2 = ((Interno)param2).id;
                else
                    param2 = ((Externo)param2).id;
            }
            else
                param2 = DBNull.Value;
            cmd.Parameters.AddWithValue("@comprador_id", param2);
            object param3 = prensa.actividadId;
            if (param3 == null)
                param3 = DBNull.Value;
            cmd.Parameters.AddWithValue("@actividad_id", param3);
            cmd.Parameters.AddWithValue("@observacion", prensa.observacion);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                if (closeConnection)
                    con.Close();
            }

            return resultado;
        }

        public Notificacion GuardarPrensa(PrensaOB prensa, bool closeConnection = false)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE prensa SET tipo_pasaje = @tipo_pasaje, tipo_comprador = @tipo_comprador, interno_id = @interno_id, 
                prensa_numero = @prensa_numero, aporte = @aporte, comprador_id = @comprador_id, actividad_id = @actividad_id, observacion = @observacion 
                WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", prensa.id);
            cmd.Parameters.AddWithValue("@tipo_pasaje", prensa.tipoPasaje.id);
            int param1 = prensa.comprador == null || prensa.comprador.GetType() == typeof(Externo) ? 2 : 1;
            cmd.Parameters.AddWithValue("@tipo_comprador", param1);
            cmd.Parameters.AddWithValue("@interno_id", prensa.interno.id);
            cmd.Parameters.AddWithValue("@prensa_numero", prensa.prensaNumero);
            cmd.Parameters.AddWithValue("@aporte", prensa.aporte);
            object param2 = prensa.comprador;
            if (param2 != null)
            {
                if (param2.GetType() == typeof(Interno))
                    param2 = ((Interno)param2).id;
                else
                    param2 = ((Externo)param2).id;
            }
            else
                param2 = DBNull.Value;
            cmd.Parameters.AddWithValue("@comprador_id", param2);
            object param3 = prensa.actividadId;
            if (param3 == null)
                param3 = DBNull.Value;
            cmd.Parameters.AddWithValue("@actividad_id", param3);
            cmd.Parameters.AddWithValue("@observacion", prensa.observacion);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                if (closeConnection)
                    con.Close();
            }

            return resultado;
        }

        public Notificacion GuardarSuscripcion(SuscripcionOB suscripcion, bool closeConnection = false)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE suscripcion SET tipo_suscripcion = @tipo_suscripcion, tipo_suscriptor = @tipo_suscriptor, suscriptor_id = @suscriptor_id, 
                fecha_suscripcion = @fecha_suscripcion, fecha_vencimiento = @fecha_vencimiento, observacion = @observacion, usuario_id = @usuario_id, entrada_prensa_id = @entrada_prensa_id) 
                WHERE id = @id)", con);
            cmd.Parameters.AddWithValue("@id", suscripcion.id);
            cmd.Parameters.AddWithValue("@tipo_suscripcion", suscripcion.tipoSuscripcion);
            int param1 = suscripcion.suscriptor.GetType() == typeof(Interno) ? 1 : 2;
            cmd.Parameters.AddWithValue("@tipo_suscriptor", param1);
            long param2 = suscripcion.suscriptor.GetType() == typeof(Interno) ? ((Interno)suscripcion.suscriptor).id : ((Externo)suscripcion.suscriptor).id;
            cmd.Parameters.AddWithValue("@suscriptor_id", param2);
            cmd.Parameters.AddWithValue("@fecha_suscripcion", suscripcion.fechaSuscripcion);
            cmd.Parameters.AddWithValue("@fecha_vencimiento", suscripcion.fechaVencimiento);
            cmd.Parameters.AddWithValue("@observacion", suscripcion.observacion);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);
            cmd.Parameters.AddWithValue("@entrada_prensa_id", suscripcion.entradaPrensaId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                if (closeConnection)
                    con.Close();
            }

            return resultado;
        }

        public PrensaOB ObtenerPrensa(long id, bool closeConnection = true)
        {
            PrensaOB prensa = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT p.id, p.entrada_prensa_id, p.tipo_pasaje, p.tipo_comprador, 
                    p.interno_id, p.prensa_numero, p.aporte, p.comprador_id, p.actividad_id, 
                    p.observacion, ppt.descripcion
                FROM prensa p
                INNER JOIN prensa_pasaje_tipo ppt ON p.tipo_pasaje = ppt.id
                WHERE p.id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    prensa = new PrensaOB();
                    prensa.id = rdr.GetInt64(0);
                    prensa.entradaPrensaId = rdr.GetInt64(1);
                    prensa.tipoPasaje = new Tipo(rdr.GetInt32(2), rdr.GetString(10));
                    if (rdr.GetInt32(3) == 1)
                        prensa.comprador = ObtenerInterno(rdr.GetInt64(7));
                    else if (!rdr.IsDBNull(7))
                        prensa.comprador = ObtenerExterno(rdr.GetInt64(7));
                    prensa.interno = ObtenerInterno(rdr.GetInt64(4), false);
                    prensa.prensaNumero = rdr.GetInt32(5);
                    prensa.aporte = rdr.GetInt32(6);
                    prensa.actividadId = rdr.GetInt64(8);
                    prensa.observacion = rdr.GetString(9);
                }
            }

            if (closeConnection)
                con.Close();

            return prensa;
        }

        public SuscripcionOB ObtenerSuscripcion(long id, bool closeConnection = true)
        {
            SuscripcionOB suscripcion = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM suscripcion 
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    suscripcion = new SuscripcionOB();
                    suscripcion.id = rdr.GetInt64(0);
                    suscripcion.tipoSuscripcion = rdr.GetInt32(1);
                    if (rdr.GetInt32(2) == 1)
                        suscripcion.suscriptor = ObtenerInterno(rdr.GetInt64(3));
                    else if (!rdr.IsDBNull(3))
                        suscripcion.suscriptor = ObtenerExterno(rdr.GetInt64(3));
                    suscripcion.fechaSuscripcion = rdr.GetDateTime(4);
                    suscripcion.fechaVencimiento = rdr.GetDateTime(5);
                    suscripcion.observacion = rdr.GetString(6);
                    suscripcion.usuarioId = rdr.GetInt32(7);
                    suscripcion.entradaPrensaId = rdr.GetInt64(8);
                }
            }

            if (closeConnection)
                con.Close();

            return suscripcion;
        }

        public ObservableCollection<PrensaOB> ObtenerPrensasByEntradaId(long entradaId, bool closeConnection = true)
        {
            ObservableCollection<PrensaOB> prensas = new ObservableCollection<PrensaOB>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT p.id, p.entrada_prensa_id, p.tipo_pasaje, p.tipo_comprador, 
                    p.interno_id, p.prensa_numero, p.aporte, p.comprador_id, p.actividad_id, 
                    p.observacion, ppt.descripcion
                FROM prensa p
                INNER JOIN prensa_pasaje_tipo ppt ON p.tipo_pasaje = ppt.id
                WHERE p.entrada_prensa_id=@entrada_prensa_id", con);
            cmd.Parameters.AddWithValue("@entrada_prensa_id", entradaId);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    PrensaOB prensa = new PrensaOB();
                    prensa.id = rdr.GetInt64(0);
                    prensa.entradaPrensaId = rdr.GetInt64(1);
                    prensa.tipoPasaje = prensa.tipoPasaje = new Tipo(rdr.GetInt32(2), rdr.GetString(10));
                    if (rdr.GetInt32(3) == 1)
                        prensa.comprador = ObtenerInterno(rdr.GetInt64(7));
                    else if (!rdr.IsDBNull(7))
                        prensa.comprador = ObtenerExterno(rdr.GetInt64(7));
                    prensa.interno = ObtenerInterno(rdr.GetInt64(4), false);
                    prensa.prensaNumero = rdr.GetInt32(5);
                    prensa.aporte = rdr.GetInt32(6);
                    if (!rdr.IsDBNull(8))
                        prensa.actividadId = rdr.GetInt64(8);
                    prensa.observacion = rdr.GetString(9);
                    prensas.Add(prensa);
                }
            }

            if (closeConnection)
                con.Close();

            return prensas;
        }

        public ObservableCollection<SuscripcionOB> ObtenerSuscripciones(bool closeConnection = true)
        {
            ObservableCollection<SuscripcionOB> suscripciones = new ObservableCollection<SuscripcionOB>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM suscripcion 
                WHERE usuario_id=@usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    SuscripcionOB suscripcion = new SuscripcionOB();
                    suscripcion.id = rdr.GetInt64(0);
                    suscripcion.tipoSuscripcion = rdr.GetInt32(1);
                    if (rdr.GetInt32(2) == 1)
                        suscripcion.suscriptor = ObtenerInterno(rdr.GetInt64(3));
                    else if (!rdr.IsDBNull(3))
                        suscripcion.suscriptor = ObtenerExterno(rdr.GetInt64(3));
                    suscripcion.fechaSuscripcion = rdr.GetDateTime(4);
                    suscripcion.fechaVencimiento = rdr.GetDateTime(5);
                    suscripcion.observacion = rdr.GetString(6);
                    suscripcion.usuarioId = rdr.GetInt32(7);
                    suscripciones.Add(suscripcion);
                }
            }

            if (closeConnection)
                con.Close();

            return suscripciones;
        }

        public Notificacion BorrarPrensa(long id)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                DELETE FROM prensa 
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                con.Close();
            }

            return resultado;
        }

        public Notificacion BorrarPrensasByEntradaId(long entradaId)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                DELETE FROM prensa 
                WHERE entrada_prensa_id=@entrada_prensa_id", con);
            cmd.Parameters.AddWithValue("@entrada_prensa_id", entradaId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                con.Close();
            }

            return resultado;
        }

        public Notificacion BorrarSuscripcion(long id)
        {
            Notificacion resultado = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                DELETE FROM suscripcion 
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado.Detalle = Notificacion.FALTA_ASIGNAR_EXCEPCION;
            }
            finally
            {
                con.Close();
            }

            return resultado;
        }

        public EntradaPrensaUO ObtenerEntradaPrensa(long idEntrada)
        {
            EntradaPrensaUO entradaPrensa = null;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();
            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT ep.id, ei.informe_id, ei.titulo, et.descripcion
                FROM entrada_prensa ep 
                INNER JOIN entrada_informe ei ON ei.id = ep.id 
                INNER JOIN entrada_tipo et ON et.id = ei.tipo 
                WHERE ei.id=@id", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    entradaPrensa = new EntradaPrensaUO();
                    entradaPrensa.id = rdr.GetInt64(0);
                    entradaPrensa.informeId = rdr.GetInt64(1);
                    entradaPrensa.titulo = rdr.GetString(2);
                    entradaPrensa.tipo = new Tipo(8, rdr.GetString(3));
                    entradaPrensa.tipoDescripcion = entradaPrensa.tipo.descripcion;
                }
            }

            if (entradaPrensa.id != null)
            {
                entradaPrensa.prensas = ObtenerPrensasByEntradaId((long)entradaPrensa.id, false);
                entradaPrensa.suscripciones = ObtenerSuscripciones();
            }

            con.Close();

            return entradaPrensa;
        }

        public IList<Tipo> ObtenerPrensaTipoPasaje()
        {
            IList<Tipo> tipos = new List<Tipo>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM prensa_pasaje_tipo", con);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    tipos.Add(new Tipo(rdr.GetInt32(0), rdr.GetString(1)));
                }
            }

            con.Close();

            return tipos;
        }

        public IList<Actividad> ObtenerActividades()
        {
            IList<Actividad> actividades = new List<Actividad>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"
                SELECT * 
                FROM actividad
                WHERE usuario_id = @usuario_id", con);
            cmd.Parameters.AddWithValue("@usuario_id", App.customPrincipal.Identity.Id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Actividad actividad = new Actividad();
                    actividad.id = rdr.GetInt64(0);
                    actividad.nombre = rdr.GetString(1);
                    actividad.detalle = rdr.GetString(2);
                    actividad.fecha = rdr.GetDateTime(3);
                    actividades.Add(actividad);
                }
            }

            con.Close();

            return actividades;
        }
    }
}
