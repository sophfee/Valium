#version 460 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNor;
layout (location = 2) in vec2 aUv0;
layout (location = 3) in uvec4 aJt0;
layout (location = 4) in vec4 aWt0;

out struct {
    vec3 position;
    vec3 normal;
    vec2 uv0;
} vertexOut;

uniform mat4 model;
uniform mat4 view;
uniform mat4 proj;

void main() {
    gl_Position = (proj * view * model * vec4(aPos, 1.0));
    vertexOut.position = (proj * view * model * vec4(aPos, 1.0)).xyz;
    vertexOut.normal = aNor;
    vertexOut.uv0 = aUv0;
}