#version 450 core

layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec4 aColor;

out vec2 texCoord;
out vec4 color;

uniform mat4 transform;

void main()
{
	texCoord = aTexCoord;
	color = aColor;

	gl_Position = vec4(aPosition, 0.0f, 1.0f) * transform;
}