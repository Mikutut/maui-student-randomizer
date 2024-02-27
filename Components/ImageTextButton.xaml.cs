using System.ComponentModel;

namespace StudentRandomizer.Components;

public partial class ImageTextButton : ContentView, INotifyPropertyChanged
{
	public static readonly BindableProperty TextProperty =
		BindableProperty.Create("Text", 
			typeof(string), 
			typeof(ImageTextButton), 
			"Button");

	public static readonly BindableProperty ImageProperty =
		BindableProperty.Create("Image", 
			typeof(ImageSource), 
			typeof(ImageTextButton), 
			null);

	public event Action<object>? Clicked;

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set
		{
			SetValue(TextProperty, value);
			OnPropertyChanged("Text");
		}
	}

	public ImageSource Image
	{
		get => (ImageSource)GetValue(ImageProperty);
		set
		{
			SetValue(ImageProperty, value);
			OnPropertyChanged("Image");
		}
	}

	public ImageTextButton()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void imageTextButton_button_Clicked(object sender, EventArgs e)
	{
		Clicked?.Invoke(this);
	}
}