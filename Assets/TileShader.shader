// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/TileShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_TileScale("TileScale", Range(0.1, 10)) = 1	
		_BunkatsuSuu("BunkatsuSuu",int) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				float3 worldPos;
				float3 worldNormal;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			half _TileScale;
			int  _BunkatsuSuu;
			//プロトタイプ宣言
			float fmodcg(float x, float y);

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {

				// Albedo comes from a texture tinted by color
				fixed factorX = abs(dot(IN.worldNormal, float3(1,0,0)));
				fixed factorY = abs(dot(IN.worldNormal, float3(0,1,0)));
				fixed factorZ = abs(dot(IN.worldNormal, float3(0,0,1)));

				float3 scaledWorldPos = IN.worldPos / _TileScale;

				//分割数に応じた小数を作成
				float TempNum = 1.0f / _BunkatsuSuu;

				fixed4 cx = tex2D(_MainTex, float2(fmodcg(scaledWorldPos.z, 1) * TempNum + TempNum, fmodcg(scaledWorldPos.y, 1) * TempNum)) * factorX;
				fixed4 cy = tex2D(_MainTex, float2(fmodcg(scaledWorldPos.x, 1) * TempNum + TempNum, fmodcg(scaledWorldPos.z, 1) * TempNum + TempNum)) * factorY;
				fixed4 cz = tex2D(_MainTex, float2(fmodcg(scaledWorldPos.x, 1) * TempNum, fmodcg(scaledWorldPos.y, 1) * TempNum)) * factorZ;

				fixed4 c = (cx + cy + cz);

				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;



				//if (abs(IN.worldNormal.x) >= 0.1)
				//{
				//	fixed4 cx = tex2D(_MainTex, float2(scaledWorldPos.z * 2, scaledWorldPos.y)) * factorX;
				//	fixed4 cy = tex2D(_MainTex, float2(scaledWorldPos.x, scaledWorldPos.z)) * factorY;
				//	fixed4 cz = tex2D(_MainTex, float2(scaledWorldPos.x, scaledWorldPos.y)) * factorZ;

				//	fixed4 c = (cx + cy + cz);

				//	o.Albedo = c.rgb;
				//	// Metallic and smoothness come from slider variables
				//	o.Metallic = _Metallic;
				//	o.Smoothness = _Glossiness;
				//	o.Alpha = c.a;
				//}
				//else if (abs(IN.worldNormal.y) >= 0.1)
				//{
				//	fixed4 cx = tex2D(_MainTex, float2(scaledWorldPos.z * 2, scaledWorldPos.y)) * factorX;
				//	fixed4 cy = tex2D(_MainTex, float2(scaledWorldPos.x, scaledWorldPos.z)) * factorY;
				//	fixed4 cz = tex2D(_MainTex, float2(scaledWorldPos.x, scaledWorldPos.y)) * factorZ;

				//	fixed4 c = (cx + cy + cz);

				//	o.Albedo = c.rgb;
				//	// Metallic and smoothness come from slider variables
				//	o.Metallic = _Metallic;
				//	o.Smoothness = _Glossiness;
				//	o.Alpha = c.a;
				//}
				//else if (abs(IN.worldNormal.z) >= 0.1)
				//{
				//	fixed4 cx = tex2D(_MainTex, float2(scaledWorldPos.z * 2, scaledWorldPos.y)) * factorX;
				//	fixed4 cy = tex2D(_MainTex, float2(scaledWorldPos.x, scaledWorldPos.z)) * factorY;
				//	fixed4 cz = tex2D(_MainTex, float2(scaledWorldPos.x, scaledWorldPos.y)) * factorZ;

				//	fixed4 c = (cx + cy + cz);

				//	o.Albedo = c.rgb;
				//	// Metallic and smoothness come from slider variables
				//	o.Metallic = _Metallic;
				//	o.Smoothness = _Glossiness;
				//	o.Alpha = c.a;
				//}
				//else
				//{
				//	//斜め
				//}

				
			}

			// Equivalent to fmod() in CG.
			float fmodcg(float x, float y)
			{
				const float c = frac(abs(x / y)) * abs(y);
				return x < 0 ? -c : c;
			}

			ENDCG
		}
			FallBack "Diffuse"
}