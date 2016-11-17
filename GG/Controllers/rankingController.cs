using GG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GG.Controllers
{
    public class rankingController : ApiController
    {
        [System.Web.Http.HttpGet]
        public List<Ranking> carregarranking()
        {
            var r = new Ranking();

            return r.listarranking();
        }

        [HttpPost]
        public void inserir(Ranking r)
        {
            var rankin = new Ranking();
            rankin.cadastrar(r);
        }
    }
}
