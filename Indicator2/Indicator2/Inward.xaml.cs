using Indicator2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms.Xaml;

namespace Indicator2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inward : TabbedPage
    {
        public Command NavToDetailCommand { get; set; }
        bool carregando;

        public Inward()
        {
            InitializeComponent();

            BindingContext = this;
            CarregaLista();
        }
        public async void CarregaLista()
        {
            var listaworker = new Services();
            var listagem = listaworker.ListaWorker();


            CVLista.ItemsSource = await listagem;
        }

        public void TelaCarregamento()
        {
            if (carregando)
            {
                BVTelaPreta.IsVisible = false;
                ACTRoda.IsVisible = false;
                ACTRoda.IsRunning = false;

                carregando = false;
            }
            else
            {
                BVTelaPreta.IsVisible = true;
                ACTRoda.IsVisible = true;
                ACTRoda.IsRunning = true;

                carregando = true;
            }
        }

        private async void BTNCriarIndie_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CriarIndie());
            CarregaLista();
        }

        private async void BTNGerenciar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GerenciarIndie());
            CarregaLista();
        }

        private async void SVApagar_Invoked(object sender, EventArgs e)
        {
            var buscar = (sender as SwipeItem)?.BindingContext as Classes.WorkerData;
            var item = buscar.servicoworker;

            try
            {
                var confirmacao = await DisplayAlert("❗ Aviso", "Você está prestes a apagar " + buscar.servicoworker.ToString()
                    + ", Deseja continuar?", "Sim", "Não");

                if (confirmacao)
                {
                    var servicos = new Services();
                    var apagar = await servicos.ApagarIndie(item);

                    CarregaLista();
                    return;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                await DisplayAlert("⛔Erro", "Algo deu errado!", "Ok");
            }

        }

        private async void SBBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var servicobuscado = new Services();
            var listafiltro = await servicobuscado.ListaFiltrada(SBBuscar.Text);

            CVLista.ItemsSource = listafiltro;
        }

        public async void CVLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aba = Children[2];
            CurrentPage = aba;

            var selecionado = (WorkerData)e.CurrentSelection.FirstOrDefault();
            string servicoworker_selecionado = selecionado.servicoworker;

            var servicoss = new Services();
            var localizacontato = await servicoss.LocalizaIndie(servicoworker_selecionado);

            if (localizacontato != null)
            {
                LBLServicoWorker.Text = localizacontato.servicoworker;
                LBLEmpresa.Text = localizacontato.empresa;
                LBLTelefone1.Text = localizacontato.telefone1;
                LBLCep.Text = localizacontato.cep;
                LBLCNPJ.Text = localizacontato.cnpj;
                LBLBairro.Text = localizacontato.bairro;
                LBLLogradouro.Text = localizacontato.logradouro;
                LBLNumero.Text = localizacontato.numero;
                LBLComplemento.Text = localizacontato.complemento;
                LBLCidade.Text = localizacontato.cidade;
                LBLUF.Text = localizacontato.uf;
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível localizar o contato", "OK");
            }
        }

        private async void BTNContratar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string telefone = LBLTelefone1.Text;
                Chat.Open(telefone);
            }
            catch (Exception ex)  
            {
               await DisplayAlert("Alerta",ex.Message,"OK");
            }   
        }
    }
}