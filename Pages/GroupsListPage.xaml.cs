using StudentRandomizer.Models;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.Groups.Inputs;

namespace StudentRandomizer.Pages;

public partial class GroupsListPage : ContentPage
{
	private readonly IGroupDataService _groupDataService;

	private List<Group> groups = new List<Group>();

	public List<Group> Groups
	{
		get => groups;
		set
		{
			groups = value;
			OnPropertyChanged("Groups");
		}
	}

	public GroupsListPage(IGroupDataService groupDataService)
	{
		_groupDataService = groupDataService;
		RefreshGroupsList();
		InitializeComponent();

		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		RefreshGroupsList();
	}

	private void RefreshGroupsList()
	{
		Groups = _groupDataService.GetAll(new GetAllGroupsInput())
			.ToList();
	}

	private async void groupsListPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
	}

	private async void GroupEntry_OnDelete(Group obj)
	{
		var answer = await DisplayAlert("Usuwanie grupy", "Czy na pewno chcesz usunąć tą grupę", "Tak", "Nie");

		if(answer)
		{
			try
			{
				_groupDataService.Delete(obj.GroupRefId);
			}
			catch
			{
				await DisplayAlert("Usuwanie grupy", "Nie udało się usunąć grupy.", "OK");
			}

			RefreshGroupsList();
		}
	}

	private async void GroupEntry_OnUpdate(Group obj)
	{
		var queryParams = new Dictionary<string, object>
		{
			{ "Group", obj }
		};

		await Shell.Current.GoToAsync("//groups/form", queryParams);
	}

	private async void groupsListPage_createToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//groups/form");
	}

	private async void GroupEntry_OnAddStudent(Group group)
	{
		var queryParams = new Dictionary<string, object>
		{
			{ "Group", group }
		};
		await Shell.Current.GoToAsync("//groups/addstudent", queryParams);
	}
}