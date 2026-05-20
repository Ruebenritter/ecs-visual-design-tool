using Godot;
using System;

namespace Xtype.ViewModels;

public partial class ComponentViewModel : VisualNode
{
    // Inputs
    [Export]
    public string ComponentName { get; set; } = "Component Name";

    // Children
    [Export]
    public Label ComponentNameLabel { get; set; }
    [Export]
    public ColorRect ComponentColorCode { get; set; }


    public override void _Ready()
    {
        base._Ready();
        ComponentNameLabel.Text = ComponentName;
    }
}
