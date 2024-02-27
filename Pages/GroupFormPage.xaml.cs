using StudentRandomizer.Models;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.Groups.Inputs;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(Group), "Group")]
public partial class GroupFormPage : ContentPage
{
	private readonly IGroupDataService _groupDataService;

	private Group? group = null;
	private bool IsEdit = false;

	public Group? Group
	{
		get => group;
		set
		{
			group = value;
			OnPropertyChanged("Group");
			SetBindingContext();
		}
	}

	public GroupFormPage(IGroupDataService groupDataService)
	{
		_groupDataService = groupDataService;
		InitializeComponent();
		SetBindingContext();
	}

	private void SetBindingContext()
	{
		if(Group == null)
		{
			BindingContext = new CreateGroupInput();
			IsEdit = false;
		}
		else
		{
			var updateStudent = new UpdateGroupInput();
			updateStudent.GroupRefId = Group.GroupRefId;
			updateStudent.Name = Group.Name;

			BindingContext = updateStudent;
			IsEdit = true;
		}
	}

	private async void groupFormPage_cancelButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//groups/list");
	}

	private async void groupFormPage_submitButton_Clicked(object sender, EventArgs e)
	{
		try
		{
			if(IsEdit)
			{
				_groupDataService.Update((UpdateGroupInput) BindingContext);
			}
			else
			{
				_groupDataService.Create((CreateGroupInput) BindingContext);
			}
		}
		catch(Exception ex)
		{
			await DisplayAlert("Formularz grupy", $"Operacja nie powiodła się.\n{ex.Message}\n{ex.InnerException?.Message}", "OK");
		}

		await Shell.Current.GoToAsync("//groups/list");
	}
}