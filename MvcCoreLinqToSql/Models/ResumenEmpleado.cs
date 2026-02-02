namespace MvcCoreLinqToSql.Models
{
    public class ResumenEmpleado
    {
        public int Personas { get; set; }
        public int MaximoSalario { get; set; }
        public double MediaSalarial { get; set; }
        public List<Empleado> Empleados { get; set; }

        public ResumenEmpleado()
        {
            this.Empleados = new List<Empleado>();
        }
    }
}
