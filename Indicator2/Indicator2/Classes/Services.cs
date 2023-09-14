using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Linq;

namespace Indicator2.Classes
{
    public class Services
    {
        public static string SenhaFirebase = "fNcSxUZ9cTVK9FXRnZKLwLED9NXDCJdRgcbZEGJG";
        FirebaseClient Cliente = new FirebaseClient("https://indicator-project-default-rtdb.firebaseio.com/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(SenhaFirebase) });


        public async Task<bool> RegistraW (string servicoworker, string empresa, string telefone1, string cep, string bairro, string logadouro, string numero,
            string complemento, string cnpj, string cidade, string uf)
        {
            try
            {
                await Cliente.Child("CadastroWorker")
                    .PostAsync(new WorkerData()
                    {
                        servicoworker=servicoworker,
                        empresa=empresa,
                        telefone1=telefone1,
                        cep=cep,
                        bairro=bairro,
                        cnpj=cnpj,
                        logradouro=logadouro,
                        numero=numero,
                        cidade=cidade,
                        complemento=complemento,
                        uf=uf


                    });
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<List<WorkerData>> ListaWorker()
        {
            var listagem = (await Cliente.Child("CadastroWorker")
                .OnceAsync<WorkerData>())
                .Select(f => new WorkerData()
                { 
                  servicoworker = f.Object.servicoworker,
                }).ToList();

            return listagem;
        }

        
        public async Task<bool> ApagarIndie(string servicoworker)
        {
            try
            {
                var localizar = (await Cliente.Child("CadastroWorker")
                      .OnceAsync<WorkerData>())
                      .Where(a => a.Object.servicoworker == servicoworker)
                      .FirstOrDefault();

                await Cliente.Child("CadastroWorker").Child(localizar.Key).DeleteAsync();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        public async Task<List<WorkerData>> ListaFiltrada(string texto_da_busca)
        {
            var lista = (await Cliente.Child("CadastroWorker")
                .OnceAsync<WorkerData>())
                .Where(a => a.Object.servicoworker.ToLower().Contains(texto_da_busca.ToLower()))
                .Select(f => new WorkerData()
                {
                    servicoworker = f.Object.servicoworker,
                }).ToList();
                
            return lista;
        }

        public async Task<WorkerData> LocalizaIndie(string servicoworker)
        {
            var achaindie = (await Cliente.Child("CadastroWorker")
                .OnceAsync<WorkerData>())
                .Where(u => u.Object.servicoworker == servicoworker)
                .FirstOrDefault();
            if (achaindie != null)
            {
                return achaindie.Object;
            }
            else { return null; }
        }
    }
}
