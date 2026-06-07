using Godot;
using System;
using Xtype.Contracts;

public partial class DragPreviewIconViewModel : TextureRect
{
    [Export]
    public required Texture2D IconTexture { get; set; }
    [Export]
    public Vector2 IconSize { get; set; } = new Vector2(32, 32); // Default size for the icon

    public override void _Ready()
    {
        Texture = IconTexture;
        ExpandMode = ExpandModeEnum.IgnoreSize;
        StretchMode = StretchModeEnum.KeepAspectCentered;
        CustomMinimumSize = IconSize;
        MouseFilter = MouseFilterEnum.Ignore; // Allow events to pass through to the main window
    }
}
