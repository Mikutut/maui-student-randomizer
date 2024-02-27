using StudentRandomizer.Services.LuckyNumbers;

namespace StudentRandomizer.Pages;

public partial class LuckyNumberPage : ContentPage
{
	private readonly ILuckyNumberDataService _luckyNumberDataService;

	private Models.LuckyNumber luckyNumber;

	public Models.LuckyNumber LuckyNumber
	{
		get => luckyNumber;
		set
		{
			luckyNumber = value;
			OnPropertyChanged("LuckyNumber");
		}
	}

	public LuckyNumberPage(ILuckyNumberDataService luckyNumberDataService)
	{
		_luckyNumberDataService = luckyNumberDataService;
		InitializeComponent();

		GetLuckyNumber();
		BindingContext = LuckyNumber;
	}

	private void GetLuckyNumber()
	{
		LuckyNumber = _luckyNumberDataService.GetOrCreate(DateTime.UtcNow);
	}

	private async void luckyNumberPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
	}
}