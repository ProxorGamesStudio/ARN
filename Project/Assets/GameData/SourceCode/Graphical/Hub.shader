Shader "Proxor/Hub" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" { }
	    _Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_RimColor("Цвет подсветки", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Глубина подсветки", Range(0,8.0)) = 3.0
		_RimAplha("Прозрачность подсветки", Range(0,2)) = 1.0
		_DetailLightColor("DetailLightColor", Color) = (1,1,1,1)
		_DetailLightMask("DetailLightMask", 2D) = "white" { }
		_ItensityLightR("_ItensityLightR", Range(0,1)) = 0.5
		_ItensityLightG("_ItensityLightG", Range(0,1)) = 0.5
		_ItensityLightB("_ItensityLightB", Range(0,1)) = 0.5
		_ColorTint("ColorTint", Color) = (0,0,0,0)
	}
	SubShader {
			Tags{ "RenderType" = "Opaque" }
			LOD 200
			Lighting Off
			
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

			struct Input
		{ 
			float2 uv_MainTex;
			float3 viewDir;
			float2 uv_DetailLightMask;
		};

		sampler2D _MainTex;
		sampler2D _DetailLightMask;

		half _Glossiness;
		half _Metallic;

		float4 _RimColor;
		float4 _DetailLightColor;
		float _RimPower; 
		float _RimAplha;
		float _ItensityLightR;
		float _ItensityLightG;
		float _ItensityLightB;
		fixed4 _Color;
		float4 _ColorTint;

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 mask = tex2D(_DetailLightMask, IN.uv_DetailLightMask);
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Specular = _Metallic;
			o.Emission =  _RimColor.rgba * pow(rim, 8 - _RimPower) * _RimAplha + _DetailLightColor * mask.r * _ItensityLightR +  _DetailLightColor * mask.g * _ItensityLightG + _DetailLightColor * mask.b* _ItensityLightB + _ColorTint;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
