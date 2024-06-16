using System.ComponentModel.DataAnnotations;

namespace EXAMENMVC.Models
{
    public class VehiculoDTO
    {
        public int IDVEHICULO { get; set; }
        public string NRO_PLACA { get; set; }

        [DataType(DataType.Date)]
        public DateTime año { get; set; }
        public string Color { get; set; }

        // Cambia el nombre de la propiedad a ModeloIDMODELO
        public int ModeloIDMODELO { get; set; }
        // Propiedad de navegación hacia la clase Modelo
        public Modelo? Modelo { get; set; }
        public IFormFile? ArchivoImagen { get; set; }
        public int MarcaID { get; set; } // Asume que cada vehículo pertenece a una marca

    }
}
