﻿Shader "Unlit/DisplacementShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplacementTex ("DisplacementTex", 2D) = "white" {}
		_WaterTex ("WaterTex", 2D) = "white" {}
		_MaskTex ("MaskTex", 2D) = "white" {}
		_BaseHeight ("BaseHeight", float) = 0.4
		_Turbulence ("Turbulence", float) = 0.3
		_ScrollOffset ("ScrollOffset", float) = 0
		_Opacity ("Opacity", float) = 1
		_Tick ("Tick", float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _DisplacementTex;
			sampler2D _WaterTex;
			sampler2D _MaskTex;
			float4 _MainTex_ST;
			fixed4 _MainTex_TexelSize;
			float _BaseHeight;
			fixed _Turbulence;
			fixed _ScrollOffset;
			fixed _Opacity;
			fixed _Tick;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float wave(float x) {
				fixed waveOffset = 	cos((x - _Tick + _ScrollOffset) * 60) * 0.004
									+ cos((x - 2 * _Tick + _ScrollOffset) * 20) * 0.008
									+ sin((x + 2 * _Tick + _ScrollOffset) * 35) * 0.01
									+ cos((x + 4 * _Tick + _ScrollOffset) * 70) * 0.001;
				return _BaseHeight + waveOffset * _Turbulence;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{


				fixed4 waterCol = tex2D(_WaterTex, i.uv);
				fixed maskValue = tex2D(_MaskTex, i.uv).r;

				float waveHeight = wave(i.uv.x);
				fixed isTexelAbove = step(waveHeight, i.uv.y);
				fixed isTexelBelow = 1 - isTexelAbove;

				fixed2 disPos = i.uv;
				disPos.x += (_Tick) % 2;
				disPos.y += (_Tick) % 2;
				fixed4 dis = tex2D(_DisplacementTex, disPos);
				i.uv.x += dis * 0.006 * isTexelBelow * maskValue * _Turbulence;
				i.uv.y += dis * 0.006 * isTexelBelow * maskValue * _Turbulence;

				fixed4 col = tex2D(_MainTex, fixed2(i.uv.x, i.uv.y)); 
				fixed waterColBlendFac = maskValue * isTexelBelow * _Opacity;				
				col.r = lerp(col.r, waterCol.r, waterColBlendFac);
				col.g = lerp(col.g, waterCol.g, waterColBlendFac);
				col.b = lerp(col.b, waterCol.b, waterColBlendFac);

				float topDist = abs(i.uv.y - waveHeight);
				fixed isNearTop = 1 - step(abs(_MainTex_TexelSize.y * 6), topDist);
				fixed isVeryNearTop = 1 - step(abs(_MainTex_TexelSize.y * 3), topDist);

				fixed topColorBlendFac = isNearTop * isTexelBelow * maskValue;
				col.r = lerp(col.r, waterCol.r * 0.83, topColorBlendFac);
				col.g = lerp(col.g, waterCol.g * 0.83, topColorBlendFac);
				col.b = lerp(col.b, waterCol.b * 0.83, topColorBlendFac);

				col.r += 0.2 * isVeryNearTop * isTexelBelow * maskValue;
				col.g += 0.2 * isVeryNearTop * isTexelBelow * maskValue;
				col.b += 0.2 * isVeryNearTop * isTexelBelow * maskValue;
				col.a = 1;
	

				return col;
			}
			ENDCG
		}
	}
}
