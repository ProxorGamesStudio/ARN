Shader "Proxor/Hub" {
	Properties{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0.0, 0.03)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }

	    _Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_AlphaFactor("Alpha Factor", Range(0,2)) = 0.6
		_RimColor("Цвет подсветки", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Глубина подсветки", Range(0,8.0)) = 3.0
		_RimAplha("Прозрачность подсветки", Range(0,2)) = 1.0

	
	}

		CGINCLUDE
#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;
	float _AlphaFactor;

	v2f vert(appdata v) {
		// just make a copy of incoming vertex data but scaled according to normal direction
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
	ENDCG

		SubShader{
		Tags{ "Queue" = "Transparent" }

		// note that a vertex shader is specified here but its using the one above
		Pass{
		Name "OUTLINE"

		Cull Off
		ZWrite Off
		ZTest Always

		// you can choose what kind of blending mode you want for the outline
		Blend SrcAlpha OneMinusSrcAlpha // Normal
										//Blend One One // Additive
										//Blend One OneMinusDstColor // Soft Additive
										//Blend DstColor Zero // Multiplicative
										//Blend DstColor SrcColor // 2x Multiplicative

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

		half4 frag(v2f i) : COLOR{
		return i.color;
	}
		ENDCG
	}


		CGPROGRAM
#pragma surface surf Lambert Standard alpha
		struct Input {
		float2 uv_MainTex;
		float3 viewDir;
	};
	sampler2D _MainTex;
	half _Glossiness;
	half _Metallic;

	float4 _RimColor;
	float _RimPower;
	float _RimAplha;
	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutput o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		// Metallic and smoothness come from slider variables
		o.Alpha = _AlphaFactor;
		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = _RimColor.rgba * pow(rim, 8 - _RimPower)* _RimAplha;
	}
	ENDCG

	}

		SubShader{
		Tags{ "Queue" = "Transparent" }

		Pass{
		Name "OUTLINE"
		Tags{ "LightMode" = "Always" }
		Cull Front
		ZWrite Off
		ZTest Always
		Offset 15,15

		// you can choose what kind of blending mode you want for the outline
		Blend SrcAlpha OneMinusSrcAlpha // Normal
										//Blend One One // Additive
										//Blend One OneMinusDstColor // Soft Additive
										//Blend DstColor Zero // Multiplicative
										//Blend DstColor SrcColor // 2x Multiplicative

		CGPROGRAM
#pragma vertex vert
#pragma exclude_renderers gles xbox360 ps3
		ENDCG
	
	}

	

	}

		Fallback "Outlined/Silhouetted Diffuse"
}