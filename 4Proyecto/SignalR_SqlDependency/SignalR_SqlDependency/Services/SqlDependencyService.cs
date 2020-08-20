using SignalR_SqlDependency.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SignalR_SqlDependency.Services
{
    public interface IDatabaseChangeNofiticationService
    {
        /* Realizar la configuración y decirle a SQL SERVER que quermos 
         * que esta aplicación sea notificada cada vez que exista un cambio. En la tabla persona. */
        void Config();
    }

    public class SqlDependencyService: IDatabaseChangeNofiticationService
    {
        private readonly IConfiguration configuration;
        private readonly IHubContext<ChatHub> chatHub;
        /* IConfiguration. Lo necesitamos para extraer los setting de nuestro conexion string.
         * IHubContext. Para decirle a SIGNAL R cada vez que hay un cambio en nuestra tabla de SQL SERVER.
         */
        public SqlDependencyService(
            IConfiguration configuration,
            IHubContext<ChatHub> chatHub
            )
        {
            this.configuration = configuration;
            this.chatHub = chatHub;
        }

        public void Config()
        {
            SuscribirseALosCambiosDeLaTablaPersonas();
        }

        private void SuscribirseALosCambiosDeLaTablaPersonas()
        {

            string connString = configuration.GetConnectionString("SQLServerDB");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                // No funciona si usas SELECT *
                using (var cmd = new SqlCommand(@"SELECT Nombres FROM [dbo].Alumno",conn))
                {
                    cmd.Notification = null;
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += Personas_Cambio;
                    SqlDependency.Start(connString);
                    cmd.ExecuteReader();// Hay que correr elquery
                }
            }
        }
        private void Personas_Cambio(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                string mensaje = ObtenerMensajeAMostrar(e);
                chatHub.Clients.All.SendAsync("ReceiveMessage", "Admin", mensaje);
            }

            SuscribirseALosCambiosDeLaTablaPersonas(); //Importante: Hay que volver a suscribirse
        }

        private string ObtenerMensajeAMostrar(SqlNotificationEventArgs e)
        {
            // ctrl + .
            switch (e.Info)
            {
                case SqlNotificationInfo.Insert:
                    return "Un registro ha sido INSERTADO";
                case SqlNotificationInfo.Delete:
                    return "Un registro ha sido DELETE";
                case SqlNotificationInfo.Update:
                    return "Un registro ha sido UPDATE";
                default:
                    return "Un cambio desconocido ha ocurrido";
            }
        }


    }
    
}
