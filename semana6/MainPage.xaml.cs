using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace semana6
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://172.21.112.1/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<semana6.Ws.Datos> _post;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<semana6.Ws.Datos> posts = JsonConvert.DeserializeObject<List<semana6.Ws.Datos>>(content);
                _post = new ObservableCollection<semana6.Ws.Datos>(posts);
                MyListView.ItemsSource = _post;
            }
            catch (Exception ex)
            {
                await DisplayAlert("alerta", "Error: " + ex.Message, "ok");
            }
        }

        private async void btnAbrir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Post());
        }

        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (semana6.Ws.Datos)e.SelectedItem;
            var codigo = item.codigo;
            var nombre = item.nombre;
            var apellido = item.apellido;
            var edad = item.edad;

            await Navigation.PushAsync(new Registro(codigo, nombre, apellido, edad));
        }





    }
}
