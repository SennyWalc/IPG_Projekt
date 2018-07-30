using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebIPG.Models;

namespace WebIPG.Controllers
{
    public class HomeController : Controller
    {
        private IServicePodstawowa Podstawowa = new ServicePodstawowa();
        public ActionResult Index()
        {
            return View();
        }
        public string GetAll()
        {
            try
            {
                var podstawowa = Podstawowa.GetAll();
                var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                string elementy = JsonConvert.SerializeObject(podstawowa, Formatting.None, settings);
                return elementy;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult PodstawowaEdycja(Podstawowa podstawowa,Podstawowa arch)
        {
            bool status = Porownanie(arch);
            if (status)
            {
                Podstawowa.Update(podstawowa);
                return Json(new { success = true, status = "OK" });
            }
            else
            {
                return Json(new { success = false, status =" Opercja zapisu zatrzymana" });
            }
        }
        private bool Porownanie(Podstawowa arch)
        {
            var podstawowa = Podstawowa.GetById(arch.id);
            bool q = podstawowa.id.Equals(arch.id);
            if (!q) return false;
            q = podstawowa.nazwa.Equals(arch.nazwa);
            if (!q) return false;
            q = podstawowa.dataPoczatek.Equals(arch.dataPoczatek);
            if (!q) return false;
            q = podstawowa.dataKoniec.Equals(arch.dataKoniec);
            if (!q) return false;
            q = podstawowa.osobaOdpowiedzialna.Equals(arch.osobaOdpowiedzialna);
            if (!q) return false;
            q = podstawowa.typ.Equals(arch.typ);
            if (!q) return false;
            return true;
        }
    }
  
}

