using Godot;

public partial class MainWindowViewModel : Control
{
    [Export]
    public ColorRect Background { get; set; }
    [Export]
    public Control VisualNodeRoot { get; set; }

    private bool _panning = false;

    public override void _Ready()
    {
        VisualNodeRoot.MouseFilter = MouseFilterEnum.Pass;
        Background.MouseFilter = MouseFilterEnum.Stop;
    }

    public override void _GuiInput(InputEvent @event)
    {
        // Pan the canvas with middle mouse button drag
        if (@event is InputEventMouseButton mouseButton
            && mouseButton.ButtonIndex == MouseButton.Middle)
        {
            GD.Print("Middle mouse button " + (mouseButton.Pressed ? "pressed, starting pan." : "released, stopping pan."));
            _panning = mouseButton.Pressed;
        }
        else if (@event is InputEventMouseMotion mouseMotion && _panning)
        {
            VisualNodeRoot.Position += mouseMotion.Relative;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        // Debug all input events and what node they are received by
        if (@event is InputEventMouseButton mouseButton)
        {
            GD.Print($"Unhandled Mouse Button Event: ButtonIndex={mouseButton.ButtonIndex}, Pressed={mouseButton.Pressed}, Received by {GetPath()}");
        }
        else if (@event is InputEventMouseMotion mouseMotion)
        {
            GD.Print($"Unhandled Mouse Motion Event: Relative={mouseMotion.Relative}, Received by {GetPath()}");
        } else {
            GD.Print($"Unhandled Input Event: {@event.GetType().Name}, Received by {GetPath()}");
        }
    }
}
