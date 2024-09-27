using Backend.Datos;
using Backend.DTO;
using Backend.Models;
using Inacap.Siga.MantenedordeHorarios.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Backend.Controllers
{
    public class ComboboxController : Controller
    {
        public ActionResult getComboTurno()
        {
            try
            {
                da_combobox<Turno> planOrigen = new da_combobox<Turno>();
                List<Turno> lstCbo = planOrigen.getComboTurno();
                return Json(lstCbo);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        public ActionResult getCboSedes(int i_pers_nrut)
        {
            try
            {
                da_combobox<ComboBox> planOrigen = new da_combobox<ComboBox>();
                List<ComboBox> lst = planOrigen.getCboSedes(i_pers_nrut);

                return Json(lst);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //No devuelve ningun dato
        public ActionResult getPeriodoActual()
        {
            try
            {
                da_combobox<PeriodoActual> periodoOrigen = new da_combobox<PeriodoActual>();
                List<PeriodoActual> lst = periodoOrigen.getPeriodoActual();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { errorCapturado = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /*     {
        "codigo": 244,
        "descripcion": "PRIMAVERA 2024"
        */
        public ActionResult getCboOtrosPeriodos()
        {
            try
            {
                da_combobox<ComboBox> otrosPeriodosOrigen = new da_combobox<ComboBox>();
                List<ComboBox> lst = otrosPeriodosOrigen.getCboOtrosPeriodos();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { errorCapturado = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}
