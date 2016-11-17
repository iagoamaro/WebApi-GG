using GG.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace GG.Controllers
{
    public class jogadorController : ApiController
    {
       [HttpPost]
       public Retornojson inserir(Jogador jog)
        {
            var player = new Jogador();            
            var retorno = player.cadastrar(jog);
            if (retorno.Equals("OK"))
                return new Retornojson { valid = retorno + "|" + jog.Id };
            else
                return new Retornojson { valid = retorno };

        }

        //[System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public List<Jogador> listarjogador(string nome = "")
        {
            var jogador = new Jogador();
            return jogador.BuscarJogador(nome);
        }
    
    }
}
