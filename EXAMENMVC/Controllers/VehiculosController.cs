using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EXAMENMVC.Datos;
using EXAMENMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EXAMENMVC.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment entorno;

        public VehiculosController(ApplicationDbContext context,IWebHostEnvironment entorno)
        {
            _context = context;
            this.entorno = entorno;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            var vehiculos = _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca);

            return View(await vehiculos.ToListAsync());
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
            return View();
        }

        // POST: Vehiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(VehiculoDTO vehiculoDTO)
        {
            if (vehiculoDTO.ArchivoImagen == null || vehiculoDTO.ArchivoImagen.Length == 0)
            {
                ModelState.AddModelError("ArchivoImagen", "El archivo de imagen es obligatorio");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
                return View(vehiculoDTO);
            }

            string rutaCarpetaImagenes = Path.Combine(entorno.WebRootPath, "imagenes");
            string nombreArchivo = $"{DateTime.Now:yyyyMMddHHmmssfff}{Path.GetExtension(vehiculoDTO.ArchivoImagen.FileName)}";
            string rutaCompletaImagen = Path.Combine(rutaCarpetaImagenes, nombreArchivo);

            using (var stream = new FileStream(rutaCompletaImagen, FileMode.Create))
            {
                await vehiculoDTO.ArchivoImagen.CopyToAsync(stream);
            }

            Vehiculo vehiculo = new Vehiculo
            {
                NRO_PLACA = vehiculoDTO.NRO_PLACA,
                año = vehiculoDTO.año,
                Color = vehiculoDTO.Color,
                ModeloIDMODELO = vehiculoDTO.ModeloIDMODELO,
                Imagen = nombreArchivo,
                FechaCreacion = DateTime.Now
            };

            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var v = _context.Vehiculos.Find(id);
            if (v == null)
            {
                return RedirectToAction("Index");
            }
            
            var vehiculo = await _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca)
                .FirstOrDefaultAsync(v => v.IDVEHICULO == id);

            var vehiculoDTO = new VehiculoDTO()
            {
                NRO_PLACA=v.NRO_PLACA,
                año=v.año,
                Color=v.Color,
                ModeloIDMODELO=v.ModeloIDMODELO,
                Modelo=v.Modelo,

            };

            ViewData["IdVehiculo"] = v.IDVEHICULO;
            ViewData["Imagen"] = v.Imagen;
            ViewData["FechaCreacion"] = v.FechaCreacion.ToString("MM/dd/yyyy");



            if (vehiculo == null)
            {
                return NotFound();
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", vehiculo.Modelo.MarcaIDMARCA);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == vehiculo.Modelo.MarcaIDMARCA), "IDMODELO", "NOM_MODELO", vehiculo.ModeloIDMODELO);
            return View(vehiculoDTO);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehiculoDTO vehiculoDTO)
        {
            var v = _context.Vehiculos.Find(id);

            if (v==null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ViewData["IdVehiculo"] = v.IDVEHICULO;
                ViewData["Imagen"] = v.Imagen;
                ViewData["FechaCreacion"] = v.FechaCreacion.ToString("MM/dd/yyyy");
                return View(vehiculoDTO);
            }

            string nuevoNombreArchivo = v.Imagen;
            if (vehiculoDTO.ArchivoImagen!=null)
            {
                nuevoNombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                nuevoNombreArchivo += Path.GetExtension(vehiculoDTO.ArchivoImagen.FileName);
                string rutaCompletaImagen = entorno.WebRootPath + "/imagenes/" + nuevoNombreArchivo;
                using (var stream = System.IO.File.Create(rutaCompletaImagen))
                {
                    vehiculoDTO.ArchivoImagen.CopyTo(stream);
                }
                string rutaCompletaImagenAntigua = entorno.WebRootPath + "/imagenes/" + v.Imagen;
                System.IO.File.Delete(rutaCompletaImagenAntigua);
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", vehiculoDTO.MarcaID);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == vehiculoDTO.MarcaID), "IDMODELO", "NOM_MODELO", vehiculoDTO.ModeloIDMODELO);


            v.NRO_PLACA=vehiculoDTO.NRO_PLACA;
            v.año=vehiculoDTO.año;
            v.Color=vehiculoDTO.Color;
            v.ModeloIDMODELO=vehiculoDTO.ModeloIDMODELO;
            v.Imagen=nuevoNombreArchivo;
            
            _context.SaveChanges();
          return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(int? id)
        {
            var v = _context.Vehiculos.Find(id);

            if (v == null)
            {
                return RedirectToAction ("Index");
            }

            string rutaCompletaImagen = entorno.WebRootPath + "/imagenes/" + v.Imagen;
            System.IO.File.Delete(rutaCompletaImagen);
            _context.Vehiculos.Remove(v);
            await _context.SaveChangesAsync();
             
            return RedirectToAction("Index");
        }

 
        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.IDVEHICULO == id);
        }
        public JsonResult ObtenerModelos(int idMarca)
        {
            var modelos = _context.Modelos
                .Where(m => m.MarcaIDMARCA == idMarca)
                .Select(m => new { idModelo = m.IDMODELO, noM_MODELO = m.NOM_MODELO })
                .ToList();
            return Json(modelos);
        }
    }
}