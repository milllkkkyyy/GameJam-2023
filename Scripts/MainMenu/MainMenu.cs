 using Godot;
using System;
using Godot.Collections;

public class MainMenu : Control
{
	private Control _settingsMenu;
	private bool _disabled = false;
	private bool _newplayer = false;
	
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
		_settingsMenu = GetNode<Control>("SettingsMenu");
		_settingsMenu.Visible = false;
	}

	private void OnStartButtonPressed()
	{
		if (_disabled)
			return;

		_disabled = true;
	}

	private void OnSettingsButtonPressed()
	{
		_settingsMenu.Visible = true;
	}

	private void OnQuitButtonPressed()
	{
		if (_disabled)
			return;

		_disabled = true; 
		GetTree().Quit();
	}

	private void _OnExitSettingsPressed()
	{
		_settingsMenu.Visible = false;
	}
	
}
