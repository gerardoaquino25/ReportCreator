using System;
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

        public long AgregarEntrada(long idInforme, string asunto, int tipo)
        {
            long resultado = 0;

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO entrada_informe (informe_id, asunto, tipo) VALUES (@informe_id, @asunto, @tipo)", con);
            cmd.Parameters.AddWithValue("@informe_id", idInforme);
            cmd.Parameters.AddWithValue("@asunto", asunto);
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
                        //MainWindow.self.Content = new NuevoBorrador();
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

        public Notificacion GuardarEntradaGenerica(long idEntrada, string data)
        {
            Notificacion respuesta = new Notificacion();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand("UPDATE entrada_generica SET data=@data WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", idEntrada);
            cmd.Parameters.AddWithValue("@data", data);

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

        public Informe GetDatosInforme(long id)
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

        public IList<Entrada> GetEntradasInforme(long id)
        {
            IList<Entrada> entradas = new List<Entrada>();

            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            SqlCeCommand cmd = new SqlCeCommand(@"SELECT ei.id, ei.informe_id, ei.tipo, ei.asunto, et.descripcion FROM entrada_informe ei 
            INNER JOIN entrada_tipo et ON et.id = ei.tipo WHERE ei.informe_id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlCeDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Entrada entrada = new Entrada();
                    entrada.id = rdr.GetInt64(0);
                    entrada.idInforme = rdr.GetInt64(1);
                    entrada.tipo = new TipoEntrada(rdr.GetInt16(2), rdr.GetString(4));
                    entrada.asunto = rdr.GetString(3);
                    entrada.tipoDescripcion = entrada.tipo.descripcion;
                    entradas.Add(entrada);
                }
            }

            con.Close();

            return entradas;
        }

        public Informe GetInforme(long id)
        {
            Informe informe = GetDatosInforme(id);
            informe.entradas = GetEntradasInforme(id);

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

        public IList<Informe> GetInformesBorrador()
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

        public IList<Interno> GetInternos()
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

        public Notificacion GuardarMailSender(int id, string email, string password, int puerto, string smtp)
        {
            Notificacion respuesta = new Notificacion();
            if (!con.State.Equals(ConnectionState.Open))
                con.Open();

            string prueba = Decrypt(Encrypt(password));

            SqlCeCommand cmd = new SqlCeCommand(@"
                UPDATE mail_sender 
                SET email=@email, puerto=@puerto, smtp=@smtp, password=@password
                WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", Encrypt(password));
            cmd.Parameters.AddWithValue("@puerto", puerto);
            cmd.Parameters.AddWithValue("@smtp", smtp);

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

            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO mail_sender (email, password, puerto, smtp) VALUES (@email, @password, @puerto, @smtp)", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", Encrypt(password));
            cmd.Parameters.AddWithValue("@puerto", puerto);
            cmd.Parameters.AddWithValue("@smtp", smtp);

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

            SqlCeCommand cmd = new SqlCeCommand(@"SELECT * FROM mail_sender", con);

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

            con.Close();

            return mailSenders;
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
    }
}
