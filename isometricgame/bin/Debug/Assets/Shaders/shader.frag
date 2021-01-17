#version 450 core

out vec4 outputColor;

in vec2 texCoord;
in vec4 color;

uniform sampler2D texture0;

void main()
{
	outputColor = texture(texture0, texCoord);
}