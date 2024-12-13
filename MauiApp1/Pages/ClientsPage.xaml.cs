using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1.Pages;
public partial class ClientsPage : ContentPage
{
    public ObservableCollection<Client> Clients { get; set; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }


    private readonly ApiService _apiService;

    public ClientsPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;


        Clients = new ObservableCollection<Client>();
        EditCommand = new Command<Client>(OnEditClient);
        DeleteCommand = new Command<Client>(OnDeleteClient);

        BindingContext = this;
        // Fetch and display the clients when the page loads
        LoadClientsAsync();
    }

    private async Task LoadClientsAsync()
{
    try
    {
        string url = "https://localhost:7232/api/Clients/all";

        // Fetch updated list of clients
        List<Client> clients = await _apiService.GetClientsAsync(url);

        // Clear and refresh the list
        ClientsCollectionView.ItemsSource = null;
        ClientsCollectionView.ItemsSource = clients;
    }
    catch (Exception ex)
    {
        await DisplayAlert("Error", ex.Message, "OK");
    }
}



    private async void OnEditClient(Client client)
    {
        if (client == null)
        {
            await DisplayAlert("Error", "No client selected", "OK");
            return;
        }

        // Open the EditClientPage as a modal
        var editPage = new EditClientPage(client);

        // Subscribe to the ClientUpdated event
        editPage.ClientUpdated += async (sender, updatedClient) =>
        {
            try
            {
                string url = "https://localhost:7232/api/Clients";
                var response = await _apiService.UpdateClientAsync(url, updatedClient);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", $"Client updated successfully", "OK");

                    // Reload the client list to reflect changes
                    await LoadClientsAsync();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Failed to update client: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        };

        await Navigation.PushModalAsync(editPage);
    }



    private async void OnDeleteClient(Client client)
    {
        bool confirm = await DisplayAlert("Delete", $"Are you sure you want to delete {client.Name}?", "Yes", "No");

        if (confirm)
        {
            try
            {
                string url = $"https://localhost:7232/api/Clients/{client.Id}";
                var response = await _apiService.DeleteClientAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Client deleted successfully", "OK");

                    // Reload the clients to reflect changes
                    await LoadClientsAsync();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Failed to delete client: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
    private async void OnAddClientClicked(object sender, EventArgs e)
    {
        // Open the AddClientPage as a modal
        var addPage = new AddClientPage(_apiService);

        // Subscribe to the ClientAdded event
        addPage.ClientAdded += async (s, newClient) =>
        {
            // Reload the client list after adding a new client
            await LoadClientsAsync();
        };

        await Navigation.PushModalAsync(addPage);
    }

}
