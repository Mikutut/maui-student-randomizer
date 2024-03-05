using StudentRandomizer.Models;
using StudentRandomizer.Services.LuckyNumbers;
using System.Collections.ObjectModel;

namespace StudentRandomizer.Pages;

public partial class LuckyNumberPage : ContentPage
{
	private readonly ILuckyNumberDataService _luckyNumberDataService;

	private LuckyNumber luckyNumber;
	private ObservableCollection<LuckyNumber> archivedNumbers = new ObservableCollection<LuckyNumber>();

	public LuckyNumber LuckyNumber
	{
		get => luckyNumber;
		set
		{
			luckyNumber = value;
			OnPropertyChanged("LuckyNumber");
		}
	}

	public ObservableCollection<LuckyNumber> ArchivedNumbers
	{
		get => archivedNumbers;
	}

	public LuckyNumberPage(ILuckyNumberDataService luckyNumberDataService)
	{
		_luckyNumberDataService = luckyNumberDataService;
		InitializeComponent();

		GetCurrentLuckyNumber();
		GetArchivedNumbers();
	}

	private void GetCurrentLuckyNumber()
	{
		LuckyNumber = _luckyNumberDataService.GetOrCreate(DateTime.UtcNow);
	}

	private void GetArchivedNumbers()
	{
		ArchivedNumbers.Clear();

		_luckyNumberDataService.GetAll()
			.OrderByDescending(x => x.CreationDate)
			.Skip(1)
			.ToList()
			.ForEach(x => ArchivedNumbers.Add(x));
	}

	private async void luckyNumberPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
	}
}