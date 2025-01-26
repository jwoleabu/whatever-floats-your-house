using Godot;
using System;
using Godot.Collections;

public partial class House : RigidBody2D
{
    [Export]
    public PackedScene BubbleScene { get; set; }
    private RigidBody2D _prevBubble;
    private Node2D parentScene;
    private Timer _cooldownTimer;
    [Export] public Dictionary<Node2D, Boolean> StaticBodies = new Dictionary<Node2D, Boolean>();

    public override void _Ready()
    {
        parentScene = GetParent<Node2D>();
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D3"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D5"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D7"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D9"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D11"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D12"), false);
        StaticBodies.Add(GetNode<Node2D>("anchors/StaticBody2D13"), false);
        _cooldownTimer = GetNode<Timer>("Timer");
    }

    private Node2D GetClosestNode(Vector2 position)
    {
        GD.Print(StaticBodies);
        var min = float.MaxValue;
        Node2D minnode = null;
        foreach (var pair in StaticBodies)
        {
            var distance = position.DistanceTo(pair.Key.GetGlobalTransformWithCanvas().Origin);
            if (distance < min && pair.Value == false)
            {
                min = distance;
                minnode = pair.Key;
            }
            GD.Print($"comparing position {position} with point {pair.Key.GetGlobalTransformWithCanvas().Origin} the distance is: {distance}");
        }
        return minnode;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouse)
        {
            if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
            {
                Node2D bubbleNode = BubbleScene.Instantiate<Node2D>();
                Bubble bubble = (Bubble)bubbleNode;
                Vector2 clickPosition = eventMouse.Position;
                Node2D closestNode = GetClosestNode(clickPosition);
                if (closestNode == null)
                {
                    return;
                }
                GD.Print(closestNode.Name);
                Vector2 closestNodeGlobalPosition = closestNode.GlobalPosition;
                closestNodeGlobalPosition.Y -= 60f;
                bubbleNode.GlobalPosition = closestNodeGlobalPosition;
                parentScene.AddChild(bubbleNode);
                if (_prevBubble == null)
                {
                    _prevBubble = (RigidBody2D)bubbleNode;
                }
                else
                {
                    RigidBody2D thisBubble = (RigidBody2D)bubbleNode;
                    thisBubble.LinearVelocity = _prevBubble.LinearVelocity;
                }
                StaticBodies[(StaticBody2D)closestNode] = true;
                GD.Print(StaticBodies[(StaticBody2D)closestNode]);
                bubble.ConnectToHouse();
                bubble.SetNodeAB(bubbleNode, closestNode);
            }
        }

        if (@event is InputEventKey eventKey && !eventKey.Echo)
        {
            if (!_cooldownTimer.IsStopped())
            {
                GD.Print("on cd");
                return;
            }
            if (eventKey.Pressed && eventKey.IsCtrlPressed() && eventKey.Keycode == Key.A)
            {
                ApplyImpulse(new Vector2(-500f, 0));
                GD.Print("BURST LEFT");
            }
            else if (eventKey.Pressed && eventKey.IsCtrlPressed() && eventKey.Keycode == Key.D)
            {
                ApplyImpulse(new Vector2(500f, 0));
                GD.Print("BURST RIGHT");
            }
            else if (eventKey.Pressed && eventKey.Keycode == Key.A)
            {
                ApplyImpulse(new Vector2(-250f, 0));
                GD.Print("BURST RIGHTOOO");
            }
            else if (eventKey.Pressed && eventKey.Keycode == Key.D)
            {
                ApplyImpulse(new Vector2(250f, 0));
                GD.Print("BURST RIGHTOOO");
            }
            _cooldownTimer.Start();
        }

    }
    
    
}
