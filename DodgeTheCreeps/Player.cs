using Godot;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400;
	public Vector2 ScreenSize;
	public bool IsReloading { get; set; }

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public override void _Process(double delta)
	{
		var velocity = getVelocity();

		handleMovement(velocity, delta);

		handleAnimation(velocity);
	}

	private Vector2 getVelocity()
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_up"))
			velocity.Y--;

		if (Input.IsActionPressed("move_down"))
			velocity.Y++;

		if (Input.IsActionPressed("move_right"))
			velocity.X++;

		if (Input.IsActionPressed("move_left"))
			velocity.X--;

		velocity = velocity.Normalized() * Speed;

		return velocity;
	}

	private void handleMovement(Vector2 velocity, double delta)
	{
	  

		Position += velocity * (float)delta;

		Position = new Vector2
			(
				x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
				y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
			);
	}

	private void handleAnimation(Vector2 velocity)
	{
		var animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (Input.IsActionJustPressed("reload_gun"))
		{
			IsReloading = true;
		}


		if (velocity.Length() != 0 && IsReloading == false)
			animation.Animation = "Move";
		else if (IsReloading == true)
		{
			animation.Animation = "Reload";
			IsReloading = false;
		}
		else
			animation.Animation = "Idle";

        animation.Play();

        LookAt(GetGlobalMousePosition());
	}
}
