using CommunityToolkit.Maui.Storage;
using StudentRandomizer.Services.ExternalData;
using System.Text;

namespace StudentRandomizer.Pages;

public partial class ExternalDataPage : ContentPage
{
	private readonly IExternalDataService _externalDataService;

	public ExternalDataPage(IExternalDataService externalDataService)
	{
		_externalDataService = externalDataService;

		InitializeComponent();
	}

	private async void externalDataPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
	}

	private async void externalDataPage_importButton_Clicked(object obj)
	{
		PickOptions pickOptions = new PickOptions()
		{
			PickerTitle = "Wybierz źródło importu danych",
			FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{ DevicePlatform.WinUI, new[] { ".txt", ".csv" } }
			})
		};

		await Navigation.PushModalAsync(new Modals.ExternalDataImportInProgressModal());

		var result = await FilePicker.Default.PickAsync(pickOptions);

		if(result != null)
		{
			try
			{
				var fileContents = File.ReadAllText(result.FullPath);
				_externalDataService.ImportAll(fileContents);

				await Navigation.PopModalAsync();
				await DisplayAlert("Import danych", "Pomyślnie zaimportowano dane!", "OK");
			}
			catch(Exception e)
			{
				await Navigation.PopModalAsync();
				await DisplayAlert("Import danych", $"Wystąpił błąd podczas importu danych!\n{e.Message}\n{e.InnerException?.Message}", "OK");
			}
		}
		else
		{
			await Navigation.PopModalAsync();
		}
	}

	private async void externalDataPage_exportButton_Clicked(object obj)
	{
		try
		{
			await Navigation.PushModalAsync(new Modals.ExternalDataExportInProgressModal());

			var outputString = _externalDataService.ExportAll();
			byte[] outputBytes = Encoding.UTF8.GetBytes(outputString);

			using (var stream = new MemoryStream(outputBytes))
			{
				var result = await FileSaver.Default.SaveAsync("export.txt", stream);

				if(!result.IsSuccessful)
				{
					throw result.Exception;
				}

				await Navigation.PopModalAsync();

				await DisplayAlert("Eksport danych", $"Pomyślnie wykesportowano dane do: {result.FilePath}", "OK");
			}
		}
		catch(Exception)
		{
			await Navigation.PopModalAsync();
			await DisplayAlert("Eksport danych", "Wystąpił błąd podczas eksportowania danych.", "OK");
		}
	}
}