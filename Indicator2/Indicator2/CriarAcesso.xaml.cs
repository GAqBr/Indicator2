using Indicator2.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Indicator2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarAcesso : ContentPage
    {
        bool carregando;
        public CriarAcesso()
        {
            InitializeComponent();
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
        private async void BTNCriarAcesso_Clicked(object sender, EventArgs e)
        {
            TelaCarregamento();
            if (TXTCriarSenha.Text != TXTConfirmarSenha.Text)
            {
                await DisplayAlert("Falha", "As senha não são iguais", "Ok");

                TelaCarregamento();
                return;
            }

            var login = new ServicosUser();
            bool loginduplicado = await login.VerificarDuplicado(TXTCriarEmail.Text);
            if (loginduplicado )
            {
                await DisplayAlert("Erro", "Login já existente", "Ok");

                TelaCarregamento();
                return;
            }

            try 
            {
                var acesso = new ServicosUser();
                bool criaracesso = await acesso.RegistrarUsuario(TXTCriarEmail.Text, TXTCriarSenha.Text);

                if (criaracesso)
                {
                    await DisplayAlert("Sucesso", "Usuário criado!", "Ok");

                    TelaCarregamento();
                    return;
                }
                else
                {
                    await DisplayAlert("Falha", "Não foi possível criar um usuário, tente novamente", "Ok");

                    TelaCarregamento();
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Ocorreu um erro", "Ok");
                TelaCarregamento();
            }

            
        }

        private void BTNCancelar_Clicked(object sender, EventArgs e)
        {
            TXTCriarEmail.Text = string.Empty;
            TXTConfirmarSenha.Text = string.Empty;
            TXTCriarSenha.Text = string.Empty;
        }
    }
}