using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDCORE.Models;
using CRUDCORE.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CRUDCORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBCRUDCOREContext _DBcontext;

        public HomeController(DBCRUDCOREContext context)
        {
            _DBcontext = context;
        }

        public IActionResult Index()
        {
            List<Empleado> list = _DBcontext.Empleados.Include(c => c.oCargo).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Empleado_Detalle(int idEmpleado)
        {
            EmpleadoVM oEmpleadoVM = new EmpleadoVM()
            {
                oEmpleado = new Empleado(),
                oListaCargo = _DBcontext.Cargos.Select(cargo => new SelectListItem()
                {
                    Text = cargo.Description,
                    Value = cargo.IdCargo.ToString()

                }).ToList()
            };

            if (idEmpleado != 0)
            {

                oEmpleadoVM.oEmpleado = _DBcontext.Empleados.Find(idEmpleado);
            }


            return View(oEmpleadoVM);
        }

        [HttpPost]
        public IActionResult Empleado_Detalle(EmpleadoVM oEmpleadoVM)
        {
            if (oEmpleadoVM.oEmpleado.IdEmpleado == 0)
            {
                _DBcontext.Empleados.Add(oEmpleadoVM.oEmpleado);

            }
            else
            {
                _DBcontext.Empleados.Update(oEmpleadoVM.oEmpleado);
            }

            _DBcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Eliminar(int idEmpleado)
        {
            Empleado oEmpleado = _DBcontext.Empleados.Include(c => c.oCargo).Where(e => e.IdEmpleado == idEmpleado).FirstOrDefault();

            return View(oEmpleado);
        }

        [HttpPost]
        public IActionResult Eliminar(Empleado oEmpleado)
        {
            _DBcontext.Empleados.Remove(oEmpleado);
            _DBcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



    }
}