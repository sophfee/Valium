#version 460 core

out vec4 FragColor;

in struct {
    vec3 position;
    vec3 normal;
    vec2 uv0;
} vertexOut;

void main() {
    FragColor = vec4(1.0);
}