using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiEnviaFila.Controllers
{
    public class ValuesController : ApiController
    {
        static CloudQueue cloudQueue;

        public ValuesController()
        {
            //connectionString para o meu projeto publicado
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=pizzadecalabresa;AccountKey=Ae6xWf5CCVBj8fky2lNQRbVZVdmgf0boWXolnJGtCqAjZ3J5NKmsA5sf4hMHbSSUv7ZuCUllMj63zx3TPI7IYw==;EndpointSuffix=core.windows.net";//ConfigurationManager.ConnectionStrings["Azure Storage Account Demo Primary"].ConnectionString;
            CloudStorageAccount cloudStorageAccount;

            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Console.WriteLine("Erro na conexão!");
            }

            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            cloudQueue = cloudQueueClient.GetQueueReference("cloudqueue");

            //Cria a fila no objeto cloudQueue
            cloudQueue.CreateIfNotExists();

        }


        // GET api/values
        public IEnumerable<string> Get()
        {
            //conexão já está sendo feita no construtor

            int? mensagensNaFila = cloudQueue.ApproximateMessageCount;

            String totalMensagens = "Total de mensagens na fila 1 = " + mensagensNaFila.ToString();

            return new string[] { totalMensagens };

        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            //conexão já está sendo feita no construtor
            var message = new CloudQueueMessage("Mensagem enviada da API para a fila");

            cloudQueue.AddMessage(message);

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
