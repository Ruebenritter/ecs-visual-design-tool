shader_type canvas_item;

uniform float zoom = 1.0;

const vec4 BG_COLOR    = vec4(0.294, 0.482, 0.925, 1.0);
const vec4 MINOR_COLOR = vec4(0.55, 0.68, 0.92, 0.45);
const vec4 MAJOR_COLOR = vec4(0.70, 0.82, 1.00, 0.75);

const float BASE_PX_PER_MM = 3.78;
const float MINOR_W = 0.6;
const float MAJOR_W = 1.4;

void fragment() {
    vec2 px   = FRAGCOORD.xy;
    float cell = 10.0; // multiply by zoom for zoom effects

    vec2 minor = mod(px, cell);
    vec2 major = mod(px, cell * 10.0);

    vec4 color = BG_COLOR;

    if (minor.x < MINOR_W || minor.y < MINOR_W)
        color = mix(BG_COLOR, MINOR_COLOR, MINOR_COLOR.a);

    if (major.x < MAJOR_W || major.y < MAJOR_W)
        color = mix(BG_COLOR, MAJOR_COLOR, MAJOR_COLOR.a);

    COLOR = color;
}