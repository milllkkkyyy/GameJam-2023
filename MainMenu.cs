using Godot;
using System;
using Godot.Collections;

public class MainMenu : Control
{
	private Control _settingsMenu;
	
	private bool _disabled = false;
	private bool _newplayer = false;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
		_settingsMenu = GetNode<Control>("SettingsMenu");
		_settingsMenu.Visible = false;
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	private void OnStartButtonPressed()
	{
		if (_disabled)
			return;

		_disabled = true; // so multiple instances cant be hit
		GD.Print("Play Button Pressed.");
	}

	private void OnSettingsButtonPressed()
	{
		_settingsMenu.Visible = true;
		GD.Print("Settings Button Pressed.");
	}

	private void OnQuitButtonPressed()
	{
		if (_disabled)
			return;

		_disabled = true; // so multiple instances cant be hit

		GD.Print("Quit Button Pressed.");
		GetTree().Quit();
	}

	private void OnExitSettingsPressed()
	{
		_settingsMenu.Visible = false;
	}
	
}
