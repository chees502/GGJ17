﻿Shader "Hidden/Post"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 Sample(float2 uv,float x, float y) {
				float size = 0.003;
				return max(tex2D(_MainTex, uv + float2(x, y)*size)-1,fixed4(0,0,0,0));
			}
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
			col += Sample(i.uv, +0, +1);
			col += Sample(i.uv, +1, +0);
			col += Sample(i.uv, +0, -1);
			col += Sample(i.uv, -1, +0);
			col += Sample(i.uv, +1, +1);
			col += Sample(i.uv, -1, -1);
			col += Sample(i.uv, +1, -1);
			col += Sample(i.uv, -1, +1);

			col += Sample(i.uv, +0, +2)*0.2;
			col += Sample(i.uv, +2, +0)*0.2;
			col += Sample(i.uv, +0, -2)*0.2;
			col += Sample(i.uv, -2, +0)*0.2;
				return col;
			}
			ENDCG
		}
	}
}
