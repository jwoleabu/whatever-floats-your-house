using Godot;
using System;
using System.Diagnostics;

public partial class Bubble : RigidBody2D
{
    private DampedSpringJoint2D _springJoint;
    private RigidBody2D player;
    private Node2D nodeA;
    private Node2D nodeB;

    public override void _Ready()
    {
        player = GetNode<RigidBody2D>("/root/Joscene/House");
        _springJoint = GetNode<DampedSpringJoint2D>("DampedSpringJoint2D");
    }

    public void SetNodeAB(Node2D nodeA, Node2D nodeB)
    {
        this.nodeA = nodeA;
        this.nodeB = nodeB;
    }

    public override void _Draw()
    {
        if (nodeA == null|| nodeB == null)
        {
            return;
        }
        DrawLine(nodeA.Position, nodeB.Position, Colors.Red, 2f);
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public void ConnectToHouse()
    {
        _springJoint.NodeB = player.GetPath();
    }
    public void ConnectToHouse(NodePath path)
    {
        _springJoint.NodeB = path;
    }
}
