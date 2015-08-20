using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class Notificacion
    {
        public string Detalle { get; set; }

        public static string USUARIO_NO_LOGUEADO = "USUARIO_O_CONTRASENIA_INCORRECTA";
        public static string USUARIO_LOGUEADO = "USUARIO_LOGUEADO";
        public static string USUARIO_CREADO = "USUARIO_CREADO";
        public static string USUARIO_YA_EXISTENTE = "USUARIO_YA_EXISTENTE";
        public static string USUARIO_CREADO_KO = "USUARIO_CREADO_KO";
    }
}
