// Made with Amplify Shader Editor v1.9.2.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BT_RainbowParticle"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_ColorTex("ColorTex", 2D) = "white" {}
		_ColorPowerA("ColorPowerA", Float) = 2
		_ColorPowerB("ColorPowerB", Float) = 0.5
		_Emissive("Emissive", Float) = 1
		_ScrollSpeed("ScrollSpeed", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 vertexColor : COLOR;
			float4 uv_texcoord;
		};

		uniform sampler2D _ColorTex;
		uniform float2 _ScrollSpeed;
		uniform float _ColorPowerA;
		uniform float _ColorPowerB;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _Emissive;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 panner38 = ( 1.0 * _Time.y * _ScrollSpeed + i.uv_texcoord.xy);
			float2 appendResult28 = (float2(i.uv_texcoord.z , i.uv_texcoord.w));
			float4 tex2DNode3 = tex2D( _ColorTex, ( panner38 + appendResult28 ) );
			float4 temp_cast_0 = (_ColorPowerA).xxxx;
			float4 temp_cast_1 = (_ColorPowerB).xxxx;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
			float4 lerpResult8 = lerp( pow( tex2DNode3 , temp_cast_0 ) , saturate( pow( tex2DNode3 , temp_cast_1 ) ) , tex2DNode1.r);
			o.Emission = ( ( i.vertexColor * ( 1.0 * lerpResult8 ) ) * _Emissive ).rgb;
			o.Alpha = tex2DNode1.r;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19202
Node;AmplifyShaderEditor.VertexColorNode;9;-256.8196,-208.0791;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-25.81958,3.920898;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-936.7248,264.2274;Inherit;True;Property;_ColorTex;ColorTex;2;0;Create;True;0;0;0;False;0;False;-1;110713a73ae19154fa5f363efa14c07c;fedc73bae40d7da4e95d0b23faf0f3af;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-809.6933,-1.837924;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;-1;bf010bf9001e6f2438bd35d7a210b47a;bf010bf9001e6f2438bd35d7a210b47a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;11;-614.7642,269.704;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;2.45;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;8;-371.5422,272.3584;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;456.5,-26.3;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;BT_RainbowParticle;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;260.1548,0.8088989;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;18;255.3624,-84.8186;Inherit;False;Property;_Emissive;Emissive;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-1107.994,277.1823;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;40;-1609.994,271.1823;Inherit;False;Property;_ScrollSpeed;ScrollSpeed;6;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-1599.116,449.9482;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;28;-1337.852,522.0092;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;38;-1362.994,267.0601;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-1600.153,109.311;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-623.7744,384.582;Inherit;False;Property;_ColorPowerA;ColorPowerA;3;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;16;-454.2484,494.1009;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;15;-612.7433,484.1619;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;2.45;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-611.7535,607.0399;Inherit;False;Property;_ColorPowerB;ColorPowerB;4;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-212.1893,21.39561;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-381.8669,19.91394;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
WireConnection;10;0;9;0
WireConnection;10;1;2;0
WireConnection;3;1;39;0
WireConnection;11;0;3;0
WireConnection;11;1;12;0
WireConnection;8;0;11;0
WireConnection;8;1;16;0
WireConnection;8;2;1;1
WireConnection;0;2;17;0
WireConnection;0;9;1;1
WireConnection;17;0;10;0
WireConnection;17;1;18;0
WireConnection;39;0;38;0
WireConnection;39;1;28;0
WireConnection;28;0;37;3
WireConnection;28;1;37;4
WireConnection;38;0;42;0
WireConnection;38;2;40;0
WireConnection;16;0;15;0
WireConnection;15;0;3;0
WireConnection;15;1;14;0
WireConnection;2;0;43;0
WireConnection;2;1;8;0
ASEEND*/
//CHKSM=D4B4F3EF6919FD4063A8BB22C0952E24A76F82E9