using Indicator2.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Indicator2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarIndie : ContentPage
    {
        public CriarIndie()
        {
            InitializeComponent();
            BindingContext = this;

        }


        private async void TXTCEP_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                //usa json para buscar dados a partir do CEP
                var cliente = new HttpClient();
                var cep = TXTCEP.Text;
                var json = await cliente.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
                var dados = JsonConvert.DeserializeObject<endereco>(json);
                //fim da consulta

                //usa a classe endereco para devolver os dados
                TXTRua.Text = dados.logradouro;
                TXTBairro.Text = dados.bairro;
                TXTCidade.Text = dados.localidade;
                TXTUF.Text = dados.uf;

                TXTNumeroEmpresa.Focus();

            }
            catch 
            {
                await DisplayAlert("❌Erro", "Ocorreu um erro inesperado", "OK");
                return;
            }
        }

        public async void BTNSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var salvar = new Services();
                bool registrar = await salvar.RegistraW(
                    TXTServicoWorker.Text,
                    TXTEmpresa.Text,
                    TXTTel.Text,
                    TXTCNPJ.Text,
                    TXTCEP.Text,
                    TXTBairro.Text,
                    TXTRua.Text,
                    TXTNumeroEmpresa.Text,
                    TXTCidade.Text,
                    TXTComplemento.Text,
                    TXTUF.Text
                    );

                if ( registrar )
                {
                    await DisplayAlert("✅ Bem vindo!", "Agora é só esperar por clientes!", "OK");
                    LimpaCampos();
                    return;
                }
                else
                {
                    await DisplayAlert("❌Erro", "Ocorreu um erro inesperado", "OK");
                    return;
                }
            }
            catch
            {
                await DisplayAlert("❌Erro", "Ocorreu um erro inesperado", "OK");
                return;
            }
        }
        public void LimpaCampos()
        {
            TXTServicoWorker.Text = string.Empty;
            TXTEmpresa.Text = string.Empty;
            TXTTel.Text = string.Empty;
            TXTCNPJ.Text = string.Empty;
            TXTCEP.Text = string.Empty;
            TXTBairro.Text = string.Empty;
            TXTRua.Text=string.Empty;
            TXTNumeroEmpresa.Text = string.Empty;
            TXTCidade.Text = string.Empty;
            TXTComplemento.Text = string.Empty;
            TXTUF.Text = string.Empty;
        }
    }
}