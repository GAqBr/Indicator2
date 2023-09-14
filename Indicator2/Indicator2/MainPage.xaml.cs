using Indicator2.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Indicator2
{
    public partial class MainPage : ContentPage
    {
        bool carregando;
        public MainPage()
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

        private async void BTNLogar_Clicked(object sender, EventArgs e)
        {
            try
            {
                TelaCarregamento();

                var logar = new ServicosUser();
                bool verificarlogin = await logar.VerificarLogin(TXTEmail.Text, TXTSenha.Text);

                if (verificarlogin)
                {

                    TXTEmail.Text = string.Empty; TXTSenha.Text = string.Empty;

                    await Navigation.PushAsync(new Inward());

                    TelaCarregamento();
                }
                else
                {
                    await DisplayAlert("Erro", "Usuário ou senha não correspondem", "OK");

                    TelaCarregamento();
                }
            }
            catch 
            {
                await DisplayAlert("Erro", "Usuário ou senha não correpondem", "Ok");

                TelaCarregamento();
            }
        }

        private async void BTNCriar_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new CriarAcesso());
        }
    }
}
