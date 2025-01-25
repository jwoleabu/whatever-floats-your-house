using Godot;
using System;

public partial class Bubble : RigidBody2D
{
	private DampedSpringJoint2D _springJoint;
	private RigidBody2D player;

	public override void _Ready()
	{
		player = GetNode<RigidBody2D>("../House");
		GD.Print(player.ToString());
		_springJoint = GetNode<DampedSpringJoint2D>("DampedSpringJoint2D");
		ConnectToHouse();
	}

	public void ConnectToHouse()
	{
		_springJoint.NodeB = player.GetPath();
	}
}
