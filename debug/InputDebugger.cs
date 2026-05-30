using Godot;
using Godot.Collections;

namespace Xtype.Debug;

/// <summary>
/// Attach this node anywhere in the scene tree (e.g. as a child of the root or MainWindow).
/// It reports which Control is currently under the mouse and logs click events together with
/// the full ancestor chain ("click stacktrace") of the receiving node.
///
/// Signals can be connected in code:
///   inputDebugger.ClickDetected  += OnClickDetected;
///   inputDebugger.HoverChanged   += OnHoverChanged;
/// </summary>
public partial class InputDebugger : Node
{
    // -------------------------------------------------------------------------
    // Signals
    // -------------------------------------------------------------------------

    /// <summary>
    /// Emitted when a mouse button is pressed or released.
    /// </summary>
    /// <param name="button">Which mouse button triggered the event.</param>
    /// <param name="pressed">True = press, false = release.</param>
    /// <param name="receivingControl">The Control that will handle the click, or null.</param>
    /// <param name="ancestorChain">Ordered list of node paths from the receiving control up to the scene root.</param>
    [Signal]
    public delegate void ClickDetectedEventHandler(
        MouseButton button,
        bool pressed,
        Control receivingControl,
        Array<string> ancestorChain);

    /// <summary>
    /// Emitted whenever the hovered Control changes (i.e. the mouse moves onto a different node).
    /// </summary>
    /// <param name="previousControl">The previously hovered control, or null.</param>
    /// <param name="currentControl">The newly hovered control, or null.</param>
    [Signal]
    public delegate void HoverChangedEventHandler(Control previousControl, Control currentControl);

    // -------------------------------------------------------------------------
    // Configuration
    // -------------------------------------------------------------------------

    /// <summary>When true, all events are also printed to the Godot console.</summary>
    [Export] public bool PrintToConsole { get; set; } = true;

    /// <summary>When true, mouse-motion hover changes are tracked and reported.</summary>
    [Export] public bool TrackHover { get; set; } = true;

    // -------------------------------------------------------------------------
    // State
    // -------------------------------------------------------------------------

    private Control _lastHovered;

    // -------------------------------------------------------------------------
    // Godot overrides
    // -------------------------------------------------------------------------

    public override void _Input(InputEvent @event)
    {
        if (TrackHover && @event is InputEventMouseMotion)
            CheckHoverChange();

        if (@event is InputEventMouseButton mouseButton)
            HandleClick(mouseButton);
    }

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private void CheckHoverChange()
    {
        Control current = GetViewport().GuiGetHoveredControl();
        if (current == _lastHovered)
            return;

        Control previous = _lastHovered;
        _lastHovered = current;

        if (PrintToConsole)
        {
            string prevName = previous?.GetPath().ToString() ?? "<none>";
            string currName = current?.GetPath().ToString() ?? "<none>";
            GD.Print($"[InputDebugger] Hover changed: {prevName}  →  {currName}");
        }

        EmitSignal(SignalName.HoverChanged, previous, current);
    }

    private void HandleClick(InputEventMouseButton mouseButton)
    {
        // Resolve the control that would receive this click.
        // GuiGetHoveredControl returns the topmost visible Control under the cursor.
        Control receiver = GetViewport().GuiGetHoveredControl();

        Array<string> chain = BuildAncestorChain(receiver);

        if (PrintToConsole)
        {
            string action = mouseButton.Pressed ? "PRESSED" : "RELEASED";
            string receiverPath = receiver?.GetPath().ToString() ?? "<none>";
            GD.Print($"[InputDebugger] Click {action} | Button={mouseButton.ButtonIndex} | Receiver={receiverPath}");
            GD.Print("[InputDebugger] Click stacktrace (receiver → root):");
            for (int i = 0; i < chain.Count; i++)
                GD.Print($"  [{i}] {chain[i]}");
        }

        EmitSignal(SignalName.ClickDetected, (int)mouseButton.ButtonIndex, mouseButton.Pressed, receiver, chain);
    }

    /// <summary>
    /// Walks from <paramref name="node"/> up to the scene root and returns the
    /// path of every ancestor, ordered from the node itself to the root.
    /// </summary>
    private static Array<string> BuildAncestorChain(Node node)
    {
        var chain = new Array<string>();
        Node current = node;
        while (current != null)
        {
            chain.Add(current.GetPath().ToString());
            current = current.GetParent();
        }
        return chain;
    }
}
