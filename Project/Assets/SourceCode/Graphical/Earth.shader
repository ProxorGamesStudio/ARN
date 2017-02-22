Shader "Proxor/Earth" {
	Properties {
		_Color("Основной цвет", Color) = (1,1,1,1)
		_MainTex("Альбедо (RGB)", 2D) = "white" {}
	_BumpMap("Нормал мапа", 2D) = "bump" {}
	_NormalDepth("Глубина нормал мапы", Range(0,4)) = 1.0
		_Brigtness("Яркость", Range(0,4)) = 1.0
		_Contrast("Контраст", Range(0,4)) = 1.0
		_Saturation("Насыщенность", Range(0,1)) = 1.0
		_RimColor("Цвет подсветки", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Глубина подсветки", Range(0,8.0)) = 3.0
		_RimAplha("Прозрачность подсветки", Range(0,2)) = 1.0
		_Detail("Статичные детали", 2D) = "gray" {}
	_DetailAlpha("Насыщенность деталей", Range(0,1)) = 0.5
		_ColorTint("Цветокоррекция", Color) = (1.0, 0.6, 0.6, 1.0)
		_DetailGrid("Сеточка деталей для вращения", 2D) = "gray" {}
	    _RotationSpeed("Скорость вращения", float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Lambert finalcolor:mycolor 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
	    sampler2D _BumpMap;
	    sampler2D _Detail;
		sampler2D _DetailGrid;

	struct Input {
		float2 uv_MainTex ;
		float2 uv_BumpMap;
		float2 uv_Detail : TEXCOORD1;
		float2 uv_DetailGrid;
		float3 viewDir;
		float3 Pos: TEXCOORD0;
		float3 Normal: TEXCOORD1;
		float3 worldPos;
		};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;
	float _Brigtness;
	float4 _RimColor;
	float _RimPower;
	float _RimAplha;
	float _NormalDepth;
	float _Contrast;
	float _Saturation;
	float _DetailAlpha;
	fixed4 _ColorTint;
	float4 _MousePosition;
	fixed4 _ColorPoloska;
	float _Width;
	float _RotationSpeed;

	void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
	{
		color *= _ColorTint;
	}

		void surf (Input IN, inout SurfaceOutput o) {
			float2 move = IN.uv_DetailGrid;
			move.x += _Time.x * _RotationSpeed;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Brigtness;
			fixed4 DetailRotate = tex2D(_DetailGrid, move);
			float x1 = 1 - smoothstep(_MousePosition.y, _MousePosition.y + _Width, IN.worldPos.y);
			float x2 = smoothstep(_MousePosition.y - _Width, _MousePosition.y, IN.worldPos.y);
			float x = min(x1, x2);

			float Lum = dot(c, float3(0.2126, 0.7152, 0.0722));
			half4 color = half4(lerp(Lum.xxx, c, _Saturation), 1);
			color = color - _Contrast * (color - 1.0f) * color *(color - 0.5f);
			c = color;
			fixed4 tex = tex2D(_Detail, IN.uv_Detail);
			o.Albedo = c.rgb * tex * lerp(1, DetailRotate, DetailRotate.a * 3) * _Color;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalDepth;
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), float3(0,0,1)));
			o.Emission = _RimColor.rgba * pow(rim, 1 / _RimPower) * _RimAplha + x * _MousePosition.w * _ColorPoloska;
		}
		ENDCG
	}
	FallBack "Diffuse" 
}
