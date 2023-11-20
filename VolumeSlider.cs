using Godot;
using System;
using System.Drawing.Text;
using System.IO;
using Godot.Collections;

public class VolumeSlider : HSlider
{
	[Export]
	public string bus_name;
	public int busIndex;

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		busIndex = AudioServer.GetBusIndex(bus_name);

	}
	

private void _on_HSlider_value_changed(float value)
{
	GD.Print(value);
}

}

