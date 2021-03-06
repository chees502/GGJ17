﻿Shader "Custom/Glow" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_GlowMap("GlowMap", 2D) = "Black" {}
		[HDR]
		_GlowColor("GlowColor", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma vertex vert
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		struct v2f
		{
			float4 pos : POSITION;
			float4 color : COLOR;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color; 
		sampler2D _GlowMap;
		fixed4 _GlowColor;
		v2f vert(inout appdata_full v)
		{
			v2f o;
			o.pos = v.vertex;
			return o;
		}
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 g = tex2D(_GlowMap, IN.uv_MainTex)*_GlowColor;
			float sweep = sin(IN.uv_MainTex.x*20 + _Time.x*-150)*0.5+1;
			o.Albedo = c.rgb*0.1;
			o.Emission= g.rgb*sweep;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
