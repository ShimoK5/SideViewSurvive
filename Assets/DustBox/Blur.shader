Shader "Sample/Blur"
{
	Properties
	{
	_MainTex("Texture", 2D) = "white" {}
	_PosterizeStep("Posterize Step", float) = 255
	_BlurSize("Blur Size", float) = 1
	}

		SubShader
	{

	Pass
	{
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"

	struct appdata
	{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	};

	struct v2f
	{
	float2 uv : TEXCOORD0;
	float4 vertex : SV_POSITION;
	};

	v2f vert(appdata v)
	{
	v2f o;
	o.vertex = UnityObjectToClipPos(v.vertex);
	o.uv = v.uv;
	return o;
	}

	sampler2D _MainTex;
	float4 _MainTex_TexelSize;

	float _PosterizeStep;
	float _BlurSize;

	float3 Posterize(float3 x, float step)
	{
	return floor(x * step) / step;
	}

	float3 Blur(float2 uv, float2 offset)
	{
	float3 color = 0;
	for (int i = -1; i <= 1; i++)
	{
	for (int j = -1; j <= 1; j++)
	{
	color += tex2D(_MainTex, saturate(uv + offset * float2(i, j))).rgb;
	}
	}
	color /= 9.0;
	return color;
	}

	float4 frag(v2f IN) : SV_Target
	{
	float2 uv = IN.uv;
	float2 offset = _MainTex_TexelSize.xy * _BlurSize;

	float3 color = 0;

	color = Blur(uv, offset);
	color = Posterize(color, _PosterizeStep);

	return float4(color.rgb, 1.0);
	}

	ENDCG
	}
	}
}