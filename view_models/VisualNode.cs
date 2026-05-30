using Godot;

namespace Xtype.ViewModels;


public abstract partial class VisualNode : PanelContainer
{
    [Export]
    public required PackedScene DragPreviewScene;

    private bool _dragging = false;
    private Vector2 _dragOffset = Vector2.Zero;

    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Stop;
    }

    //ToDo: Revisit drag & drop using Control's built-in drag & drop system

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            GD.Print($"Mouse Button Event: ButtonIndex={mouseButton.ButtonIndex}, Pressed={mouseButton.Pressed}, Received by {GetPath()}");
            if (mouseButton.ButtonIndex == MouseButton.Left)
            {
                if (mouseButton.Pressed)
                {
                    GD.Print("Mouse button pressed, starting drag.");
                    _dragging = true;
                    _dragOffset = GetLocalMousePosition();
                }
                else
                {
                    _dragging = false;
                }
            }
        }
        else if (@event is InputEventMouseMotion mouseMotion && _dragging)
        {
            // Move relative to parent (VisualNodeRoot)
            Position += mouseMotion.Relative;
        }
    }

    public override void _Input(InputEvent @event)
    {
        // Release drag if mouse button is released outside the control
        if (@event is InputEventMouseButton mouseButton
            && mouseButton.ButtonIndex == MouseButton.Left
            && !mouseButton.Pressed)
        {
            GD.Print("Mouse button released outside control, stopping drag.");
            _dragging = false;
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        //SetDragPreview(DragPreviewScene.Instantiate() as DragPreviewIconViewModel);
        return base._GetDragData(atPosition);
    }
}
