using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Financial.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaPFController : ControllerBase
    {
        private static readonly string[] TipoConta = new[]
        {
            "Poupança", "Conta Corrente", "Conta Salário", "Depósito Judicial"
        };

        private static readonly string[] Nome = new[]
{
            "João", "Maria", "Creuza", "José"
        };

        private readonly ILogger<ContaPFController> _logger;

        public ContaPFController(ILogger<ContaPFController> logger)
        {
            _logger = logger;
        }

        // simula uma tabela de banco de dados
        public int ultimoId { get; set; }
        public List<ContaPF> lista { get; set; }

        private List<ContaPF> GerarLista()
        {
            if (this.lista == null)
            {
                var rng = new Random();
                this.lista = Enumerable.Range(1, 5).Select(index => new ContaPF
                {
                    Agencia = rng.Next(1111, 9999),
                    Conta = rng.Next(111111, 999999),
                    TipoConta = TipoConta[rng.Next(TipoConta.Length)],
                    NomeCompleto = Nome[rng.Next(Nome.Length)],
                }).ToList();

                this.lista.Add(new ContaPF
                {
                    Agencia = 1234,
                    Conta = 123456,
                    TipoConta = TipoConta[rng.Next(TipoConta.Length)],
                    NomeCompleto = "Maria Antonieta da Silva"
                });

                this.lista.Add(new ContaPF
                {
                    Agencia = 1235,
                    Conta = 123457,
                    TipoConta = TipoConta[rng.Next(TipoConta.Length)],
                    NomeCompleto = "José Maria de Souza e Albuquerque de Medeiros e Sá"
                });

                var id = 1;
                foreach (var item in lista)
                {
                    item.Id = id;
                    id++;
                }
                ultimoId = id;
            }
            return lista;
        }

        /// <summary>
        /// GetAll - gera lista lista de contas pessoa física e exibe
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ContaPF> GetAll()
        {
            return GerarLista();
        }

        // GET api/<ContaPFController>/5
        /// <summary>
        /// GetById - exibe um registro da lista
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ContaPF GetById(int id)
        {
            return GerarLista()
                .FirstOrDefault(conta => conta.Id == id);
        }

        // POST api/<ContaPFController>
        /// <summary>
        /// Post - acrecenta um novo registro na lista
        /// </summary>
        /// <param name="contaPF"></param>
        [HttpPost]
        public void Post([FromBody] ContaPF contaPF)
        {
            GerarLista().Add(new ContaPF
            {
                Id = ultimoId + 1,
                Agencia = contaPF.Agencia,
                Conta = contaPF.Conta,
                TipoConta = contaPF.TipoConta,
                NomeCompleto = contaPF.NomeCompleto
            });

        }

        // PUT api/<ContaPFController>/5
        /// <summary>
        /// Put - altera um item da lista pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contaPF"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ContaPF Put(int id, [FromBody] ContaPF contaPF)
        {
            var conta = GetById(id);

            conta.Agencia = contaPF.Agencia;
            conta.Conta = contaPF.Conta;
            conta.TipoConta = contaPF.TipoConta;
            conta.NomeCompleto = contaPF.NomeCompleto;

            return conta;
        }

        // DELETE api/<ContaPFController>/5
        /// <summary>
        /// Delete - deleta um item por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                GerarLista().Remove(new ContaPF() { Id = id });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
