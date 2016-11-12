using GG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GG.Controllers
{
    public class perguntaController : ApiController
    {
        [HttpPost]
        public Retornojson inserir(Pergunta p)
        {
            var per = new Pergunta();
            var retorno = per.cadastrar(p);
            if (retorno.Equals("OK"))
                return new Retornojson { valid = retorno + "|" + per.id };
            else
                return new Retornojson { valid = retorno };

        }

        [HttpGet]
        public List<Pergunta> listagem()
        {
            var p = new Pergunta();
            return p.listagem();

        }

    }
}
