using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ClaseModelo;
using static SenaPlanning.Controllers.LoginController;

namespace SenaPlanning.Controllers
{
    public class HomeController : Controller
    {
        private SenaPlanningEntities db = new SenaPlanningEntities();

        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Index()
        {
            var metas = db.Meta.Where(m => m.MetaFecha == DateTime.Now.Year.ToString()).ToList();

            if (metas.Count == 0)
            {
                return View(db.Meta.Where(m => m.MetaFecha == DateTime.Now.Year.ToString()).ToList());
            }

            double META_TGO = 0;
            double META_TCO = 0;
            double META_ET = 0;
            double META_OTROS = 0;

            double META_TGO_AP_PASAN = 0;
            double META_TCO_AP_PASAN = 0;
            double META_ET_AP_PASAN = 0;
            double META_OTRO_AP_PASAN = 0;

            double APRENDICES_POR_CURSOS = 30;
            double APRENDICES_POR_CURSOS_OTROS = 35;
            long id = 0;

                id = metas[0].IdMeta;

                META_TGO = int.Parse(metas[0].MetaTecnPresencial.ToString()) + int.Parse(metas[0].MetaTecnVirtual.ToString());
                META_TCO = int.Parse(metas[0].MetaTecPresencial.ToString()) + int.Parse(metas[0].MetaTecVirtual.ToString());
                META_ET = int.Parse(metas[0].MetaETPresencial.ToString()) + int.Parse(metas[0].MetaETVirtual.ToString());
                META_OTROS = int.Parse(metas[0].MetaOtros.ToString());

                META_TGO_AP_PASAN = int.Parse(metas[0].MetaTGOApPasan.ToString());
                META_TCO_AP_PASAN = int.Parse(metas[0].MetaTCOApPasan.ToString());
                META_ET_AP_PASAN = int.Parse(metas[0].MetaETApPasan.ToString());
                META_OTRO_AP_PASAN = int.Parse(metas[0].MetaOTROApPasan.ToString());

                //APRENDICES_POR_CURSOS = long.Parse(item.APorCurso.ToString());
                //APRENDICES_POR_CURSOS_OTROS = long.Parse(item.APorCursoOtros.ToString());
            double RESULT_META_TGO_AP_PROG = Math.Round((META_TGO_AP_PASAN / META_TGO) * 100, 2);
            double RESULT_META_TCO_AP_PROG = Math.Round((META_TCO_AP_PASAN / META_TCO) * 100, 2);
            double RESULT_META_ET_AP_PROG = Math.Round((META_ET_AP_PASAN / META_ET) * 100, 2);
            double RESULT_META_OTROS_AP_PROG = Math.Round((META_OTRO_AP_PASAN / META_OTROS) * 100, 2);


            double META_GENERAL = Math.Round((RESULT_META_TGO_AP_PROG + RESULT_META_TCO_AP_PROG + RESULT_META_ET_AP_PROG + RESULT_META_OTROS_AP_PROG) / 4, 2);

            ViewData["RESULT_META_TGO_AP_PROG"] = RESULT_META_TGO_AP_PROG;
            ViewData["RESULT_META_TCO_AP_PROG"] = RESULT_META_TCO_AP_PROG;
            ViewData["RESULT_META_ET_AP_PROG"] = RESULT_META_ET_AP_PROG;
            ViewData["RESULT_META_OTROS_AP_PROG"] = RESULT_META_OTROS_AP_PROG;

            ViewData["META_GENERAL"] = META_GENERAL;

            ViewData["idMeta"] = id;

            return View(db.Oferta.Where(o => o.Meta.MetaFecha == DateTime.Now.Year.ToString()).ToList());
        }
        [AutorizarTipoUsuario("Coordinador", "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meta meta = db.Meta.Find(id);
            if (meta == null)
            {
                return HttpNotFound();
            }
            return View(meta);
        }
    }
}