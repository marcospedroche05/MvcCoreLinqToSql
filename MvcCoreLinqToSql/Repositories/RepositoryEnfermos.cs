using Microsoft.Data.SqlClient;
using MvcCoreLinqToSql.Models;
using System.Data;
using System.Threading.Tasks;

namespace MvcCoreLinqToSql.Repositories
{
    public class RepositoryEnfermos
    {
        private DataTable tablaEnfermos;
        SqlConnection cn;
        SqlCommand com;

        public RepositoryEnfermos()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "SELECT * FROM ENFERMO";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            ad.Fill(this.tablaEnfermos);
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           select datos;
            List<Enfermo> enfermos = new List<Enfermo>();
            foreach(var row in consulta)
            {
                Enfermo enfermo = new Enfermo();
                enfermo.Inscripcion = row.Field<string>("INSCRIPCION");
                enfermo.Apellido = row.Field<string>("APELLIDO");
                enfermo.Direccion = row.Field<string>("DIRECCION");
                enfermo.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
                enfermo.Sexo = row.Field<string>("S");
                enfermo.NumeroSeguridad = row.Field<string>("NSS");
                enfermos.Add(enfermo);
            }
            return enfermos;
        }

        public Enfermo FindEnfermo(string inscripcion)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<string>("INSCRIPCION") == inscripcion
                           select datos;
            var row = consulta.First();
            Enfermo enfermo = new Enfermo();
            enfermo.Inscripcion = row.Field<string>("INSCRIPCION");
            enfermo.Apellido = row.Field<string>("APELLIDO");
            enfermo.Direccion = row.Field<string>("DIRECCION");
            enfermo.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
            enfermo.Sexo = row.Field<string>("S");
            enfermo.NumeroSeguridad = row.Field<string>("NSS");
            return enfermo;
        }

        public async Task DeleteEnfermo(string inscripcion)
        {
            string sql = "DELETE FROM ENFERMO WHERE INSCRIPCION = @inscripcion";
            this.com.Parameters.AddWithValue("@inscripcion", inscripcion);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
