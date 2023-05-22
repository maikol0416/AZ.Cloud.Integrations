using System;
namespace Api.Base
{
    [Serializable]
    public class ResponseApi<T>
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; }
    }
}
