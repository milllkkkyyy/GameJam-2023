using Godot;
using System;
using System.Drawing.Text;
using System.IO;
using Godot.Collections;

public class VolumeSlider : HSlider
{
	[Export]
	public string busName;
	public int busIndex;

	public override void _Ready()
	{
		busIndex = AudioServer.GetBusIndex(busName);
		// value_changed.connect(_OnHSliderValueChanged);

		Value = GD.Db2Linear(AudioServer.GetBusVolumeDb(busIndex));

	}
	

	private void _OnHSliderValueChanged(float value)
	{
		AudioServer.SetBusVolumeDb(busIndex, GD.Linear2Db(value));
	}

}

