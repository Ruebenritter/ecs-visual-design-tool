extends ColorRect

@export var zoom: float = 1.0:
	set(v):
		zoom = clampf(v, 0.1, 20.0)
		if _mat:
			_mat.set_shader_parameter("zoom", zoom)

@export var zoom_speed: float = 0.15

var _mat: ShaderMaterial

func _ready() -> void:
	anchor_right = 1.0
	anchor_bottom = 1.0
	_mat = material as ShaderMaterial
	assert(_mat != null, "BlueprintBackground needs a ShaderMaterial assigned")
	_mat.set_shader_parameter("zoom", zoom)

func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed:
		match event.button_index:
			MOUSE_BUTTON_WHEEL_UP: zoom *= 1.0 + zoom_speed
			MOUSE_BUTTON_WHEEL_DOWN: zoom *= 1.0 - zoom_speed
