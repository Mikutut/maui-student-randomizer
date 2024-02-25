using Microsoft.EntityFrameworkCore;
using StudentRandomizer.EntityFrameworkCore;

namespace StudentRandomizer;

public partial class App : Application
{
	private readonly DatabaseContext _context;

	public App(DatabaseContext context)
	{
		_context = context;

		InitializeComponent();

		MainPage = new AppShell();
	}

	protected override Window CreateWindow(IActivationState activationState)
	{
		var window = base.CreateWindow(activationState);

		window.Created += (s, e) =>
		{
			_context.Database.Migrate();
		};

		return window;
	}
}
