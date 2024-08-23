namespace Microservicio.PersonaCliente.Infraestructura.Utilitarios
{
    public class Respuesta<T>
    {
        public bool EsExitoso { get; set; }
        public T Datos { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }

        public static Respuesta<T> CrearRespuestaExitosa(T datos)
        {
            return new Respuesta<T>
            {
                EsExitoso = true,
                Datos = datos,
                Codigo = 100000,
                Mensaje = "OK"
            };
        }

        public static Respuesta<T> CrearRespuestaFallida(int codigo, string mensaje)
        {
            return new Respuesta<T>
            {
                EsExitoso = false,
                Datos = default,
                Codigo = codigo,
                Mensaje = mensaje
            };
        }
    }

}
