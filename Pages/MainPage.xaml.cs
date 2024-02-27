namespace StudentRandomizer.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void mainPage_studentsListButton_Clicked(object sender)
	{
		await Shell.Current.GoToAsync("//students/list");
	}

	private async void mainPage_schoolClassesListButton_Clicked(object sender)
	{
		await Shell.Current.GoToAsync("//schoolClasses/list");
	}

	private async void mainPage_groupsListButton_Clicked(object sender)
	{
		await Shell.Current.GoToAsync("//groups/list");
	}
}

